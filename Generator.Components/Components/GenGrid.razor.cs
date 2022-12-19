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

    // private MudTable<object> GridRef { get; set; }


    public MudIconButton EditButtonRef { get; set; }

    public bool DetailClicked { get; set; }

    #region NonParams
    public List<IGenComponent> Components { get; set; } = new();
    private string _SearchString = string.Empty;
    public bool IsFirstRender { get; set; } = true;
    public bool SearchDisabled { get; set; } = false;
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

        var searchableFields = GetComponentsOf<IGenTextField>()
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

   


    #endregion

    #region RowEditMethods

    private List<Action> EditButtonActionList { get; set; } = new();


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (ViewState == ViewState.Create)
        {
            if (EditButtonActionList.Count() == 0 && EditButtonRef is not null)
            {
                await EditButtonRef.OnClick.InvokeAsync();
                return;
            }

            var firstItem = EditButtonActionList.FirstOrDefault(x => x.Target?.CastTo<MudTr>().Item == SelectedItem);
            firstItem?.Invoke();

            EditButtonActionList.Clear();
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

        //await base.OnInitializedAsync();
    }



    private void OnBackUp(object element)
    {
        NewDisabled = true;
        ExpandDisabled = true;
        SearchDisabled = true;
        SelectedItem = element;
        OriginalEditItem = element.DeepClone();

        //await InvokeAsync(StateHasChanged);

    }

 
    private async Task OnCommit(object model)
    {
        NewDisabled = false;
        ExpandDisabled = false;

        if (ViewState == ViewState.Create && Create.HasDelegate)
            await Create.InvokeAsync(new GenGridArgs(null, model));

        if (ViewState == ViewState.Update && Update.HasDelegate)
            await Update.InvokeAsync(new GenGridArgs(OriginalEditItem, model));

        if (ViewState == ViewState.Delete && Delete.HasDelegate)
            await Update.InvokeAsync(new GenGridArgs(OriginalEditItem, model));

    }

    public async Task OnCreateClick()
    {
        EditButtonActionList.Clear();

        ViewState = ViewState.Create;

        var DatasourceModelType = DataSource.GetType().GenericTypeArguments[0];

        var newData = Components.ToDictionary<IGenComponent, string, object>(comp => comp.BindingField, comp => comp.GetDefaultValue);

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



    public  Task OnDeleteClicked(Action buttonAction)
    {
        ViewState = ViewState.Delete;
        //var param1s = buttonAction.Method.GetParameters();

        var dataToRemove = buttonAction.Target.CastTo<MudTr>().Item;

         DataSource.Remove(dataToRemove);

        //StateHasChanged();

        return Task.CompletedTask;
    }

    private void OnCancelClick(object element)
    {
        var datasourceItem = DataSource.Select((item, index) => new { item, index }).FirstOrDefault(x => x.item == element);

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

