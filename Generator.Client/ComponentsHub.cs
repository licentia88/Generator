using MagicOnion.Client;
using Generator.Shared.Hubs;
using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using MagicOnion.Serialization.MemoryPack;
using Generator.Shared.Models.ComponentModels.Abstracts;
using Generator.Client.Hubs.Base;

namespace Generator.Client;

public class ComponentsHub : MagicHubBase<IComponentsHub, IComponentsReceiver, COMPONENTS_BASE>, IComponentsReceiver
{
    public ComponentsHub(IServiceProvider provider) : base(provider)
    {
    }

     
}

