using Generator.Shared.Hubs;
using Generator.Shared.Models.ComponentModels;
using MagicOnion.Server.Hubs;

namespace Generator.Server.Hubs;

public class GridMHub : MagicHubBase<IGridMHub, IGridMReceiver, GRID_M>, IGridMHub
{
    public GridMHub(IServiceProvider provider) : base(provider)
    {
    }
}

