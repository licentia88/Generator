using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Spreadsheet;
using Generator.Components.Args;
using Generator.Components.Components;
using Generator.Components.Interfaces;
using Generator.Shared.Extensions;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Models.ComponentModels.Abstracts;
using Generator.Shared.Models.ComponentModels.NonDB;
using Generator.UI.Enums;
using Generator.UI.Extensions;
using Generator.UI.Models;

namespace Generator.UI.Pages.GeneratorPages;

public partial class GridFieldsPage
{
    private GRID_FIELDS CurrentModel;

	private List<DISPLAY_FIELD_INFORMATION> DisplayFieldsList { get; set; } = new();
    private List<DISPLAY_FIELD_INFORMATION> ComboDisplayFieldList { get; set; } = new();

    private List<TABLE_INFORMATION> TableList { get; set; } = new();

	
	private GenComboBox DisplayFieldCombobox;
	private GenCheckBox IsSearchFieldCheckBox;
    private GenComboBox ControlTypeComboBox;

    private GenComboBox InputTypeComboBox;
    private GenComboBox DataSourceComboBox;
    private GenComboBox DataSourceValueFieldComboBox;
    private GenComboBox DataSourceDisplayFieldComboBox;
    private GenTextField TrueTextCheckBox;
    private GenTextField FalseTextCheckBox;
    private GenTextField FormatTextField;
    private GenTextField DataSourceQueryTextField;

    private string CurrentDatabase;

    private string SourceDatabase;

    private List<IGenComponent> HiddenFields;

    

	public override async Task Load(IGenView<GRID_FIELDS> View)
	{
        CurrentModel = View.SelectedItem;

        HiddenFields = new List<IGenComponent>
        {
                InputTypeComboBox, DataSourceComboBox,
                DataSourceValueFieldComboBox,
                DataSourceDisplayFieldComboBox,
                TrueTextCheckBox, FalseTextCheckBox,
                FormatTextField, DataSourceQueryTextField
        };

        if (View.ViewState == Components.Enums.ViewState.Create)
		{
			View.SelectedItem.GF_XS = 12;
            View.SelectedItem.GF_SM = 12;
            View.SelectedItem.GF_MD = 12;
            View.SelectedItem.GF_LG = 12;
            View.SelectedItem.GF_XLG = 12;
            View.SelectedItem.GF_XXLG = 12;
        }


        TableList = View.Parameters.GetFromDict(nameof(TableList)).CastTo<List<TABLE_INFORMATION>>();

        CurrentDatabase = View.Parameters.GetFromDict(nameof(COMPONENTS_BASE.CB_DATABASE)).ToString();
        //SourceDatabase = View.Parameters.GetFromDict(nameof(CRUD_VIEW.VBM_SOURCE)).ToString();

        DisplayFieldCombobox.BindingField = nameof(GRID_FIELDS.GF_BINDINGFIELD);

		IsSearchFieldCheckBox.Checked = IsSearchField(View.SelectedItem.GF_BINDINGFIELD);

        await GetQuerySelectFields(View);

		GetQueryParameters(View);

    }


	private void DisplayFieldChanged(object obj)
	{
		if (obj is not DISPLAY_FIELD_INFORMATION comboModel) return;

        IsSearchFieldCheckBox.SetValue(comboModel.DFI_IS_SEARCH_FIELD);

        DisplayFieldCombobox.SetValue(obj);
    }

    private void DataSourceDisplayFieldChanged(object obj)
    {
        if (obj is not DISPLAY_FIELD_INFORMATION comboModel) return;

        DataSourceDisplayFieldComboBox.SetValue(obj);

        BuildDbQuery(CurrentModel.GF_DATASOURCE, CurrentModel.GF_VALUEFIELD, CurrentModel.GF_DISPLAYFIELD);
    }

    private void DataSourceValueFieldChanged(object obj)
    {
        if (obj is not DISPLAY_FIELD_INFORMATION comboModel) return;

        DataSourceValueFieldComboBox.SetValue(obj);

        BuildDbQuery(CurrentModel.GF_DATASOURCE, CurrentModel.GF_VALUEFIELD, CurrentModel.GF_DISPLAYFIELD);
    }

    private async Task DataSourceChanged(object model)
    {
        if (model is not TABLE_INFORMATION data) return;

        var fieldList = await DatabaseService.GetTableFields(CurrentDatabase, data.TI_TABLE_NAME);

        DisplayFieldsList = fieldList.Data;

        DataSourceDisplayFieldComboBox.SetValue(new TABLE_INFORMATION());
        DataSourceValueFieldComboBox.SetValue(new TABLE_INFORMATION());

        DataSourceDisplayFieldComboBox.DataSource = DisplayFieldsList;
        DataSourceValueFieldComboBox.DataSource = DisplayFieldsList;

        
        DataSourceQueryTextField.SetValue($"SELECT * FROM {data.TI_TABLE_NAME}");
        DataSourceComboBox.SetValue(model);

        BuildDbQuery(CurrentModel.GF_DATASOURCE, CurrentModel.GF_VALUEFIELD, CurrentModel.GF_DISPLAYFIELD);
    }


