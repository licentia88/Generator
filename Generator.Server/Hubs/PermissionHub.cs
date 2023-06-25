using Generator.Server.FIlters;
using Generator.Server.Helpers;
using Generator.Server.Hubs.Base;
using Generator.Shared.Hubs;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Models.ServiceModels;
using Microsoft.EntityFrameworkCore;

namespace Generator.Server.Hubs;

 public class PermissionHub : MagicHubBase<IPermissionsHub, IPermissionReceiver, PERMISSIONS>, IPermissionsHub
{
    public PermissionHub(IServiceProvider provider) : base(provider)
    {

        //Subscriber.sub(PipeData, (PERMISSIONS obj) =>
        //{
        //    Console.WriteLine();
        //});
        //Subscriber.Subscribe(Operation.Create, model =>
        //{
        //    Collection.Add(model);
        //    Broadcast(Room).OnCreate(model);
        //});

        //Subscriber.Subscribe(Operation.Update, model =>
        //{
        //    var index = Collection.IndexOf(model);

        //    Collection[index] = model;

        //    Broadcast(Room).OnUpdate(model);
        //});

        //Subscriber.Subscribe(Operation.Delete, model =>
        //{
        //    Collection.Remove(model);

        //    Broadcast(Room).OnDelete(model);
        //});
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
