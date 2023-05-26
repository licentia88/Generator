using Generator.Client;
using Generator.Components.Components;
using Generator.Components.Interfaces;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;
using Generator.UI.Extensions;
using Generator.UI.Models;
using Grpc.Net.Client;

namespace Generator.UI.Pages.GeneratorPages;

public partial class GridCrudViewGrid 
{
    public List<TABLE_INFORMATION> TableList { get; set; }

    private GenComboBox SourceComboBox;

 
    public Task Create(CRUD_VIEW model)
    {

        return Task.CompletedTask;
    }

    private async Task Load(IGenView<CRUD_VIEW> view)
    {
        if (string.IsNullOrEmpty(ParentModel.CB_DATABASE))
            NotificationsView.Notifications.Add(new NotificationVM("Select Database First", MudBlazor.Severity.Warning));


        if (string.IsNullOrEmpty(ParentModel.CB_QUERY_OR_METHOD))
            NotificationsView.Notifications.Add(new NotificationVM("Specify Query First", MudBlazor.Severity.Warning));


        if (NotificationsView.Notifications.Any())
        {
            view.ShoulShowDialog = false;

            NotificationsView.Fire();
            return;
        }

        view.Parameters.AddOrReplace(nameof(ParentModel.CB_QUERY_OR_METHOD), ParentModel.CB_QUERY_OR_METHOD);
        view.Parameters.AddOrReplace(nameof(ParentModel.CB_DATABASE), ParentModel.CB_DATABASE);
        view.Parameters.AddOrReplace(nameof(ParentModel.CB_COMMAND_TYPE), ParentModel.CB_COMMAND_TYPE);
        //view.Parameters.AddOrReplace(nameof(CRUD_VIEW.CB_SOURCE), view.SelectedItem.CB_SOURCE);


        var res = await DatabaseService.GetTableListForConnection(ParentModel.CB_DATABASE);

        TableList = res.Data;

        SourceComboBox.DataSource = TableList;
        //view.SelectedItem.VB_TYPE = nameof(CrudViewGrid);

        //return Task.CompletedTask;
    }

}

