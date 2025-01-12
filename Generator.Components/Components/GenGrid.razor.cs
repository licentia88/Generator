using Generator.Components.Enums;
using Generator.Components.Interfaces;
using Generator.Components.Validators;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazorFix;
using System.Diagnostics.CodeAnalysis;
using Force.DeepCloner;
using Generator.Components.Extensions;
using Mapster;
using Generator.Components.Args;
using Generator.Components.Helpers;
using DocumentFormat.OpenXml.Spreadsheet;
using Color = MudBlazor.Color;

namespace Generator.Components.Components;

public partial class GenGrid<TModel> : MudTable<TModel>, IPageBase, IDisposable, INonGenGrid where TModel : new()
{
    private int _index;

    [Inject]
    public GenValidator<TModel> GenValidator { get; set; }

    [Inject]
    public GeneratorJs GeneratorJs { get; set; }

    [Inject]
    public GenExcel GenExcel { get; set; }

    [Parameter]
    public string ExcelButtonText { get; set; }  

    [Parameter]
    public MaxWidth MaxWidth { get; set; } = MaxWidth.Medium;

    [Parameter]
    public bool CloseButton { get; set; } = true;

    [Parameter]
    public Dictionary<string,object> Parameters { get; set; } = new();

    [CascadingParameter(Name = nameof(Parameters))]
    private Dictionary<string, object> _Parameters { get => Parameters; set => Parameters = value; }

    [Parameter]
    public bool CloseOnEscapeKey { get; set; } = true;

    [Parameter]
    public bool BackdropClick { get; set; }

    [Parameter]
    public bool EnableSorting { get; set; }

    [Parameter]
    public bool IsIndividual { get; set; }

    [Inject]
    IDialogService  INonGenGrid.DialogService { get; set; }

    private MudTable<TModel> _OriginalTable { get; set; }


    public bool IsValid { get; set; }


    public MudTable<TModel> OriginalTable { get; set; }
    // internal MudTable<TModel> OriginalTable
    // {
    //     get => _OriginalTable;
    //
    //     set {
    //         _OriginalTable = value;
    //
    //         //Burada render de stabil kalmasini istedigin parametreleri set et
    //         // OriginalTable.RowsPerPage = RowsPerPage;
    //     }
    // }

    IPageBase INonGenGrid.CurrentGenPage { get; set; }

    public DialogResult DialogResult { get; set; }

    public DialogParameters DialogParameters { get; set; } = new DialogParameters();

    internal List<Action> EditButtonActionList { get; set; } = new();

    public bool GridIsBusy { get; set; } = false;

    [Parameter]
    public Color TemplateColor { get; set; } = Color.Primary;

    [Parameter]
    public Func<TModel, bool> Where { get; set; } = x => 1 == 1;

    public DialogOptions DialogOptions()
    {
        return new DialogOptions
        {
            MaxWidth = MaxWidth,
            FullWidth = true,
            CloseButton = CloseButton,
            CloseOnEscapeKey = CloseOnEscapeKey,
            BackdropClick = BackdropClick,
            Position = DialogPosition.Center
        };
    }

    //[CascadingParameter(Name =(nameof(ViewState)))]
    public ViewState ViewState { get; set; } = ViewState.None;

    internal MudIconButton EditButtonRef { get; set; }

    bool INonGenGrid.DetailClicked { get; set; }

    public List<(Type type, IGenComponent component)> Components { get; set; } = new();

    public List<IGenControl> SearchFieldComponents { get; set; } = new();


    private string _searchString = string.Empty;

    bool INonGenGrid.IsFirstRender { get; set; } = true;

    bool INonGenGrid.SearchDisabled { get; set; }

    TModel IGenView<TModel>.OriginalEditItem { get; set; }

    private string AddIcon { get; set; } = Icons.Material.Filled.AddCircle;


    [Parameter]
    public EventCallback<GenArgs<TModel>> Create { get; set; }

    [Parameter]
    public EventCallback<SearchArgs> Search { get; set; }

    [Parameter]
    public EventCallback<GenArgs<TModel>> Delete { get; set; }

    [Parameter]
    public EventCallback<GenArgs<TModel>> Update { get; set; }

    [Parameter]
    public EventCallback<GenArgs<TModel>> Cancel { get; set; }

    [Parameter]
    public EventCallback<IGenView<TModel>> Load { get; set; }

    [Parameter]
    public EventCallback<TModel> OnBeforeSubmit { get; set; }

    [Parameter]
    public EventCallback<TModel> OnAfterSubmit { get; set; }

    [Parameter]
    public EventCallback<TModel> OnBeforeCancel { get; set; }

    [Parameter]
    public EventCallback<TModel> OnAfterCancel { get; set; }

    [Parameter]
    public EventCallback Close { get; set; }

    [Parameter]
    public bool Disabled { get; set; }

