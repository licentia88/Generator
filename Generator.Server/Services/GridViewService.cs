using Generator.Server.Services.Base;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;

namespace Generator.Server.Services;

// ReSharper disable once UnusedType.Global
public class GridViewService : MagicBase<IGridViewService, GRID_VIEW>, IGridViewService
{
    public GridViewService(IServiceProvider provider) : base(provider)
    {
    }

}
