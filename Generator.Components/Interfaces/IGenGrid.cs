using Generator.Components.Args;
using Generator.Components.Validators;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Generator.Components.Interfaces;

public interface INonGenGrid:INonGenView
{
    public INonGenPage CurrentGenPage { get; set; }

    public IDialogService DialogService { get; set; }

    public DialogResult DialogResult { get; set; }

    public DialogParameters DialogParameters { get; set; }

    public DialogOptions DialogOptions();

    public bool Disabled { get; set; }

    public string SearchText { get; set; }

    public string CancelText { get; set; }

    public string CreateText { get; set; }

    public string UpdateText { get; set; }

    public string DeleteText { get; set; }

    public bool EnableSearch { get; set; }

    public bool IsFirstRender { get; set; }

    public bool NewDisabled { get; set; }

    public bool ExpandDisabled { get; set; }

    public bool SearchDisabled { get; set; }

    public bool EnableSorting { get; set; }

    public bool IsRendered { get; set; }

    public RenderFragment GenColumns { get; set; }

    public RenderFragment GenSearchFields { get; set; }

    public RenderFragment GenHeaderButtons { get; set; }

    public bool HasDetail { get; }

    public bool DetailClicked { get; set; }

    public INonGenGrid ParentGrid { get; set; }

    public string SearchPlaceHolderText { get; set; }

    public bool ForceRenderOnce { get; set; }

    public bool ValidateModel(bool all=false);

    public bool ValidateValue(string propertyName);

    public bool ValidateSearchFields();

    public bool ValidateSearchFields(IEnumerable<IGenComponent> searchFields);

    public bool ValidateSearchFields(string bindingField);
 
    public void ResetValidation(IGenComponent component);

    public void AddChildComponent(IGenComponent component);

    public void AddSearchFieldComponent(IGenComponent component);

    public bool HasErrors();


}

public interface IGenGrid<TModel> : INonGenGrid, IGenView<TModel> where TModel:new()  
{
    public GenValidator<TModel> GenValidator { get; set; }

    public ICollection<TModel> DataSource { get; set; }

    public RenderFragment<TModel> GenDetailGrid { get; set; }

    public EventCallback<TModel> Create { get; set; }

    public EventCallback<TModel> Update { get; set; }

    public EventCallback<TModel> Delete { get; set; }

    public EventCallback<TModel> Cancel { get; set; }

    public EventCallback<TModel> OnBeforeSubmit { get; set; }

    public EventCallback<TModel> OnAfterSubmit { get; set; }

    public EventCallback<TModel> OnBeforeCancel { get; set; }

    public EventCallback<TModel> OnAfterCancel { get; set; }

    public EventCallback<IGenView<TModel>> Load { get; set; }


    //public EventCallback<IGenGrid<TModel>> OnBeforeShowDialog { get; set; }

    public EventCallback Close { get; set; }

    public EventCallback<SearchArgs> Search { get; set; }

    public Task OnDetailClicked(TModel context);

   

}
