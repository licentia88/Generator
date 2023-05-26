using System.Data;
using System.Data.Common;
using QueryMaker.Extensions;
using QueryMaker.Interfaces;

namespace QueryMaker.MSSql;


public class SqlServerManager : SqlQueryBuilder, IDatabaseManager
{

    public SqlServerManager(DbConnection dbConnection)
    {
        Connection = dbConnection;
    }

    public async Task<IDictionary<string, object>> InsertAsync(string TableName, IDictionary<string, object> Model, CommandBehavior CommandBehavior = CommandBehavior.Default)
    {
        var isAutoInrementQuery = IsAutoIncrementStatement(TableName);

        var whereStatement = new (string, object)[] { (nameof(TableName), TableName) };


        var isAutoIncrementResult = await QueryAsync(isAutoInrementQuery, CommandBehavior, CommandType.Text, whereStatement);

        var primaryKeyName = isAutoIncrementResult.First()["PrimaryKeyName"].ToString();

        var isIdentity = isAutoIncrementResult.First()["IS_IDENTITY"].CastTo<bool>();

        if (isIdentity && primaryKeyName != null)
            Model.Remove(primaryKeyName);
        
        var insertStatement = CreateInsertStatement(TableName, Model, primaryKeyName, isIdentity);

        var insertResult = await QueryAsync(insertStatement, Model.GetAsWhereStatement());

        return insertResult.FirstOrDefault();
    }

    public async Task<IDictionary<string, object>> UpdateAsync(string TableName, IDictionary<string, object> Model, CommandBehavior CommandBehavior = CommandBehavior.Default)
    {
        var isAutoInrementQuery = IsAutoIncrementStatement(TableName);

        var whereStatement = new (string, object)[] { (nameof(TableName), TableName) };

        var isAutoIncrementResult = await QueryAsync(isAutoInrementQuery, CommandBehavior, CommandType.Text, whereStatement);

        var primaryKeyName = isAutoIncrementResult.First()["PrimaryKeyName"].ToString();

        //bool IsIdentity = (bool)(isAutoIncrementResult.First()["IS_IDENTITY"].CastTo<bool>());

        var updateStatement = CreateUpdateStatement(TableName, Model, primaryKeyName);

        var updateResult = await QueryAsync(updateStatement, Model.GetAsWhereStatement());

        return updateResult.FirstOrDefault();


    }


    public async Task<IDictionary<string, object>> DeleteAsync(string TableName, IDictionary<string, object> Model, CommandBehavior CommandBehavior = CommandBehavior.Default)
    {
        var isAutoInrementQuery = IsAutoIncrementStatement(TableName);

        var whereStatement = new (string, object)[] { (nameof(TableName), TableName) };

        var isAutoIncrementResult = await QueryAsync(isAutoInrementQuery, CommandBehavior, CommandType.Text, whereStatement);

        var primaryKeyName = isAutoIncrementResult.First()["PrimaryKeyName"].ToString();

        //bool IsIdentity = (bool)(isAutoIncrementResult.First()["IS_IDENTITY"].CastTo<bool>());

        var deleteStatement = CreateDeleteStatement(TableName, primaryKeyName);

        await QueryAsync(deleteStatement, Model.GetAsWhereStatement());

        return Model;
    }

    public Task<List<IDictionary<string, object>>> QueryAsync(string Query, params (string Key, object Value)[] WhereStatementParameters)
    {
        return QueryAsync(Query, CommandBehavior.Default, CommandType.Text, WhereStatementParameters);
    }

    public async Task<List<IDictionary<string, object>>> QueryAsync(string Query, CommandBehavior CommandBehavior, CommandType CommandType, params (string Key, object Value)[] WhereStatementParameters)
    {
        var command = Connection.CreateCommand();

        await command.OpenAsync();

        command.CommandText = Query;

        command.CommandType = CommandType;

        AddWhereStatementParameters(command, WhereStatementParameters);

        var result = await ExecuteCommandAsync(command, CommandBehavior);

        if (command.Connection != null) 
            await command.Connection.CloseAsync();

        return result;
    }

    public Task<List<IDictionary<string, object>>> QueryAsync(string Query, params KeyValuePair<string, object>[] WhereStatementParameters)
    {
        return QueryAsync(Query, CommandBehavior.Default, CommandType.Text, WhereStatementParameters);

    }

    public async Task<List<IDictionary<string, object>>> QueryAsync(string Query, CommandBehavior CommandBehavior, CommandType CommandType, params KeyValuePair<string, object>[] WhereStatementParameters)
    {
        var command = Connection.CreateCommand();

        await command.OpenAsync();

        command.CommandText = Query;

        command.CommandType = CommandType;

        AddWhereStatementParameters(command, WhereStatementParameters);

        var result = await ExecuteCommandAsync(command, CommandBehavior);

        if (command.Connection != null) 
            await command.Connection.CloseAsync();

        return result;
    }

    public async Task<List<IDictionary<string, object>>> GetStoredProcedureFieldMetaDataAsync(string ProcedureName)
    {
        var command = Connection.CreateCommand();

        await command.OpenAsync();

        var metadataQuery = CreateStoredProcedureFieldMetaDataStatement(ProcedureName);
        command.CommandText = metadataQuery;

        command.CommandType = CommandType.Text;
        
        var result = await ExecuteCommandAsync(command, CommandBehavior.Default);

        if (command.Connection != null) 
            await command.Connection.CloseAsync();

        return result;
    }

    public async Task<List<IDictionary<string, object>>> GetMethodParameters(string MethodName)
    {
        var command = Connection.CreateCommand();

        await command.OpenAsync();

        var metadataQuery = CreateMethodPropertyMetaDataStatement(MethodName);

        command.CommandText = metadataQuery;

        command.CommandType = CommandType.Text;

        AddWhereStatementParameters(command, (nameof(MethodName), MethodName));

        var result = await ExecuteCommandAsync(command, CommandBehavior.Default);

        if (command.Connection != null)
            await command.Connection.CloseAsync();

        return result;
    }

   
}


