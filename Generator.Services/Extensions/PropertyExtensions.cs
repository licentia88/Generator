using System.Dynamic;
using FastMember;

namespace Generator.Service.Extensions;


public static class ObjectExtensions
{
    public static IDictionary<string, object> NewExpandObject()
    {
        return new ExpandoObject() as IDictionary<string, object>;       
    }

    public static void AddKeyValuePair(this IDictionary<string, object> obj, string Key, object value)
    {
        obj[Key] = value;
    }
}
public static class PropertyExtensions
{
    public static object GetPropertyValue<T>(this T obj, string propertyName)
    {
        var accessor = TypeAccessor.Create(typeof(T));

        return accessor[obj, propertyName];
    }

    public static void SetPropertyValue<T>(this T obj, string propertyName, object propertyValue)
    {
        var accessor = TypeAccessor.Create(typeof(T));

        try
        {
            accessor[obj, propertyName] = propertyValue;
        }
        catch
        {
            // ignored
        }
    }
}

