using System.Dynamic;

namespace Generator.Server.Extensions;

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


