using System.Data;
using System.Data.Common;

namespace QueryMaker.Extensions;

internal static class QueryExtensions
{
    public static (string Key, object Value)[] GetAsWhereStatement(this IDictionary<string, object> Model)
    {
        return Model.Select((KeyValuePair<string, object> arg) => (arg.Key, arg.Value)).ToArray();
    }


    internal static object GetColumnValue(this DbDataReader reader, string columnName)
    {
        return reader.IsDBNull(columnName) ? default! : reader[columnName];
    }



    public static async Task OpenAsync(this DbCommand command)
    {
        if (command.Connection.State != ConnectionState.Open)
            await command.Connection.OpenAsync();
    }

    public static async Task OpenAsync(this DbConnection Connection)
    {
        if (Connection.State != ConnectionState.Open)
            await Connection.OpenAsync();
    }

    public static void Open(this DbCommand Command)
    {
        if (Command.Connection.State != ConnectionState.Open)
            Command.Connection.Open();
    }

    public static void Open(this DbConnection Connection)
    {
        if (Connection.State != ConnectionState.Open)
            Connection.Open();
    }

    
 

    
}



