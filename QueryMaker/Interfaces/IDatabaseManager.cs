using System.Data;
using System.Data.Common;

namespace QueryMaker.Interfaces;

public interface IDatabaseManager
{
    public DbConnection Connection { get;  }
    public Task<IDictionary<string, object>> InsertAsync(string TableName, IDictionary<string, object> Model, CommandBehavior CommandBehavior = CommandBehavior.Default);

    public Task<IDictionary<string, object>> UpdateAsync(string TableName, IDictionary<string, object> Model, CommandBehavior CommandBehavior = CommandBehavior.Default);

    public Task<IDictionary<string, object>> DeleteAsync(string TableName, IDictionary<string, object> Model, CommandBehavior CommandBehavior = CommandBehavior.Default);

    public Task<List<IDictionary<string, object>>> QueryAsync(string Query, params (string Key, object Value)[] WhereStatementParameters);

    public Task<List<IDictionary<string, object>>> QueryAsync(string Query, CommandBehavior CommandBehavior, CommandType CommandType, params (string Key, object Value)[] WhereStatementParameters);

    public Task<List<IDictionary<string, object>>> QueryAsync(string Query, params KeyValuePair<string, object>[] WhereStatementParameters);

    public Task<List<IDictionary<string, object>>> QueryAsync(string Query, CommandBehavior CommandBehavior, CommandType CommandType, params KeyValuePair<string,object>[] WhereStatementParameters);

    public Task<List<IDictionary<string, object>>> GetStoredProcedureFieldMetaDataAsync(string ProcedureName);

    public Task<List<IDictionary<string, object>>> GetMethodParameters(string MethodName);
}


