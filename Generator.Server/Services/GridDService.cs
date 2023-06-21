using Generator.Server.FIlters;
using Generator.Server.Helpers;
using Generator.Server.Services.Base;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;
using MagicOnion;
using Microsoft.EntityFrameworkCore;

namespace Generator.Server.Services;

public class GridDService : MagicBase<IGridDService, GRID_D>, IGridDService
{
    public GridDService(IServiceProvider provider) : base(provider)
    {

    }

    [Allow]
    public override UnaryResult<GRID_D> Create(GRID_D model)
    {
        model.PERMISSIONS.Add(new PERMISSIONS
        {
            AUTH_NAME = model.CB_TITLE,
            PER_DESCRIPTION = $"Read Permissions"

        });

        model.CB_IDENTIFIER = RandomStringGenerator.GenerateRandomString();

        return base.Create(model);
    }

    [Allow]
    public override async UnaryResult<List<GRID_D>> ReadAll()
    {

        return await TaskHandler.ExecuteAsyncWithoutResponse(async () =>
        {
            return await Db.Set<GRID_D>().Include(x => x.PERMISSIONS).AsNoTracking().ToListAsync();
        });

        //return base.ReadAll();
    }



}

