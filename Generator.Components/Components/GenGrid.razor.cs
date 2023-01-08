using Generator.Components.Args;
using Generator.Components.Enums;
using Generator.Components.Interfaces;
using Generator.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazorFix;
using System.Diagnostics.CodeAnalysis;

namespace Generator.Components.Components;

public partial class GenGrid<TModel>  
{
    internal bool GridIsBusy = false;

    internal GenPage<TModel> CurrentGenPage { get; set; }

    public GridManager<TModel> GridManager { get; }

    [Inject]
    public IDialogService DialogService { get; set; }

    public DialogResult DialogResult { get; set; }

    public DialogParameters DialogParameters { get; set; } = new DialogParameters();

    public DialogOptions DialogOptions { get; set; } = new()
    {
        MaxWidth = MaxWidth.Medium,
        FullWidth = true,
        CloseButton = true,
        CloseOnEscapeKey = true,
        DisableBackdropClick = true,
        Position = DialogPosition.Center
    };

    internal List<Action> EditButtonActionList { get; set; } = new();

    internal bool IgnoreErrors = true;

    public void OnEditContextButtonClick(EditButtonContext button)
    {
        if (ViewState == ViewState.None)
        {
            ViewState = ViewState.Update;
        }

        button.ButtonAction.Invoke();
    }

    public bool HasErrors()
    {
        var result = Components.Any(x => x.Error);

        IgnoreErrors = !result;

        return result;
    }

    public ViewState ViewState { get; set; } = ViewState.None;

    public MudIconButton EditButtonRef { get; set; }

    public bool DetailClicked { get; set; }

    public List<IGenComponent> Components { get; set; } = new();

    private string _searchString = string.Empty;

    public bool IsFirstRender { get; set; } = true;

    public bool SearchDisabled { get; set; }

    public object OriginalEditItem { get; set; }

    private string AddIcon { get; set; } = Icons.Material.Filled.AddCircle;

    [Parameter]
    public EventCallback<GenGridArgs> Create { get; set; }

    [Parameter]
    public EventCallback<GenGridArgs> Delete { get; set; }

    [Parameter]
    public EventCallback<GenGridArgs> Update { get; set; }

    [Parameter]
    public EventCallback<GenGridArgs> Cancel { get; set; }

    //Form Load
    [Parameter]
    public EventCallback<IGenView> Load { get; set; }

    [CascadingParameter(Name = nameof(ParentComponent))]
    public GenGrid<object> ParentComponent { get; set; }

    [Parameter]
    public string Title { get; set; }

    [Parameter]
    public bool EnableSearch { get; set; }

    [Parameter]
    public string SearchPlaceHolderText { get; set; } = "Search";

    [Parameter, EditorRequired]
    public ICollection<TModel> DataSource { get; set; }

    [Parameter, AllowNull]
    public RenderFragment GenColumns { get; set; }

    [Parameter, AllowNull]
    public RenderFragment GenHeaderButtons { get; set; }

    [Parameter, AllowNull]
    public RenderFragment<object> GenDetailGrid { get; set; }

    public bool HasDetail => GenDetailGrid is not null;

    [Parameter, EditorRequired]
    public EditMode EditMode { get; set; }

    public bool NewDisabled { get; set; }

    public bool ExpandDisabled { get; set; }

    [Parameter]
    public string CancelText { get; set; } = "Cancel";

    [Parameter]
    public string CreateText { get; set; } = "Create";

    [Parameter]
    public string UpdateText { get; set; } = "Update";

    [Parameter]
    public string DeleteText { get; set; } = "Delete";

    public GenGrid()
    {
        GridManager = new GridManager<TModel>(this);
    }

    protected override async Task OnInitializedAsync()
    {
        //Detail Grid den parent gridi refreshleme
        if (ParentComponent is not null && ParentComponent.DetailClicked)
        {
            await InvokeAsync(ParentComponent.StateHasChanged);
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender && !GridIsBusy)
            await OnNewItemAddEditInvoker();
    }

