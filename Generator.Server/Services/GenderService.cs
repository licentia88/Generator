using Generator.Examples.Shared.Models;
using Generator.Examples.Shared.Services;
using Generator.Server.Database;
using Generator.Server.Services.Base;

namespace Generator.Server.Services;

public class GenderService : MagicBase<IGenderService, GENDER, TestContext>, IGenderService
{
    public GenderService(IServiceProvider provider) : base(provider)
    {
    }


}
