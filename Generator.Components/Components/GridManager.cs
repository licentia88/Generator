using Force.DeepCloner;
using Generator.Components.Enums;
//using Generator.Shared.Extensions;
using Mapster;
using static MudBlazor.CategoryTypes;

namespace Generator.Components.Components;

public class GridManager<TModel> where TModel :new()
{
    public GenGrid<TModel> Grid { get; }

    public GridManager(GenGrid<TModel> grid)
    {
        Grid = grid;
    }

   
}