     bool CancelDisabled { get; set; }

    [CascadingParameter(Name = nameof(Parent))]
    public INonGenGrid Parent { get; set; }

    public bool ShouldShowDialog { get; set; } = true;

    [Parameter]
    public string Title { get; set; }

    [Parameter]
    public bool EnableSearch { get; set; }

    [Parameter]
    public string ExcelFile { get; set; }

    [Parameter]
    public string SearchPlaceHolderText { get; set; } = "Search";


    [Parameter, EditorRequired]
    public ICollection<TModel> DataSource { get; set; }


    [Parameter, AllowNull]
    public RenderFragment GenColumns { get; set; }

    [Parameter, AllowNull]
    public RenderFragment GenSearchFields { get; set; }


    [Parameter, AllowNull]
    public RenderFragment GenHeaderButtons { get; set; }

    [Parameter, AllowNull]
    public RenderFragment<object> GenSideButtons { get; set; }


    [Parameter, AllowNull]
    public RenderFragment<TModel> GenDetailGrid { get; set; }

 



    bool INonGenGrid.HasDetail => GenDetailGrid is not null;

    [Parameter, EditorRequired]
    public EditMode EditMode { get; set; }

    bool INonGenGrid.NewDisabled { get; set; }

    bool INonGenView.IsTopLevel { get; set; }

    bool INonGenGrid.ExpandDisabled { get; set; }

    [Parameter]
    public string CancelText { get; set; } = "Cancel";

    [Parameter]
    public string SearchText { get; set; }  


    [Parameter]
    public string CreateText { get; set; } = "Create";

    [Parameter]
    public string UpdateText { get; set; } = "Update";

    [Parameter]
    public string DeleteText { get; set; } = "Delete";

    bool INonGenGrid.ForceRenderOnce { get; set; }

    bool INonGenGrid.IsRendered { get; set; }

    public GenGrid()
    {
        EditTrigger = TableEditTrigger.EditButton;
    }
 

    protected override void OnInitialized()
    {
        base.OnInitialized();

     

        if (Parent is not null && Parent.DetailClicked)
            Parent.StateHasChanged();


        if (MultiSelection)
        {
            SelectedItemsChanged = EventCallback.Factory.Create<HashSet<TModel>>(this, x =>
            {
                //SelectedItems = x;
                //Console.WriteLine();
            });
        }


    }
    //protected override async Task OnInitializedAsync()
    //{
    //    //Detail Grid den parent gridi refreshleme, neyini?
    //    //Burada bu koda ihtiyac var mi emin degilim
    //    if (ParentGrid is not null && ParentGrid.DetailClicked)
    //        ParentGrid.StateHasChanged();

    //    //bu kod searchfieldlerin renderlenmesini engellekem icin eklendi
    //    await Task.Delay(1);
    //}

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (!ShouldShowDialog)
        {
            if (_ShouldRender)
            {
                OriginalTable.SetEditingItem(null);
                ViewState = ViewState.None;

                MudTr row = GetCurrentRow();
                row?.ManagePreviousEdition();
                RefreshButtonState();

                _ShouldRender = false;
                return;
            }

            _ShouldRender = !_ShouldRender;

            ((INonGenGrid)this).IsFirstRender = false;


            return;
        }


        if ((ViewState == ViewState.Create || ViewState == ViewState.Update) && EditMode == EditMode.Inline)
        {
            if (GridIsBusy) return;

           
            var editingRow = GetCurrentRow();

            //if (editingRow is null)
            //    throw new Exception("If Grid has Filter, Make sure that when adding an new Item the default value ");
            //if (editingRow is null)
            //{
            //    //row bossa once itemi tekrar ekle
            //    DataSource.Insert(0, SelectedItem);
            //    //OriginalTable.Items = DataSource.ToList();
            //    StateHasChanged();

            //    if (ViewState != ViewState.Create)
            //    {
            //        ValidateModel();
            //    }

            //    return;
            //}

            var editingItem = OriginalTable.GetFieldValue("_editingItem");

            if (editingItem is null)
            {
                OriginalTable.SetEditingItem(SelectedItem);
            }

            //if (editingRow.IsEditing && !((INonGenGrid)this).ForceRenderOnce)
            //{

            //    if (editingItem is null)
            //    {
            //        OriginalTable.SetEditingItem(SelectedItem);
            //    }
            //}

            //editingRow.Context.Table.can
            //InvokeEditActions(editingRow);



            //Bu Gerekli yoksa surekli OnAfterRenderAsync methoduna duser.
            if (editingItem is null)
                StateHasChanged();


            editingRow?.Context.Editable(true); 
            // editingRow.Context.Table.SetEditingItem(editingItem);
            // editingRow.IsEditing = true;

            ((INonGenGrid)this).ForceRenderOnce = false;
            //}
        }

        //if(ViewState == ViewState.None)
        //{

