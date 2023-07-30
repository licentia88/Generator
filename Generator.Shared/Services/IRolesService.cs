using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services.Base;
using MagicOnion;

namespace Generator.Shared.Services;

public interface IRolesService : IGenericService<IRolesService, ROLES>
{
    UnaryResult<bool> RoleExist(int RoleId);
}
