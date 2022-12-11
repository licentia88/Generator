using System.Diagnostics.CodeAnalysis;
using Generator.Components.Components;
using Generator.Components.Enums;
using Microsoft.AspNetCore.Components;
using static Generator.Components.Components.GenGrid;

namespace Generator.Components.Interfaces;

public interface IGenGrid : IGenCompRenderer
{
    public string Title { get; set; }

    public bool EnableSearch { get; set; }

    public bool IsFirstRender { get; set; }

    public EditMode EditMode { get; set; }

    public IEnumerable<object> DataSource { get; set; }

    public bool AddNewTriggered { get; set; }

    public object OriginalEditItem { get; set; }

    public bool NewDisabled { get; set; }
    public bool ExpandDisabled { get; set; }

    
    public bool SearchDisabled { get; set; }

    public RenderFragment GenColumns { get; set; }

    public RenderFragment<object> GenDetailGrid { get; set; }

    public bool  HasDetail { get;}

    public bool DetailClicked { get; set; }

   
    public GenGrid ParentComponent { get; set; }
}

