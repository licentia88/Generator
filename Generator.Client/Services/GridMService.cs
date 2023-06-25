using Generator.Client.Services.Base;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;

namespace Generator.Client.Services;

[RegisterSingleton]
public class GridMService : ServiceBase<IGridMService, GRID_M>, IGridMService
{
    public GridMService()
    {

    }
}



