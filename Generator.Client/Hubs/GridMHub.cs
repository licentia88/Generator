using Generator.Client.Hubs.Base;
using Generator.Shared.Hubs;
using Generator.Shared.Models.ComponentModels;

namespace Generator.Client.Hubs;

public class GridMHub : MagicHubClientBase<IGridMHub, IGridMReceiver, GRID_M>, IGridMReceiver
{
    public GridMHub(IServiceProvider provider) : base(provider)
    {
    }

 
    public override async Task ConnectAsync()
    {
        await base.ConnectAsync();

        //await Client.ReadAsync();
    }
}


