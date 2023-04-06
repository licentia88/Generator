using System.Data.Common;
using System.Configuration;
using System.Data;
using Generator.Shared.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Generator.Server.DatabaseResolvers
{
    public interface IDatabaseManager
    {
        public Task<IDictionary<string, object>> InsertAsync(string TableName, IDictionary<string, object> Model, CommandBehavior CommandBehavior = CommandBehavior.Default);

        public Task<IDictionary<string, object>> UpdateAsync(string TableName, IDictionary<string, object> Model, CommandBehavior CommandBehavior = CommandBehavior.Default);

        public Task<IDictionary<string, object>> DeleteAsync(string TableName, IDictionary<string, object> Model, CommandBehavior CommandBehavior = CommandBehavior.Default);

        public Task<List<IDictionary<string, object>>> QueryAsync(string Query, params WhereStatement[] WhereStatementParameters);

        public Task<List<IDictionary<string, object>>> QueryAsync(string Query, CommandBehavior CommandBehavior, CommandType CommandType, params WhereStatement[] WhereStatementParameters);

        public Task<List<IDictionary<string, object>>> GetStoredProcedureFieldMetaDataAsync(string ProcedureName);

        public Task<List<IDictionary<string, object>>> GetMethodParameters(string MethodName);

    }



}
