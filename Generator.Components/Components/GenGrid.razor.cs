using System;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using MudBlazor;
using System.Collections.ObjectModel;
using Generator.Components.Enums;
using System.Reflection;
using Generator.Shared.Extensions;
using static MudBlazor.CategoryTypes;
using Generator.Components.Interfaces;

namespace Generator.Components.Components;

public partial class GenGrid : MudTable<object>, IGenGrid
{
    #region NonParams
    public List<IGenCompRenderer> Components { get; set; } = new();
    private string _SearchString = string.Empty;
    public bool IsFirstRender { get; set; } = true;

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
    public ICollection<object> DataSource { get; set; }

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

    private List<T> GetComponentOf<T>() where T : IGenCompRenderer
    {
        return Components.Where(x => x is T).Cast<T>().ToList() ;
        //return Components.
        //    Where(x => x is TType).
        //    Exclude<ColumnBase<TModel>, IButton>()
        //    .Exclude<ColumnBase<TModel>, GridSpacer<TModel>>()
        //    .Cast<TType>().ToList();
    }

    public void AddChildComponent(IGenCompRenderer childComponent)
    {
        Components.Add(childComponent);
    }

   

    public RenderFragment Render(object Model, ComponentType componentType, params (string key, object value)[] AdditionalParameters)
    {
        throw new NotImplementedException();
    }
    #endregion
}

