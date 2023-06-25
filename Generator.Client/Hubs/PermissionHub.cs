using Generator.Client.Hubs.Base;
using Generator.Shared.Hubs;
using Generator.Shared.Models.ComponentModels;

namespace Generator.Client.Hubs;

public class PermissionHub : MagicHubBase<IPermissionsHub, IPermissionReceiver, PERMISSIONS>, IPermissionReceiver
{
    public PermissionHub(IServiceProvider provider) : base(provider)
    {
    }
}
