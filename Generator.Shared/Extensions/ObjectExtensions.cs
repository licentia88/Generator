namespace Generator.Shared.Extensions;

public static class ObjectExtensions
{
    public static T CastTo<T>(this object o) => (T)o;
}
