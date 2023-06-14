using Generator.Server.Services.Base;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;
using MagicOnion;

namespace Generator.Server.Services;

public class RolesDetailsService : MagicBase<IRolesDetailsService, ROLES_DETAILS>, IRolesDetailsService
{
    public RolesDetailsService(IServiceProvider provider) : base(provider)
    {
    }

    public override async UnaryResult<List<ROLES_DETAILS>> FindByParent(string parentId, string foreignKey)
    {
        var result = await base.FindByParent(parentId, foreignKey);

        return result;
    }
}