using System.Data.Common;
using Generator.Server.Helpers;
using Generator.Server.OptionsTemplates;
using AQueryMaker;
using Generator.Server.Seed;
using MagicOnion.Server.Hubs;
using Grpc.Core;
using MagicOnion.Server;

namespace Generator.Server.Extensions;

public static class HubExtensions {

    public static string GetClientName(this ServiceContext context) {
        return context.CallContext.RequestHeaders.FirstOrDefault(x => x.Key == "client").Value;
    }
}


 
public static class ServiceCollectionExtensions
{
    public static  IServiceCollection RegisterGenServer(this IServiceCollection services)
    {
        DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", Microsoft.Data.SqlClient.SqlClientFactory.Instance);
        DbProviderFactories.RegisterFactory("Oracle.ManagedDataAccess.Client", Oracle.ManagedDataAccess.Client.OracleClientFactory.Instance);
 
        var configuration = ConnectionHelper.GetConfiguration();


        var connections =  ConnectionHelper.GetConnectionStrings(configuration);


        services.AddConnectionFactories(connections);

        return services;
    }

    public static IServiceCollection RegisterModels(this IServiceCollection services)
    {
        services.AddScoped<SeedData>();
        services.AddScoped(typeof(List<>));

        return services;
    }

    public static string GetConnection(this IConfiguration configuration, string Name)
    {
        return configuration.GetSection("ConnectionStrings").Get<List<Connections>>().FirstOrDefault(x => x.Name.Equals(Name))?.ConnectionString;
    }

 

    private static void AddConnectionFactories(this IServiceCollection services, List<Connections> connections)
    {
        services.AddSingleton<IDictionary<string, Func<SqlQueryFactory>>>(provider =>
        {
            var connectionFactories = new Dictionary<string, Func<SqlQueryFactory>>();

            foreach (var ConnectionSetting in connections)
            {
                connectionFactories.Add(ConnectionSetting.Name, () => new SqlQueryFactory(ConnectionHelper.AddDatabaseResolver(ConnectionSetting)));
            }
            return connectionFactories;
        });


      
        //services.AddSingleton<IDictionary<string, Func<SqlQueryFactory>>>(_ =>
        //{
        //    return connections.ToDictionary<Connections, string, Func<SqlQueryFactory>>(connectionSetting => connectionSetting.Name,
        //                                                                                ConnectionSetting => () => new SqlQueryFactory(ConnectionHelper.AddDatabaseResolver(ConnectionSetting)));
        //});
    }


    //public static void RegisterFastJwt()
    //{
    //    var key = HS256Algorithm.GenerateRandomRecommendedKey();

    //    var encoder = new JwtEncoder(new HS256Algorithm(key));
    //    var decoder = new JwtDecoder(encoder.SignAlgorithm);

    //    FastJwtTokenService.Encoder = encoder;
    //    FastJwtTokenService.Decoder = decoder;
    //}
}
