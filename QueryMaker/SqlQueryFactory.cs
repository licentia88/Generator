using System.Data;
using System.Data.Common;
using QueryMaker.Interfaces;

namespace QueryMaker;

public class SqlQueryFactory : IDatabaseManager
{
    private readonly IDatabaseManager _manager;

    public DbConnection Connection => _manager.Connection;

    public SqlQueryFactory(IDatabaseManager manager)
    {
        _manager = manager;
    }

    public Task<IDictionary<string, object>> InsertAsync(string tableName, IDictionary<string, object> model, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return _manager.InsertAsync(tableName, model, commandBehavior);
    }

    public Task<IDictionary<string, object>> DeleteAsync(string tableName, IDictionary<string, object> model, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return _manager.DeleteAsync(tableName, model, commandBehavior);
    }

    public Task<IDictionary<string, object>> UpdateAsync(string tableName, IDictionary<string, object> model, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return _manager.UpdateAsync(tableName, model, commandBehavior);
    }

    public Task<List<IDictionary<string, object>>> QueryAsync(string query, params (string Key, object Value)[] whereStatementParameters)
    {
        return _manager.QueryAsync(query, whereStatementParameters);
    }

    public Task<List<IDictionary<string, object>>> QueryAsync(string query, CommandBehavior commandBehavior, CommandType commandType, params (string Key, object Value)[] whereStatementParameters)
    {
        return _manager.QueryAsync(query, commandBehavior, commandType, whereStatementParameters);
    }
    public Task<List<IDictionary<string, object>>> QueryAsync(string query, params KeyValuePair<string, object>[] whereStatementParameters)
    {
        return _manager.QueryAsync(query, whereStatementParameters);
    }

    public Task<List<IDictionary<string, object>>> QueryAsync(string query, CommandBehavior commandBehavior, CommandType commandType, params KeyValuePair<string, object>[] whereStatementParameters)
    {
        return _manager.QueryAsync(query, commandBehavior, commandType, whereStatementParameters);
    }

    public Task<List<IDictionary<string, object>>> GetStoredProcedureFieldMetaDataAsync(string procedureName)
    {
        return _manager.GetStoredProcedureFieldMetaDataAsync(procedureName);
    }

    public Task<List<IDictionary<string, object>>> GetMethodParameters(string methodName)
    {
        return _manager.GetMethodParameters(methodName);
    }

    
}


