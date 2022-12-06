using System;
using Generator.Components.Enums;
using Generator.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using Generator.Components.Args;
using MudBlazor;
using Mapster.Utils;

namespace Generator.Components.Components;

public partial class GenGrid : GenUI
{

    public HashSet<object> SelectedItems { get; set; } = new HashSet<object>();

    [Parameter]
    public bool Virtualize { get; set; }

    [Parameter]
    public bool MultiSelection { get; set; }
    [Parameter]
    public string Height { get; set; }

    [Parameter, AllowNull]
    public bool FixedHeader { get; set; } = true;

    [Parameter, AllowNull]
    public bool FixedFooter { get; set; } = true;

    [CascadingParameter(Name = nameof(SmartCrud))]
    private bool _smartCrud { get; set; }

    [Parameter, AllowNull]
    public bool SmartCrud { get { return _smartCrud; } set { _smartCrud = value; } }


    [CascadingParameter(Name = nameof(TopLevel))]
    public object TopLevel { get; set; }

    [CascadingParameter(Name = nameof(ChildSubmit))]
    public EventCallback ChildSubmit { get; set; }


    [CascadingParameter(Name = nameof(ParentContext))]
    public object ParentContext { get; set; }

    [Parameter, AllowNull]
    public bool EnableModelValidation { get; set; }
    //private MudTable<TModel> MyTable { get; set; }


    [Parameter, AllowNull]
    public ICollection<object> DataSource { get; set; }

    /// <summary>
    /// Displays the Search bar
    /// </summary>
    [Parameter]
    public bool EnableSearch { get; set; }



    #region Page Information

    [Parameter, AllowNull]
    public string Title { get; set; }

    #endregion Page Commands

    [Parameter, AllowNull]
    public virtual EventCallback<GenGridArgs> OnCreate { get; set; }

    [Parameter, AllowNull]
    public virtual EventCallback<GenGridArgs> OnDelete { get; set; }

    [Parameter, AllowNull]
    public virtual EventCallback<GenGridArgs> OnUpdate { get; set; }

    [Parameter, AllowNull]
    public virtual EventCallback<GenGridArgs> OnBeforeSubmit { get; set; }


    [Parameter, AllowNull]
    public virtual EventCallback<GenGridArgs> OnLoad { get; set; }

    [Parameter, AllowNull]
    public virtual EventCallback<object> SelectedItemsChanged { get; set; }


    #region Dialog

    [Inject]
    protected IDialogService DialogService { get; set; }

    protected DialogResult DialogResult { get; set; }

    private DialogParameters Parameters { get; set; } = new DialogParameters();

    protected DialogOptions Options { get; set; }


    #endregion Dialog

    #region RenderFragments

    [Parameter, AllowNull]
    public RenderFragment GridColumns { get; set; }

    [Parameter, AllowNull]
    public RenderFragment HeaderButtons { get; set; }

    [Parameter, AllowNull]
    public RenderFragment<object> GridButtons { get; set; }

    [Parameter, AllowNull]
    public RenderFragment<object> DetailGrid { get; set; }

    private string _searchString = "";

    public ObservableCollection<GenColumn> Components { get; set; } = new();

    private bool isFirstRender = true;

    [CascadingParameter(Name = nameof(IsChild))]
    internal bool IsChild { get; set; }





    public GenGrid()
    {

        Options = new DialogOptions
        {
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
            CloseButton = true,
            CloseOnEscapeKey = true,
            DisableBackdropClick = true,
            Position = DialogPosition.Center
        };



    }



    //internal void ShowDetailAsync(TModel context)
    //{
    //    //if(VisibleDetailKey)
    //    //if(context.GetPrimaryKeyValue().ToString() == VisibleDetailKey.ToString())

    //    isClicked = !isClicked;

    //    if (isClicked)
    //    {
    //        VisibleDetailKey = context.GetPrimaryKeyValue();

    //    }
    //    else if (VisibleDetailKey.ToString() != context.GetPrimaryKeyValue().ToString())
    //    {
    //        VisibleDetailKey = context.GetPrimaryKeyValue().ToString();
    //        ShowDetailAsync(context);
    //    }
    //    else
    //        VisibleDetailKey = null;


    //}

    //private bool ShouldDisplay(TModel context)
    //{
    //    if (VisibleDetailKey is null) return false;


    //    return isClicked && (context.GetPrimaryKeyValue().ToString() == VisibleDetailKey.ToString());
    //}

    private bool isClicked;

    private object VisibleDetailKey;


    private List<TType> GetComponentOf<TType>()
    {
        
        return Components.
            Where(x => x is TType).
            Exclude<GenColumn, MudButton>()
            .Exclude<GenColumn, GridSpacer>()
            .Cast<TType>().ToList();
    }