    /// <summary>
    /// Invokes the eventcallback depending on the viewstate
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    internal async Task InvokeCallBackByViewState(object model)
    {
        try
        {
            GridIsBusy = true;
            switch (ViewState)
            {
                case ViewState.Create when Create.HasDelegate:

                    if (EditMode == EditMode.Inline)
                    {
                        DataSource.RemoveAt(0);
                    }
                    await Create.InvokeAsync(new GenGridArgs(null, model, EditMode, DataSource.IndexOf(SelectedItem)));
                    break;

                case ViewState.Update when Update.HasDelegate:
                    await Update.InvokeAsync(new GenGridArgs(OriginalEditItem, model, EditMode, DataSource.IndexOf(SelectedItem)));
                    break;

                case ViewState.Delete when Delete.HasDelegate:
                    await Delete.InvokeAsync(new GenGridArgs(OriginalEditItem, model, EditMode, DataSource.IndexOf(SelectedItem)));
                    break;

                case ViewState.None when Cancel.HasDelegate:
                    await Cancel.InvokeAsync(new GenGridArgs(OriginalEditItem, model, EditMode, DataSource.IndexOf(SelectedItem)));
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            if(EditMode != EditMode.Inline)
                CurrentGenPage.PreventClose = false;

            SearchDisabled = false;
            NewDisabled = false;
            ExpandDisabled = false;
            ViewState = ViewState.None;

            GridIsBusy = false;

        }
        catch (Exception e)
        {
            if (EditMode != EditMode.Inline)
                CurrentGenPage.PreventClose = true;

            GridIsBusy = false;
        }

        StateHasChanged();

    }

    public Task OnEditCLick()
    {
        return Task.CompletedTask;

        //if (Load.HasDelegate && ViewState != ViewState.Create)
        //    await Load.InvokeAsync(this);
    }

    private async void OnCancelClick(TModel element)
    {
        if (ViewState == ViewState.None) return;

        if (ViewState == ViewState.Create)
            DataSource.Remove(element);
        else 
            DataSource.Replace(element, OriginalEditItem.CastTo<TModel>());

        ViewState = ViewState.None;

        await InvokeCallBackByViewState(element);

        Components.ForEach(x => x.Error = false);
    }

    public virtual async ValueTask<IDialogReference> ShowDialogAsync<TPage>() where TPage : IGenPage<TModel>
    {
        var paramList = new List<(string, object)>
        {
            (nameof(GenPage<TModel>.ViewModel), SelectedItem),
            (nameof(GenPage<TModel>.ViewState), ViewState),
            (nameof(GenPage<TModel>.GenGrid), this)
        };

        return await ShowDialogAsync<TPage>(paramList.ToArray());
    }


    public virtual async ValueTask<IDialogReference> ShowDialogAsync<TPage>(params (string key, object value)[] parameters) where TPage : IGenPage<TModel>
    {
        foreach (var prm in parameters)
        {
            DialogParameters.Add(prm.key, prm.value);
        }

        return await DialogService.ShowAsync<GenPage<TModel>>(Title, DialogParameters, DialogOptions);
    }

    public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false)
    {
        throw new NotImplementedException();
    }

    public RenderFragment RenderAsGridComponent(object model)
    {
        throw new NotImplementedException();
    }

    public TComponent GetComponent<TComponent>(string bindingField) where TComponent : IGenComponent
    {
        var item = Components.FirstOrDefault(x => x.BindingField.Equals(bindingField));

        return item is null ? default : item.CastTo<TComponent>();
    }

    private List<TComponent> GetComponentsOf<TComponent>() where TComponent : IGenComponent
    {
        return Components.Where(x => x is TComponent).Cast<TComponent>().ToList();
    }

    public void AddChildComponent(IGenComponent childComponent)
    {
        if (Components.Any(x => x.BindingField == childComponent.BindingField)) return;
        Components.Add(childComponent);
    }

    private object _selectedDetailObject;

    public void OnDetailClicked(object context)
    {
        DetailClicked = !DetailClicked;

        if (DetailClicked)
        {
            _selectedDetailObject = context;
        }
        else if (_selectedDetailObject != context)
        {
            _selectedDetailObject = context;
            OnDetailClicked(context);
        }
        else
            _selectedDetailObject = null;
    }

    private bool ShouldDisplay(object context)
    {
        if (_selectedDetailObject is null) return false;

        return DetailClicked && _selectedDetailObject.Equals(context);
    }
}
