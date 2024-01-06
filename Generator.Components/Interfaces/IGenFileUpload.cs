using MudBlazor;

namespace Generator.Components.Interfaces;

public interface IGenFileUpload: IGenComponent
{
    public string EmptyText { get; set; }
 
    public bool FullWidth { get; set; }

    public Color Color { get; set; }

    public Variant Variant { get; set; }

 }