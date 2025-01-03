using Generator.Client.Hubs.Base;
using Generator.Shared.Hubs;
using Generator.Shared.Models.ComponentModels;

namespace Generator.Client.Hubs;

public class RolesHub : MagicHubClientBase<IRolesHub, IRolesReceiver, ROLES>, IRolesReceiver
{
    public RolesHub(IServiceProvider provider) : base(provider)
    {
    }
}