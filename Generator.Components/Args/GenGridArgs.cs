namespace Generator.Components.Args;

public class GenGridArgs//<TModel> where TModel : new()
{
    internal GenGridArgs(object oldData, object newData)
    {
        OldData = oldData;
        NewData = newData;
    }
    //public MudXPage<TModel> Page { get; set; }

    public object OldData { get; }

    public object NewData { get; }

    //public int Index { get; set; }

    //public bool FromChild { get; set; }

   
}

