using Generator.Components.Enums;

namespace Generator.Components.Args;

public class GenGridArgs//<TModel> where TModel : new()
{
    internal GenGridArgs(object oldData, object newData, EditMode editMode, int index)
    {
        OldData = oldData;
        NewData = newData;
        EditMode = editMode;
        Index = index;
    }
    //public MudXPage<TModel> Page { get; set; }

    public object OldData { get; }

    public object NewData { get; }

    public EditMode EditMode { get; set; }

    public int Index { get; set; }

    //public int Index { get; set; }

    //public bool FromChild { get; set; }


}

