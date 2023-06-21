using System.Diagnostics;
using AQueryMaker;
using Generator.Server.Database;
using Generator.Server.Helpers;
using Generator.Server.Jwt;
using Generator.Shared.Hubs;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Models.ComponentModels.Abstracts;
using MagicOnion;
using MagicOnion.Server.Hubs;
using MessagePipe;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace Generator.Server.Hubs;

public class PermissionHub:StreamingHubBase<IPermissionsHub, IPermissionReceiver>, IPermissionsHub
{
    IGroup room;
    IInMemoryStorage<List<PERMISSIONS>> storage;

    List<PERMISSIONS> collection;

   

    public GeneratorContext Db { get; set; }

 


    public PermissionHub(IServiceProvider provider)
    {
        Db = provider.GetService<GeneratorContext>();

        //pubs = provider.GetService<IPublisher<PERMISSIONS>>();

        
    }


    public async Task Subscribe()
    {
         //pubs.Publish(new PERMISSIONS());

         collection = await Db.PERMISSIONS.AsNoTracking().ToListAsync();

        // Group can bundle many connections and it has inmemory-storage so add any type per group.
        (room, storage) = await Group.AddAsync(nameof(PERMISSIONS), collection);

        // Typed Server->Client broadcast.
        Broadcast(room).OnSubscribe(collection);
    }

    public Task OnCreate(PERMISSIONS model)
    {
        Broadcast(room).OnCreate(model);

        return Task.CompletedTask;
    }

    public Task OnRemove(PERMISSIONS model)
    {
        Broadcast(room).OnRemove(model);
        return Task.CompletedTask;
    }

    public Task OnUpdate(PERMISSIONS model)
    {
        Broadcast(room).OnUpdate(model);
        return Task.CompletedTask;
    }

    public async Task CollectionChanged()
    {
        collection = await Db.PERMISSIONS.AsNoTracking().ToListAsync();

        Broadcast(room).OnCollectionChanged(collection);
    }
 
}

