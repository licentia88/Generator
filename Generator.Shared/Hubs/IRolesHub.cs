using Generator.Shared.Hubs.Base;
using Generator.Shared.Models.ComponentModels;

namespace Generator.Shared.Hubs;

public interface IRolesHub :IHubBase<IRolesHub, IRolesReceiver, ROLES>
{
}

public interface IRolesReceiver : IHubReceiverBase<ROLES>
{
}

