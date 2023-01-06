using Microsoft.AspNetCore.Components;

namespace Generator.Components.Interfaces;

public interface IGenCompRenderer
{
    public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false);

    public RenderFragment RenderAsGridComponent(object model);
}
