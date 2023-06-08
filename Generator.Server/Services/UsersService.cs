using Generator.Server.Services.Base;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;

namespace Generator.Server.Services;

public class UsersService : MagicBase<IUsersService, USERS>, IUsersService
{
    public UsersService(IServiceProvider provider) : base(provider)
    {
    }
}
