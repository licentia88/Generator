using System.Data;
using System.Data.Common;
using Cysharp.Text;
using Generator.Server.Helpers;
using Generator.Server.Models.Shema;
using Generator.Shared.Enums;
using Mapster;

namespace Generator.Server.Extensions;

public static class QueryExtensions
{
     
    public static async ValueTask ExecuteNonQuery(this DbConnection connection, string query)
    {
        await using var command = connection.CreateCommand();

        try
        {
            command.CommandText = query;

            command.CommandType = CommandType.Text;

            if (command.Connection.State != ConnectionState.Open)
                await command.Connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            await connection.CloseAsync();
        }
        catch (Exception e)
        {
            await command.Connection.CloseAsync();
            Console.WriteLine(e.Message);
            throw new Exception(e.Message);
        }
    }

    public static async ValueTask<List<TReturnType>> QueryAsync<TReturnType>(this DbConnection connection, string query, params (string Key, LogicalOperator LogicalOperator, object[] Value)[] parameters)
    {
        var result = await QueryAsync(connection, query, parameters);

        return result.Adapt<List<TReturnType>>();
    }

    public static async ValueTask<List<IDictionary<string, object>>> QueryAsync(this DbConnection connection, string query, params (string Key, LogicalOperator LogicalOperator, object[] Value)[] parameters)
    {
        await using var command = connection.CreateCommand();

        try
        {
            command.CommandText = query;

            command.CommandType = CommandType.Text;

            if (command.Connection.State != ConnectionState.Open)
                await command.Connection.OpenAsync();


            command.AddParameters(parameters);


            await using var result = await command.ExecuteReaderAsync(CommandBehavior.Default);

            var columns = result.GetColumnSchema().ToList();

            var dataList = await result.ReadDataAsync();

            await connection.CloseAsync();

            return dataList;
        }
        catch (Exception e)
        {
            await command.Connection.CloseAsync();
            Console.WriteLine(e.Message);
            throw new Exception(e.Message);
        }
    }

    public static ValueTask<List<IDictionary<string, object>>> QueryAsync(this DbConnection connection, string TableName, IDictionary<string,object>  objectModel = null, params (string Key, LogicalOperator LogicalOperator, object[] Value)[] parameters)
    {
        var query = QueryGenerator.CreateParametricQuery(TableName, null, parameters);
        return QueryAsync(connection, query, parameters);

    }

    public static async IAsyncEnumerable<List<TReturnType>> QueryStreamAsync<TReturnType>(this DbConnection connection, string query, string OrderBy = default, int pageSize = 30, params (string Key, LogicalOperator LogicalOperator, object[] Value)[] parameters)
    {
        await foreach (var item in QueryStreamAsync(connection, query, OrderBy, pageSize,parameters))
        {
            yield return item.Adapt<List<TReturnType>>();
        }
     }

    public static async IAsyncEnumerable<List<IDictionary<string, object>>> QueryStreamAsync(this DbConnection connection, string query,string OrderBy = default, int pageSize = 30, params (string Key, LogicalOperator LogicalOperator, object[] Value)[] parameters)
    {
        await using var command = connection.CreateCommand();

        var pageIndex = 0;

        bool hasMore;

        do
        {
            await command.Connection.OpenIfAsync();



            OrderBy = string.IsNullOrEmpty(OrderBy) ? " ORDER BY 1 " : OrderBy;

            var newQuery = $" {query} {OrderBy} OFFSET {pageIndex * pageSize} ROWS  FETCH NEXT {pageSize} ROWS ONLY ";

            command.CommandText = newQuery;

            command.CommandType = CommandType.Text;

            command.AddParameters(parameters);

            await using var reader = await command.ExecuteReaderAsync(CommandBehavior.SequentialAccess);

            var columns = reader.GetColumnSchema().ToList();

            var dataList = await reader.ReadDataAsync();

            pageIndex++;

            hasMore = reader.HasRows;

            yield return dataList;
        }
        while (hasMore);

        await command.Connection.CloseAsync();
    }

    public static async ValueTask<T> QueryScalar<T>(this DbConnection connection, string query, bool CloseConnection = true) where T : unmanaged
    {
        await using var command = connection.CreateCommand();

        try
        {
            if (command.Connection.State != ConnectionState.Open)
                await command.Connection.OpenAsync();

            command.CommandText = query; 

            command.CommandType = CommandType.Text;

            var result = await command.ExecuteScalarAsync();

            if (CloseConnection)
                await connection.CloseAsync();

            return result is null ? default : (T)result;
        }
        catch (Exception e)
        {
            await command.Connection.CloseAsync();

            Console.WriteLine(e.Message);
            throw new Exception(e.Message);
        }
    }

