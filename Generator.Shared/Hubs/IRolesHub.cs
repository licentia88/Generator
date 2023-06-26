using Generator.Shared.Hubs.Base;
using Generator.Shared.Models.ComponentModels;

namespace Generator.Shared.Hubs;

public interface IRolesHub :IMagicHub<IRolesHub, IRolesReceiver, ROLES>
{
}

public interface IRolesReceiver : IMagicReceiver<ROLES>
{
}

