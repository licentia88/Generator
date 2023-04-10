using System;
using Generator.Components.Args;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;
using Generator.Shared.Models;
using Generator.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using Generator.Components.Interfaces;
using Generator.Components.Components;
using System.Data.Common;
using System.Data;
using Generator.UI.Models;
using System.Collections.ObjectModel;
using ProtoBuf.Meta;
using Force.DeepCloner;
using Generator.UI.Extensions;

namespace Generator.UI.Pages.GeneratorPages
{

	public partial class GridPagesM: PagesBase<PAGES_M, IPagesMService> 
    {
 
        [Inject]
        public IDatabaseService DatabaseServices { get; set; }

        public List<COMMAND_TYPES> CommandTypes { get; set; }

        public List<STORED_PROCEDURES> StoredProcedures { get; set; } = new();


        public List<DATABASE_INFORMATION> DatabaseInformationList { get; set; }

        public List<TABLE_INFORMATION> TableInformationList { get; set; }

        public bool DisableDisplayFields { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            var result = await DatabaseServices.GetDatabaseList();

            DatabaseInformationList = result.Data;

            CommandTypes = Enum.GetValues<CommandType>().Where(x => x != CommandType.TableDirect)
                               .Select(x => new COMMAND_TYPES { CT_ROWID = x.CastTo<int>() , CT_DESC = x.ToString() }).ToList();

            await base.OnInitializedAsync();

        }



        private GenComboBox CB_TABLE;

        private GenComboBox CB_COMMAND_TYPE;

        private GenComboBox CB_DATABASE;

        private GenComboBox StoredProcedureCombo;

        private GenTextField SqlQueryTextField;

        private GenCheckBox PM_CREATE;

        private GenCheckBox PM_UPDATE;

        private GenCheckBox PM_DELETE;


        public IGenView<PAGES_M> currentpage;

        private List<DISPLAY_FIELD_INFORMATION> dat;
        private void StoredProcedureChanged(object model)
        {
            if (model is not STORED_PROCEDURES sp) return;

            //StoredProcedureCombo.Value = model;
            StoredProcedureCombo.SetValue(model);

            Task.Run(async () =>
            {
                var result = await DatabaseServices.GetStoredProcedureFields(new((currentpage.SelectedItem.CB_DATABASE, sp.SP_NAME)));
                dat = result.Data;
            });

            //currentpage.SelectedItem.CB_SQL_COMMAND = sp.SP_NAME;
            DisableDisplayFields = false;

            currentpage.StateHasChanged();
        }

        private void SqlQueryTextFieldChanged(object text)
        {
            if (text is not string newText) return;

            SqlQueryTextField.SetValue(newText);

            //currentpage.SelectedItem.CB_SQL_COMMAND = newText;

            currentpage.StateHasChanged();
        }

        private async void CommandTypeChanged(object model)
        {
 
            if (model is not COMMAND_TYPES ct) return;

            CB_COMMAND_TYPE.SetValue(model);

            currentpage.SelectedItem.CB_SQL_COMMAND = string.Empty;

            if (string.IsNullOrEmpty(currentpage.SelectedItem.CB_DATABASE))
            {
                NotificationsView.Notifications.Add(new NotificationVM("Please Select a Database First", MudBlazor.Severity.Warning));
                NotificationsView.Fire();

                StoredProcedureCombo.EditorVisible = false;
                SqlQueryTextField.EditorVisible = false;
                CB_COMMAND_TYPE.SetValue(new COMMAND_TYPES());

                return;
            }

            if (ct.CT_ROWID == CommandType.StoredProcedure.CastTo<int>())
            {
              
                SqlQueryTextField.SetText("");
                StoredProcedureCombo.EditorVisible = true;
                SqlQueryTextField.EditorVisible = false;

                var result = await DatabaseServices.GetStoredProcedures(new RESPONSE_REQUEST<string>(currentpage.SelectedItem.CB_DATABASE));
 
                StoredProcedures = result.Data;

                StoredProcedureCombo.DataSource = StoredProcedures;

            }
            else if (ct.CT_ROWID == CommandType.Text.CastTo<int>())
            {
                StoredProcedureCombo.Clear();
                StoredProcedureCombo.EditorVisible = false;
                SqlQueryTextField.EditorVisible = true;

                DisableDisplayFields = true;
            }
            else
            {
                StoredProcedures?.Clear();
                StoredProcedureCombo.DataSource = StoredProcedures;
                StoredProcedureCombo.EditorVisible = false;
                SqlQueryTextField.EditorVisible = false;
                DisableDisplayFields = true;
            }

            currentpage.StateHasChanged();
        }

        
        private async Task LoadTablesIfDatabaseSelected(string DatabaseName)
        {
            if (string.IsNullOrEmpty(DatabaseName))
            {
                CB_TABLE.EditorVisible = false;
                return;
            }

            CB_TABLE.EditorVisible = true;

            var tables = await DatabaseServices.GetTableListForConnection(new RESPONSE_REQUEST<string>(DatabaseName));
            TableInformationList = tables.Data;

            CB_TABLE.DataSource = TableInformationList;

        }

