using Generator.Server.Helpers;
using Generator.Shared.Hubs;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Models.ServiceModels;
using Microsoft.EntityFrameworkCore;

namespace Generator.Server.Hubs;

public class GridMHub : MagicHubBase<IGridMHub, IGridMReceiver, GRID_M>, IGridMHub
{
    public GridMHub(IServiceProvider provider) : base(provider)
    {
    }

    public override async Task<RESPONSE_RESULT<List<GRID_M>>> ReadAsync()
    {
        return  await TaskHandler.ExecuteAsync(async () =>
        {
            Collection = await Db.GRID_M.Include(x => x.PERMISSIONS).AsNoTracking().ToListAsync();

            Broadcast(Room).OnRead(Collection);

            return Collection;
        });
    }
}

