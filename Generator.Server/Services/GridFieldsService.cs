using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;

namespace Generator.Server.Services;

public class GridFieldsService : GenericServiceBase<GeneratorContext, GRID_FIELDS>, IGridFieldsService
{
    public GridFieldsService(IServiceProvider provider) : base(provider)
    {
    }
}
