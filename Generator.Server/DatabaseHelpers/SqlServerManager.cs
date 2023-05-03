using System.Data;
using System.Data.Common;
using Generator.Server.Extensions;
using Generator.Shared.Extensions;
using Generator.Shared.Models;
//using Microsoft.Data.SqlClient;

namespace Generator.Server.DatabaseResolvers
{
    public class SqlServerManager : SqlQueryBuilder, IDatabaseManager
    {

        public SqlServerManager(DbConnection dbConnection)
        {
            Connection = dbConnection;
        }

        public async Task<IDictionary<string, object>>  InsertAsync(string TableName, IDictionary<string, object> Model, CommandBehavior CommandBehavior = CommandBehavior.Default)
        {
            var isAutoInrementQuery = IsAutoIncrementStatement(TableName);

            var whereStatement = new WhereStatement(nameof(TableName), TableName);

            var isAutoIncrementResult = await QueryAsync(isAutoInrementQuery, CommandBehavior, CommandType.Text, whereStatement);

            string primaryKeyName = isAutoIncrementResult.First()["PrimaryKeyName"].ToString();

            bool IsIdentity = (bool)(isAutoIncrementResult.First()["IS_IDENTITY"].CastTo<bool>());

            if (IsIdentity)
                Model.Remove(primaryKeyName);

            var insertStatement = CreateInsertStatement(TableName, Model,primaryKeyName,IsIdentity);

            var insertResult = await QueryAsync(insertStatement, WhereStatement.CreateStatementCollection(Model));
 
            return insertResult.FirstOrDefault();
        }

        public async Task<IDictionary<string, object>> UpdateAsync(string TableName, IDictionary<string, object> Model, CommandBehavior CommandBehavior = CommandBehavior.Default)
        {
            var isAutoInrementQuery = IsAutoIncrementStatement(TableName);

            var whereStatement = new WhereStatement(nameof(TableName), TableName);

             var isAutoIncrementResult = await QueryAsync(isAutoInrementQuery, CommandBehavior, CommandType.Text, whereStatement);

            string primaryKeyName = isAutoIncrementResult.First()["PrimaryKeyName"].ToString();

            //bool IsIdentity = (bool)(isAutoIncrementResult.First()["IS_IDENTITY"].CastTo<bool>());

            var updateStatement = CreateUpdateStatement(TableName, Model, primaryKeyName);

            var updateResult = await QueryAsync(updateStatement, WhereStatement.CreateStatementCollection(Model));

            return updateResult.FirstOrDefault();


        }

  
        public async Task<IDictionary<string, object>> DeleteAsync(string TableName, IDictionary<string, object> Model, CommandBehavior CommandBehavior = CommandBehavior.Default)
        {
            var isAutoInrementQuery = IsAutoIncrementStatement(TableName);

            WhereStatement whereStatement = new WhereStatement(nameof(TableName), TableName);

            var isAutoIncrementResult = await QueryAsync(isAutoInrementQuery, CommandBehavior, CommandType.Text, whereStatement);

            string primaryKeyName = isAutoIncrementResult.First()["PrimaryKeyName"].ToString();

            bool IsIdentity = (bool)(isAutoIncrementResult.First()["IS_IDENTITY"].CastTo<bool>());

            var deleteStatement = CreateDeleteStatement(TableName, primaryKeyName);

            var deleteStatementResult = await QueryAsync(deleteStatement, WhereStatement.CreateStatementCollection(Model));

            return Model;
        }

        public Task<List<IDictionary<string, object>>> QueryAsync(string Query,  params WhereStatement[] WhereStatementParameters)
        {
            return QueryAsync(Query, CommandBehavior.Default, CommandType.Text, WhereStatementParameters);
        }

        public async Task<List<IDictionary<string, object>>> QueryAsync(string Query, CommandBehavior CommandBehavior, CommandType CommandType, params WhereStatement[] WhereStatementParameters)
        {
            var command = Connection.CreateCommand();

            await command.OpenAsync();

            command.CommandText = Query;

            command.CommandType = CommandType;

            AddWhereStatementParameters(command, WhereStatementParameters);

            var result = await ExecuteCommandAsync(command, CommandBehavior);

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

            //AddWhereStatementParameters(command, new WhereStatement(nameof(ProcedureName), ProcedureName));

            var result = await ExecuteCommandAsync(command, CommandBehavior.Default);

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

            AddWhereStatementParameters(command, new WhereStatement(nameof(MethodName), MethodName));

            var result = await ExecuteCommandAsync(command, CommandBehavior.Default);

            await command.Connection.CloseAsync();

            return result;
        }
    }


}
