using Generator.Components.Components;
using Generator.Components.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Generator.Components.Renderers;

public class DictionaryRenderer<T> : IRendererBase<T> where T:new()
{
    public RenderTreeBuilder Builder { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void Render(GenColumn<T> component, RenderTreeBuilder builder, T Context, ComponentType componentType, params (string key, object value)[] parameters)
    {
        throw new NotImplementedException();
    }
}
