using FluentValidation;
using Generator.Components.Args;
using Generator.Components.Enums;
using Generator.Components.Interfaces;
using Generator.Components.Validators;
using Generator.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Interfaces;
using MudBlazorFix;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
 
namespace Generator.Components.Components;

public partial class GenGrid<TModel> : MudTable<TModel>, INonGenGrid, IGenGrid<TModel> where TModel : new()
{
    [Inject]
    public GenValidator<TModel> GenValidator { get; set; }

    [Inject]
    public IDialogService DialogService { get; set; }

    internal MudTable<TModel> OriginalTable { get; set; }

    public GenPage<TModel> CurrentGenPage { get; set; }

    public GridManager<TModel> GridManager { get; }

    public DialogResult DialogResult { get; set; }

    public DialogParameters DialogParameters { get; set; } = new DialogParameters();

    internal List<Action> EditButtonActionList { get; set; } = new();

    internal bool GridIsBusy = false;

    public DialogOptions DialogOptions { get; set; } = new()
    {
        MaxWidth = MaxWidth.Medium,
        FullWidth = true,
        CloseButton = true,
        CloseOnEscapeKey = true,
        DisableBackdropClick = true,
        Position = DialogPosition.Center
    };

    public ViewState ViewState { get; set; } = ViewState.None;

    public MudIconButton EditButtonRef { get; set; }

    public bool DetailClicked { get; set; }

    public List<IGenComponent> Components { get; set; } = new();

    private string _searchString = string.Empty;

    public bool IsFirstRender { get; set; } = true;

    public bool SearchDisabled { get; set; }

    public TModel OriginalEditItem { get; set; }

    private string AddIcon { get; set; } = Icons.Material.Filled.AddCircle;

    [CascadingParameter(Name = nameof(ChildSubmit))]
    public EventCallback ChildSubmit { get; set; }

    [Parameter]
    public EventCallback<TModel> Create { get; set; }

    [Parameter]
    public EventCallback<TModel> Delete { get; set; }

    [Parameter]
    public EventCallback<TModel> Update { get; set; }

    [Parameter]
    public EventCallback<TModel> Cancel { get; set; }

    [Parameter]
    public EventCallback<IGenView<TModel>> Load { get; set; }

    [CascadingParameter(Name = nameof(ParentSubmit))]
    internal EventCallback ParentSubmit { get; set; }

    internal EventCallback<TModel> GridSubmit { get; set; }

    [CascadingParameter(Name = nameof(ParentComponent))]
    public INonGenGrid ParentComponent { get; set; }

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
    public RenderFragment<TModel> GenDetailGrid { get; set; }

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

    public bool ForceRenderOnce { get; set; } = false;

    public GenGrid()
    {
        GridManager = new GridManager<TModel>(this);
    }

    protected override Task OnInitializedAsync()
    {
        //Detail Grid den parent gridi refreshleme
        if (ParentComponent is not null && ((dynamic)ParentComponent).DetailClicked)
        {
            ((dynamic)ParentComponent).StateHasChanged();
        }

        GridSubmit = EventCallback.Factory.Create<TModel>(this, async x =>
        {
            await InvokeCallBackByViewState(x);

            await InvokeAsync(StateHasChanged);
        });

        return Task.CompletedTask;
    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (ViewState == ViewState.Create && EditMode == EditMode.Inline)
        {
            if (EditButtonActionList.Any())
            {
                var editingRow = GetCurrentRow();

                if (editingRow.IsEditing && !ForceRenderOnce)
                    return Task.CompletedTask;

                var firstItem = EditButtonActionList.FirstOrDefault(x => (x.Target?.CastTo<MudTr>()).Item.CastTo<TModel>().Equals(SelectedItem));

                if (firstItem is null)
                    return Task.CompletedTask;

                firstItem.Invoke();

                //Bu Gerekli yoksa surekli OnAfterRenderAsync methoduna duser.

                StateHasChanged();

                editingRow.IsEditing = true;
                ForceRenderOnce = false;
            }
        }
        return Task.CompletedTask;
    }

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

