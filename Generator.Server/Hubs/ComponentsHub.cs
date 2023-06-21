using Generator.Shared.Hubs;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Models.ComponentModels.Abstracts;
using MagicOnion.Server.Hubs;
using MessagePipe;

namespace Generator.Server.Hubs;

public class ComponentsHub : StreamingHubBase<IComponentsHub, IComponentsReceiver>, IComponentsHub
{
    IGroup room;


    List<COMPONENTS_BASE> collection;


    public ISubscriber<PERMISSIONS> PermissionSubscriber { get; set; }

    public ComponentsHub(IServiceProvider provider)
    {
        collection = new List<COMPONENTS_BASE>();

        PermissionSubscriber = provider.GetService<ISubscriber<PERMISSIONS>>();

        PermissionSubscriber.Subscribe(async (PERMISSIONS obj) =>
        {
            await Subscribe();
        });
    }


    public async Task Subscribe()
    {
        room = await Group.AddAsync(nameof(COMPONENTS_BASE));

        Broadcast(room).OnSubscribe(collection);
    }
}

