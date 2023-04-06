using System.Configuration;
using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace Generator.Server.DatabaseResolvers
{
    public class ConnectionHelpers
    {
        public static IDatabaseManager GetConnection(DbContext context)
        {
            //context.Database.GetDbConnection
            if (context.Database.ProviderName.Contains("Microsoft", StringComparison.OrdinalIgnoreCase))
                return new SqlServerManager(context.Database.GetDbConnection());

            if (context.Database.ProviderName.Contains("Oracle", StringComparison.OrdinalIgnoreCase))
                return new OracleServerManager(context.Database.GetDbConnection());

            throw new NotSupportedException();
        }

    }

    
}
