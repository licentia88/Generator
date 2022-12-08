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
using Generator.Server.Extensions;

namespace Generator.Components.Components;

public partial class GenGrid : MudTable<object>, IGenGrid
{
    

    #region NonParams
    public List<IGenComponent> Components { get; set; } = new();
    private string _SearchString = string.Empty;
    public bool IsFirstRender { get; set; } = true;
    private bool IsSearchDisabled = false;

    private string AddIcon { get; set; } = Icons.Material.Filled.AddCircle;
    #endregion


    #region Parameters

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
        //return Components.
        //    Where(x => x is TType).
        //    Exclude<ColumnBase<TModel>, IButton>()
        //    .Exclude<ColumnBase<TModel>, GridSpacer<TModel>>()
        //    .Cast<TType>().ToList();
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

    private object elementBeforeEdit;



    private void BackupItem(object element)
    {
        IsSearchDisabled = true;
        elementBeforeEdit = element.DeepClone();
        StateHasChanged();

    }

    private void OnSubmit(object element)
    {
        if (1 != 2)//Fail olursa
        {
            OnCancel(element);
        }

        IsSearchDisabled = false;
        //AddEditionEvent($"RowEditCommit event: Changes to Element {((Element)element).Name} committed");
    }

    private void OnCancel(object element)
    {
        var datasourceItem = DataSource.Select((item, index) => new { item, index }).FirstOrDefault(x => x.item == element);

        DataSource = DataSource.Replace(datasourceItem.index, elementBeforeEdit);



        IsSearchDisabled = false;
        StateHasChanged();
    }

    public void OnCommitEditEvent()
    {

    }
    #endregion
}

