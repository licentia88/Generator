using Generator.Components.Enums;
using static Generator.Components.Components.GenGrid;

namespace Generator.Components.Interfaces;

public interface IGenGrid : IGenCompRenderer
{
    public string Title { get; set; }

    public bool EnableSearch { get; set; }

    public bool SmartCrud { get; set; }

    public bool IsFirstRender { get; set; }

    public EditMode EditMode { get; set; }

    public IEnumerable<object> DataSource { get; set; }

    public bool AddNewTriggered { get; set; }

    public object OriginalEditItem { get; set; }

}

