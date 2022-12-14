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

namespace Generator.Components.Components;


public partial class GenGrid : MudTable<object>, IGenGrid
{
    public ViewState ViewState { get; set; } = ViewState.None;

    private MudTable<object> GridRef { get; set; }

    //public bool AddNewTriggered { get; set; }

    public MudIconButton EditButtonRef { get; set; }

    public bool DetailClicked { get; set; }

    #region NonParams
    public List<IGenComponent> Components { get; set; } = new();
    private string _SearchString = string.Empty;
    public bool IsFirstRender { get; set; } = true;
    public bool SearchDisabled { get; set; } = false;
    //protected object OriginalEditItem { get; set; }÷
    public object OriginalEditItem { get; set; }


    private string AddIcon { get; set; } = Icons.Material.Filled.AddCircle;
    #endregion


    #region Parameters

    [Parameter]
    public EventCallback<object> Create { get; set; }

    [Parameter]
    public EventCallback<object> Delete { get; set; }

    [Parameter]
    public EventCallback<object> Update { get; set; }

    //Form Load
    [Parameter]
    public EventCallback Load { get; set; }

    [CascadingParameter(Name = nameof(ParentComponent))]
    public GenGrid ParentComponent { get; set; }

    
    [Parameter]
    public string Title { get; set; }

    [Parameter]
    public bool EnableSearch { get; set; }

    [Parameter]
    public string SearchPlaceHolderText { get; set; } = "Search";

    [Parameter, EditorRequired()]
    public IEnumerable<object> DataSource { get; set; }

    #endregion

    #region CascadingParameters

    //[Parameter, AllowNull]
    //public bool SmartCrud { get { return _smartCrud; } set { _smartCrud = value; } }

    //[CascadingParameter(Name = nameof(SmartCrud))]
    //private bool _smartCrud { get; set; }


    #endregion

    #region RenderFragments
    [Parameter, AllowNull]
    public RenderFragment GenColumns { get; set; }

    [Parameter, AllowNull]
    public RenderFragment<object> GenDetailGrid { get; set; }

    public bool HasDetail => GenDetailGrid is not null;

    [Parameter, EditorRequired]
    public EditMode EditMode { get; set; }


    public bool NewDisabled { get; set; } = false;
    public bool ExpandDisabled { get; set; } = false;
    

    #endregion


    #region Methods

    private bool SearchFunction(object Model)
    {
        if (string.IsNullOrEmpty(_SearchString)) return true;

        var searchableFields = GetComponentOf<IGenTextField>()
            .Where((x) => x.BindingField is not null && x.VisibleOnGrid );

        foreach (var field in searchableFields)
        {
            var columnValue = Model.GetPropertyValue(field.BindingField);

            if (columnValue is null) continue;

            if (columnValue.ToString().Contains(_SearchString, StringComparison.OrdinalIgnoreCase))
                return true;
        }
        return false;
    }

    private List<T> GetComponentOf<T>() where T : IGenComponent
    {
        return Components.Where(x => x is T).Cast<T>().ToList() ;
         
    }

    public void AddChildComponent(IGenComponent childComponent)
    {
        if (Components.Any(x => x.BindingField == childComponent.BindingField)) return;
        Components.Add(childComponent);
    }

    
    #endregion

    #region RowEditMethods


 
   


    

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        //if (!IsFirstRender && AddNewTriggered)
        //{
           

        //}

        if(ViewState == ViewState.Create)
        {
            await EditButtonRef.OnClick.InvokeAsync();
            //AddNewTriggered = false;
            //await InvokeAsync(StateHasChanged);
        }
        await base.OnAfterRenderAsync(firstRender);
    }

    protected override async Task OnInitializedAsync()
    {
        if(ParentComponent is not null && ParentComponent.DetailClicked)
        {
            await InvokeAsync(ParentComponent.StateHasChanged);
        }
        await base.OnInitializedAsync();
    }



    private async Task OnBeforeAnyAction(object element)
    {
        NewDisabled = true;
        ExpandDisabled = true;
        SearchDisabled = true;
        SelectedItem = element;
        OriginalEditItem = element.DeepClone();

        await InvokeAsync(StateHasChanged);

    }

    private async Task OnCommit(object element)
    {
        NewDisabled = false;
        ExpandDisabled = false;
        if (Create.HasDelegate)
            await Create.InvokeAsync(element);
    }

    public async Task OnAddNewEvent()
    {
        ViewState = ViewState.Create;

        var DatasourceModelType = DataSource.GetType().GenericTypeArguments[0];

        var newData = Components.ToDictionary<IGenComponent, string, object>(comp => comp.BindingField, comp => comp.GetDefaultValue);

        var adaptedData = newData.Adapt(typeof(Dictionary<string, object>), DatasourceModelType);

        SelectedItem = adaptedData;

        DataSource = DataSource.Add(SelectedItem);

        //AddNewTriggered = true;

        //await InvokeAsync(StateHasChanged);
    }

    public async Task OnEditCLick()
    {
        if(Load.HasDelegate)
            await Load.InvokeAsync();
    }

    public  Task OnDeleteClicked(Action buttonAction)
    {
        var dataToRemove = buttonAction.Target.CastTo<MudTr>().Item;

        DataSource = DataSource.Remove(dataToRemove);

        StateHasChanged();

        return Task.CompletedTask;
    }

    private void OnCancel(object element)
    {

        var datasourceItem = DataSource.Select((item, index) => new { item, index }).FirstOrDefault(x => x.item == element);

        DataSource = DataSource.Replace(datasourceItem.index, OriginalEditItem);

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

    public RenderFragment RenderAsComponent(object model)
    {
        throw new NotImplementedException();
    }

    public RenderFragment RenderAsGridComponent(object model)
    {
        throw new NotImplementedException();
    }
    #endregion
}

