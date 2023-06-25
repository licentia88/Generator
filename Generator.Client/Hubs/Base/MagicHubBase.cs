using MagicOnion.Client;
using Grpc.Net.Client;
using MagicOnion.Serialization.MemoryPack;
using Microsoft.Extensions.DependencyInjection;
using MessagePipe;
using Generator.Shared.Enums;
using Generator.Shared.Hubs.Base;
using Generator.Shared.Models.ServiceModels;
using System.Reflection;
using Grpc.Core;

namespace Generator.Client.Hubs.Base;

public abstract class MagicHubBase<THub, TReceiver, TModel> : IHubReceiverBase<TModel>
    where THub : IHubBase<THub, TReceiver,TModel> 
    where TReceiver : class, IHubReceiverBase<TModel>
{
    protected THub Client;

    private IPublisher<Operation,TModel> ModelPublisher { get; set; }

    private IPublisher<Operation,List<TModel>> ListPublisher { get; set; }

    private List<TModel> Collection { get; set; }

    public MagicHubBase(IServiceProvider provider)
    {
        Collection = provider.GetService<List<TModel>>();
        ModelPublisher = provider.GetService<IPublisher<Operation,TModel>>();
        ListPublisher = provider.GetService<IPublisher<Operation,List<TModel>>>();
    }



    public virtual async Task ConnectAsync()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5002");

        Client = await StreamingHubClient.ConnectAsync<THub, TReceiver>(channel, this as TReceiver, null, SenderOption, MemoryPackMagicOnionSerializerProvider.Instance);

        await Client.ConnectAsync();
     }

    public virtual void OnCreate(TModel model)
    {
        var aass = Assembly.GetEntryAssembly();
        Collection.Add(model);

        ModelPublisher.Publish(Operation.Create, model);
    }

    public virtual void OnRead(List<TModel> collection)
    {
        var aass = Assembly.GetEntryAssembly();

        Collection.AddRange(collection);

        ListPublisher.Publish(Operation.Read,Collection);
    }

    public void OnStreamRead(List<TModel> collection)
    {
        var aass = Assembly.GetEntryAssembly();
                Collection.AddRange(collection);

        ListPublisher.Publish(Operation.Stream,collection);
    }


    public virtual void OnUpdate(TModel model)
    {
        var index = Collection.IndexOf(model);

        Collection[index] = model;

        ModelPublisher.Publish(Operation.Update, model);
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


    public async Task StreamReadAsync()
    {
         Collection.Clear();
         await Client.StreamReadAsync(1);
    }


    public async Task<RESPONSE_RESULT<TModel>> CreateAsync(TModel model)
    {
        return await Client.CreateAsync(model);
    }

    public Task<RESPONSE_RESULT<List<TModel>>> ReadAsync()
    {
        return Client.ReadAsync();
    }

    public Task<RESPONSE_RESULT<TModel>> UpdateAsync(TModel model)
    {
        return Client.UpdateAsync(model);
    }

    public Task<RESPONSE_RESULT<TModel>> DeleteAsync(TModel model)
    {
        return Client.DeleteAsync(model);
    }

    private CallOptions SenderOption => new CallOptions().WithHeaders(new Metadata
        {
             { "client", Assembly.GetEntryAssembly().GetName().Name}
         });

}


