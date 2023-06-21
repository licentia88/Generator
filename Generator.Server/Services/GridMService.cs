using Generator.Server.FIlters;
using Generator.Server.Helpers;
using Generator.Server.Hubs;
using Generator.Server.Services.Base;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;
using MagicOnion;
using MessagePipe;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Generator.Server.Services;

//[MAuthorize(1)]
public class GridMService : MagicBase<IGridMService, GRID_M>, IGridMService
{
    public IPublisher<PERMISSIONS> pubs { get; set; }

    public GridMService(IServiceProvider provider) : base(provider)
    {
        pubs = provider.GetService<IPublisher<PERMISSIONS>>();
    }

    [Allow]
    public override async UnaryResult<GRID_M> Create(GRID_M model)
    {
        var newPermission = new PERMISSIONS
        {
            AUTH_NAME = model.CB_TITLE,
            PER_DESCRIPTION = $"Read Permissions"

        };

        pubs.Publish(newPermission);
        model.PERMISSIONS.Add(newPermission);

        model.CB_IDENTIFIER = RandomStringGenerator.GenerateRandomString();

        return await base.Create(model);

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

    public override async UnaryResult<GRID_M> Delete(GRID_M model)
    {
        var result = await base.Delete(model);

        return result;
    }


}

