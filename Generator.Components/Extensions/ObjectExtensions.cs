﻿namespace Generator.Components.Extensions;

internal static class ObjectExtensions
{
    public static T CastTo<T>(this object o) => (T)o;
}