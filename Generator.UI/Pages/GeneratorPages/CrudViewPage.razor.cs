using System;
using Generator.Components.Args;
using Generator.Components.Components;
using Generator.Components.Interfaces;
using Generator.Shared.Extensions;
using Generator.Shared.Models.ComponentModels;
using Generator.UI.Extensions;
using Generator.UI.Models;
using Microsoft.AspNetCore.Components.Web;

namespace Generator.UI.Pages.GeneratorPages;

public partial class CrudViewPage
{
    public List<TABLE_INFORMATION> TableList { get; set; }

    private GenComboBox SourceComboBox;

    private GenCheckBox CreateCheckBox;
    private GenCheckBox UpdateCheckBox;
    private GenCheckBox DeleteCheckBox;

     

    public override async Task Load(IGenView<CRUD_VIEW> view)
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

        if (view.ViewState != Components.Enums.ViewState.Create)
            MyInitialize();

       


        var res = await DatabaseService.GetTableListForConnection(ParentModel.CB_DATABASE);

        TableList = res.Data;

        SourceComboBox.DataSource = TableList;


        view.Parameters.AddOrReplace(nameof(ParentModel.CB_QUERY_OR_METHOD), ParentModel.CB_QUERY_OR_METHOD);
        view.Parameters.AddOrReplace(nameof(ParentModel.CB_DATABASE), ParentModel.CB_DATABASE);
        view.Parameters.AddOrReplace(nameof(ParentModel.CB_COMMAND_TYPE), ParentModel.CB_COMMAND_TYPE);
        view.Parameters.AddOrReplace(nameof(TableList), TableList);
    }

    private void SourceComboBoxChanged(object model)
    {
        if (model is not TABLE_INFORMATION data) return;

        CreateCheckBox.EditorEnabled = true;
        UpdateCheckBox.EditorEnabled = true;
        DeleteCheckBox.EditorEnabled = true;

        SourceComboBox.SetValue(model);

    }

    private void OnClear(MouseEventArgs args)
    {
        SourceComboBox.OnClearClicked(args);

        CreateCheckBox.EditorEnabled = false;
        UpdateCheckBox.EditorEnabled = false;
        DeleteCheckBox.EditorEnabled = false;

        CreateCheckBox.SetValue(false);
        UpdateCheckBox.SetValue(false);
        DeleteCheckBox.SetValue(false);
    }

    public void MyInitialize()
    {
        CreateCheckBox.EditorEnabled = SourceComboBox.Model is null ? false : true;
        UpdateCheckBox.EditorEnabled = SourceComboBox.Model is null ? false : true;
        DeleteCheckBox.EditorEnabled = SourceComboBox.Model is null ? false : true;
    }

    
}

