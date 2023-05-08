using System.ComponentModel;
using System.Data;
using System.Data.Common;
using QueryMaker.Interfaces;

namespace QueryMaker.Oracle;

public class OracleServerManager : OracleQueryBuilder, IDatabaseManager
{

    public OracleServerManager(DbConnection dbConnection)
    {
        Connection = dbConnection;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public Task<IDictionary<string, object>> DeleteAsync(string TableName, IDictionary<string, object> Model, CommandBehavior CommandBehavior = CommandBehavior.Default)
    {
        throw new NotImplementedException();
    }

    public Task<List<IDictionary<string, object>>> GetMethodParameters(string MethodName)
    {
        throw new NotImplementedException();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public Task<List<IDictionary<string, object>>> GetStoredProcedureFieldMetaDataAsync(string ProcedureName)
    {
        throw new NotImplementedException();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public Task<IDictionary<string, object>> InsertAsync(string TableName, IDictionary<string, object> Model, CommandBehavior CommandBehavior = CommandBehavior.Default)
    {
        throw new NotImplementedException();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public Task<List<IDictionary<string, object>>> QueryAsync(string Query, params (string Key, object Value)[] WhereStatementParameters)
    {
        throw new NotImplementedException();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public Task<List<IDictionary<string, object>>> QueryAsync(string Query, CommandBehavior CommandBehavior, CommandType CommandType, params (string Key, object Value)[] WhereStatementParameters)
    {
        throw new NotImplementedException();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public Task<IDictionary<string, object>> UpdateAsync(string TableName, IDictionary<string, object> Model, CommandBehavior CommandBehavior = CommandBehavior.Default)
    {
        throw new NotImplementedException();
    }
}


