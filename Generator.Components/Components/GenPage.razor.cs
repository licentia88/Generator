using Generator.Components.Enums;
using Generator.Components.Extensions;
using Generator.Components.Interfaces;
//using Generator.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Generator.Components.Components;

public partial class GenPage<TModel> : IGenPage<TModel>, IDisposable where TModel : new()
{
    [CascadingParameter]
    public MudDialogInstance MudDialog { get; set; }

    [Parameter]
    public GenGrid<TModel> GenGrid { get; set; }

    [Parameter]
    public string Title { get; set; }

    [Parameter]
    public INonGenGrid Parent { get; set; }

    [Parameter]
    public ViewState ViewState { get; set; }

    [Parameter]
    public EditMode EditMode { get; set; }

    [Parameter]
    public TModel OriginalEditItem { get; set; }

    [Parameter]
    public TModel SelectedItem { get; set; }

    [Parameter]
    public bool IsIndividual { get; set; }

    [Parameter]
    public List<(Type type, IGenComponent component)> Components { get; set; }

    [Parameter]
    public List<IGenControl> SearchFieldComponents { get; set; }

    [Parameter]
    public EventCallback<IGenView<TModel>> Load { get; set; }

    [Parameter]
    public Dictionary<string, object> Parameters { get; set; } = new();

    [CascadingParameter(Name =nameof(Parameters))]
    private Dictionary<string, object> _Parameters { get => Parameters; set => Parameters = value; }

    internal EventCallback RefreshParentGrid { get; set; }

    public bool EnableModelValidation { get; set; }

    bool INonGenView.IsTopLevel { get; set; }

    bool INonGenPage.IsValid { get; set; }

    public bool ShouldShowDialog { get; set; } = true;

    protected override async Task OnInitializedAsync()
    {
        ((INonGenGrid)GenGrid).CurrentGenPage = this;


        if (Load.HasDelegate)
        {
            await Load.InvokeAsync(this);
            if (!ShouldShowDialog)
            {
                Close(true);
                return;
            }
        }

        RefreshParentGrid = EventCallback.Factory.Create(this, GenGrid.RefreshButtonState);

        await base.OnInitializedAsync();
          
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        GetSubmitTextFromViewState();

        await base.OnAfterRenderAsync(firstRender);
    }       

    public bool Validate()
    {
        ((INonGenPage)this).IsValid = GenGrid.ValidateModel();
        GenGrid.IsValid = ((INonGenPage)this).IsValid;
        StateHasChanged();

        return ((INonGenPage)this).IsValid;
    }

    internal void OnInvalidSubmit()
    {
        Validate();
    }

        
    async Task INonGenView.OnCommit()
    {
        ((INonGenView)this).IsTopLevel = true;
        await ((IGenView<TModel>)this).OnCommit(SelectedItem, ViewState.None);
    }

    async Task IGenView<TModel>.OnCommit(TModel model)
    {
        await ((IGenView<TModel>)this).OnCommit(model, ViewState);
    }

    async Task IGenView<TModel>.OnCommit(TModel model, ViewState viewState)
    {
        if (!Validate()) return;

        if (IsIndividual)
        {
            await ((IGenGrid<TModel>)GenGrid).OnCommit(SelectedItem, viewState);

            ViewState = ViewState.None;

            CloseIfAllowed();
            return;
        }

        //Parent Save
        if (GenGrid.Parent?.ViewState == ViewState.Create)
        {
            GenGrid.Parent.CurrentGenPage.Validate();
            await GenGrid.Parent.CurrentGenPage.OnCommitAndWait();
        }

        if (((INonGenView)this).IsTopLevel || GenGrid.Parent.CurrentGenPage.IsValid)
        {
           
            //GenGrid.OriginalTable.RowEditCommit.Invoke(SelectedItem);
            await ((IGenGrid<TModel>)GenGrid).OnCommit(SelectedItem, viewState);

            if (!GenGrid.IsValid) return;

            ViewState = ViewState.None;

            CloseIfAllowed();
        }
    }

    async Task INonGenView.OnCommitAndWait()
    {
        if (((INonGenPage)this).IsValid)
        {
            ViewState = ViewState.Update;

            await ((IGenGrid<TModel>)GenGrid).OnCommit(SelectedItem, ViewState.Update);
        }

        StateHasChanged();
    }

    private void CloseIfAllowed()
    {
        if (!((INonGenView)this).IsTopLevel) return;
        GenGrid.RefreshButtonState();
        ((INonGenGrid)GenGrid).ResetValidations(Components.Select(x => x.component as IGenControl));
        MudDialog.Close();
    }

    void  INonGenPage.Close()
    {
        Close(false);
        CloseIfAllowed();
    }

    public void Close(bool force)
    {
        if (force) ((INonGenView)this).IsTopLevel = true;

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

       

    public TComponent GetComponent<TComponent>(string bindingField) where TComponent : IGenControl
    {
        var item = Components.FirstOrDefault(x => x.component.BindingField is not null && x.component.BindingField.Equals(bindingField));

        return item.component is null ? default : item.component.CastTo<TComponent>();
    }
 
    public void Dispose()
    {
        if (ViewState != ViewState.None)
        {
            Close(true);
            //MudDialog.Close();

            //Cancel eventini tetikler
            GenGrid.OriginalTable.RowEditCancel.Invoke(OriginalEditItem);
        }

        (GenGrid as INonGenGrid).ForceRenderAll();

      

        RefreshParentGrid.InvokeAsync();

        ((INonGenGrid)GenGrid).ResetValidations(Components.Select(x => x.component as IGenControl));

        MudDialog.Dispose();

           
    }

    public TComponent GetSearchFieldComponent<TComponent>(string bindingField) where TComponent : IGenControl
    {
        var item = SearchFieldComponents.FirstOrDefault(x => x.BindingField is not null && x.BindingField.Equals(bindingField));

        return item is null ? default : item.CastTo<TComponent>();
    }

    public bool HasErrors()
    {
        var result = Components.Any(x => x.component is IGenControl cntrl && cntrl.Error);

        return result;
    }

    public void AddChildComponent(IGenComponent component)
    {
        //throw new NotImplementedException();
    }

    object INonGenPage.GetSelectedItem()
    {
        return SelectedItem;
    }
}