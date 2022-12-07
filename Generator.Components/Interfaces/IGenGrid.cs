namespace Generator.Components.Interfaces;

public interface IGenGrid : IGenCompRenderer
{
    public string Title { get; set; }

    public bool EnableSearch { get; set; }

    public bool SmartCrud { get; set; }

    public bool IsFirstRender { get; set; }
}

