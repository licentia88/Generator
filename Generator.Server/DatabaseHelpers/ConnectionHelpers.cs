using Microsoft.EntityFrameworkCore;

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
