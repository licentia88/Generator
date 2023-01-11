using Generator.Components.Enums;
using Generator.Components.Interfaces;

namespace Generator.Components.Args;

#nullable enable
public class GenGridArgs<TModel> where TModel : new()
{
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    internal GenGridArgs(TModel? originalItem, TModel newItem, EditMode editMode, int index)
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        Initialize(originalItem, newItem, editMode, index);
    }

    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    internal GenGridArgs(TModel? originalItem, TModel newItem, EditMode editMode, int index, IGenView<TModel> view)
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        View = view;
        Initialize(originalItem, newItem, editMode, index);
    }

    private void Initialize(TModel? originalItem, TModel newItem, EditMode editMode, int index)
    {
        OriginalItem = originalItem;
        NewItem = newItem;
        EditMode = editMode;
        Index = index;
    }

    public TModel? OriginalItem { get; private set; }

    public TModel NewItem { get; private set; }

    public EditMode EditMode { get; set; }

    public int Index { get; set; }

    public IGenView<TModel> View { get; set; }
}

#nullable disable