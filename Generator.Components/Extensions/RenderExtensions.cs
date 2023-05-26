using Generator.Components.Components;
using Generator.Components.Interfaces;
//using Generator.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Generator.Components.Extensions;

public static class RenderExtensions
{
    public static void RenderGrid(RenderTreeBuilder builder ,object value)
    {
        builder.OpenComponent(0, typeof(GridLabel));
        builder.AddAttribute(1, nameof(GridLabel.Value), value);
          
        builder.CloseComponent();
    }

    public static void RenderComponent<T>(this RenderTreeBuilder builder, T component, bool ignoreLabels, params (string Key, object Value)[] AdditionalParams) where T:IGenComponent
    {
        //if (component.Model is null) return;

        var i = 1;
        builder.OpenComponent(0, typeof(T));

        foreach (var param in GetPropertyParameters(component,ignoreLabels))
        {
            if (param.Value is null) continue;

            var index = i;

            builder.AddAttribute(index, param.Key, param.Value);

            i++;
        }
            
        foreach (var additional in AdditionalParams)
        {
            builder.AddAttribute(i, additional.Key, additional.Value);
            i++;
        }
        //builder.AddComponentReferenceCapture(i++, ins => { component.Reference = (IGenComponent)ins; });
        builder.CloseComponent();

    }

    internal static IEnumerable<(string Key, object Value)> GetPropertyParameters<T>(T Component, bool IgnoreLabels) where T : IGenComponent
    {
        var properties = Component.GetType().GetProperties()
            .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(ParameterAttribute))
                        && x.Name != "UserAttributes");

        if (IgnoreLabels)
            properties = properties.Where(x => !x.Name.Equals(nameof(IGenComponent.Label)));


        foreach (var property in properties)
            yield return (property.Name, Component.GetPropertyValue(property.Name));
    }


}


internal static class ComponentExtensions
{
    public static GenGrid<object> ConvertToGridObject<TModel>(this GenGrid<TModel> genericClass) where TModel:new ()
    {
        var objectType = typeof(GenGrid<object>);

        var genericType = genericClass.GetType();

        var objectClass = (GenGrid<object>)Activator.CreateInstance(objectType);

        return objectClass;
    }
}