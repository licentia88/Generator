using System;
using System.Reflection;
using System.Threading.Tasks;
using Grpc.Core;
using MagicOnion.Client;
using ProtoBuf.Grpc;

namespace Generator.Client.Filters;

public class AppendHeaderFilter : IClientFilter
{
    public async ValueTask<ResponseContext> SendAsync(RequestContext context, Func<RequestContext, ValueTask<ResponseContext>> next)
    {
        // add the common header(like authentication).
        var header = context.CallOptions.Headers;
        header.Add("client", Assembly.GetEntryAssembly().GetName().Name);

         
        return await next(context);
    }
}
