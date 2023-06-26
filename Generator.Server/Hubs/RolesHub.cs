using Generator.Server.Hubs.Base;
using Generator.Shared.Hubs;
using Generator.Shared.Models.ComponentModels;

namespace Generator.Server.Hubs;

public class RolesHub : MagicHubServerBase<IRolesHub, IRolesReceiver, ROLES>, IRolesHub
{
    public RolesHub(IServiceProvider provider) : base(provider)
    {
    }
}