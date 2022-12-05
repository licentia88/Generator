using System.Collections.ObjectModel;
using System.IO.Compression;
using Generator.Server.Models.Shema;
using Generator.Server.Services;
using Generator.Services.Seed;
using Generator.Shared.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Server;

namespace Generator.Server.Dependency;

public static class Registrations
{
    public static void RegisterGenerator(this IServiceCollection Services)
    {
        RegisterGrpc(Services);
        RegisterMisc(Services);
    }

    public static void RegisterGrpcServices(this WebApplication app)
    {
        app.MapGrpcService<DatabaseService>().EnableGrpcWeb();
        app.MapGrpcService<FooterButtonService>().EnableGrpcWeb();
        app.MapGrpcService<HeaderButtonService>().EnableGrpcWeb();
        app.MapGrpcService<GridsMService>().EnableGrpcWeb();
        app.MapGrpcService<GridsDService>().EnableGrpcWeb();

        app.MapGrpcService<AllForOneService>().EnableGrpcWeb();

    }

    private static void RegisterGrpc(this IServiceCollection Services)
    {
        Services.AddCodeFirstGrpc(config =>
        {
            config.ResponseCompressionLevel = CompressionLevel.Optimal;
            config.EnableDetailedErrors = false;
            config.MaxSendMessageSize = null;  
            config.MaxReceiveMessageSize = null;  
        });
    }

    private static void RegisterMisc(this IServiceCollection Services)
    {
        Services.AddScoped<SeedData>();
        Services.AddSingleton<Lazy<ObservableCollection<COMPONENT>>>();
        //Services.AddSingleton(typeof(TableSchema<>));
         
        //builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
    }
}