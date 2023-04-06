using System.Data;
using Generator.Shared.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Generator.Server.DatabaseResolvers
{
    public class SqlQueryFactory : IDatabaseManager
    {
        IDatabaseManager Manager;

        public SqlQueryFactory(IDatabaseManager manager)
        {
            Manager = manager;
        }

        public Task<IDictionary<string, object>> InsertAsync(string TableName, IDictionary<string, object> Model, CommandBehavior CommandBehavior = CommandBehavior.Default)
        {
            return Manager.InsertAsync(TableName, Model, CommandBehavior);
        }

        public Task<IDictionary<string, object>> DeleteAsync(string TableName, IDictionary<string, object> Model, CommandBehavior CommandBehavior = CommandBehavior.Default)
        {
            return Manager.DeleteAsync(TableName, Model, CommandBehavior);
        }

        public Task<IDictionary<string, object>> UpdateAsync(string TableName, IDictionary<string, object> Model, CommandBehavior CommandBehavior = CommandBehavior.Default)
        {
            return Manager.UpdateAsync(TableName, Model, CommandBehavior);
        }

        public Task<List<IDictionary<string, object>>> QueryAsync(string Query, params WhereStatement[] WhereStatementParameters)
        {
            return Manager.QueryAsync(Query,WhereStatementParameters);
        }

        public Task<List<IDictionary<string, object>>> QueryAsync(string Query, CommandBehavior CommandBehavior, CommandType CommandType, params WhereStatement[] WhereStatementParameters)
        {
            return Manager.QueryAsync(Query,CommandBehavior,CommandType, WhereStatementParameters);
        }

        public Task<List<IDictionary<string, object>>> GetStoredProcedureFieldMetaDataAsync(string ProcedureName)
        {
            return Manager.GetStoredProcedureFieldMetaDataAsync(ProcedureName);
        }

        public Task<List<IDictionary<string, object>>> GetMethodParameters(string MethodName)
        {
            return Manager.GetMethodParameters(MethodName);
        }
    }


}
