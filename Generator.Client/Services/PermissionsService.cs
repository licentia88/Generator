using Generator.Client.Services.Base;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;
using MagicOnion;

namespace Generator.Client.Services;

[RegisterSingleton]
public class PermissionsService : ServiceBase<IPermissionsService, PERMISSIONS>, IPermissionsService
{
    public UnaryResult<List<PERMISSIONS>> FindByComponent(int ComponentPk)
    {
        return Client.FindByComponent(ComponentPk);
    }
}
