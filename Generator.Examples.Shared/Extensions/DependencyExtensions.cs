using System;
using System.Net;
using System.Net.Security;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Configuration;
using Grpc.Net.Client.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProtoBuf.Grpc.ClientFactory;

namespace Generator.Examples.Shared.Extensions
{
    public static class DependencyExtensions
    {
        
        public static void RegisterGrpcService<TService>(this IServiceCollection Services) where TService : class
        {

            var certificate =
              X509Certificate2.CreateFromPemFile("/Users/asimgunduz/server.crt", Path.ChangeExtension("/Users/asimgunduz/server.crt", "key"));

            var socketsHandler = new SocketsHttpHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
                KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
                EnableMultipleHttp2Connections = true,
            };

            socketsHandler.SslOptions.RemoteCertificateValidationCallback = (message, cert, chain, errors) =>
            {
                // Perform custom validation here and return a boolean indicating whether the certificate is valid
                return certificate.Equals(cert);
            };

            var hand = new GrpcWebHandler(GrpcWebMode.GrpcWeb, socketsHandler);
           
            hand.HttpVersion = HttpVersion.Version11;

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

        public static void RegisterGrpcServiceWithSsl2<TService>(this IServiceCollection services, string address) where TService : class
        {

            var certificate =
              X509Certificate2.CreateFromPemFile("/Users/asimgunduz/server.crt", Path.ChangeExtension("/Users/asimgunduz/server.crt", "key"));

            var socketsHandler = new SocketsHttpHandler
            {
                SslOptions = new SslClientAuthenticationOptions
                {
                    RemoteCertificateValidationCallback = (sender, cert, chain, errors) =>
                    {
                        X509Chain x509Chain = new X509Chain();
                        x509Chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
                        bool isChainValid = x509Chain.Build(new X509Certificate2(cert));
                        return isChainValid;
                    },
                    ClientCertificates = new X509Certificate2Collection { certificate }
                },
                PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
                KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
                EnableMultipleHttp2Connections = true
            };


            var hand = new GrpcWebHandler(GrpcWebMode.GrpcWeb, socketsHandler);

            hand.HttpVersion = HttpVersion.Version11;

            services.AddCodeFirstGrpcClient<TService>(x =>
            {
                x.ChannelOptionsActions.Add(x => new GrpcChannelOptions
                {
                    HttpHandler = hand,

                    MaxReceiveMessageSize = null, 
                    MaxSendMessageSize = null, 
                    Credentials = ChannelCredentials.Insecure,
                    UnsafeUseInsecureChannelCallCredentials = true,
                    ServiceConfig = new ServiceConfig { LoadBalancingConfigs = { new RoundRobinConfig() } }
                });

                x.Address = new Uri(address);

            })
           .ConfigurePrimaryHttpMessageHandler(x => hand);

            
        }

    }
}

   

