using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace Generator.Components.Renderers;


 
public class RenderManager<T>
{
    [Inject]
    public IRendererBase<T> Renderer { get; set; }
     
    public RenderManager(IServiceProvider Services)
    {
         if(typeof(T) == typeof(Dictionary<string, object>))
            Renderer = Services.GetService<DictionaryRenderer<T>>();

        if (typeof(T).IsTypeDefinition)
            Renderer = Services.GetService<ModelRenderer<T>>();

    }
    
}
