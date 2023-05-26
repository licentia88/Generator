//using Generator.Shared.Extensions;

namespace Generator.Components.Components;

public class GridManager<TModel> where TModel :new()
{
    public GenGrid<TModel> Grid { get; }

    public GridManager(GenGrid<TModel> grid)
    {
        Grid = grid;
    }

   
}
