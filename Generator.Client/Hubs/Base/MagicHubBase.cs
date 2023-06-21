using MagicOnion.Client;
using MagicOnion;
using Grpc.Net.Client;
using MagicOnion.Serialization.MemoryPack;
using Generator.Shared.Hubs;
using Microsoft.Extensions.DependencyInjection;

namespace Generator.Client.Hubs.Base;

public abstract class MagicHubBase<THub, TReceiver, TModel> : IHubReceiverBase<TModel>
    where THub : IHubBase<THub,TReceiver,TModel>//, IStreamingHub<THub, TReceiver>
    where TReceiver : class, IHubReceiverBase<TModel>
{
    protected THub Client;

    public List<TModel> Collection { get; set; }

    TReceiver Receiver;

    public IServiceProvider Provider { get; }

    public MagicHubBase(IServiceProvider provider)
    {
        Provider = provider;
        Receiver = this as TReceiver;
        Collection = provider.GetService<List<TModel>>();
    }

    public virtual async Task ConnectAsync()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5002");

        Client = await StreamingHubClient.ConnectAsync<THub, TReceiver>(channel, Receiver, null, default, MemoryPackMagicOnionSerializerProvider.Instance);

        await Client.ConnectAsync();
     }

    public virtual void OnCreate(TModel model)
    {
        Collection.Add(model);
    }

    public virtual void OnRead(List<TModel> collection)
    {
        Collection.AddRange(collection);
    }

    public virtual void OnUpdate(TModel model)
    {
        //Collection.
    }

    public virtual void OnDelete(TModel model)
    {
        Collection.Remove(model);
    }

    
}


