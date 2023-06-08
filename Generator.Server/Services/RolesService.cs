using Generator.Server.Services.Base;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;

namespace Generator.Server.Services;

public class RolesService : MagicBase<IRolesService, ROLES>, IRolesService
{
    public RolesService(IServiceProvider provider) : base(provider)
    {
    }
}
