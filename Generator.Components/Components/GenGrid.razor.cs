using FluentValidation;
using Generator.Components.Enums;
using Generator.Components.Interfaces;
using Generator.Components.Validators;
using Generator.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazorFix;
using System.Diagnostics.CodeAnalysis;
using Force.DeepCloner;
using Generator.Components.Extensions;
using Mapster;
using static MudBlazor.CategoryTypes;

namespace Generator.Components.Components;

public partial class GenGrid<TModel> :  MudTable<TModel> ,INonGenGrid, IGenGrid<TModel> where TModel : new()
{
    [Inject]
    public GenValidator<TModel> GenValidator { get; set; }

    [Parameter]
    public MaxWidth MaxWidth { get; set; } = MaxWidth.Medium;

    [Parameter]
    public bool CloseButton { get; set; } = true;

    [Parameter]
    public bool CloseOnEscapeKey { get; set; } = true;

    [Parameter]
    public bool DisableBackdropClick { get; set; } = true;

    [Inject]
    public IDialogService DialogService { get; set; }

    internal MudTable<TModel> OriginalTable { get; set; }   //

    public INonGenPage CurrentGenPage { get; set; }


    public DialogResult DialogResult { get; set; }

    public DialogParameters DialogParameters { get; set; } = new DialogParameters();

    internal List<Action> EditButtonActionList { get; set; } = new();

    private bool GridIsBusy = false;

    public DialogOptions DialogOptions()
    {
        return new DialogOptions
        {
            MaxWidth = MaxWidth,
            FullWidth = true,
            CloseButton = CloseButton,
            CloseOnEscapeKey = CloseOnEscapeKey,
            DisableBackdropClick = DisableBackdropClick,
            Position = DialogPosition.Center
        };
    }

    public ViewState ViewState { get; set; } = ViewState.None;

    internal MudIconButton EditButtonRef { get; set; }

    public bool DetailClicked { get; set; }

    public List<IGenComponent> Components { get; set; } = new();

    private string _searchString = string.Empty;

    public bool IsFirstRender { get; set; } = true;

    public bool SearchDisabled { get; set; }

    public TModel OriginalEditItem { get; set; }

    private string AddIcon { get; set; } = Icons.Material.Filled.AddCircle;


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

    [Parameter]
    public EventCallback<IGenView<TModel>> OnRender { get; set; }


    [CascadingParameter(Name = nameof(ParentGrid))]
    public INonGenGrid ParentGrid { get; set; }

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

    public bool ForceRenderOnce { get; set; }

    

    protected override Task OnInitializedAsync()
    {
        //Detail Grid den parent gridi refreshleme, neyini?
        //Burada bu koda ihtiyac var mi emin degilim
        if (ParentGrid is not null && ParentGrid.DetailClicked)
            ParentGrid.StateHasChanged();


        return Task.CompletedTask;
    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if ((ViewState == ViewState.Create || ViewState == ViewState.Update) && EditMode == EditMode.Inline)
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

    public async Task CreateClick()
    {

        EditButtonActionList.Clear();

        ViewState = ViewState.Create;

        var newData =  Components.Where(x => x is not GenSpacer).ToDictionary(comp => comp.BindingField, comp => comp.GetDefaultValue);

        TypeAdapterConfig.GlobalSettings.NewConfig(newData.GetType(), typeof(TModel)).AddDestinationTransform(DestinationTransform.EmptyCollectionIfNull);

        var adaptedData = newData.Adapt<TModel>();

        SelectedItem = adaptedData;

        if (EditMode == EditMode.Inline)
        {
            DataSource.Insert(0, SelectedItem);

            SetEditingItem(SelectedItem);
            //Grid.SetSelectedItem(Grid.SelectedItem);

            //Grid.originalTable.SetEditingItem(Grid.SelectedItem);

            //Grid.originalTable.SetSelectedItem(Grid.SelectedItem);
            return;
        }

        OnBackUp(SelectedItem);
        await ShowDialogAsync<GenPage<TModel>>();

    }

    //private ICollection<TModel> ActualDataSource()
    //{
    //    return OriginalTable?.Items.ToList() ?? DataSource;
    //}
    public async Task EditClick()
    {
        ViewState = ViewState.Update;

        await ShowDialogAsync<GenPage<TModel>>();

    }
    public Task OnCommit()
    {
        return OnCommit(SelectedItem, ViewState);
    }

    public Task OnCommit(TModel model)
    {
        return OnCommit(model, ViewState.None);
    }

    public async Task OnCommit(TModel model, ViewState viewState)
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
            ViewState = viewState;
 
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

           

            CurrentGenPage?.Close();
            GridIsBusy = false;
        }

