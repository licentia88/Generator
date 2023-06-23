using Generator.Server.FIlters;
using Generator.Server.Helpers;
using Generator.Server.Services.Base;
using Generator.Shared.Enums;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;
using MagicOnion;
using MessagePipe;
using Microsoft.EntityFrameworkCore;

namespace Generator.Server.Services;

//[MAuthorize(1)]
public class GridMService : MagicBase<IGridMService, GRID_M>, IGridMService
{
    public IPublisher<Operation,PERMISSIONS> PermissionPublisher { get; set; }

    public GridMService(IServiceProvider provider) : base(provider)
    {
        PermissionPublisher = provider.GetService<IPublisher<Operation,PERMISSIONS>>();
    }

    [Allow]
    public override async UnaryResult<GRID_M> Create(GRID_M model)
    {
        var newPermission = new PERMISSIONS
        {
            AUTH_NAME = model.CB_TITLE,
            PER_DESCRIPTION = $"Read Permissions"

        };

        model.PERMISSIONS.Add(newPermission);

        model.CB_IDENTIFIER = RandomStringGenerator.GenerateRandomString();

        return await base.Create(model).OnComplete(() =>
        {
            PermissionPublisher.Publish(Operation.Create, newPermission);
        });


    }

    [Allow]
    public override async UnaryResult<List<GRID_M>> ReadAll()
    {

        return await TaskHandler.ExecuteAsyncWithoutResponse(async () =>
        {
            return await Db.Set<GRID_M>().Include(x => x.PERMISSIONS).AsNoTracking().ToListAsync();
        });

        //return base.ReadAll();
    }

    public override  async UnaryResult<GRID_M> Update(GRID_M model)
    {
        return await base.Update(model).OnComplete(() =>
        {
             foreach (var permission in model.PERMISSIONS)
            {
                PermissionPublisher.Publish(Operation.Update,permission);
            }
           
        });
    }
    public override  async UnaryResult<GRID_M> Delete(GRID_M model)
    {
        return await base.Delete(model).OnComplete(() =>
        {
            foreach (var permission in model.PERMISSIONS)
            {
                PermissionPublisher.Publish(Operation.Delete, permission);
            }
        });

        
    }


}

