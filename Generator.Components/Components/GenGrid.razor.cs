using System;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using MudBlazor;
using System.Collections.ObjectModel;
using System.Reflection;
using Generator.Shared.Extensions;
using Generator.Components.Interfaces;
using Force.DeepCloner;
using Generator.Components.Enums;
using System.ComponentModel;
using Mapster;
using Generator.Shared.TEST_WILL_DELETE_LATER;
using MudBlazorFix;
using System.Data.Common;
using System.Windows.Input;
using System.Dynamic;
using Generator.Components.Args;
using Microsoft.FSharp.Data.UnitSystems.SI.UnitNames;

namespace Generator.Components.Components;


public partial class GenGrid : MudTable<object>, IGenView
{
    public ViewState ViewState { get; set; } = ViewState.None;

    public MudIconButton EditButtonRef { get; set; }

    public bool DetailClicked { get; set; }

    #region NonParams
    public List<IGenComponent> Components { get; set; } = new();
    private string _SearchString = string.Empty;
    public bool IsFirstRender { get; set; } = true;
    public bool SearchDisabled { get; set; }
    public object OriginalEditItem { get; set; }
 
    private string AddIcon { get; set; } = Icons.Material.Filled.AddCircle;
    #endregion


    #region Parameters

    [Parameter]
    public EventCallback<GenGridArgs> Create { get; set; }

    [Parameter]
    public EventCallback<GenGridArgs> Delete { get; set; }

    [Parameter]
    public EventCallback<GenGridArgs> Update { get; set; }

    //Form Load
    [Parameter]
    public EventCallback<IGenView> Load { get; set; }

    [CascadingParameter(Name = nameof(ParentComponent))]
    public GenGrid ParentComponent { get; set; }

    
    [Parameter]
    public string Title { get; set; }

    [Parameter]
    public bool EnableSearch { get; set; }

    [Parameter]
    public string SearchPlaceHolderText { get; set; } = "Search";

    [Parameter, EditorRequired()]
    public ICollection<object> DataSource { get; set; }

    #endregion

    #region CascadingParameters


    #endregion

    [Parameter, AllowNull]
    public RenderFragment GenColumns { get; set; }

    [Parameter, AllowNull]
    public RenderFragment<object> GenDetailGrid { get; set; }

    public bool HasDetail => GenDetailGrid is not null;

    [Parameter, EditorRequired]
    public EditMode EditMode { get; set; }


    public bool NewDisabled { get; set; } 

    public bool ExpandDisabled { get; set; } 
    



    #region Methods

    private bool SearchFunction(object Model)
    {
        if (string.IsNullOrEmpty(_SearchString)) return true;

        var searchableFields = GetComponentsOf<IGenTextField>()
            .Where((x) => x.BindingField is not null && x.VisibleOnGrid );

        foreach (var field in searchableFields)
        {
            var columnValue = Model.GetPropertyValue(field.BindingField);

            if (columnValue is null ) continue;

            if (columnValue.ToString()!.Contains(_SearchString, StringComparison.OrdinalIgnoreCase))
                return true;
        }
        return false;
    }

   


    #endregion

    #region RowEditMethods

    private List<Action> EditButtonActionList { get; set; } = new();


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
            await OnNewItemAddEditInvoker();
        // await base.OnAfterRenderAsync(firstRender);
    }

    /// <summary>
    /// When a new item is added in inlinemode, for better user experience it has to be on the first row of the table,
    /// therefore the newly added item is inserted to 0 index of datasource however the component is not rendered at that moment
    /// and this method must be called on after render method
    /// </summary>
    /// <returns></returns>
    private async Task OnNewItemAddEditInvoker()
    {
        if (ViewState == ViewState.Create)
        {
            if (!EditButtonActionList.Any() && EditButtonRef is not null)
            {
                await EditButtonRef.OnClick.InvokeAsync();
                return;
            }

            var firstItem = EditButtonActionList.FirstOrDefault(x => x.Target?.CastTo<MudTr>().Item == SelectedItem);
            firstItem?.Invoke();

            EditButtonActionList.Clear();
        }
    }

    protected override async Task OnInitializedAsync()
    {
        //Detail Grid
        if(ParentComponent is not null && ParentComponent.DetailClicked)
        {
            await InvokeAsync(ParentComponent.StateHasChanged);
        }
    }



    private void OnBackUp(object element)
    {
        NewDisabled = true;
        ExpandDisabled = true;
        SearchDisabled = true;
        SelectedItem = element;
        OriginalEditItem = element.DeepClone();
    }

 
    private async Task OnCommit(object model)
    {
        NewDisabled = false;
        ExpandDisabled = false;
        
        await ViewInvokeDecisioner(model);
    }

    /// <summary>
    /// Invokes the eventcallback depending on the viewstate
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private async Task ViewInvokeDecisioner(object model)
    {
        switch (ViewState)
        {
            case ViewState.Create when Create.HasDelegate:
                await Create.InvokeAsync(new GenGridArgs(null, model));
                break;
            case ViewState.Update when Update.HasDelegate:
                await Update.InvokeAsync(new GenGridArgs(OriginalEditItem, model));
                break;
            case ViewState.Delete when Delete.HasDelegate:
                await Delete.InvokeAsync(new GenGridArgs(OriginalEditItem, model));
                break;
            case ViewState.None:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        ViewState = ViewState.None;
    }

    public async Task OnCreateClick()
    {
        EditButtonActionList.Clear();

        ViewState = ViewState.Create;

        var DatasourceModelType = DataSource.GetType().GenericTypeArguments[0];

        var newData = Components.ToDictionary(comp => comp.BindingField, comp => comp.GetDefaultValue);

        var adaptedData = newData.Adapt(typeof(Dictionary<string, object>), DatasourceModelType);

        SelectedItem = adaptedData;

        DataSource.Insert(0, SelectedItem);
       
        if (Load.HasDelegate)
            await Load.InvokeAsync(this);

        //await InvokeAsync(StateHasChanged);
        }

    public async Task OnEditCLick()
    {
        if(Load.HasDelegate)
            await Load.InvokeAsync();
    }



    public Task OnDeleteClicked(Action buttonAction)
    {
        ViewState = ViewState.Delete;

        var dataToRemove = buttonAction.Target.CastTo<MudTr>().Item;

        DataSource.Remove(dataToRemove);

        return Task.CompletedTask;
    }

    private void OnCancelClick(object element)
    {
        if (ViewState == ViewState.Create)
            DataSource.RemoveAt(0);
        else
            DataSource.Replace(element, OriginalEditItem);

        SearchDisabled = false;
        NewDisabled = false;
        ExpandDisabled = false;

        ViewState = ViewState.None;
        StateHasChanged();
    }

    public async Task OnDetailClicked()
    {
        DetailClicked = !DetailClicked;

        await InvokeAsync(StateHasChanged);

    }

    public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false)
    {
        throw new NotImplementedException();
    }

    public RenderFragment RenderAsGridComponent(object model)
    {
        throw new NotImplementedException();
    }

    public TComponent GetComponent<TComponent>(string BindingField) where TComponent : IGenComponent
    {
        var item = Components.FirstOrDefault(x => x.BindingField.Equals(BindingField));

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

    #endregion
}

