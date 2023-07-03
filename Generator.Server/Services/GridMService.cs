using System.Diagnostics;
using Generator.Server.Extensions;
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
    public IPublisher<string, (Operation,PERMISSIONS)> PermissionPublisher { get; set; }

    public GridMService(IServiceProvider provider) : base(provider)
    {
        PermissionPublisher = provider.GetService<IPublisher<string,(Operation,PERMISSIONS)>>();
    }

    //[Allow]
    public override async UnaryResult<GRID_M> Create(GRID_M model)
    {
        return await TaskHandler.ExecuteAsyncWithoutResponse(async () =>
        {
            var newPermission = new PERMISSIONS().SetPermissionInfo(model.CB_TITLE, Operation.Read);
            
            model.PERMISSIONS.Add(newPermission);

            await  Db.GRID_M.AddAsync(model);

            await  Db.SaveChangesAsync();

            return model;

        }).OnComplete(x =>
        {
            Debug.WriteLine($"Client: {Context.CallContext.Peer}");

            PermissionPublisher.Publish(Context.GetClientName(), (Operation.Create, x.PERMISSIONS.First()));
        });
 
    }
 
    public override async UnaryResult<GRID_M> Update(GRID_M model)
    {
        var existingPermission = await Db.PERMISSIONS.AsNoTracking().FirstAsync(x => x.PER_COMPONENT_REFNO == model.CB_ROWID);
        existingPermission.SetPermissionInfo(model.CB_TITLE, Operation.Read);

        return await TaskHandler.ExecuteAsyncWithoutResponse(async () =>
        {

            Db.PERMISSIONS.Update(existingPermission);
            Db.GRID_M.Update(model);
            await Db.SaveChangesAsync();

            return model;

        }).OnComplete((x,y) =>
        {
            PermissionPublisher.Publish(Context.GetClientName(), (Operation.Update, y));

            
        }, existingPermission);
         
    }
    public override  async UnaryResult<GRID_M> Delete(GRID_M model)
    {
        var existingPermission = await Db.PERMISSIONS.AsNoTracking().FirstAsync(x => x.PER_COMPONENT_REFNO == model.CB_ROWID);

        return await TaskHandler.ExecuteAsyncWithoutResponse(async () =>
        {
            Db.GRID_M.Remove(model);

            await Db.SaveChangesAsync();
 
            return model;
        }).OnComplete((x, existingPermission) =>
        {
            PermissionPublisher.Publish(Context.GetClientName(), (Operation.Delete, existingPermission));
        }, existingPermission);

    }
}

