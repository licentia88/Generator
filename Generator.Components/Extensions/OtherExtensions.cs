using System.Dynamic;

namespace Generator.Components.Extensions;

internal static class OtherExtensions
{
    public static bool IsDictionary<TModel>(this ICollection<TModel> collection)
    {
        var type = typeof(TModel);
        return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>);
    }

    public static bool IsModel<TModel>(this TModel collection)
    {
        return !(collection is ExpandoObject || collection is Dictionary<string, object>);
    }
     
}