        //}
        //return Task.CompletedTask;
    }

    // private void InvokeEditActions(MudTr editingRow)
    // {
    //     if (EditButtonActionList.Any())
    //     {
    //         // ReSharper disable once PossibleNullReferenceException
    //         var firstItem = EditButtonActionList.FirstOrDefault(x => (x.Target?.CastTo<MudTr>()).Item.CastTo<TModel>().Equals(SelectedItem));
    //
    //         if (firstItem is null) return;
    //
    //
    //         firstItem.Invoke();
    //     }
    //     else
    //     {
    //         if (Disabled)
    //         {
    //
    //             editingRow.Context.Table.RowEditCancel.Invoke(null);
    //             StateHasChanged();
    //             ((INonGenGrid)this).ForceRenderOnce = false;
    //         }
    //         else
    //         {
    //             //editingRow.Context.Table.OnPreviewEditClick.InvokeAsync(SelectedItem);
    //
    //            
    //             editingRow.OnRowClickedAsync(new Microsoft.AspNetCore.Components.Web.MouseEventArgs());
    //             CancelDisabled = true;
    //         }
    //
    //     }
    // }
    //*****BURASI edit button rowclick de tetiklenmez, kurguyu ona gore kur*****
    public async Task OnEditContextButtonClick(EditButtonContext button)
    {
        ////Should be at top
        if (ViewState == ViewState.None)
            ViewState = ViewState.Update;


        if (Load.HasDelegate)
        {
            var item = button.ButtonAction.Target.CastTo<MudTr>().Item.CastTo<TModel>();
            SelectedItem = item;

            //Form degilken tetiklensin formken load event tetiklenir zaten
            if(EditMode == EditMode.Inline)
                await InvokeLoad();

            if (!ShouldShowDialog)
            {
                //ShoulShowDialog = true;
                SelectedItem = default;
                return;
            }
        }

        
        button.ButtonAction.Invoke();
    }

    public bool HasErrors()
    {
        var result = Components.Any(x => x.component is IGenControl {Error: true});
        

        return result;
    }

    internal async Task EditClick()
    {

        ViewState = ViewState.Update;

        var dialogReference = await ShowDialogAsync<GenPage<TModel>>();

        await dialogReference.Result;

    }

   
    private async Task Commit()
    {
        IsValid = Validate();

        if (!IsValid)
        {
             //UseTableItems = true;
            StateHasChanged();
            //_ShouldRender = false;
            return;
        }

        ((INonGenView)this).IsTopLevel = true;

        //Parent Save
        if (Parent?.ViewState == ViewState.Create)
        {
            Parent.Validate();

            await Parent.OnCommitAndWait();
        }

        if (((INonGenView)this).IsTopLevel)
        {
           
            await ((IGenView<TModel>)this).OnCommit(SelectedItem, ViewState.None);
        }

    }

    Task INonGenView.OnCommit()
    {
        return ((IGenView<TModel>)this).OnCommit(SelectedItem, ViewState);
    }

    Task IGenView<TModel>.OnCommit(TModel model)
    {
        return ((IGenView<TModel>)this).OnCommit(model, ViewState.None);
    }


    async Task IGenView<TModel>.OnCommit(TModel model, ViewState viewState)
    {
        try
        {
            GridIsBusy = true;

            if (((INonGenGrid)this).CurrentGenPage is not null)
                ((INonGenGrid)this).CurrentGenPage.GridIsBusy = true;

            if (OnBeforeSubmit.HasDelegate)
               await OnBeforeSubmit.InvokeAsync(model);

            if (!IsValid)
            {
                if (((INonGenGrid)this).CurrentGenPage is not null)
                    (((INonGenGrid)this).Parent?.CurrentGenPage).IsValid = false;

                EditButtonActionList.Clear();

                (this as INonGenGrid).ForceRenderAll();

                OriginalTable.UpdateSelection();
                OriginalTable.Context.TableStateHasChanged();
                SetEditingItem(SelectedItem);
                //Components.Clear();

                GridIsBusy = false;
                if (((INonGenGrid)this).CurrentGenPage is not null)
                    ((INonGenGrid)this).CurrentGenPage.GridIsBusy = false;
                //StateHasChanged();
                return;
            }

            var args = new GenArgs<TModel>
            {
                OldValue = ((IGenView<TModel>)this).OriginalEditItem,
                CurrentValue = model,
                Index = _index,
                Parent = (((INonGenGrid)this).Parent?.CurrentGenPage)?.GetSelectedItem()

            };



            switch (ViewState)
            {
                case ViewState.Create when Create.HasDelegate:

                    if (EditMode == EditMode.Inline)
                        DataSource.Remove(SelectedItem);

               
                    await Create.InvokeAsync(args);
  
                    break;

                case ViewState.Update when Update.HasDelegate:
                    
                    _index = DataSource.IndexOf(args.CurrentValue);
                    args.Index = _index;
                    
                    await Update.InvokeAsync(args);
                   
                    //RefreshButtonState(true, args.CurrentValue);
                    break;

                case ViewState.Delete when Delete.HasDelegate:

                    args.OldValue = ((IGenView<TModel>)this).OriginalEditItem;
                    args.CurrentValue = ((IGenView<TModel>)this).OriginalEditItem;
                    await Delete.InvokeAsync(args);
                    break;

                case ViewState.None when Cancel.HasDelegate:
                    await Cancel.InvokeAsync(args);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }


            if(((INonGenGrid)this).CurrentGenPage is not null && ((INonGenGrid)this).CurrentGenPage is IGenView<TModel> page)
            {
                page.SelectedItem = args.CurrentValue;
                SelectedItem = args.CurrentValue;
            }

            ((INonGenGrid)this). SearchDisabled = false;

            ((INonGenGrid)this).NewDisabled = false;
 
            ((INonGenGrid)this).ExpandDisabled = false;

            ViewState = viewState;
 
            RefreshButtonState();

            if (OnAfterSubmit.HasDelegate)
                await OnAfterSubmit.InvokeAsync(SelectedItem);

            //GridIsBusy = false;

        }
        catch  (Exception ex )
        {
            Console.WriteLine(ex.Message);
            var currentRow = GetCurrentRow();

            if (currentRow is not null)
            {
                TableContext.ManagePreviousEditedRow(currentRow);
            }

            if(((INonGenGrid)this).CurrentGenPage is not null)
            {
                ((INonGenGrid)this).CurrentGenPage.ViewState = ViewState;
                ((INonGenGrid)this).CurrentGenPage.Close(); 
            }

            if(ViewState == ViewState.Update)
            {
              await OnCancelClick(SelectedItem);
            }

            GridIsBusy = false;

            if (((INonGenGrid)this).CurrentGenPage is not null)
                ((INonGenGrid)this).CurrentGenPage.GridIsBusy = false;
        }

 
        //_ShouldRender = true;

        if (Close.HasDelegate)
            await Close.InvokeAsync(this);

        //ForceRenderAll();

        EditButtonActionList.Clear();

        (this as INonGenGrid).ForceRenderAll();

        //Components.Clear();

        GridIsBusy = false;
        if (((INonGenGrid)this).CurrentGenPage is not null)
            (((INonGenGrid)this)?.CurrentGenPage).GridIsBusy = false;

        StateHasChanged();

     }


    async Task IPageBase.OnCommitAndWait()
    {
        await ((IGenGrid<TModel>)this).OnCommit(SelectedItem, ViewState.Update);

        ((INonGenGrid)this).CurrentGenPage.ViewState = ViewState.Update;

        // 13.11.2024 Inline iken commit yapildiginda parent page de commit yapilirken sayfa refresh olmamasi icin
        //sayfa refresh olunca racing conditiondan oturu sayfa refreshlenir ve data sonra kaydedilir bu yuzden ekranda gozukmez.
        
        // await InvokeAsync(() => ((INonGenGrid)this).CurrentGenPage?.StateHasChanged());
        //((INonGenGrid)this).CurrentGenPage?.StateHasChanged();

        OnBackUp(SelectedItem);

        await InvokeAsync(StateHasChanged);

    }

    public async Task OnCancelClick(TModel element)
    {
        if (ViewState == ViewState.None) return;

        if (OnBeforeCancel.HasDelegate)
            await OnBeforeCancel.InvokeAsync(element);

        if (ViewState == ViewState.Create)
        {
            DataSource.Remove(element);
        }

        else
        {
            DataSource.Replace(_index, ((IGenView<TModel>)this).OriginalEditItem);
            //if (Cancel.HasDelegate)
            //    await Cancel.InvokeAsync(new GenArgs<TModel>(element, ((IGenView<TModel>)this).OriginalEditItem, index));
        }

        ViewState = ViewState.None;

        foreach (var item in Components)
        {
            if (item.component is IGenControl cntrl)
                ((INonGenGrid)this).ResetValidation(cntrl);
        }
      
        //RefreshButtonState sonunda statehaschanged var
      


        if (OnAfterCancel.HasDelegate)
            await OnAfterCancel.InvokeAsync(((IGenView<TModel>)this).OriginalEditItem);

        if (Close.HasDelegate)
            await Close.InvokeAsync();

        //itemToRemove = SelectedItem;

        //Components.Clear();
        //components clear veya reset validation
        //eger reset validation sikinti cikarirsa clear components yap

        //var controls = Components.Where(x => x.component is IGenControl).Select(x => (IGenControl)x.component).ToList();

        //((INonGenGrid)this).ResetValidations(controls);


        SelectedItem = default;
         
        (this as INonGenGrid).ForceRenderAll();

        RefreshButtonState(true);

        StateHasChanged();
    }

    void INonGenGrid.ForceRenderAll()
    {
        ((INonGenGrid)this).IsFirstRender = true;
        //((INonGenGrid)this).IsRendered = false;

         
       
        //GenColumns.Invoke(this.BuildRenderTree);
        //Components.Clear();
    }

    public virtual async ValueTask<IDialogReference> ShowDialogAsync<TPage>() where TPage : IGenPage<TModel>
    {
 
        var paramList = new List<(string, object)>
        {
           
            (nameof(GenPage<TModel>.Load), Load),
            (nameof(GenPage<TModel>.SearchFieldComponents), SearchFieldComponents),
            (nameof(GenPage<TModel>.Components), Components),
            (nameof(GenPage<TModel>.SelectedItem), SelectedItem),
            (nameof(GenPage<TModel>.OriginalEditItem), ((IGenView<TModel>)this).OriginalEditItem),
            (nameof(GenPage<TModel>.IsIndividual), IsIndividual),
            (nameof(GenPage<TModel>.Parameters), Parameters),
            (nameof(GenPage<TModel>.Title), Title),
            (nameof(GenPage<TModel>.TemplateColor), TemplateColor),
            (nameof(GenPage<TModel>.ViewState), ViewState),
            (nameof(GenPage<TModel>.GenGrid), this)
        };

        if(Parent is not null)
        {
            paramList.Add((nameof(GenPage<TModel>.Parent), Parent));
        }

        return await ShowDialogAsync<TPage>(paramList.ToArray());
    }

    public virtual async ValueTask<IDialogReference> ShowDialogAsync<TPage>(params (string key, object value)[] parameters) where TPage : IGenPage<TModel>
    {
        foreach (var prm in parameters)
        {
            DialogParameters.Add(prm.key, prm.value);
        }
 
        if (ShouldShowDialog)
            return await ((INonGenGrid)this).DialogService.ShowAsync<GenPage<TModel>>(Title, DialogParameters, DialogOptions());

 
        RefreshButtonState();

        return default;
    }

   
    public TComponent GetSearchFieldComponent<TComponent>(string bindingField) where TComponent : IGenControl
    {
        var item = SearchFieldComponents.FirstOrDefault(x => x.BindingField is not null && x is TComponent && x.BindingField.Equals(bindingField));

        return item is null ? default : item.CastTo<TComponent>();
    }

    public TComponent GetComponent<TComponent>(string bindingField) where TComponent : IGenControl
    {
        var item = Components.FirstOrDefault(x => x.component.BindingField is not null && x.component is TComponent && x.component.BindingField.Equals(bindingField));

        return item.component is null ? default : item.component.CastTo<TComponent>();
    }

    private List<TComponent> GetComponentsOf<TComponent>() where TComponent : IGenControl
    {
        var result = Components.Where(arg => arg.component is TComponent).Select(x=> x.component).Cast<TComponent>().ToList();
        return result;
    }

    //private List<IGenComponent> GetRowTemplateComponents()  
    //{
    //    var result = Components.Select(x => x.component)
    //    .Where(x => x is not GenSpacer)
    //    .Where(x => x.EditorVisible || (x.EditorVisibleFunc?.Invoke(x.Model) ?? false)).OrderBy(x => x.Order).ToList();

    //    Components.Select(x => x.component).Except(result).ToList().ForEach(x => x.SetEmpty());

    //    return result;
    //}


    void IPageBase.AddChildComponent(IGenComponent childComponent)
    {
        if (((INonGenGrid)this).IsRendered) return;
        //if (childComponent is not GenSpacer && string.IsNullOrEmpty(childComponent.BindingField)) return;

        var componentType = childComponent.GetType();

        if (Components.Any(x => x.type == componentType && x.component.BindingField == childComponent.BindingField )) return;
 
        Components.Add((componentType, childComponent));
    }

    void INonGenGrid.AddSearchFieldComponent(IGenControl component)
    {
        if (((INonGenGrid)this).IsRendered) return;
        //if (component is not GenSpacer && string.IsNullOrEmpty(component.BindingField)) return;

        if (SearchFieldComponents.Any(x => x.BindingField == component.BindingField )) return;

        //component.Model = string.Empty;
        //component.Model = new();

        SearchFieldComponents.Add(component);
    }

    private TModel _selectedDetailObject;

    private void SelectDetailObject(TModel context)
    {
        _selectedDetailObject = ((INonGenGrid)this).DetailClicked ? context : default;
    }

    public void DisableRender()
    {
        _ShouldRender = false;
    }

    public void EnableRender()
    {
        _ShouldRender = true;
        // throw new NotImplementedException();
    }

    async Task IGenGrid<TModel>.OnDetailClicked(TModel context)
    {
        ((INonGenGrid)this).DetailClicked = !((INonGenGrid)this).DetailClicked;
        CancelDisabled = ((INonGenGrid)this).DetailClicked;
 
        SelectDetailObject(context);

        var style = CancelDisabled ? "none" : "auto";

        await  GeneratorJs.ChangeRowStyle(style);
    }

    private bool ShouldDisplay(object context)
    {
        if (_selectedDetailObject is null) return false;

        return ((INonGenGrid)this).DetailClicked && _selectedDetailObject.Equals(context);
    }


    bool INonGenGrid.ValidateSearchField(string BindingField)
    {
        var comp = GetSearchFieldComponent<IGenControl>(BindingField);

        return ((INonGenGrid)this).ValidateSearchFields(new List<IGenControl> { comp });
        //return GenValidator.ValidateComponentValue(comp);
    }

    bool INonGenGrid.ValidateSearchFields()
    {
        return ((INonGenGrid)this).ValidateSearchFields(SearchFieldComponents);
    }

    bool INonGenGrid.ValidateSearchFields(IEnumerable<IGenControl> searchFields)
    {
        //var genComponents = searchFields as IGenComponent[] ?? searchFields.ToArray();
       
        ((INonGenGrid)this).ResetValidations(searchFields);

        foreach (var comp in searchFields)
        {
            if (!comp.Required && !(comp.RequiredIf?.Invoke(comp.Model) ?? false)) continue;
            var currentVal = comp.Model.GetPropertyValue(comp.BindingField);

            comp.Error = currentVal is null or "";

        }

        var isValid = searchFields.All(x => !x.Error);

        StateHasChanged();
        return isValid;
    }


    public bool Validate()
    {
        var isValid = GenValidator.ValidateDataSource(SelectedItem, Components.Where(x=> x.component is IGenControl).Select(x=>   x.component as IGenControl));
     
        if (!isValid)
            ((INonGenGrid)this).ForceRenderOnce = true;


        IsValid = isValid;
        //StateHasChanged();
        return IsValid;
    }

    //public bool ValidateRequiredRules(IGenComponent component)
    //{
    //    return GenValidator.ValidateRequiredRules(component);
    //}

    public bool ValidateField(string propertyName)
    {
        var component = Components.FirstOrDefault(x => x.component.BindingField == propertyName);

        var result = GenValidator.ValidateComponentValue(component.component as IGenControl);

        if (!result)
            ((INonGenGrid)this).ForceRenderOnce = true;

        ((INonGenGrid)this).CurrentGenPage?.StateHasChanged();
        StateHasChanged();
        return result;
    }


    void INonGenGrid.ResetValidations(IEnumerable<IGenControl> components)
    {
        foreach (var component in components.Where(x=> x is IGenControl && x.Error))
        {
            GenValidator.ResetValidation(component);
        }
    }

    void INonGenGrid.ResetConditionalSearchFields()
    {
        foreach (var component in SearchFieldComponents.Where(x=> x.Error && (!x.RequiredIf?.Invoke(x.Model) ?? false) ) )
        {
            GenValidator.ResetValidation(component);
        }
    }

    void INonGenGrid.ResetValidation(IGenControl component)
    {
        GenValidator.ResetValidation(component);
    }

 

    /// <summary>
    /// Creates a new instance of the data model using the provided components.
    /// If the data model is a concrete class with a parameterless constructor,
    /// the constructor will be invoked to create the instance. Otherwise, a
    /// dictionary-based creation will be used.
    /// </summary>
    /// <typeparam name="TModel">The type of the data model.</typeparam>
    /// <returns>A new instance of the data model.</returns>
    private TModel CreateNewDataModel()
    {
        // Check if TModel is a concrete class and has a parameterless constructor
        if (typeof(TModel).IsClass && !typeof(TModel).IsAbstract && !typeof(TModel).IsInterface)
        {
            var constructor = typeof(TModel).GetConstructor(Type.EmptyTypes);

            // If a parameterless constructor exists, create a new instance using it
            if (constructor != null)
                return new TModel();
        }

        // Create a dictionary of default values from the Components collection
        var newData = Components.Where(x => x.type != typeof(GenSpacer) && x.type !=(typeof(GenButton))).ToDictionary(comp => comp.component.BindingField, comp => (comp.component as IGenControl)?.GetDefaultValue);

        // Configure TypeAdapter to transform newData dictionary to TModel
        TypeAdapterConfig.GlobalSettings.NewConfig(newData.GetType(), typeof(TModel))
            .AddDestinationTransform(DestinationTransform.EmptyCollectionIfNull);

        // Adapt and return the newData dictionary as TModel instance
        return newData.Adapt<TModel>();
    }
 
    internal async Task OnSearchClicked()
    {
        if (!((INonGenGrid)this).ValidateSearchFields()) return;

        ((INonGenGrid)this).SearchDisabled = true;
        GridIsBusy = true;

        
        await Search.InvokeAsync(new SearchArgs(SearchFieldComponents));
        GridIsBusy = false;
        ((INonGenGrid)this).SearchDisabled = false;
    }

    public async Task OnCreateClick()
    {
        //_ShouldRender = true;
        ShouldShowDialog = true;

        ViewState = ViewState.Create;

        var adaptedData = CreateNewDataModel();

        //if (itemToRemove is not null)
        //{
        //    var itemsCount = OriginalTable.Context.Rows.Count();
        //    try
        //    {
              
        //        OriginalTable.Context.Rows = (Dictionary<TModel, MudTr>)OriginalTable.Context.Rows.Take(itemsCount);

        //    }
        //    catch (Exception ex)
        //    {
        //        var dictionary = OriginalTable.Context.Rows
        //                        .Take(itemsCount-1)
        //                        .ToDictionary(x => x.Key, y => y.Value);
        //        OriginalTable.Context.Rows = dictionary;

        //    }
        //    itemToRemove = default;
        // }
        SelectedItem = adaptedData;
 

        if (EditMode == EditMode.Inline)
        {
            DataSource.Insert(0, adaptedData);

            SelectedItem = adaptedData;

            OnBackUp(SelectedItem);
            return;
        }

        SelectedItem = adaptedData;

        OnBackUp(SelectedItem);

      

        var dialogReference =   await ShowDialogAsync<GenPage<TModel>>();

        await dialogReference.Result;


    }
    
    private bool SearchFunction(TModel model)
    {
        if (string.IsNullOrEmpty(_searchString)) return true;

        var searchableFields = GetComponentsOf<IGenControl>().Where((x) => x.BindingField is not null && x.GridVisible);

        var genComponents = searchableFields.ToList();
        foreach (var component in genComponents)
        {
            if (component is IGenComboBox combobox)
            {
                var comboboxModel = combobox.DataSource.FirstOrDefault(x => x.GetPropertyValue(combobox.ValueField).ToString() == model.GetPropertyValue(combobox.BindingField).ToString());

                if (comboboxModel is null) continue;

                if (comboboxModel.GetPropertyValue(combobox.DisplayField).ToString()!.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                    return true;

            }

            if (component is IGenCheckBox checkBox)
            {
                var propValue = model.GetPropertyValue(checkBox.BindingField);

                if (propValue is not null && propValue is bool boolValue)
                {
                    if (boolValue && checkBox.TrueText.Equals(_searchString, StringComparison.OrdinalIgnoreCase))
                        return true;

                    if (!boolValue && checkBox.FalseText.Equals(_searchString, StringComparison.OrdinalIgnoreCase))
                        return true;
                }


            }
        }

        var result = genComponents.Select(field => model.GetPropertyValue(field.BindingField)).Where(columnValue => columnValue is not null)
                              .Any(columnValue => columnValue.ToString()!.Contains(_searchString, StringComparison.OrdinalIgnoreCase));

        return result;
    }

    internal MudTr GetCurrentRow(TModel model)
    {
        return OriginalTable?.Context?.Rows?.FirstOrDefault(x => x.Key.Equals(model??SelectedItem)).Value;
    }

    //internal MudTr GetCurrentRow(int index)
    //{
    //    return OriginalTable?.Context?.Rows.
    //}
    internal MudTr GetCurrentRow()
    {
        return GetCurrentRow(default);
        //Console.WriteLine(SelectedItem);
        //return GetCurrentRow(OriginalTable.SelectedItem);
        //return OriginalTable?.Context?.Rows?.FirstOrDefault(x => x.Key.Equals(SelectedItem)).Value;

        //try
        //{
        //    return OriginalTable?.Context?.Rows?.FirstOrDefault(x => x.Key.Equals(SelectedItem)).Value;
        //}
        //catch  
        //{
        //    return OriginalTable?.Context?.Rows?.FirstOrDefault(x => x.Key.Equals(((IGenView<TModel>)this).OriginalEditItem)).Value;
        //}
    }

    internal MudTr GetRow(TModel model)
    {
        return OriginalTable?.Context?.Rows?.FirstOrDefault(x => x.Key.Equals(model)).Value;
    }

    private Action GetRowButtonAction()
    {
        // ReSharper disable once PossibleNullReferenceException
        return EditButtonActionList.FirstOrDefault(x => (x.Target?.CastTo<MudTr>()).Item.CastTo<TModel>().Equals(SelectedItem));
    }

    internal void RefreshButtonState()
    {
        RefreshButtonState(true);
    }


    private void RefreshButtonState(bool changeState, TModel currentModel)
    {
        var row = GetCurrentRow(currentModel);

        if (row is not null)
        {
            
            var _hasBeenCanceled = row.GetFieldValue("_hasBeenCanceled").CastTo<bool>();
            var _hasBeenCommitted = row.GetFieldValue("_hasBeenCommitted").CastTo<bool>();

            if (!_hasBeenCanceled && !_hasBeenCommitted)
            {
                OriginalTable.Context?.Table.SetEditingItem(currentModel);
                //StateHasChanged();
                //Context.Table.RowEditCancel?.Invoke(Item);
            }


            row.SetFieldValue("_hasBeenCanceled", true);
            row.SetFieldValue("_hasBeenCommitted", true);
            row.SetFieldValue("_hasBeenClickedFirstTime", false);
        }

        ((INonGenGrid)this).SearchDisabled = false;
        ((INonGenGrid)this).NewDisabled = false;
        ((INonGenGrid)this).ExpandDisabled = false;
        CancelDisabled = false;

        if (changeState)
            StateHasChanged();
    }
    private void RefreshButtonState(bool changeState)
    {
        RefreshButtonState(changeState, default);
    }

    private void RollBack()
    {
        GetCurrentRow().ManagePreviousEdition();
    }

    private async Task MyRowEditPreview(object model)
    {

        if (ViewState != ViewState.Create)
            ViewState = ViewState.Update;

        OnBackUp(model.CastTo<TModel>());

        await EditRow();

    }

    internal async ValueTask EditRow()
    {
        //ViewState = ViewState.Update;
        //_ShouldRender = true;
        ShouldShowDialog = true;

        if (EditMode != EditMode.Inline)
        {
            await EditClick();
            return;
        }

        if (!HasErrors() && EditMode == EditMode.Inline)
            await InvokeLoad();
    }

    internal void OnBackUp(TModel element)
    {
        if (HasErrors()) return;

        ((INonGenGrid)this).NewDisabled = true;

        ((INonGenGrid)this).ExpandDisabled = true;

        ((INonGenGrid)this).SearchDisabled = true;

        SelectedItem = element;//.DeepClone();

        ((IGenView<TModel>)this).OriginalEditItem = element.DeepClone();

        _index = DataSource.IndexOf(element);
    }

    private bool _ShouldRender = true;

    protected override bool ShouldRender()
    {
        return _ShouldRender ? base.ShouldRender() : _ShouldRender;
    }

    internal Task InvokeLoad()
    {
        if (Load.HasDelegate)
        {
             ShouldShowDialog = true;
            _ShouldRender = false;
             Load.InvokeAsync(this);
            _ShouldRender = true;
        }

        if (!ShouldShowDialog)
        {
            //if(ViewState == ViewState.Create)
            //{
                DataSource.Remove(SelectedItem);
            //}

            OriginalTable.SetEditingItem(null);

            ((INonGenGrid)this).IsFirstRender = false;
            //RefreshButtonState();
        }

        //StateHasChanged();
        return Task.CompletedTask;
    }

    internal async Task OnDeleteClicked(TModel model)
    {
        IsValid = true;

        ViewState = ViewState.Delete;

        ((IGenView<TModel>)this).OriginalEditItem = model;

        await ((IGenView<TModel>)this).OnCommit(model, ViewState.None);

    }

    public new async void StateHasChanged()
    {
        await InvokeAsync(base.StateHasChanged);
        //base.StateHasChanged();
    }


    

    // public void Dispose()
    // {
    //     if (EditMode == EditMode.Inline && (ViewState == ViewState.Create || ViewState == ViewState.Update))
    //     {
    //         DataSource.RemoveAt(0);
    //     }
    // }

    public object GetSelectedItem()
    {
        return SelectedItem;
    }

    void IPageBase.Close()
    {
        ((IGenGrid<TModel>)this).CurrentGenPage.Close();
    }

   

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Dispose managed resources
            OriginalTable = null;
           DataSource = null;
            Search = default;
            Create = default;
            Delete = default;
            Update = default;
            Cancel = default;
            Load = default;
            OnBeforeSubmit = default;
            OnAfterSubmit = default;
            OnBeforeCancel = default;
            OnAfterCancel = default;
            Close = default;
            GenColumns = null;
            GenSearchFields = null;
            GenHeaderButtons = null;
            GenSideButtons = null;
            GenDetailGrid = null;
            Parent = null;
            DialogParameters = null;
            ViewState = ViewState.None;
            SelectedItem = default;
            _selectedDetailObject = default;
            _searchString = string.Empty;
            EditButtonActionList = null;
          
            EditButtonRef = null;
            SelectedItemChanged= default;
            SelectedItemsChanged = default;
            RowsPerPageChanged = default;

            foreach (var component in Components)
            {
                component.component.Parent = null;

            }
            
            foreach (var component in SearchFieldComponents)
            {
                component.Parent = null;

            }
            
            SearchFieldComponents.Clear();
            Components.Clear();

        }

        base.Dispose(disposing);
    }
    //public bool Validate()
    //{
    //    throw new NotImplementedException();
    //}
}