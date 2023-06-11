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
    public List<USER_AUTHORIZATIONS> UserAuthorizationsList { get; set; }

    public MAuthorizeAttribute(params int[] Roles)
    {
        _Roles = Roles;
    }
    
    
    public IMagicOnionServiceFilter CreateInstance(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        UserAuthorizationsList = serviceProvider.GetService<List<USER_AUTHORIZATIONS>>();

            //serviceProvider.GetRequiredService<IRoleService>().GetRoles();

        return this;
    }

    public async ValueTask Invoke(ServiceContext context, Func<ServiceContext, ValueTask> next)
    {
        var isAllowed = context.MethodInfo.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(AllowAttribute));

        if (isAllowed != null)
        {
            await next(context);
            return;
        }

        var tokenResult = ProcessToken(context);

        await next(context);
    }

    private  bool ProcessToken(ServiceContext context)
    {
        using var scope = _serviceProvider.CreateScope();
        var fastJwtTokenService = scope.ServiceProvider.GetRequiredService<FastJwtTokenService>();

        var tokenHeader = context.CallContext.RequestHeaders.FirstOrDefault(x => x.Key == "auth-token-bin");

        if (tokenHeader is null)
            throw new ReturnStatusException(StatusCode.PermissionDenied, "Security Token not found");


        return fastJwtTokenService.DecodeToken(tokenHeader.ValueBytes, _Roles);
    }

}

public class AllowAttribute : MagicOnionFilterAttribute
{
    public override async ValueTask Invoke(ServiceContext context, Func<ServiceContext, ValueTask> next)
    {
        await next(context);
    }
}