using Generator.Client.Hubs.Base;
using Generator.Shared.Hubs;
using Generator.Shared.Models.ComponentModels;

namespace Generator.Client.Hubs;

public class PermissionHub : MagicHubClientBase<IPermissionsHub, IPermissionReceiver, PERMISSIONS>, IPermissionReceiver
{
    public PermissionHub(IServiceProvider provider) : base(provider)
    {
    }
}
