using MagicOnion.Client;
using Generator.Shared.Hubs;
using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using MagicOnion.Serialization.MemoryPack;
using Generator.Shared.Models.ComponentModels.Abstracts;

namespace Generator.Client;

public class ComponentsHub : IComponentsReceiver
{
    IComponentsHub Client;

    public List<COMPONENTS_BASE> Components { get; set; }

    public ComponentsHub(IServiceProvider provider)
    {
        Components = provider.GetService<List<COMPONENTS_BASE>>();
    }

    public void OnSubscribe(List<COMPONENTS_BASE> collection)
    {
        Components.AddRange(collection);
    }


    public async Task SubscribeAsync()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5002");

        Client = await StreamingHubClient.ConnectAsync<IComponentsHub, IComponentsReceiver>(channel, this, null, default, MemoryPackMagicOnionSerializerProvider.Instance);

        await Client.Subscribe();
    }

   
}

