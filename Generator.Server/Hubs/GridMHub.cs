using Generator.Server.Helpers;
using Generator.Server.Hubs.Base;
using Generator.Shared.Enums;
using Generator.Shared.Hubs;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Models.ServiceModels;
using MessagePipe;

namespace Generator.Server.Hubs;

public class GridMHub : MagicHubServerBase<IGridMHub, IGridMReceiver, GRID_M>, IGridMHub
{
     public IPublisher<Operation,PERMISSIONS> PermissionPublisher { get; set; }

    public GridMHub(IServiceProvider provider) : base(provider)
    {
        PermissionPublisher = provider.GetService<IPublisher<Operation, PERMISSIONS>>();

    }


    public override async Task<RESPONSE_RESULT<GRID_M>> CreateAsync(GRID_M model)
    {
        return await TaskHandler.ExecuteAsync(async () =>
        {
            var newPermission = new PERMISSIONS().SetPermissionInfo(model.CB_TITLE, Operation.Read);

            model.PERMISSIONS.Add(newPermission);

            await Db.GRID_M.AddAsync(model);

            await Db.SaveChangesAsync();

            return model;

        }).OnComplete(x =>
        {
            PermissionPublisher.Publish(Operation.Create, x.Data.PERMISSIONS.First());
        });
    }
}

