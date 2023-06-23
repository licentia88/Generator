using Generator.Shared.Models.ComponentModels.Abstracts;
using MagicOnion;

namespace Generator.Shared.Hubs;

public interface IComponentsHub : IHubBase<IComponentsHub, IComponentsReceiver,COMPONENTS_BASE>
{
}

public interface IComponentsReceiver:IHubReceiverBase<COMPONENTS_BASE>
{
}
