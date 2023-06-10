using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;
using MagicOnion;

namespace Generator.Client;

public class PermissionsService : ServiceBase<IPermissionsService, PERMISSIONS>, IPermissionsService
{
    public UnaryResult<List<PERMISSIONS>> FindByComponent(int ComponentPk)
    {
        return Client.FindByComponent(ComponentPk);
    }
}
