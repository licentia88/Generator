using Generator.Shared.Models;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;

namespace Generator.Server.Services;

public class GridsDService : GenericServiceBase<GeneratorContext, GRIDS_D>, IGridsDService
{
    public GridsDService(IServiceProvider provider) : base(provider)
    {
    }
}
