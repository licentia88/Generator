using System.Reflection;

namespace Generator.Shared.Extensions;

public static class PropertyExtensions
{
    public static List<T> CreateDynamicList<T>(this T type)
    {
        return new List<T>();
    }

    public static List<dynamic> CreateDynamicList(string typeName,Assembly assembly)
    {
        var type = ReflectionExtensions.ReconstructType(typeName,true, assembly);

        var lt = typeof(List<>);

        var result = (IEnumerable<dynamic>)Activator.CreateInstance(lt.MakeGenericType(type));

        return result.ToList();

    }
   

    public static T CreateNew<T>(this object type) where T : new()
    {
        return new T();
    }

}



