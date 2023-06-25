using Generator.Server.Jwt;
using Generator.Server.Services.Base;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;
using MagicOnion;

namespace Generator.Server.Services.Authentication;

// ReSharper disable once UnusedType.Global
public class AuthService : MagicBase<IAuthService, byte[]>, IAuthService
{
    public FastJwtTokenService TokenService { get; set; }

    public Lazy<List<PERMISSIONS>> Permissions { get; set; }
    public AuthService(IServiceProvider provider) : base(provider)
    {
        TokenService = provider.GetService<FastJwtTokenService>();

        Permissions = provider.GetService<Lazy<List<PERMISSIONS>>>();
    }


    public UnaryResult<byte[]> Request(int id, string password)
    {
        //Permissions.Value.Add(new PERMISSIONS { PER_ROWID = 1 });
        return new UnaryResult<byte[]>(TokenService.CreateToken(id, 1));
    }
}