        return result;
    }

    /// <summary>
    /// Invokes the eventcallback depending on the viewstate
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    internal async Task InvokeCallBackByViewState(TModel model)
    {
        await InvokeCallBackByViewState(model, ViewState.None);
    }

    internal async Task InvokeCallBackByViewState(TModel model, ViewState state)
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

                    await Create.InvokeAsync(model);
                    break;

                case ViewState.Update when Update.HasDelegate:
                    await Update.InvokeAsync(model);
                    break;

                case ViewState.Delete when Delete.HasDelegate:
                    await Delete.InvokeAsync(model);
                    break;

                case ViewState.None when Cancel.HasDelegate:
                    await Cancel.InvokeAsync(model);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            SearchDisabled = false;
            NewDisabled = false;
            ExpandDisabled = false;
            ViewState = state;
            if (CurrentGenPage is not null)
            {
                CurrentGenPage.PreventClose = false;

                CurrentGenPage.ViewState = state;
            }

            GridIsBusy = false;

            RefreshButtonState();
        }
        catch (Exception e)
        {
            var currentRow = GetCurrentRow();

            if (currentRow is not null)
            {
                this.TableContext.ManagePreviousEditedRow(currentRow);
            }

            //RollBack();

            if (CurrentGenPage is not null)
                CurrentGenPage.PreventClose = true;

            CurrentGenPage?.Close();
            GridIsBusy = false;
        }

        StateHasChanged();
    }

    internal async Task InvokeCallBackFromChild()
    {
        await InvokeCallBackByViewState(SelectedItem, ViewState.Update);

        CurrentGenPage.StateHasChanged();

        OnBackUp(SelectedItem);

        await InvokeAsync(StateHasChanged);
    }

    public Task OnEditCLick()
    {
        return Task.CompletedTask;

        //if (Load.HasDelegate && ViewState != ViewState.Create)
        //    await Load.InvokeAsync(this);
    }

    private void OnCancelClick(TModel element)
    {
        if (ViewState == ViewState.None) return;

        //ViewState = ViewState.None;

        //RollBack();
        //Components.ForEach(x => ResetValidation(x));

        //return;
        //var rw = GetCurrentRow();

    ;

        if (ViewState == ViewState.Create)
            DataSource.Remove(element);
        else
            DataSource.Replace(element, OriginalEditItem);


        ViewState = ViewState.None;

        Components.ForEach(x => ResetValidation(x));

        //RefreshButtonState sonunda statehaschanged var
        RefreshButtonState();
       
    }

    public virtual async ValueTask<IDialogReference> ShowDialogAsync<TPage>() where TPage : IGenPage<TModel>
    {
        var paramList = new List<(string, object)>
        {
            (nameof(GenPage<TModel>.SelectedItem), SelectedItem),
            (nameof(GenPage<TModel>.Load), Load),
            (nameof(GenPage<TModel>.Title), Title),
            (nameof(GenPage<TModel>.ViewState), ViewState),
            (nameof(GenPage<TModel>.ParentSubmit), ParentSubmit),
            (nameof(GenPage<TModel>.GridSubmit), GridSubmit),
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

    public async Task<bool> ValidateModel()
    {
        var result = await GenValidator.ValidateModel(SelectedItem);

        if (!result)
            ForceRenderOnce = !result;

        StateHasChanged();
        return result;
    }

    public async Task<bool> ValidateValue(string propertyName)
    {
        var component = Components.FirstOrDefault(x => x.BindingField == propertyName);

        var result = await GenValidator.ValidateValue(component, SelectedItem, propertyName);

        if (!result)
            ForceRenderOnce = !result;

        CurrentGenPage?.ValidateAsync();
        StateHasChanged();
        return result;
    }

    public void ResetValidation(IGenComponent component)
    {
        GenValidator.ResetValidation(component);
    }

    
}