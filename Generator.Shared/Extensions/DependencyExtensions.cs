using System;
using System.Net;
using Generator.Shared.Services;
using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Configuration;
using Grpc.Net.Client.Web;
using Grpc.Net.ClientFactory;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.ClientFactory;

namespace Generator.Shared.Extensions
{
	public static class DependencyExtensions
	{
        public static void RegisterGrpcService<TService>(this IServiceCollection Services) where TService : class
        {
            var httpHandler = new SocketsHttpHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
                KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
                EnableMultipleHttp2Connections = true
            };

            var hand = new GrpcWebHandler(GrpcWebMode.GrpcWeb, httpHandler);
            //hand.HttpVersion = HttpVersion.Version11;

            Services.AddCodeFirstGrpcClient<TService>(x =>
            {
                x.ChannelOptionsActions.Add(x => new GrpcChannelOptions
                {
                    HttpHandler = hand,

                    MaxReceiveMessageSize = null, //30000000
                    MaxSendMessageSize = null, //30000000
                    Credentials = ChannelCredentials.Insecure,
                    UnsafeUseInsecureChannelCallCredentials = true,
                    ServiceConfig = new ServiceConfig { LoadBalancingConfigs = { new RoundRobinConfig() } }
                });

                x.Address = new Uri("http://localhost:5010");

            })
           .ConfigurePrimaryHttpMessageHandler(x => hand);
        }

        
    }
}

