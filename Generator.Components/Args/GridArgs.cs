//using Generator.Shared.Extensions;
//using Generator.Shared.Models;

namespace Generator.Components.Args;

public class GridArgs:EventArgs
{
    public object Model { get; set; }

    public int Index { get; set; }

    public GridArgs(object model, int index)
    {
        Model = model;
        Index = index;
    }
}

public class GridArgs<TModel> : GridArgs
{
    public GridArgs(object model, int index) : base(model, index)
    {
    }

    public new TModel Model { get; set; }

     
}

 

