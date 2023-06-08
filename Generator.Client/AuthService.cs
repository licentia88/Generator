using Generator.Shared.Services;
using MagicOnion;

namespace Generator.Client;

public class AuthService : ServiceBase<IAuthService, byte[]>, IAuthService
{

    public UnaryResult<byte[]> Request(int id, string password)
    {
        return Client.Request(id, password);
    }
}
