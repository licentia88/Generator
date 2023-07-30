using Generator.Client.Services.Base;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;
using MagicOnion;

namespace Generator.Client.Services;

[RegisterSingleton]
public class RolesService : ServiceBase<IRolesService, ROLES>, IRolesService
{
    public UnaryResult<bool> RoleExist(int RoleId)
    {
        return Client.RoleExist(RoleId);
    }
}
