using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using Generator.Shared.Models;
using Oracle.ManagedDataAccess.Client;

namespace Generator.Server.DatabaseResolvers
{
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
        public Task<List<IDictionary<string, object>>> QueryAsync(string Query, params WhereStatement[] WhereStatementParameters)
        {
            throw new NotImplementedException();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Task<List<IDictionary<string, object>>> QueryAsync(string Query, CommandBehavior CommandBehavior, CommandType CommandType, params WhereStatement[] WhereStatementParameters)
        {
            throw new NotImplementedException();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Task<IDictionary<string, object>> UpdateAsync(string TableName, IDictionary<string, object> Model, CommandBehavior CommandBehavior = CommandBehavior.Default)
        {
            throw new NotImplementedException();
        }
    }


}