        public void OnClose()
        {
            //StoredProcedureCombo.BindingField = nameof(StoredProcedureCombo);

            //SqlQueryTextField.BindingField = nameof(SqlQueryTextField);
        }

        public async Task OnLoadAsync(IGenView<PAGES_M> page)
        {
            currentpage = page;
            page.SelectedItem.PM_READ = true;
            page.Parameters.AddOrReplace("TEST",1);

            StoredProcedureCombo = page.GetComponent<GenComboBox>(nameof(StoredProcedureCombo));

            SqlQueryTextField = page.GetComponent<GenTextField>(nameof(SqlQueryTextField));

            StoredProcedureCombo.BindingField = nameof(PAGES_M.CB_SQL_COMMAND);

            SqlQueryTextField.BindingField = nameof(PAGES_M.CB_SQL_COMMAND);


            CB_DATABASE = page.GetComponent<GenComboBox>(nameof(CB_DATABASE));

            CB_COMMAND_TYPE = page.GetComponent<GenComboBox>(nameof(CB_COMMAND_TYPE));

            CB_TABLE = page.GetComponent<GenComboBox>(nameof(CB_TABLE));

            PM_CREATE = page.GetComponent<GenCheckBox>(nameof(PM_CREATE));

            PM_UPDATE = page.GetComponent<GenCheckBox>(nameof(PM_UPDATE));

            PM_DELETE = page.GetComponent<GenCheckBox>(nameof(PM_DELETE));


            await LoadTablesIfDatabaseSelected(page.SelectedItem.CB_DATABASE);
        }


        private void ChangeCrudChangeStates(bool value)
        {
            PM_CREATE.SetValue(value);
            PM_CREATE.EditorEnabled = value;

            PM_UPDATE.SetValue(value);
            PM_UPDATE.EditorEnabled = value;

            PM_DELETE.SetValue(value);
            PM_DELETE.EditorEnabled = value;
        }

        public void OnTableSelectionChanged(object model)
        {
            CB_TABLE.SetValue(model);

            var hasTableValue = !string.IsNullOrEmpty(currentpage.SelectedItem.CB_TABLE);

            ChangeCrudChangeStates(hasTableValue);

            currentpage.StateHasChanged();
        }

        public async Task OnDatabaseChangedAsync(object model)
        {
            //if (model is null)
            //{
            //    CommandTypeChanged(new COMMAND_TYPES());
            //}

            if (model is not DATABASE_INFORMATION databaseInformation) return;
 
            CB_DATABASE.SetValue(model);

            await LoadTablesIfDatabaseSelected(databaseInformation.DI_DATABASE_NAME);

            await CB_TABLE.ValueChanged.InvokeAsync(new TABLE_INFORMATION());

            CommandTypeChanged(new COMMAND_TYPES());

            currentpage.StateHasChanged();
        }

        public async Task ReadAsync(SearchArgs args)
        {
            var result = await Service.ReadAsync();

            DataSource = result.Data;

        }

        public async Task CreateAsync(PAGES_M model)
        {
            var result = await Service.CreateAsync(new RESPONSE_REQUEST<PAGES_M>(model));

            model = result.Data;

            DataSource.Add(model);
        }

      

        public async Task UpdateAsync(PAGES_M model)
        {
            var result = await Service.UpdateAsync(new RESPONSE_REQUEST<PAGES_M>(model));

            DataSource.Replace(model, result.Data);
        }

        public async Task DeleteAsync(PAGES_M model)
        {
            var result = await Service.DeleteAsync(new RESPONSE_REQUEST<PAGES_M>(model));

            DataSource.Remove(model);
        }

        
    }

}

