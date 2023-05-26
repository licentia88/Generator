using Microsoft.AspNetCore.Components;

namespace Generator.Components.Interfaces;

public interface IGenCompRenderer
{
    public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false);

    public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false, params KeyValuePair<string, object>[] valuePairs);

    public RenderFragment RenderAsGridComponent(object model);
}
