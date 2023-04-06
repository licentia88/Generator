using System;
using System.Collections.ObjectModel;
using System.Data;
using Generator.Components.Components;
using Generator.Components.Interfaces;
using Generator.Examples.Shared;
using Generator.Shared.Extensions;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;
using Generator.UI.Models;
using Microsoft.AspNetCore.Components;

namespace Generator.UI.Pages.GeneratorPages
{
	public partial class DisplayFieldsPage
	{
        [Inject]
        public IDatabaseService DatabaseServices { get; set; }

        public List<DATABASE_INFORMATION> DataBaseList { get; set; }  

        public List<TABLE_INFORMATION> TableList { get; set; }  

        public List<DISPLAY_FIELD_INFORMATION> DisplayFieldsList { get; set; }
 
        private IGenView<DISPLAY_FIELDS> currentPage;

        private GenTextField DisplayFieldTextbox;

        private GenComboBox DisplayFieldComboBox;

        [Parameter]
        public bool DisableDisplayFields { get; set; }

        private bool HasErrors = false;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        public void OnBeforeShow(IGenGrid<DISPLAY_FIELDS> grid)
        {
            if (string.IsNullOrEmpty(ParentModel.CB_DATABASE))
            {
                NotificationsView.Notifications.Add(new NotificationVM("Select Database", MudBlazor.Severity.Warning));
            }

            if (ParentModel.CB_COMMAND_TYPE == 0)
            {
                NotificationsView.Notifications.Add(new NotificationVM("Select Command Type", MudBlazor.Severity.Warning));
            }

            if (ParentModel.CB_COMMAND_TYPE == CommandType.Text.CastTo<int>() && string.IsNullOrEmpty(ParentModel.CB_TABLE))
            {
                NotificationsView.Notifications.Add(new NotificationVM("Select Table", MudBlazor.Severity.Warning));
            }



            if (ParentModel.CB_COMMAND_TYPE == CommandType.StoredProcedure.CastTo<int>() && string.IsNullOrEmpty(ParentModel.CB_SQL_COMMAND))
            {
                NotificationsView.Notifications.Add(new NotificationVM("Select Procedure", MudBlazor.Severity.Warning));
            }

            if(NotificationsView.Notifications.Any())
                grid.ShowDialog = false;


            NotificationsView.Fire();


        }

        public async Task OnLoad(IGenView<DISPLAY_FIELDS> page)
        {
            currentPage = page;

            DisplayFieldTextbox = page.GetComponent<GenTextField>(nameof(DisplayFieldTextbox));
            DisplayFieldComboBox = page.GetComponent<GenComboBox>(nameof(DisplayFieldComboBox));
            DisplayFieldTextbox.BindingField = nameof(DISPLAY_FIELDS.DF_FIELD_NAME);
            DisplayFieldComboBox.BindingField = nameof(DISPLAY_FIELDS.DF_FIELD_NAME);


            var result = await DatabaseServices.GetDatabaseList();

            //DataBaseList = result.Data;




            //var parameter = (SelectedDatabase, StoredProcedureName);

            //var response = await DatabaseServices.GetStoredProcedureFields(new(parameter));

            //DisplayFieldsList = response.Data;

            //var parameter = (SelectedDatabase, SelectedTable);

            //var response = await DatabaseServices.GetStoredProcedureFields(new(parameter));

            //DisplayFieldsList = response.Data;


            //DisplayFieldComboBox.EditorVisible = true;

        }

        public void OnClose()
        {
            DisplayFieldTextbox.BindingField = nameof(DisplayFieldTextbox);
            DisplayFieldComboBox.BindingField = nameof(DisplayFieldComboBox);
        }

        public Task CreateAsync(DISPLAY_FIELDS model)
        {
            return Task.CompletedTask;
        }

        public Task ReadAsync()
        {
            return Task.CompletedTask;
        }

        public Task UpdateAsync(DISPLAY_FIELDS model)
        {
            return Task.CompletedTask;
        }

        public Task DeleteAsync(DISPLAY_FIELDS model)
        {
            return Task.CompletedTask;
        }

       
        public async Task OnDatabaseChangedAsync(object model)
        {

        }

        public async Task OnTableChangedAsync(object model)
        {

        }
        
    }
}

