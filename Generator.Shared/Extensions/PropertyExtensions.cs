using System.Collections.ObjectModel;
using System.Reflection;
using FastMember;
using Generator.Server.Extensions;

namespace Generator.Shared.Extensions;

public static class PropertyExtensions
{
    public static object GetPropertyValue<T>(this T obj, string propertyName) //where T:new()
    {
        if (obj is null) return default;

        if(obj.GetType() == typeof(Dictionary<string, object>) )
        {
            var cast = obj.CastTo<Dictionary<string, object>>();

            return cast[propertyName];
        }
        else
        {
            var accessor = TypeAccessor.Create(obj.GetType());

            return accessor[obj, propertyName];
        }
       
    }
 
    public static void SetPropertyValue<T>(this T obj, string propertyName, object propertyValue)
    {
        if (obj.GetType() == typeof(Dictionary<string, object>))
        {
            var cast = obj.CastTo<Dictionary<string, object>>();

            cast[propertyName] = propertyValue;
        }
        else
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

    public static Dictionary<string, object> CreateNewDictionaryModel(this IDictionary<string, object> type) 
    {
        var newDict = new Dictionary<string, object>(type);

        foreach (var val in newDict)
        {
            newDict[val.Key] = default;
        }

        return  newDict;
    }

    public static ObservableCollection<T> ToObservable<T>(this IEnumerable<T> enumerableList)
    {
        return enumerableList != null ? new ObservableCollection<T>(enumerableList) : new();
    }

    private static bool IsNullable<T>(this T obj)
    {
        return typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(Nullable<>);
    }

    private static bool IsNullable(Type obj)
    {
        return obj.IsGenericType && obj.GetGenericTypeDefinition() == typeof(Nullable<>);
    }

    public static bool HasMethod<T>(this T obj, string methodName)
    {
        return typeof(T).GetMethod(methodName) != null;
    }

    public static bool HasMethod(this object obj, string methodName)
    {
        var type = obj.GetType();
        return type.GetMethod(methodName) != null;
    }

    public static bool HasProperty<T>(this T obj, string propertyName)
    {
        return typeof(T).GetProperty(propertyName) != null;
    }

    public static bool HasProperty(this object objectToCheck, string propertyName)
    {
        var type = objectToCheck.GetType();
        return type.GetProperty(propertyName) != null;
    }

    public static IEnumerable<TType> Exclude<TType, ExcludeType>(this IEnumerable<TType> list)
    {
        return list.Where(x => x is not ExcludeType).Cast<TType>();
    }
}