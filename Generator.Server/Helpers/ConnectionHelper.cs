using System.Data.Common;
using Generator.Server.OptionsTemplates;
using Microsoft.Data.SqlClient;
//using Microsoft.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;
using AQueryMaker.Interfaces;
using AQueryMaker.MSSql;
using AQueryMaker.Oracle;

namespace Generator.Server.Helpers;
public static class ConnectionHelper
{
    
    internal static List<Connections> GetConnectionStrings(IConfiguration configuration)
    {
        return configuration.GetSection("ConnectionStrings").Get<List<Connections>>();

    }

    public static IConfiguration GetConfiguration()
    {
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }

    private static DbConnection CreateConnection(Connections settings)
    {
        var factory = DbProviderFactories.GetFactory(settings.Provider);
        var connection = factory.CreateConnection();
        if (connection == null) return null;
        connection.ConnectionString = settings.ConnectionString;
        return connection;
    }


    internal static IDatabaseManager AddDatabaseResolver(Connections settings)
    {
        var connection = CreateConnection(settings);

        if (connection is OracleConnection)
        {
            return new OracleServerManager(connection);
        }

        if (connection is SqlConnection)
        {
            return new SqlServerManager(connection);
        }


        throw new InvalidOperationException("Unsupported database type");



    }


}

 
