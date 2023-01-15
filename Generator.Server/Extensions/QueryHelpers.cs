using System.Data;
using System.Data.Common;
using Generator.Server.Models.Shema;
using Generator.Shared.Enums;
using Generator.Shared.Extensions;

namespace Generator.Server.Extensions;

public static class QueryHelpers
{

    //internal static object GetColumnValue(this DbDataReader reader, string columnName)
    //{
    //    return reader.IsDBNull(columnName) ? default! : reader[columnName];
    //}

    //internal static async ValueTask<List<IDictionary<string, object>>> ReadDataAsync(this DbDataReader reader, List<Columns> columns)
    //{
    //    var expObj = ObjectExtensions.NewExpandObject();
    //    var expObjList = expObj.CreateDynamicList();

 
    //    while (  await reader.ReadAsync())
    //    {
    //        var newObj = expObj.CreateNew<Dictionary<string, object>>();

    //        foreach (var column in columns)
    //            newObj.Add(column.FieldName, reader.GetColumnValue(column.FieldName));

    //        expObjList.Add(newObj);
    //    }

    //    //await reader.DisposeAsync();
    //    return expObjList;
    //}

    //internal static async ValueTask<List<IDictionary<string, object>>> ReadDataAsync(this DbDataReader reader)
    //{
    //    var expObj = ObjectExtensions.NewExpandObject();
    //    var expObjList = expObj.CreateDynamicList();

    //    var columns = reader.GetColumnSchema().ToList();

    //    while (await reader.ReadAsync())
    //    {
    //        var newObj = expObj.CreateNew<Dictionary<string, object>>();

    //        foreach (var column in columns)
    //            newObj.Add(column.ColumnName, reader.GetColumnValue(column.ColumnName));

    //        expObjList.Add(newObj);
    //    }

    //    //await reader.DisposeAsync();
    //    return expObjList;
    //}

    //internal static void AddParameters(this DbCommand command, params (string Key, LogicalOperator LogicalOperator, object[] Value)[] parameters)
    //{
    //    var parameterArray = new List<DbParameter>();

    //    foreach (var parameter in parameters)
    //    {
    //        if (parameter.LogicalOperator == LogicalOperator.In)
    //        {
    //            foreach (var innerParam in parameter.Value)
    //            {
    //                var parm = command.CreateParameter();
    //                parm.ParameterName = $"@{innerParam}";
    //                parm.Value = innerParam;
    //                parameterArray.Add(parm);
    //            }
    //            continue;
    //        }

    //        var prm = command.CreateParameter();
    //        prm.ParameterName = $"@{parameter.Key}";
    //        prm.Value = parameter.Value.First();

    //        parameterArray.Add(prm);
    //        continue;

    //    }
         
         

    //    if (parameterArray != null && parameterArray.Count() > 0)
    //        command.Parameters.AddRange(parameterArray.ToArray());
    //}

    //public static async Task OpenIfAsync(this DbCommand command)
    //{
    //    if (command.Connection.State != ConnectionState.Open)
    //        await command.Connection.OpenAsync();
    //}

    //public static async Task OpenIfAsync(this DbConnection Connection)
    //{
    //    if (Connection.State != ConnectionState.Open)
    //        await Connection.OpenAsync();
    //}

    //public static void OpenIf(this DbCommand Command)
    //{
    //    if (Command.Connection.State != ConnectionState.Open)
    //        Command.Connection.Open();
    //}

    //public static void OpenIf(this DbConnection Connection)
    //{
    //    if (Connection.State != ConnectionState.Open)
    //          Connection.Open();
    //}

    //internal static void AddParameters(this DbCommand command, IDictionary<string, object> model, List<string> fields)
    //{
    //    var prms = fields?.Select((x) =>
    //    {
    //        var parameter = command.CreateParameter();
    //        parameter.ParameterName = $"@{x}";
    //        parameter.Value = model[x]?? DBNull.Value;
    //        return parameter;
    //    }).ToArray();

    //    if (prms != null && prms.Count() > 0)
    //        command.Parameters.AddRange(prms);
    //}

    //internal static async Task<object> ExecuteInsert(this DbCommand command, string statement)
    //{
    //    if (command.Connection.State != ConnectionState.Open)
    //        await command.Connection.OpenAsync();

    //    command.CommandText = $"{statement}";

    //    //command.CommandText += "; SELECT SCOPE_IDENTITY() ";


    //    object lastID = command.ExecuteScalar();

    //    return lastID;
    //}

    //internal static async Task<bool> IsAutoIncrementAsync(this DbConnection connection, string TableName, bool CloseConnection = false)
    //{
    //    var result = await connection.QueryScalar<bool>($"SELECT IS_IDENTITY FROM SYS.IDENTITY_COLUMNS JOIN SYS.TABLES ON SYS.IDENTITY_COLUMNS.OBJECT_ID = SYS.TABLES.OBJECT_ID WHERE SYS.TABLES.name = '{TableName}' AND IS_IDENTITY = 1 ", CloseConnection);

    //    return result;
    //}
}

