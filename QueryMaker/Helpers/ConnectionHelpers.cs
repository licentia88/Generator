﻿using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using QueryMaker.Interfaces;
using QueryMaker.MSSql;
using QueryMaker.Oracle;

namespace QueryMaker.Helpers;

public class ConnectionHelpers
{
    //public static IDatabaseManager GetConnection(DbContext context)
    //{
    //    if (context.Database.ProviderName.Contains("Microsoft", StringComparison.OrdinalIgnoreCase))
    //        return new SqlServerManager(context.Database.GetDbConnection());

    //    if (context.Database.ProviderName.Contains("Oracle", StringComparison.OrdinalIgnoreCase))
    //        return new OracleServerManager(context.Database.GetDbConnection());

    //    throw new NotSupportedException();
    //}

    //public static IDatabaseManager GetConnection(DbConnection dbConnection)
    //{
    //    if (dbConnection is SqlConnection)
    //        return new SqlServerManager(dbConnection);

    //    if (dbConnection is OracleConnection)
    //        return new OracleServerManager(dbConnection);

    //    throw new NotSupportedException();
    //}
}


