using Generator.Client.Services.Base;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;

namespace Generator.Client.Services;

[RegisterSingleton]
public class GridFieldsService : ServiceBase<IGridFieldsService, GRID_FIELDS>, IGridFieldsService
{

}