using MagicOnion.Server;

namespace Generator.Server.FIlters;

public class AllowAttribute : MagicOnionFilterAttribute
{
    public override async ValueTask Invoke(ServiceContext context, Func<ServiceContext, ValueTask> next)
    {
        await next(context);
    }
}