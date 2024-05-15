using DocumentFormat.OpenXml.Spreadsheet;
using Generator.Components.Components;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace Generator.Components.Interfaces;

public interface ISinglePage<TModel>: IPageBase where TModel : class, new()
{
    [Parameter, AllowNull]
    public RenderFragment<TModel> GenDetailGrid { get; set; }

    [Parameter]
    public GenGrid<TModel> GenGrid { get; set; }

    [Parameter]
    public Dictionary<string, object> Parameters { get; set; }  

    //[CascadingParameter(Name = nameof(Parameters))]
    //private Dictionary<string, object> _Parameters { get => Parameters; set => Parameters = value; }
}
