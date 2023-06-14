using Generator.Server.Services.Base;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;

namespace Generator.Server.Services;

// ReSharper disable once UnusedType.Global
public class GridCrudViewService : MagicBase<IGRidCrudViewService, CRUD_VIEW>, IGRidCrudViewService
{
    public GridCrudViewService(IServiceProvider provider) : base(provider)
    {
    }

}
