using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Generator.Components.Interfaces;

public interface INonGenGrid:INonGenView
{
    IDialogService DialogService { get; set; }

    INonGenPage CurrentGenPage { get; set; }

    bool IsFirstRender { get; set; }

    bool ExpandDisabled { get; set; }

    bool IsRendered { get; set; }

    bool HasDetail { get; }

    bool DetailClicked { get; set; }

    bool SearchDisabled { get; set; }

    public DialogResult DialogResult { get; set; }

    public DialogParameters DialogParameters { get; set; }

    public DialogOptions DialogOptions();

    public string ExcelButtonText { get; set; }

    public bool Disabled { get; set; }

    public string SearchText { get; set; }

    public string CancelText { get; set; }

    public string CreateText { get; set; }

    public string UpdateText { get; set; }

    public string DeleteText { get; set; }

    public bool EnableSearch { get; set; }

    bool NewDisabled { get; set; }

    public bool EnableSorting { get; set; }

    public string ExcelFile { get; set; }

    public RenderFragment GenColumns { get; set; }

    public RenderFragment GenSearchFields { get; set; }

    public RenderFragment GenHeaderButtons { get; set; }

 
    public string SearchPlaceHolderText { get; set; }

    bool ForceRenderOnce { get; set; }

    public bool ValidateModel(bool all=false);

    public bool ValidateField(string propertyName);


    bool ValidateSearchFields();

    bool ValidateSearchFields(IEnumerable<IGenComponent> searchFields);

    bool ValidateSearchField(string BindingField);
 
    void ResetValidation(IGenComponent component);

    void ResetValidations(IEnumerable<IGenComponent> components);

    void ResetConditionalSearchFields();
    
    void AddChildComponent(IGenComponent component);

    void AddSearchFieldComponent(IGenComponent component);

    
    //public bool HasErrors();

    void ForceRenderAll();
}
