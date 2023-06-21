using Generator.Shared.Models.ComponentModels.Abstracts;
using MagicOnion;

namespace Generator.Shared.Hubs;

public interface IComponentsHub : IStreamingHub<IComponentsHub, IComponentsReceiver>
{
    Task Subscribe();
}

public interface IComponentsReceiver
{
    void OnSubscribe(List<COMPONENTS_BASE> collection);
}
