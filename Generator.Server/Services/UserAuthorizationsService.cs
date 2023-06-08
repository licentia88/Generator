using Generator.Server.Services.Base;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;

namespace Generator.Server.Services;

public class UserAuthorizationsService : MagicBase<IUserAuthorizationsService, USER_AUTHORIZATIONS>, IUserAuthorizationsService
{
    public UserAuthorizationsService(IServiceProvider provider) : base(provider)
    {
    }
}
