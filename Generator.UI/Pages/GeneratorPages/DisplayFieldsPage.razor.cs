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

        [Parameter]
        public List<DATABASE_INFORMATION> DataBaseList { get; set; }

        [Parameter]
        public bool DisableDisplayFields { get; set; }

        [Parameter]
        public List<DISPLAY_FIELD_INFORMATION> DisplayFieldsList { get; set; }
 
        private IGenView<DISPLAY_FIELDS> currentPage;

        private GenTextField DisplayFieldTextbox;

        private GenComboBox DisplayFieldComboBox;
 
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
 
            if (ParentModel.CB_COMMAND_TYPE == CommandType.StoredProcedure.CastTo<int>() && string.IsNullOrEmpty(ParentModel.CB_SQL_COMMAND))
            {
                NotificationsView.Notifications.Add(new NotificationVM("Select Procedure", MudBlazor.Severity.Warning));
            }

            

            if (NotificationsView.Notifications.Any())
                grid.ShowDialog = false;

            NotificationsView.Fire();
        }

        public async Task OnLoad(IGenView<DISPLAY_FIELDS> page)
        {
            currentPage = page;

            currentPage.SelectedItem.DF_DATABASE = ParentModel.CB_DATABASE;

            currentPage.SelectedItem.DF_STORED_PROCEDURE = ParentModel.CB_SQL_COMMAND;

            currentPage.SelectedItem.DF_TABLE = ParentModel.CB_TABLE;

            var result = await DatabaseServices.GetStoredProcedureFields(new((ParentModel.CB_DATABASE,ParentModel.CB_SQL_COMMAND)));

            DisplayFieldsList = result.Data;

            page.GetComponent<GenComboBox>(nameof(DISPLAY_FIELDS.DF_FIELD_NAME)).DataSource = DisplayFieldsList;


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

       
        
        
    }
}

