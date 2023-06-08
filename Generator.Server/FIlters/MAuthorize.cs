using Generator.Server.Jwt;
using Generator.Shared.Models.ComponentModels;
using Grpc.Core;
using MagicOnion;
using MagicOnion.Server;
using MagicOnion.Server.Filters;
using Microsoft.AspNetCore.Components;

namespace Generator.Server.FIlters;

public class MAuthorizeAttribute : Attribute, IMagicOnionFilterFactory<IMagicOnionServiceFilter>, IMagicOnionServiceFilter
{
    private int[] _Roles { get; }

    public IServiceProvider _serviceProvider { get; set; }

    [Inject]
    public List<PERMISSIONS> Permissions { get; set; }

    public MAuthorizeAttribute(params int[] Roles)
    {
        _Roles = Roles;
    }
    
    
    public IMagicOnionServiceFilter CreateInstance(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        Permissions = serviceProvider.GetService<List<PERMISSIONS>>();

            //serviceProvider.GetRequiredService<IRoleService>().GetRoles();

        return this;
    }

    public async ValueTask Invoke(ServiceContext context, Func<ServiceContext, ValueTask> next)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var fastJwtTokenService = scope.ServiceProvider.GetRequiredService<FastJwtTokenService>();

            var token = context.CallContext.RequestHeaders.FirstOrDefault(x => x.Key == "auth-token-bin");

            if (token is null)
                throw new ReturnStatusException(StatusCode.PermissionDenied, "Security Token not found");


            var tokenResult = fastJwtTokenService.DecodeToken(token.ValueBytes, _Roles, context);

             //AuthCheck("TEST");

            //var tokenResult = fastJwtTokenService.DecodeToken(token.ValueBytes, attribute.Roles, context);

        }

        await next(context);
    }

    private bool AuthCheck(int requiredPermission)
    {
        var result =  Permissions.FirstOrDefault(x => x.AUTH_ROWID == requiredPermission);

        return result is not null ? true: throw new ReturnStatusException(StatusCode.PermissionDenied, "Not Authorized");

    }
}