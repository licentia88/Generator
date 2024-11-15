﻿using System.Data.Common;
using Generator.Services.OptionsTemplates;
using Microsoft.Data.SqlClient;
//using Microsoft.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;
using QueryMaker.Interfaces;
using QueryMaker.MSSql;
using QueryMaker.Oracle;

namespace Generator.Services.Helpers;

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

 