    /// Add a child component (will be done by the child itself)
    public void AddChildComponent(MudComponentBase pChildComponent)
    {

        if (GetComponentOf<MudComponentBase>().Any(x => x.FieldId == pChildComponent.BindingField))
        {
            return;
        }

        if (pChildComponent is GenButton button)
        {
            if (!button.OnClick.HasDelegate && button.ViewState != ViewState.None)
            {

                var CurrentModel = button.Context ?? new();

                var paramList = new List<(string key, object value)>();



                //paramList.Add((nameof(MudXPage<TModel>.Components), Components));
                //paramList.Add((nameof(MudXPage<TModel>.OnCreate), OnCreate));
                //paramList.Add((nameof(MudXPage<TModel>.OnUpdate), OnUpdate));
                //paramList.Add((nameof(MudXPage<TModel>.OnDelete), OnDelete));
                //paramList.Add((nameof(MudXPage<TModel>.OnLoad), OnLoad));
                //paramList.Add((nameof(MudXPage<TModel>.OnBeforeSubmit), OnBeforeSubmit));

                //paramList.Add((nameof(MudXPage<TModel>.EnableModelValidation), EnableModelValidation));
                //paramList.Add((nameof(MudXPage<TModel>.DetailGrid), DetailGrid));

                //paramList.Add((nameof(MudXPage<TModel>.IsChild), IsChild));
                //paramList.Add((nameof(MudXPage<TModel>.ParentContext), ParentContext));

                //paramList.Add((nameof(MudXPage<TModel>.ParentGrid), this));
                //paramList.Add((nameof(MudXPage<TModel>.ViewState), button.ViewState));
                //paramList.Add((nameof(MudXPage<TModel>.DialogTitle), button.Title));

                //paramList.Add((nameof(MudXPage<TModel>.ChildSubmit), ChildSubmit));

                //paramList.Add((nameof(MudXPage<TModel>.TopLevel), TopLevel));

                ////ParentGrid
                //button.OnClick = EventCallback.Factory.Create<TModel>(this, callback: async () => await ShowDialogAsync<MudXPage<TModel>>(CurrentModel, button.PageSize, paramList.ToArray()));


            }
        }

        Components.Add(pChildComponent);

    }


    #endregion RenderFragments



    //protected virtual TModel OnClose()
    //{
    //    return !DialogResult.Cancelled ? ViewModel : default;
    //}

    //public virtual async ValueTask<DialogResult> ShowDialogAsync<TMudXPage>(params (string key, object value)[] parameters) where TMudXPage : UIBase
    //{


    //    foreach (var prm in parameters)
    //    {
    //        Parameters.Add(prm.key, prm.value);
    //    }


    //    var dialogTItle = parameters.FirstOrDefault(x => x.key.Equals(nameof(MudXPage<TModel>.DialogTitle)));

    //    var dialogReference = DialogService.Show<TMudXPage>(dialogTItle.value?.ToString(), Parameters, Options);

    //    return await dialogReference.Result;



    //}


    //public virtual async ValueTask<DialogResult> ShowDialogAsync<TMudXPage>(TModel viewModel, params (string key, object value)[] parameters) where TMudXPage : UIBase
    //{
    //    var stringifiedModel = JsonSerializer.Serialize(viewModel);

    //    var cloned = JsonSerializer.Deserialize<TModel>(stringifiedModel);

    //    var newParamList = parameters.ToList();

    //    newParamList.Add((nameof(MudXPage<TModel>.ViewModel), cloned));

    //    return await ShowDialogAsync<TMudXPage>(newParamList.ToArray());
    //}

    //public virtual async ValueTask<DialogResult> ShowDialogAsync<TMudXPage>(MaxWidth pageSize, params (string key, object value)[] parameters) where TMudXPage : UIBase
    //{
    //    Options.MaxWidth = pageSize;

    //    return await ShowDialogAsync<TMudXPage>(parameters);
    //}

    //public virtual async ValueTask<DialogResult> ShowDialogAsync<TMudXPage>(TModel viewModel, MaxWidth pageSize, params (string key, object value)[] parameters) where TMudXPage : UIBase
    //{
    //    var stringifiedModel = JsonSerializer.Serialize(viewModel);

    //    var cloned = JsonSerializer.Deserialize<TModel>(stringifiedModel);

    //    var newParamList = parameters.ToList();

    //    newParamList.Add((nameof(MudXPage<TModel>.Index), ((Collection<TModel>)DataSource).IndexOf(viewModel)));
    //    newParamList.Add((nameof(MudXPage<TModel>.ViewModel), cloned));
    //    newParamList.Add((nameof(MudXPage<TModel>.Original), viewModel));
    //    newParamList.Add((nameof(MudXPage<TModel>.SmartCrud), SmartCrud));

    //    Options.MaxWidth = pageSize;

    //    var dialogResult = await ShowDialogAsync<TMudXPage>(newParamList.ToArray());


    //    return dialogResult;

    //}

    private bool SearchFilter(object element) => SearchFilterFunc(element, _searchString);

    private bool SearchFilterFunc(object model, string searchString)
    {
        if (string.IsNullOrEmpty(_searchString)) return true;

        var searchableFields = Components.Where(x => x.BindingField is not null && x.VisibleOnGrid).Select(x => x.BindingField);

        //var properties = typeof(TModel).GetProperties().ToList();

        foreach (var fieldName in searchableFields)
        {
            var columnValue = model.GetPropertyValue(fieldName);

            if (columnValue is null) continue;

            if (columnValue.ToString()!.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }


}

