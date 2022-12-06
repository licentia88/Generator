using System;
namespace Generator.Components.Args;

public class GenGridArgs//<TModel> where TModel : new()
{
    //public MudXPage<TModel> Page { get; set; }

    public IDictionary<string,object> OldData { get; set; }

    public IDictionary<string, object> NewData { get; set; }

    public int Index { get; set; }

    public bool FromChild { get; set; }

   
}

