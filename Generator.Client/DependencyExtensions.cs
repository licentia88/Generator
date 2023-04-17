﻿using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Configuration;
using Grpc.Net.Client.Web;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.ClientFactory;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Generator.Shared.Services;

namespace Generator.Client;

public static class DependencyExtensions
{

    private static SslClientAuthenticationOptions? GetSslClientAuthenticationOptions(X509Certificate2 certificate2)
    {
        if (certificate2 is null) return default;

        return new SslClientAuthenticationOptions
        {
            RemoteCertificateValidationCallback = (sender, cert, chain, errors) =>
            {
                X509Chain x509Chain = new X509Chain();
                x509Chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
                bool isChainValid = x509Chain.Build(new X509Certificate2(cert));
                return isChainValid;
            },
            ClientCertificates = new X509Certificate2Collection { certificate2 }
        };
    }

    public static void RegisterGrpcService<TService>(this IServiceCollection Services, string EndPoint, string CertificatePath, Version Version) where TService : class
    {
        X509Certificate2? certificate = null;

        if(!string.IsNullOrEmpty(CertificatePath))
            certificate = X509Certificate2.CreateFromPemFile(CertificatePath, Path.ChangeExtension(CertificatePath, "key"));


        var socketsHandler = new SocketsHttpHandler
        {
            SslOptions = GetSslClientAuthenticationOptions(certificate),
            PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
            KeepAlivePingDelay = TimeSpan.FromSeconds(60),
            KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
            EnableMultipleHttp2Connections = true
        };

        var hand = new GrpcWebHandler(GrpcWebMode.GrpcWeb, socketsHandler);

        hand.HttpVersion = Version; 

        Services.AddCodeFirstGrpcClient<TService>(x =>
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

            x.Address = new Uri(EndPoint);

        })
       .ConfigurePrimaryHttpMessageHandler(x => hand);
    }

    // /Users/asimgunduz/server.crt
    public static void RegisterGrpcService<TService>(this IServiceCollection Services, string EndPoint) where TService : class
    {
        Services.RegisterGrpcService<TService>(EndPoint, "", HttpVersion.Version20);

    }

    public static async void LoadData<TService,TModel>(IServiceCollection Services) where TService : IGenericServiceBase<TModel> where TModel : new()
    {
        Services.AddSingleton<Lazy<List<TModel>>>();

        var provider = Services.BuildServiceProvider();

        var service = provider.GetService<TService>();

        var getSingletonList = provider.GetService<Lazy<List<TModel>>>();

        var result = await service.ReadAsync();

        getSingletonList.Value.AddRange(result.Data);
    }
    
}

 
