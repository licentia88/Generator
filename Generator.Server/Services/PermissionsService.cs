using Generator.Server.Helpers;
using Generator.Server.Services.Base;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;
using MagicOnion;
using Microsoft.EntityFrameworkCore;

namespace Generator.Server.Services;
public class PermissionsService : MagicBase<IPermissionsService, PERMISSIONS>, IPermissionsService
{
    public PermissionsService(IServiceProvider provider) : base(provider)
    {
    }

    public async UnaryResult<List<PERMISSIONS>> FindByComponent(int ComponentPk)
    {
        return await Db.PERMISSIONS.Where(x => x.PER_COMPONENT_REFNO == ComponentPk).AsNoTracking().ToListAsync();
    }

    public override UnaryResult<List<PERMISSIONS>> ReadAll()
    {
        return TaskHandler.ExecuteAsyncWithoutResponse(async () =>
        {
            var result = await Db.Set<PERMISSIONS>().Include(x=> x.COMPONENTS_BASE).ToListAsync();

            return result;
        });
    }

}
