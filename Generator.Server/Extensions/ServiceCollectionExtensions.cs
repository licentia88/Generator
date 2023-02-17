using System.Configuration;
using System.Data.Common;
using Generator.Server.DatabaseResolvers;
using Generator.Server.Helpers;
using Generator.Server.OptionsTemplates;
using Generator.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Generator.Server.Extensions;


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

    public static string GetConnection(this IConfiguration configuration, string Name)
    {
        return configuration.GetSection("ConnectionStrings").Get<List<Connections>>().FirstOrDefault(x => x.Name.Equals(Name)).ConnectionString;
    }



    private static void AddConnectionFactories(this IServiceCollection services, List<Connections> connections)
    {
        services.AddSingleton<IDictionary<string, Func<IDatabaseManager>>>(provider =>
        {
            var connectionFactories = new Dictionary<string, Func<IDatabaseManager>>();

            foreach (var ConnectionSetting in connections)
            {
                connectionFactories.Add(ConnectionSetting.Name, () => ConnectionHelper.AddDatabaseResolver(ConnectionSetting));
            }
            return connectionFactories;
        });
    }

}
