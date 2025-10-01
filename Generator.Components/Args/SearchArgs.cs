using System.Globalization;
using Generator.Components.Components;
using Generator.Components.Extensions;
using Generator.Components.Interfaces;
//using Generator.Shared.Extensions;
//using Generator.Shared.Models;

namespace Generator.Components.Args;



public class SearchArgs:EventArgs
{
    public List<IGenControl> Components { get; set; }

    public SearchArgs()
    {

    }

    public SearchArgs(List<IGenControl> components)
    {
        Components = components;
    }

    public KeyValuePair<string, object>[] WhereStatements =>
                                     Components.Where(x => x.BindingField is not null && x is not GenSpacer)
                                     .Select(component => new KeyValuePair<string, object>(component.BindingField, component.GetValue())).ToArray();


    private Dictionary<string, object> WhereStatementDictionary => WhereStatements.ToDictionary(kv => kv.Key, kv => kv.Value);

 

    public T GetComponentValueAs<T>(string bindingField)
    {
        // yoksa default (nullable ise null)
        if (!WhereStatementDictionary.TryGetValue(bindingField, out var value) || value is null)
            return default!;

        // zaten istenen tipteyse
        if (value is T t)
            return t;

        // metne çevir
        var s = value.ToString();
        if (string.IsNullOrWhiteSpace(s))
            return default!;

        var targetType = typeof(T);
        var underlying = Nullable.GetUnderlyingType(targetType) ?? targetType;
        var isNullable = underlying != targetType;

        // desteklenen tipleri TryParse ile tek tek ele al
        object? parsed = null;

        if (underlying == typeof(string))
            parsed = s;
        else if (underlying == typeof(int)      && int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var i)) parsed = i;
        else if (underlying == typeof(long)     && long.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var l)) parsed = l;
        else if (underlying == typeof(short)    && short.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var sh)) parsed = sh;
        else if (underlying == typeof(byte)     && byte.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var by)) parsed = by;
        else if (underlying == typeof(decimal)  && decimal.TryParse(s, NumberStyles.Number,  CultureInfo.InvariantCulture, out var m)) parsed = m;
        else if (underlying == typeof(double)   && double.TryParse(s, NumberStyles.Float  | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var d)) parsed = d;
        else if (underlying == typeof(float)    && float.TryParse(s,  NumberStyles.Float  | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var f)) parsed = f;
        else if (underlying == typeof(bool)     && bool.TryParse(s, out var b)) parsed = b;
        else if (underlying == typeof(DateTime) && DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt)) parsed = dt;
        else if (underlying == typeof(Guid)     && Guid.TryParse(s, out var g)) parsed = g;
        else if (underlying.IsEnum && Enum.TryParse(underlying, s, ignoreCase: true, out var e)) parsed = e;

        if (parsed is null)
            return default!;

        // boxing kuralı: underlying değerini T (veya Nullable<T>)’ye güvenle dök
        return (T)parsed;
    }
}



 

