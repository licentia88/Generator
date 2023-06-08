using Generator.Shared.Services.Base;
using MagicOnion;

namespace Generator.Shared.Services;

public interface IAuthService : IGenericService<IAuthService, byte[]>
{
    public UnaryResult<byte[]> Request(int id, string password);
}