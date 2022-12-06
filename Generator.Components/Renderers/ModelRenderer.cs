using Microsoft.AspNetCore.Components;

namespace Generator.Components.Renderers;

public class ModelRenderer<T> : IRendererBase<T>
{
    public RenderFragment RenderFragment { get; set; } = (builder) =>
    {

    };
}
