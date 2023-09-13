using Generator.Examples.Shared.Models;
using Generator.Examples.Shared.Services;
using Generator.Server.Database;
using Generator.Server.Services.Base;

namespace Generator.Server.Services;

public class UserService : MagicBase<IUserService, USER, TestContext>, IUserService
{
    public UserService(IServiceProvider provider) : base(provider)
    {
    }


}
