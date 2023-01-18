using System.ComponentModel;
using System.Reflection;

namespace Generator.Shared.Extensions;

public static class AttributeExtensions
{
    public static string GetDisplayName<TModel>(string propertyName) 
    {
        var modelType = typeof(TModel);

        var property = modelType.GetProperty(propertyName);

        var displayAttribute = property?.GetCustomAttribute<DisplayNameAttribute>();

        return displayAttribute?.DisplayName ?? propertyName;
    }
}
