using Microsoft.AspNetCore.Components;

namespace Generator.Components.Renderers;

public abstract class RendererBase<T> : IRendererBase<T>
{
    public RenderFragment Render { get; }
}