    private void ControlTypeChanged(object obj)
	{
		if (obj is not CODE_ENUM controlType) return;

        if (controlType.C_CODE == (int)ComponentTypes.ComboBox)
        {
			DataSourceComboBox.EditorVisible = true;
            DataSourceValueFieldComboBox.EditorVisible = true;
            DataSourceDisplayFieldComboBox.EditorVisible = true;
            DataSourceQueryTextField.EditorVisible = true;

            DataSourceComboBox.DataSource = TableList;

            ClearExcept(DataSourceComboBox, DataSourceValueFieldComboBox, DataSourceDisplayFieldComboBox, DataSourceQueryTextField);

        }
        else if (controlType.C_CODE == (int)ComponentTypes.CheckBox)
        {
			TrueTextCheckBox.EditorVisible = true;
			FalseTextCheckBox.EditorVisible = true;

            ClearExcept(TrueTextCheckBox, FalseTextCheckBox);
		}
        else if (controlType.C_CODE == (int)ComponentTypes.DatePicker)
        {
            //formatte
            FormatTextField.EditorVisible = true;
            ClearExcept(FormatTextField);
        }
        else if (controlType.C_CODE == (int)ComponentTypes.TextField)
        {
			InputTypeComboBox.EditorVisible = true;
            ClearExcept(InputTypeComboBox);
        }
		else
		{
            DataSourceComboBox.EditorVisible = false;
            TrueTextCheckBox.EditorVisible = false;
            FalseTextCheckBox.EditorVisible = false;
            InputTypeComboBox.EditorVisible = false;
            //Clear All

            ClearExcept();
        }


		ControlTypeComboBox.SetValue(obj);
    }

	private void ClearExcept(params IGenComponent[] genComponents)
	{
        var fieldsToClear = HiddenFields.Where(x => !genComponents.Contains(x)).ToList();

        foreach (var field in fieldsToClear)
        {
            field.Model?.SetPropertyValue(field.BindingField, null);
            field.EditorVisible = false;
        }

    }
	
	private async Task GetQuerySelectFields(IGenView<GRID_FIELDS> View)
	{
		var activeDatabase = View.Parameters.GetFromDict(nameof(COMPONENTS_BASE.CB_DATABASE))?.ToString();
        var sourceDatabase = View.Parameters.GetFromDict(nameof(CRUD_VIEW.VBM_SOURCE))?.ToString();

        var currentQueryOrMethod = View.Parameters.GetFromDict(nameof(COMPONENTS_BASE.CB_QUERY_OR_METHOD))?.ToString();

        var responseResult = await DatabaseService.GetFieldsUsingQuery(activeDatabase, currentQueryOrMethod);


        DisplayFieldsList.AddRange(responseResult.Data);

        if (!string.IsNullOrEmpty(sourceDatabase))
        {
            var sourceFields = await DatabaseService.GetTableFields(activeDatabase, sourceDatabase);

            foreach (var sourceField in sourceFields.Data)
            {
                var existing = DisplayFieldsList.FirstOrDefault(x => x.DFI_NAME == sourceField.DFI_NAME);

                if (existing is null)
                    DisplayFieldsList.Add(sourceField);
            }
        }
        

		DisplayFieldCombobox.DataSource = DisplayFieldsList;
    }

	private void GetQueryParameters(IGenView<GRID_FIELDS> View)
    {
        var currentQueryOrMethod = View.Parameters.GetFromDict(nameof(COMPONENTS_BASE.CB_QUERY_OR_METHOD))?.ToString();

        string pattern = @"@\w+";
        Regex regex = new Regex(pattern);

        MatchCollection matches = regex.Matches(currentQueryOrMethod);

	
        foreach (Match match in matches)
        {
            var existingField = DisplayFieldsList.FirstOrDefault((x) => x.DFI_NAME.Equals(match.Value.Substring(1)));

			if (existingField is null)
				DisplayFieldsList.Add(new DISPLAY_FIELD_INFORMATION { DFI_NAME = match.Value, DFI_IS_SEARCH_FIELD = true });
			else
				existingField.DFI_IS_SEARCH_FIELD = true;

            DisplayFieldCombobox.DataSource = DisplayFieldsList;

        }
    }

    private bool IsSearchField(string bindingField)
    {
        var field = DisplayFieldsList.FirstOrDefault(x => bindingField is not null && x.DFI_NAME == bindingField);

		if(field is null) return false;
        
		return field.DFI_IS_SEARCH_FIELD;
    }

    private void BuildDbQuery(string tableName, string valueField, string displayField)
    {
        DataSourceQueryTextField.SetValue($"SELECT {valueField??string.Empty}, {displayField??string.Empty} FROM {tableName??string.Empty}");
    }
}


