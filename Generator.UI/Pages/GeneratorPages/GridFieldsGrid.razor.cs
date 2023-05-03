using System.Data;
using Generator.Components.Args;
using Generator.Components.Components;
using Generator.Components.Interfaces;
using Generator.Shared.Models;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;
using Generator.UI.Helpers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Generator.UI.Pages.GeneratorPages;

public partial class GridFieldsGrid: PagesBase<GRID_CRUD_VIEW,GRID_FIELDS, IGridFieldsService>
{
	//public string Database { get; set; }

 //   public string Source { get; set; }

    public int ExecCommandType { get; set; }


    public List<DISPLAY_FIELD_INFORMATION> DisplayFieldsList { get; set; }

    private List<GEN_COMPONENT_TYPES> GenComponentTypesList;

    private List<TABLE_INFORMATION> TableListInformation;


    private GenComboBox DisplayFieldCombobox;

    private GenComboBox ControlTypeComboBox;

    private GenComboBox InputTypeComboBox;

    private GenComboBox DataSourceComboBox;

    private GenCheckBox IsSearchFieldCheckBox;


    private IGenView<GRID_FIELDS> GenView;

    protected override Task OnInitializedAsync()
    {
        GenComponentTypesList = new();
        DataHelpers.FillGenComponents(ref GenComponentTypesList);
        return base.OnInitializedAsync();
    }

    public Task Search(SearchArgs args)
    {

        return Task.CompletedTask;
    }

    public async Task Load(IGenView<GRID_FIELDS> View)
    {
        GenView = View;

        if (string.IsNullOrEmpty(ParentModel.CB_SOURCE))
        {
            NotificationsView.Notifications.Add(new("Select Source First", MudBlazor.Severity.Error));

            NotificationsView.Fire();

            View.ShoulShowDialog = false;

            return;
        }

        if(View.ViewState == Components.Enums.ViewState.Create)
        {
            View.SelectedItem.GF_XS = 12;
            View.SelectedItem.GF_SM = 12;
            View.SelectedItem.GF_MD = 12;
            View.SelectedItem.GF_LG = 12;
            View.SelectedItem.GF_XLG = 12;
            View.SelectedItem.GF_XXLG = 12;
        }

        DisplayFieldCombobox.BindingField = nameof(GRID_FIELDS.GF_BINDINGFIELD);

        View.SelectedItem.GF_DATABASE = View.Parameters[nameof(PAGES_M.CB_DATABASE)].ToString();

      

        ExecCommandType = Convert.ToInt32(View.Parameters[nameof(PAGES_M.CB_COMMAND_TYPE)]);

        View.SelectedItem.GF_SOURCE = ParentModel.CB_SOURCE;

        RESPONSE_RESULT<List<DISPLAY_FIELD_INFORMATION>> responseResult = null;
        
        if (ExecCommandType == (int)CommandType.Text)
            responseResult = await DatabaseService.GetTableFields(new((View.SelectedItem.GF_DATABASE, View.SelectedItem.GF_SOURCE)));


        if (ExecCommandType == (int)CommandType.StoredProcedure)
            responseResult = await DatabaseService.GetTableFields(new((View.SelectedItem.GF_DATABASE, View.SelectedItem.GF_SOURCE)));


        DisplayFieldsList = responseResult?.Data;

        SetDisplayFieldsCombobox(View);


    }

    public Task Create(GRID_FIELDS model)
    {
        return Task.CompletedTask;

    }

    public Task Update(GRID_FIELDS model)
    {
        return Task.CompletedTask;

    }

    public Task Delete(GRID_FIELDS model)
    {
        return Task.CompletedTask;

    }

    private void SetDisplayFieldsCombobox(IGenView<GRID_FIELDS> View)
    {
        //SELECT* FROM USERS WHERE U_NAME LIKE '%@U_NAME%' AND U_AGE = @U_AGE
        var query = View.Parameters[nameof(PAGES_M.CB_QUERY_METHOD)].ToString();

        var queryParametersDirty = query.Split("@");

        foreach (var dirtyParam in queryParametersDirty.Skip(1))
        {
            var cleanParam = new string(dirtyParam.TakeWhile(x => x != '\'' && x != ' ' && x!='%' ).ToArray());

            var existingField = DisplayFieldsList.FirstOrDefault(x => x.DFI_NAME == cleanParam);

            if(existingField is null)
            {
                DisplayFieldsList.Add(new DISPLAY_FIELD_INFORMATION { DFI_IS_SEARCH_FIELD = true, DFI_NAME = cleanParam });

                continue;
            }

            existingField.DFI_IS_SEARCH_FIELD = true;
        }

        DisplayFieldCombobox.DataSource = DisplayFieldsList.OrderBy(x => x.DFI_NAME);
    }


    private void DisplayFieldChanged(object model)
    {
        if (model is not DISPLAY_FIELD_INFORMATION mdl) return;

        DisplayFieldCombobox.SetValue(model);

        if (mdl.DFI_IS_SEARCH_FIELD)
            IsSearchFieldCheckBox.SetValue(true);
    }

    private async Task ControlTypeChangedAsync(object model)
    {
        if (model is not GEN_COMPONENT_TYPES mdl) return;

        ControlTypeComboBox.SetValue(model);

        if (mdl.GCT_NAME.Equals(nameof(GenTextField)))
            InputTypeComboBox.EditorVisible = true;
        else
            InputTypeComboBox.EditorVisible = false;


        if (mdl.GCT_NAME.Equals(nameof(GenComboBox)))
        {
            DataSourceComboBox.EditorVisible = true;
            //var allTablesResponse = await DatabaseService.GetTableListForConnection(new(View.SelectedItem.GF_DATABASE));
            //DataSourceComboBox.DataSource = allTablesResponse.Data;
        }
        else
            DataSourceComboBox.EditorVisible = false;

    }
}

