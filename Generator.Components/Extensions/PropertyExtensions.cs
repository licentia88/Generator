using System.Collections.ObjectModel;
using System.Dynamic;
using System.Reflection;

namespace Generator.Components.Extensions;

internal static class PropertyExtensions
{

    public static object GetPropertyValue<T>(this T obj, string propertyName)
    {
        try
        {
            if (obj is null) return default;

            if (obj is ExpandoObject || obj is Dictionary<string, object>)
                return ((IDictionary<string, object>)obj)[propertyName] ?? null;

            return obj.GetType()
                .GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                ?.GetValue(obj);

        }
        catch (Exception ex)
        {
            return null;
        }

    }

    public static void SetPropertyValue<T>(this T obj, string propertyName, object propertyValue)
    {
        try
        {
            if (obj is ExpandoObject || obj is Dictionary<string, object>)
            {
                ((IDictionary<string, object>)obj)[propertyName] = propertyValue;

                return;
            }

            var property = obj.GetType()
                .GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            property.SetValue(obj, ChangeToType(propertyValue, property.PropertyType));
        }
        catch (Exception ex)
        {

        }

    }

    private static object ChangeToType(object value, Type destinationType)
    {
        try
        {
            return Convert.ChangeType(value, Nullable.GetUnderlyingType(destinationType) ?? destinationType);
        }
        catch
        {
            return GetDefaultValue(destinationType);
        }
    }

    public static object GetFieldValue<T>(this T obj, string propertyName) //where T:new()
    {
        try
        {
            if (obj is null) return default;

            if ((obj is ExpandoObject || obj is Dictionary<string, object>))
                return ((IDictionary<string, Object>)obj)[propertyName] ?? null;

            return obj.GetType()
                .GetField(propertyName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                .GetValue(obj) ?? null;
        }
        catch (Exception ex)
        {
            return null; 
        }
    }

    public static void SetFieldValue<T>(this T obj, string propertyName, object propertyValue)
    {
        try
        {
            if (obj is ExpandoObject || obj is Dictionary<string, object>)
            {
                ((IDictionary<string, Object>)obj)[propertyName] = propertyValue;

                return;
            }

            obj.GetType().GetField(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(obj, propertyValue);

        }
        catch (Exception ex)
        {

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

        return newDict;
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

    public static bool HasProperty<T>(this T obj, string propertyName) where T : Type
    {
        var test = obj.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy).ToList();

        PropertyInfo property = obj.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
        return property != null;
    }


    //public static bool HasProperty<T>(this T obj, string propertyName)
    //{
    //    return typeof(T).GetProperty(propertyName) != null;
    //}

    public static bool HasProperty(this object objectToCheck, string propertyName)
    {
        var type = objectToCheck.GetType();
        return type.GetProperty(propertyName) != null;
    }

    public static IEnumerable<TType> Exclude<TType, ExcludeType>(this IEnumerable<TType> list)
    {
        return list.Where(x => x is not ExcludeType).Cast<TType>();
    }

    public static T GetDefaultValue<T>()
    {
        return default(T);
    }

    public static bool IsNullOrDefault<T>(this T argument)
    {
        if (argument is not ValueType && argument is null) return true;

        if (argument is string str)
            return string.IsNullOrEmpty(str);

        return argument.Equals(default);
        //return EqualityComparer<T>.Default.Equals(typeof(argument), default);
    }



    public static object GetDefaultValue(this Type type)
    {
        if (type is not null && type.IsValueType && Nullable.GetUnderlyingType(type) == null)
        {
            return Activator.CreateInstance(type);
        }
        else
        {
            return null;
        }
    }
}

