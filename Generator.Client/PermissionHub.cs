using System;
using MagicOnion.Client;
using System.Diagnostics;
using System.Numerics;
using Generator.Shared.Hubs;
using Generator.Shared.Models.ComponentModels;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using MagicOnion.Serialization.MemoryPack;

namespace Generator.Client;

public class PermissionHub : IPermissionReceiver
{
    public IPermissionsHub client;

    public List<PERMISSIONS> Permissions { get; set; }

    public PermissionHub(IServiceProvider provider)
    {
        Permissions = provider.GetService<List<PERMISSIONS>>();
    }

    public async Task SubscribeAsync()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5002");

        client = await StreamingHubClient.ConnectAsync<IPermissionsHub, IPermissionReceiver>(channel, this,null,default, MemoryPackMagicOnionSerializerProvider.Instance);

        await client.Subscribe();
    }
 

    public void OnCreate(PERMISSIONS model)
    {
        Permissions.Add(model);
    }

    public void OnRemove(PERMISSIONS model)
    {
        Permissions.Remove(model);
    }

    public void OnSubscribe(List<PERMISSIONS> collection)
    {
        Permissions.AddRange(collection);
    }

    public void OnUpdate(PERMISSIONS model)
    {
        throw new NotImplementedException();
    }

    public void OnCollectionChanged(List<PERMISSIONS> collection)
    {
        Permissions.Clear();
        Permissions.AddRange(collection);
    }
}

