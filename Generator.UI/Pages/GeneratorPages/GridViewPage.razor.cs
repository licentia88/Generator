using Generator.Components.Interfaces;
using Generator.Shared.Models.ComponentModels;
using Generator.UI.Models;

namespace Generator.UI.Pages.GeneratorPages;

public partial class GridViewPage
{
    public List<TABLE_INFORMATION> TableList { get; set; }

 
    protected override async Task OnInitializedAsync()
    {
        await ReadByParent();

    }

    protected override async Task Load(IGenView<GRID_VIEW> view)
    {
        await base.Load(view);

        View.SelectedItem.VBM_TITLE = ParentModel.CB_TITLE;
        
        if (string.IsNullOrEmpty(ParentModel.CB_DATABASE))
            NotificationsView.Notifications.Add(new NotificationVM("Select Database First", MudBlazor.Severity.Warning));


        if (string.IsNullOrEmpty(ParentModel.CB_QUERY_OR_METHOD))
            NotificationsView.Notifications.Add(new NotificationVM("Specify Query First", MudBlazor.Severity.Warning));


        if (NotificationsView.Notifications.Any())
        {
            View.ShoulShowDialog = false;

            NotificationsView.Fire();
            return;
        }

 
        //view.Parameters.AddOrReplace(nameof(ParentModel.CB_QUERY_OR_METHOD), ParentModel.CB_QUERY_OR_METHOD);
        //view.Parameters.AddOrReplace(nameof(ParentModel.CB_DATABASE), ParentModel.CB_DATABASE);

 
        //view.Parameters.AddOrReplace(nameof(ParentModel.CB_COMMAND_TYPE), ParentModel.CB_COMMAND_TYPE);
        //view.Parameters.AddOrReplace(nameof(TableList), TableList);
    }

}

