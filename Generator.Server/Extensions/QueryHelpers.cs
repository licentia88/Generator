using System.Data;
using System.Data.Common;
using Generator.Shared.Extensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Generator.Server.Extensions;

public static class QueryHelpers
{
    /// <summary>
    /// Gets Column Values
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    internal static object GetColumnValue(this DbDataReader reader, string columnName)
    {
        return reader.IsDBNull(columnName) ? default! : reader[columnName];
    }

    /// <summary>
    /// Reads data from Reader
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="columns"></param>
    /// <returns></returns>
    internal static async ValueTask<List<IDictionary<string, object>>> ReadDataAsync(this DbDataReader reader, List<DbColumn> columns)
    {
        var expObj = ObjectExtensions.NewExpandObject();
        var expObjList = expObj.CreateDynamicList();

 
        while (  await reader.ReadAsync())
        {
            var newObj = expObj.CreateNew<Dictionary<string, object>>();

            foreach (var column in columns)
                newObj.Add(column.ColumnName, reader.GetColumnValue(column.ColumnName));

            expObjList.Add(newObj);
        }

        //await reader.DisposeAsync();
        return expObjList;
    }

    internal static void AddParameters(this DbCommand command, IDictionary<string, object> parameters)
    {
        var prms =  parameters?.Select(x =>
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = $"@{x.Key}";
            parameter.Value = x.Value;
            return parameter;
        }).ToArray();

        if (prms != null && prms.Count() > 0)
            command.Parameters.AddRange(prms);
    }

    public static async Task OpenIfAsync(this DbCommand command)
    {
        if (command.Connection.State != ConnectionState.Open)
            await command.Connection.OpenAsync();
    }

    public static async Task OpenIfAsync(this DbConnection Connection)
    {
        if (Connection.State != ConnectionState.Open)
            await Connection.OpenAsync();
    }

    public static void OpenIf(this DbCommand Command)
    {
        if (Command.Connection.State != ConnectionState.Open)
            Command.Connection.Open();
    }

    public static void OpenIf(this DbConnection Connection)
    {
        if (Connection.State != ConnectionState.Open)
              Connection.Open();
    }
    internal static void AddParameters(this DbCommand command, IDictionary<string, object> model, List<string> fields)
    {
        var prms = fields?.Select((x) =>
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = $"@{x}";
            parameter.Value = model[x];
            return parameter;
        }).ToArray();

        if (prms != null && prms.Count() > 0)
            command.Parameters.AddRange(prms);
    }

    internal static async Task<object> ExecuteInsert(this DbCommand command, string statement)
    {
        if (command.Connection.State != ConnectionState.Open)
            await command.Connection.OpenAsync();

        command.CommandText = $"{statement}";

        command.CommandText += "; SELECT SCOPE_IDENTITY() ";

 
        object lastID = command.ExecuteScalar();

        return lastID;
    }

    internal static async Task<object> ExecuteUpdate(this DbCommand command, string statement)
    {
        if (command.Connection.State != ConnectionState.Open)
            await command.Connection.OpenAsync();

        command.CommandText = $"{statement}";
  
        object lastID = command.ExecuteScalar();

        return lastID;
    }

    /// <summary>
    /// Determines wheter table has Identity column or not
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="TableName"></param>
    /// <returns></returns>
    internal static async Task<bool> IsAutoIncrementAsync(this DbConnection connection, string TableName)
    {
        //Count 1 ise autoincrement 0 ise auto increment degildir 
        var result = await connection.QueryScalar<bool>($"SELECT IS_IDENTITY FROM SYS.IDENTITY_COLUMNS JOIN SYS.TABLES ON SYS.IDENTITY_COLUMNS.OBJECT_ID = SYS.TABLES.OBJECT_ID WHERE SYS.TABLES.name = '{TableName}' AND IS_IDENTITY = 1 ");

        return result;
    }
}

