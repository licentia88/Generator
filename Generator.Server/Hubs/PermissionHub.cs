using Generator.Server.Helpers;
using Generator.Shared.Enums;
using Generator.Shared.Hubs;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Models.ServiceModels;
using MessagePipe;
using Microsoft.EntityFrameworkCore;

namespace Generator.Server.Hubs;

public class PermissionHub : MagicHubBase<IPermissionsHub, IPermissionReceiver, PERMISSIONS>, IPermissionsHub
{
    public PermissionHub(IServiceProvider provider) : base(provider)
    {
        Subscriber.Subscribe(Operation.Create, (PERMISSIONS obj) =>
        {
            Collection.Add(obj);
            Broadcast(Room).OnCreate(obj);
        });

        Subscriber.Subscribe(Operation.Update, (PERMISSIONS obj) =>
        {
            var result = Collection.Find(x => x.AUTH_ROWID == obj.AUTH_ROWID);

            var index = Collection.IndexOf(result);

            Collection[index] = obj;

            Broadcast(Room).OnCollectionChanged(Collection);
        });

        Subscriber.Subscribe(Operation.Delete, (PERMISSIONS obj) =>
        {
            var result = Collection.Find(x => x.AUTH_ROWID == obj.AUTH_ROWID);

            Collection.Remove(result);

            Broadcast(Room).OnCollectionChanged(Collection);
        });
    }

    public override async Task<RESPONSE_RESULT<List<PERMISSIONS>>> ReadAsync()
    {
        return await TaskHandler.ExecuteAsync(async () =>
        {
            Collection = await Db.PERMISSIONS.Include(x => x.COMPONENTS_BASE).AsNoTracking().ToListAsync();

            Broadcast(Room).OnRead(Collection);

            return Collection;
        });
        
    }
}
 

