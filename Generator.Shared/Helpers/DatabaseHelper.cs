using System.Data.Common;

namespace Generator.Shared.Helpers;

public static class DatabaseHelper
{
    public static bool ValidateConnectionString(string connectionString)
    {
        try
        {
            var result = new DbConnectionStringBuilder { ConnectionString = connectionString };

            return true;
        }
        catch 
        {
            return false;
        }
    }
}

