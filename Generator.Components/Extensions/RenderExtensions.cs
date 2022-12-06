using System.Reflection;
using Generator.Components.Components;
using Generator.Components.Enums;
using Generator.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;

namespace Generator.Components.Extensions;

public static class RenderExtensions
{
    //public static void Render(this GenColumn component, RenderTreeBuilder builder, object context, ComponentType componentType, params (string key, object value)[] parameters) 
    //{ 
    //     var properties = component.GetType().GetProperties().Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(ParameterAttribute)) && x.Name != "UserAttributes");

    //    builder.OpenComponent(0, component.GetType());

    //    foreach (var property in properties.Select((val, index) => (val, index)))
    //    {
    //        var index = property.index + 1;

    //        var propName = property.val.Name;

    //        var value = component.GetPropertyValue(propName);

    //        builder.AddAttribute(index, propName, value);

    //    }

    //    builder.AddAttribute(99, nameof(component.Context), context);


    //    if (component.HasProperty(nameof(ComponentType)))
    //    {
    //        //Console.WriteLine($"Adding attribute {componentType}");
    //        builder.AddAttribute(100, nameof(componentType), componentType);
    //    }


    //    foreach (var additionalParameters in parameters)
    //    {
    //        builder.AddAttribute(101, additionalParameters.key, additionalParameters.value);
    //    }



    //    builder.CloseComponent();
    //}


    //public static RenderFragment CreateFragment<T>(GenColumn column, object context, params (string key, object value)[] parameters) where T : MudComponentBase => builder =>
    //{
    //    var properties = column.GetType().GetProperties().Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(ParameterAttribute)) && x.Name != "UserAttributes");


    //    builder.OpenComponent(0, typeof(T));

    //    column.Render(builder, column, column.ComponentType, parameters);
    //    //builder.OpenComponent(0, typeof(T));
    //    //builder.AddAttribute(1, "Value", 123);
    //    //builder.CloseComponent();
    //};

    //public static Type GetTypeOfObject<T>(this T obj) where T : MudComponentBase
    //{
    //    var d1 = typeof(MudComponentBase;
    //    Type[] typeArgs = { obj.GetType() };
    //    var makeme = d1.MakeGenericType(typeArgs);

    //    return makeme;
    //}
}
