using System.Security.Cryptography;
using Generator.Server.FIlters;
using Generator.Server.Helpers;
using Generator.Server.Migrations;
using Generator.Server.Services.Base;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Models.ComponentModels.Abstracts;
using Generator.Shared.Services;
using MagicOnion;
using MagicOnion.Server;
using MemoryPack;
using Microsoft.EntityFrameworkCore;

namespace Generator.Server.Services;

[MAuthorize(1)]
public class GridMService : MagicBase<IGridMService, GRID_M>, IGridMService
{
    public GridMService(IServiceProvider provider) : base(provider)
    {
        
    }

    [Allow]
    public override UnaryResult<GRID_M> Create(GRID_M model)
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
    public override async UnaryResult<List<GRID_M>> ReadAll()
    {
 
        return await TaskHandler.ExecuteAsyncWithoutResponse(async () =>
        {
            return await Db.Set<GRID_M>().Include(x=> x.PERMISSIONS).ToListAsync();
        });

        //return base.ReadAll();
    }



}