        StateHasChanged();
    }



    public async Task OnCommitAndWait()
    {
        await OnCommit(SelectedItem, ViewState.Update);

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
            (nameof(GenPage<TModel>.Components), Components),
            (nameof(GenPage<TModel>.SelectedItem), SelectedItem),
            (nameof(GenPage<TModel>.Load), Load),
            (nameof(GenPage<TModel>.OnRender), OnRender),
            (nameof(GenPage<TModel>.Title), Title),
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

        return await DialogService.ShowAsync<GenPage<TModel>>(Title, DialogParameters, DialogOptions());
    }

    public TComponent GetComponent<TComponent>(string bindingField) where TComponent : IGenComponent
    {
        var item = Components.FirstOrDefault(x => x.BindingField is not null && x.BindingField.Equals(bindingField));

        return item is null ? default : item.CastTo<TComponent>();
    }

    private List<TComponent> GetComponentsOf<TComponent>() where TComponent : IGenComponent
    {
        return Components.Where(x => x is TComponent).Cast<TComponent>().ToList();
    }

    public void AddChildComponent(IGenComponent childComponent)
    {
        if (Components.Any(x => x is not GenSpacer &&  x.BindingField == childComponent.BindingField)) return;
        Components.Add(childComponent);
    }

    private TModel _selectedDetailObject;

    private void SelectDetailObject(TModel context)
    {
        _selectedDetailObject = DetailClicked ? context : default;
    }

    public void OnDetailClicked(TModel context)
    {
        DetailClicked = !DetailClicked;
        SelectDetailObject(context);
    }


    

    private bool ShouldDisplay(object context)
    {
        if (_selectedDetailObject is null) return false;

        return DetailClicked && _selectedDetailObject.Equals(context);
    }

    public bool ValidateModel()
    {
        var result = true;

        if (SelectedItem.IsModel())
            result = GenValidator.ValidateModel(SelectedItem);
        else
            result = Components.All(x=> GenValidator.ValidateValue(x,SelectedItem,x.BindingField));

        if (!result)
            ForceRenderOnce = !result;

        StateHasChanged();
        return result;
    }



    public  bool ValidateValue(string propertyName)
    {
        var component = Components.FirstOrDefault(x => x.BindingField == propertyName);

        var result =  GenValidator.ValidateValue(component, SelectedItem, propertyName);

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

    public async Task OnCreateClick()
    {
       await CreateClick();
     }

    private async Task Commit(object model)
    {
        var isValid =  ValidateModel();
        if (!isValid) return;

        await OnCommit(SelectedItem);
    }

    private bool SearchFunction(TModel model)
    {
        if (string.IsNullOrEmpty(_searchString)) return true;

        var searchableFields = GetComponentsOf<IGenTextField>()
            .Where((x) => x.BindingField is not null && x.GridVisible);

        return searchableFields.Select(field => model.GetPropertyValue(field.BindingField)).Where(columnValue => columnValue is not null).Any(columnValue => columnValue.ToString()!.Contains(_searchString, StringComparison.OrdinalIgnoreCase));
    }

    internal MudTr GetCurrentRow()
    {
        var selectedItem = GetRowButtonAction();

        if (selectedItem is not null)
        {
            var row = selectedItem.Target.CastTo<MudTr>();
            return row;
        }

        return null;
    }

    private Action GetRowButtonAction()
    {
        //var test = EditButtonActionList.Select(x => x.Target.CastTo<MudTr>()).ToList(); 

        return EditButtonActionList.FirstOrDefault(x => (x.Target?.CastTo<MudTr>()).Item.CastTo<TModel>().Equals(SelectedItem));
    }


    
    internal void RefreshButtonState()
    {
        var row = GetCurrentRow();

        if (row is not null)
        {
            row.SetFieldValue("hasBeenCanceled", false);
            row.SetFieldValue("hasBeenCommitted", true);
            row.SetFieldValue("hasBeenClickedFirstTime", false);
        }

        SearchDisabled = false;
        NewDisabled = false;
        ExpandDisabled = false;

        StateHasChanged();
    }

    private void RollBack()
    {
        GetCurrentRow().ManagePreviousEdition();
    }
   

    internal async ValueTask EditRow()
    {
        if (EditMode != EditMode.Inline)
        {
            await EditClick();
            return;
        }
         
        await InvokeLoad();
    }

    internal void OnBackUp(TModel element)
    {
        if (HasErrors()) return;

        NewDisabled = true;

        ExpandDisabled = true;

        SearchDisabled = true;

        SelectedItem = element;//.DeepClone();

        OriginalEditItem = element.DeepClone();


    }


    public async Task InvokeLoad()
    {
        if (Load.HasDelegate)
            await Load.InvokeAsync(this);
    }

    public async Task OnDeleteClicked(Action buttonAction)
    {
        ViewState = ViewState.Delete;

        TModel dataToRemove = (TModel)buttonAction.Target.CastTo<MudTr>().Item;
        OriginalEditItem = dataToRemove;

        await OnCommit(dataToRemove.CastTo<TModel>());
 
    }

    public new void StateHasChanged()
    {
        base.StateHasChanged();
    }
}