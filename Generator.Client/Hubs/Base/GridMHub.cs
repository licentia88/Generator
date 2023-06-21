using Generator.Shared.Hubs;
using Generator.Shared.Models.ComponentModels;

namespace Generator.Client.Hubs.Base;

public class GridMHub : MagicHubBase<IGridMHub, IGridMReceiver, GRID_M>, IGridMReceiver
{
    public GridMHub(IServiceProvider provider) : base(provider)
    {
    }

 
    public override async Task ConnectAsync()
    {
        await base.ConnectAsync();

        await Client.ReadAsync();
    }
}


