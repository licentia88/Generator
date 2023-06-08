using Generator.Server.Services.Base;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;

namespace Generator.Server.Services;

public class RolesDetailsService : MagicBase<IRolesDetailsService, ROLES_DETAILS>, IRolesDetailsService
{
    public RolesDetailsService(IServiceProvider provider) : base(provider)
    {
    }
}