using Generator.Server.Services.Base;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;

namespace Generator.Server.Services;

// ReSharper disable once UnusedType.Global
public class GridFieldsService : MagicBase<IGridFieldsService,GRID_FIELDS>, IGridFieldsService
{
    public GridFieldsService(IServiceProvider provider) : base(provider)
    {
    }

   
}
