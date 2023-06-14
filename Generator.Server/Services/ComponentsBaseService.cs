using Generator.Server.Services.Base;
using Generator.Shared.Models.ComponentModels.Abstracts;
using Generator.Shared.Services;

namespace Generator.Server.Services;

public class ComponentsBaseService : MagicBase<IComponentsBaseService, COMPONENTS_BASE>, IComponentsBaseService
{
    public ComponentsBaseService(IServiceProvider provider) : base(provider)
    {
    }

}
