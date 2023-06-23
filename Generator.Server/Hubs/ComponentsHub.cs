using Generator.Shared.Hubs;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Models.ComponentModels.Abstracts;
using MagicOnion.Server.Hubs;
using MessagePipe;

namespace Generator.Server.Hubs;

public class ComponentsHub : MagicHubBase<IComponentsHub, IComponentsReceiver, COMPONENTS_BASE>, IComponentsHub
{
    public ComponentsHub(IServiceProvider provider) : base(provider)
    {
    }
}