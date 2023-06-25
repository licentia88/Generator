using Generator.Client.Services.Base;
using Generator.Shared.Services;
using MagicOnion;

namespace Generator.Client.Services;

[RegisterSingleton]
public class AuthService : ServiceBase<IAuthService, byte[]>, IAuthService
{

    public UnaryResult<byte[]> Request(int id, string password)
    {
        return Client.Request(id, password);
    }
}
