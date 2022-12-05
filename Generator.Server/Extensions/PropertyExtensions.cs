using System.Reflection;
using FastMember;

namespace Generator.Server.Extensions;


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

    public static List<T> CreateDynamicList<T>(this T type)
    {
        return new List<T>();
    }

    public static List<dynamic> CreateDynamicList(string typeName, Assembly assembly)
    {
        var type = ReflectionExtensions.ReconstructType(typeName, true, assembly);

        var lt = typeof(List<>);

        var result = (IEnumerable<dynamic>)Activator.CreateInstance(lt.MakeGenericType(type));

        return result.ToList();

    }


    public static T CreateNew<T>(this object type) where T : new()
    {
        return new T();
    }
}

