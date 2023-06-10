using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services.Base;
using MagicOnion;

namespace Generator.Shared.Services;

public interface IPermissionsService : IGenericService<IPermissionsService, PERMISSIONS>
{
    UnaryResult<List<PERMISSIONS>> FindByComponent(int ComponentPk);

}
