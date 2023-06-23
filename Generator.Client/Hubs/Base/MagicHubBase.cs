using MagicOnion.Client;
using Grpc.Net.Client;
using MagicOnion.Serialization.MemoryPack;
using Generator.Shared.Hubs;
using Microsoft.Extensions.DependencyInjection;
using MessagePipe;
using Generator.Shared.Enums;

namespace Generator.Client.Hubs.Base;

public abstract class MagicHubBase<THub, TReceiver, TModel> : IHubReceiverBase<TModel>
    where THub : IHubBase<THub,TReceiver,TModel>//, IStreamingHub<THub, TReceiver>
    where TReceiver : class, IHubReceiverBase<TModel>
{
    protected THub Client;

    TReceiver Receiver;

    private IPublisher<Operation,TModel> ModelPublisher { get; set; }

    private IPublisher<Operation,List<TModel>> ListPublisher { get; set; }

    public List<TModel> Collection { get; set; }

    public MagicHubBase(IServiceProvider provider)
    {
        Receiver = this as TReceiver;
        Collection = provider.GetService<List<TModel>>();
        ModelPublisher = provider.GetService<IPublisher<Operation,TModel>>();
        ListPublisher = provider.GetService<IPublisher<Operation,List<TModel>>>();
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

        ModelPublisher.Publish(Operation.Create, model);
    }

    public virtual void OnRead(List<TModel> collection)
    {
        Collection.AddRange(collection);

        ListPublisher.Publish(Operation.Read,Collection);
    }

    public void OnStreamRead(List<TModel> collection)
    {
        Collection.AddRange(collection);

        ListPublisher.Publish(Operation.Stream,collection);
    }


    public virtual void OnUpdate(TModel model)
    {
        //Collection.
    }

    public virtual void OnDelete(TModel model)
    {
        Collection.Remove(model);

        ModelPublisher.Publish(Operation.Delete, model);
    }

    public void OnCollectionChanged(List<TModel> collection)
    {
        Collection.Clear();
        Collection.AddRange(collection);

        ListPublisher.Publish(Operation.Read, Collection);
        
    }

    public Task CreateAsync(TModel model)
    {
        return Client.CreateAsync(model);
    }

    public Task ReadAsync()
    {
        return Client.ReadAsync();
    }

    public Task UpdateAsync(TModel model)
    {
        return Client.UpdateAsync(model);
    }

    public Task DeleteAsync(TModel model)
    {
        return Client.DeleteAsync(model);
    }

  
}


