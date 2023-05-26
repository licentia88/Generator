using System.IO.Compression;
using Generator.Services.Seed;
using ProtoBuf.Grpc.Server;

namespace Generator.Services.Dependency;

public static class Registrations
{
    public static void RegisterGenerator(this IServiceCollection Services)
    {
        RegisterGrpc(Services);
        RegisterMisc(Services);
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
    
    }
}