using Generator.Components.Enums;
using Generator.Components.Extensions;
using Generator.Components.Interfaces;
//using Generator.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Generator.Components.Components;

public partial class GenPage<TModel> :IDisposable, IGenPage<TModel> where TModel : new() 
{
    [CascadingParameter]
    public MudDialogInstance MudDialog { get; set; }

    [Parameter]
    public GenGrid<TModel> GenGrid { get; set; }

    [Parameter]
    public string Title { get; set; }

    [Parameter]
    public ViewState ViewState { get; set; }

    [Parameter]
    public EditMode EditMode { get; set; }

    public TModel OriginalEditItem { get; set; }

    //[Parameter]
    public EventCallback RefreshParentGrid { get; set; }

    [Parameter]
    public TModel SelectedItem { get; set; }


    public bool EnableModelValidation { get; set; }

    public bool IsTopLevel { get; set; }

    public bool IsValid { get; set; }

    public bool ShoulShowDialog { get; set; } = true;


    [Parameter]
    public List<IGenComponent> Components { get; set; }

    [Parameter]
    public List<IGenComponent> SearchFieldComponents { get; set; }


    //[Parameter]
    //public EventCallback<IGenView<TModel>> Load { get; set; }

    [Parameter]
    public Dictionary<string, object> Parameters { get; set; } = new();

    [CascadingParameter(Name =nameof(Parameters))]
    private Dictionary<string, object> _Parameters { get => Parameters; set => Parameters = value; }

    protected override async Task OnInitializedAsync()
    {
        GenGrid.CurrentGenPage = this;


        //if (Load.HasDelegate)
        //    await Load.InvokeAsync(this);

        RefreshParentGrid = EventCallback.Factory.Create(this, GenGrid.RefreshButtonState);

        await base.OnInitializedAsync();
          
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        //if (OnRender.HasDelegate)
        //    await OnRender.InvokeAsync(this);

       

        GetSubmitTextFromViewState();

        await base.OnAfterRenderAsync(firstRender);
    }

       

    public bool ValidateAsync()
    {
        IsValid =  GenGrid.ValidateModel(true);
        StateHasChanged();

        return IsValid;
    }



    public void onInvalidSubmit()
    {
        ValidateAsync();
    }

        
    public async Task OnCommit()
    {
        IsTopLevel = true;
        await  OnCommit(SelectedItem, ViewState.None);
    }

    public async Task OnCommit(TModel model)
    {
        await  OnCommit(model, ViewState);
    }

    public  async Task OnCommit(TModel model, ViewState viewState)
    {
        if (!ValidateAsync()) return;

        //Parent Save
        if (GenGrid.ParentGrid?.ViewState == ViewState.Create)
            await GenGrid.ParentGrid.CurrentGenPage.OnCommitAndWait();

        if (IsTopLevel || GenGrid.ParentGrid.CurrentGenPage.IsValid)
        {
            GenGrid.OriginalTable.RowEditCommit.Invoke(SelectedItem);
            //await GenGrid.OnCommit(SelectedItem, viewState);

            ViewState = ViewState.None;

            CloseIfAllowed();
        }
 
    }

    public async Task OnCommitAndWait()
    {
        ValidateAsync();

        if (IsValid)
        {
            ViewState = ViewState.Update;

            await GenGrid.OnCommit(SelectedItem, ViewState.Update);
        }

        StateHasChanged();
    }


    private void CloseIfAllowed()
    {
        if (!IsTopLevel) return;
        GenGrid.RefreshButtonState();
        GenGrid.ResetValidation();
        MudDialog.Close();
    }
    public void Close()
    {
        Close(false);
        CloseIfAllowed();
    }

    public void Close(bool force)
    {
        if (force) IsTopLevel = true;

        CloseIfAllowed();

    }


    internal string GetSubmitTextFromViewState()
    {
        return ViewState switch
        {
            ViewState.Create => GenGrid.CreateText,
            ViewState.Update => GenGrid.UpdateText,
            ViewState.Delete => GenGrid.DeleteText,
            _ => ""
        };
    }


    public new void StateHasChanged()
    {
        base.StateHasChanged();
    }

       

    public TComponent GetComponent<TComponent>(string bindingField) where TComponent : IGenComponent
    {
        var item = Components.FirstOrDefault(x => x.BindingField is not null && x.BindingField.Equals(bindingField));

        return item is null ? default : item.CastTo<TComponent>();
    }
 
    public void Dispose()
    {
        if (ViewState != ViewState.None)
        {
            MudDialog.Close();
            GenGrid.OriginalTable.RowEditCancel.Invoke(SelectedItem);
        }

 
        RefreshParentGrid.InvokeAsync();
        GenGrid.ResetValidation();
        MudDialog.Dispose();

           
    }

    public TComponent GetSearchFieldComponent<TComponent>(string bindingField) where TComponent : IGenComponent
    {
        var item = SearchFieldComponents.FirstOrDefault(x => x.BindingField is not null && x.BindingField.Equals(bindingField));

        return item is null ? default : item.CastTo<TComponent>();
    }
}