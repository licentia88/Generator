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

namespace Generator.Components.Components;

public partial class GenGrid : MudTable<object>
{
    #region NonParams
    public List<MudComponentBase> Components { get; set; } = new();

    public bool IsFirstRender { get; set; } = true;
    #endregion


    #region Parameters

    [Parameter, AllowNull]
    public bool SmartCrud { get { return _smartCrud; } set { _smartCrud = value; } }


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
    #endregion




    #region Methods

    private List<MudComponentBase> GetComponentOf()
    {
        return Components;
        //return Components.
        //    Where(x => x is TType).
        //    Exclude<ColumnBase<TModel>, IButton>()
        //    .Exclude<ColumnBase<TModel>, GridSpacer<TModel>>()
        //    .Cast<TType>().ToList();
    }

    public void AddChildComponent(MudComponentBase childComponent)
    {
        Components.Add(childComponent);
    }
    #endregion
}

