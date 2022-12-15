namespace Generator.Shared.Extensions;

public static class CollectionExtensions
{
    public static IEnumerable<T> Add<T>(this IEnumerable<T> enumerable, T value)
    {
        foreach (var item in enumerable)
            yield return item;

        yield return value;
    }

    public static IEnumerable<T> Add2<T>(this IEnumerable<T> enumerable, T value)
    {
        return enumerable.Concat(new T[] { value });
    }

    public static IEnumerable<T> Insert<T>(this IEnumerable<T> enumerable, int index, T value)
    {
        int current = 0;
        foreach (var item in enumerable)
        {
            if (current == index)
                yield return value;

            yield return item;
            current++;
        }
    }
    public static IEnumerable<T> Insert2<T>(this IEnumerable<T> enumerable, int index, T value)
    {
        return enumerable.SelectMany((x, i) => index == i ? new T[] { value, x } : new T[] { x });
    }



    public static IEnumerable<T> Replace<T>(this IEnumerable<T> enumerable, int index, T value)
    {
        int current = 0;
        foreach (var item in enumerable)
        {
            yield return current == index ? value : item;
            current++;
        }
    }

     

    public static int? FindIndexByHash<T>(this IEnumerable<T> enumerable,  T value)
    {
        int? current = null;
        foreach (var item in enumerable.Select((x, i)=> (x,i)))
        {
            if (item.x.GetHashCode() == value.GetHashCode())
            {
                current = item.i;
                break;
            }
                
        }

        return current;
        //var test = enumerable.Select((x, i) => value.GetHashCode() == x.GetHashCode() ? i : null);
    }

    public static IEnumerable<T> Replace2<T>(this IEnumerable<T> enumerable, int index, T value)
    {
        return enumerable.Select((x, i) => index == i ? value : x);
    }

    public static IEnumerable<T> ReplaceModel<T>(this IEnumerable<T> enumerable, T value)
    {
        var index = enumerable.FindIndexByHash(value);
  
        return enumerable.Select((x, i) => index == i ? value : x);
    }

    public static IEnumerable<T> RemoveAt<T>(this IEnumerable<T> enumerable, int index)
    {
        int current = 0;
        foreach (var item in enumerable)
        {
            if (current != index)
                yield return item;

            current++;
        }
    }

    public static IEnumerable<T> RemoveAt2<T>(this IEnumerable<T> enumerable, int index)
    {
        return enumerable.Where((x, i) => index != i);
    }

    public static IEnumerable<T> Remove<T>(this IEnumerable<T> enumerable, T data)
    {
        return enumerable.Where((x, i) => !x.Equals(data));
    }
}
