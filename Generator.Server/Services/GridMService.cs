using Generator.Server.Services.Base;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;

namespace Generator.Server.Services;

// ReSharper disable once UnusedType.Global
public class GridMService : MagicBase<IGridMService, GRID_M>, IGridMService
{
    public GridMService(IServiceProvider provider) : base(provider)
    {
    }
}

