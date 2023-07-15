using Microsoft.AspNetCore.Components;

namespace Generator.Components.Interfaces;

public interface IGenCompRenderer
{
    public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false, params (string Key, object Value)[] valuePairs);

    public RenderFragment RenderAsGridComponent(object model);
}
