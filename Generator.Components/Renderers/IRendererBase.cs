using Generator.Components.Components;
using Generator.Components.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Generator.Components.Renderers;

public interface IRendererBase<T> where T : new()
{
    public RenderTreeBuilder Builder { get; set; }

 
    public void Render(GenColumn<T> component,  RenderTreeBuilder builder, T Context, ComponentType componentType, params (string key, object value)[] parameters);

}
