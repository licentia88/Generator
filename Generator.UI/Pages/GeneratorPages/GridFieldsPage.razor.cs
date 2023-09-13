using Generator.Components.Components;
using Generator.Components.Interfaces;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Models.ComponentModels.NonDB;
using Generator.UI.Extensions;

namespace Generator.UI.Pages.GeneratorPages;

public partial class GridFieldsPage
{
	private List<DISPLAY_FIELD_INFORMATION> DisplayFieldsList { get; set; } = new();
    private List<DISPLAY_FIELD_INFORMATION> ComboDisplayFieldList { get; set; } = new();
    public List<TABLE_INFORMATION> TableList { get; set; } = new();


    public GenComboBox DisplayFieldsCombobox { get; set; }

    private string CurrentDatabase;

    private string SourceDatabase;

 
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        await ReadByParent();

    }

    protected override async Task Load(IGenView<GRID_FIELDS> view)
	{
        await base.Load(view);


        if (ParentModel.CB_COMMAND_TYPE == 1)
            DisplayFieldsList = await DisplayFieldsCombobox.FillAsync(() => DatabaseService.GetFieldsUsingQuery(ParentModel.CB_DATABASE, ParentModel.CB_QUERY_OR_METHOD));

        if (ParentModel.CB_COMMAND_TYPE == 4)
            DisplayFieldsList = await DisplayFieldsCombobox.FillAsync(() => DatabaseService.GetStoredProcedureFieldsAsync(ParentModel.CB_DATABASE, ParentModel.CB_QUERY_OR_METHOD));

      

        if (!view.ParentGrid.ValidateModel())
        {
            view.ShouldShowDialog = false;
            return;
        }

        if (View.ViewState == Components.Enums.ViewState.Create)
		{
			View.SelectedItem.GF_XS = 12;
            View.SelectedItem.GF_SM = 12;
            View.SelectedItem.GF_MD = 12;
            View.SelectedItem.GF_LG = 12;
            View.SelectedItem.GF_XLG = 12;
            View.SelectedItem.GF_XXLG = 12;
        }

        //StateHasChanged();
    }


	 

    

    private async Task DataSourceChanged(object model)
    {
        if (model is not TABLE_INFORMATION data) return;

        var fieldList = await DatabaseService.GetTableFieldsAsync(CurrentDatabase, data.TI_TABLE_NAME);

        DisplayFieldsList = fieldList.Data;
 
        //BuildDbQuery(CurrentModel.GF_DATASOURCE, CurrentModel.GF_VALUEFIELD, CurrentModel.GF_DISPLAYFIELD);
    }
 
    private bool IsSearchField(string bindingField)
    {
        var field = DisplayFieldsList.FirstOrDefault(x => bindingField is not null && x.DFI_NAME == bindingField);

		if(field is null) return false;
        
		return field.DFI_IS_SEARCH_FIELD;
    }

    private void BuildDbQuery(string tableName, string valueField, string displayField)
    {
        View.SelectedItem.GF_DATASOURCE_QUERY = $"SELECT {valueField ?? string.Empty}, {displayField ?? string.Empty} FROM {tableName ?? string.Empty}";
    }

     
}


