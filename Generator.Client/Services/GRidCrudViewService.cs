using Generator.Client.Services.Base;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;

namespace Generator.Client.Services;

[RegisterSingleton]
public class GRidCrudViewService : ServiceBase<IGRidCrudViewService, CRUD_VIEW>, IGRidCrudViewService
{

}