    public static async ValueTask<IDictionary<string, object>> InsertAsync(this DbConnection connection, string TableName, IDictionary<string, object> model)
    {
        await using var command = connection.CreateCommand();

        try
        {
            if (command.Connection.State != ConnectionState.Open)
                await command.Connection.OpenAsync();

            var shema = new ShemaGenerator(connection, TableName).ShemaList.First();

            var isAutoIncrement = await connection.IsAutoIncrementAsync(TableName);

            var statement = QueryGenerator.InsertStatement(model, TableName, isAutoIncrement , shema.PrimaryKey.FieldName );

            command.CommandText = statement.query;

            command.AddParameters(model, statement.fields);

            await using var reader = await command.ExecuteReaderAsync(CommandBehavior.Default);

            var columns = reader.GetColumnSchema().Where(x => statement.fields.Any(y => y.Equals(x.ColumnName))).ToList();

            //columns.Add(new DbColumn { ColumnName = shema.PrimaryKey.FieldName });
            var dataList = await reader.ReadDataAsync(shema.ColumnList);

            var returnData = dataList.First();

            return returnData;
        }
        catch (Exception e)
        {
            await command.Connection.CloseAsync();
            Console.WriteLine(e.Message);
            throw new Exception(e.Message);
        }
    }

    public static async ValueTask<TReturnType> InsertAsync<TReturnType>(this DbConnection connection, IDictionary<string, object> model)
    {
        var result = await InsertAsync(connection, typeof(TReturnType).Name, model);

        return result.Adapt<TReturnType>();
    }

    public static  ValueTask<IDictionary<string, object>> InsertAsync(this DbConnection connection, Type modelType, IDictionary<string, object> model)
    {
        return InsertAsync(connection, modelType.Name,model);
    }

    public static async ValueTask<IDictionary<string, object>> UpdateAsync(this DbConnection connection, string TableName, IDictionary<string, object> model)
    {
        await using var command = connection.CreateCommand();

        try
        {
            if (command.Connection.State != ConnectionState.Open)
                await command.Connection.OpenAsync();

            var shema = new ShemaGenerator(connection, TableName).ShemaList.First();

            var statement = QueryGenerator.UpdateStatement(model, TableName, shema.PrimaryKey.FieldName);


            command.CommandText = statement.query;

            command.AddParameters(model, statement.fields);

            await using var reader = await command.ExecuteReaderAsync(CommandBehavior.Default);

            var columns = reader.GetColumnSchema().Where(x => statement.fields.Any(y => y.Equals(x.ColumnName))).ToList();

            var dataList = await reader.ReadDataAsync();

            var returnData = dataList.First();

            return returnData;

            //var primaryKeyValue = await command.ExecuteUpdate(statement.query);

            //if (primaryKeyValue is not DBNull)
            //    model[shema.PrimaryKey.FieldName] = primaryKeyValue;

            //return model;


        }
        catch (Exception e)
        {
            await command.Connection.CloseAsync();
            Console.WriteLine(e.Message);
            throw new Exception(e.Message);
        }
    }

    public static async ValueTask<TReturnType> UpdateAsync<TReturnType>(this DbConnection connection, IDictionary<string, object> model)
    {
        var result = await UpdateAsync(connection, typeof(TReturnType).Name, model);

        return result.Adapt<TReturnType>();
    }

    public static ValueTask<IDictionary<string, object>> UpdateAsync(this DbConnection connection, Type modelType, IDictionary<string, object> model)
    {
        return UpdateAsync(connection, modelType.Name, model);
    }

    public static async ValueTask<IDictionary<string, object>> DeleteAsync(this DbConnection connection, string TableName, IDictionary<string, object> model)
    {
        await using var command = connection.CreateCommand();

        try
        {
            if (command.Connection.State != ConnectionState.Open)
                await command.Connection.OpenAsync();

            var shema = new ShemaGenerator(connection, TableName).ShemaList.First();

            var statement = QueryGenerator.DeleteStatement(model, TableName, shema.PrimaryKey.FieldName);
            command.CommandText = statement;

            command.AddParameters(model, new List<string> { shema.PrimaryKey.FieldName});

            await command.ExecuteNonQueryAsync();

            return model;

        }
        catch (Exception e)
        {
            await command.Connection.CloseAsync();
            Console.WriteLine(e.Message);
            throw new Exception(e.Message);
        }
    }

    public static async ValueTask<TReturnType> DeleteAsync<TReturnType>(this DbConnection connection, string TableName, IDictionary<string, object> model)
    {
        await DeleteAsync(connection, TableName, model);

        return model.Adapt<TReturnType>();
    }

    public static ValueTask<IDictionary<string, object>> DeleteAsync(this DbConnection connection, Type modelType, IDictionary<string, object> model)
    {
        return UpdateAsync(connection, modelType.Name, model);
    }
}

