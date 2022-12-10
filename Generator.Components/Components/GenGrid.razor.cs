using System;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using MudBlazor;
using System.Collections.ObjectModel;
using System.Reflection;
using Generator.Shared.Extensions;
using static MudBlazor.CategoryTypes;
using Generator.Components.Interfaces;
using Force.DeepCloner;
using Generator.Components.Enums;
using System.ComponentModel;
using Mapster;
using Generator.Shared.TEST_WILL_DELETE_LATER;
using MudBlazorFix;

namespace Generator.Components.Components;

public partial class GenGrid : MudTable<object>, IGenGrid
{
    private MudTable<object> GridRef { get; set; }

    public bool AddNewTriggered { get; set; }

    public MudIconButton EditButtonRef { get; set; }

    #region NonParams
    public List<IGenComponent> Components { get; set; } = new();
    private string _SearchString = string.Empty;
    public bool IsFirstRender { get; set; } = true;
    private bool IsSearchDisabled = false;
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

    [Parameter]
    public EventCallback Load { get; set; }

    [CascadingParameter(Name = nameof(ParentContext))]
    public object ParentContext { get; set; }

    [Parameter, AllowNull]
    public bool SmartCrud { get { return _smartCrud; } set { _smartCrud = value; } }

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

    [CascadingParameter(Name = nameof(SmartCrud))]
    private bool _smartCrud { get; set; }


    #endregion

    #region RenderFragments
    [Parameter, AllowNull]
    public RenderFragment GenColumns { get; set; }

    [Parameter, AllowNull]
    public RenderFragment GenDetailGrid { get; set; }

    [Parameter, EditorRequired]
    public EditMode EditMode { get; set; }
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

    public RenderFragment RenderComponent(object model, ComponentType componentType)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region RowEditMethods


 
    private async Task OnBeforeAnyAction(object element)
    {
        IsSearchDisabled = true;
        SelectedItem = element;
        OriginalEditItem = element.DeepClone();

        await InvokeAsync(StateHasChanged);

    }

    private async Task OnCommit(object element)
    {
        IsSearchDisabled = false;

        if (Create.HasDelegate)
            await Create.InvokeAsync(element);

    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (!IsFirstRender && AddNewTriggered)
        {
            EditButtonRef.OnClick.InvokeAsync();
            AddNewTriggered = false;
        }
        return base.OnAfterRenderAsync(firstRender);
    }

    private void OnCancel(object element)
    {

        var datasourceItem = DataSource.Select((item, index) => new { item, index }).FirstOrDefault(x => x.item == element);

        DataSource = DataSource.Replace(datasourceItem.index, OriginalEditItem);
 
        IsSearchDisabled = false;
        StateHasChanged();
    }


     
    public async Task OnAddNewEvent()
    {
        var DatasourceModelType = DataSource.GetType().GenericTypeArguments[0];

        var newData = Components.ToDictionary<IGenComponent, string, object>(comp => comp.BindingField, comp => comp.GetDefaultValue);

        var adaptedData = newData.Adapt(typeof(Dictionary<string, object>), DatasourceModelType);

        DataSource = DataSource.Add(adaptedData);

        AddNewTriggered = true;
        
        await InvokeAsync(StateHasChanged);

    }
    public async Task OnEditCLick()
    {
        if(Load.HasDelegate)
            await Load.InvokeAsync();
    }
    #endregion
}

