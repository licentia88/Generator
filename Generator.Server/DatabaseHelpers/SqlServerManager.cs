using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Text;
using Cysharp.Text;
using Generator.Server.Extensions;
using Generator.Shared.Enums;
using Generator.Shared.Extensions;
using Generator.Shared.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

            bool IsIdentity = (bool)(isAutoIncrementResult.First()["IS_IDENTITY"].CastTo<bool>());

            var updateStatement = CreateUpdateStatement(TableName, Model, primaryKeyName);

            var updateResult = await QueryAsync(updateStatement, WhereStatement.CreateStatementCollection(Model));

            return updateResult.FirstOrDefault();


        }

        //public  Task<List<IDictionary<string,object>>> QueryAsync(string Query, IDictionary<string, object> Model, CommandBehavior CommandBehavior = CommandBehavior.Default)
        //{
        //    return QueryAsync(Query, Model, CommandBehavior, null);
        //}

        //public async Task<List<IDictionary<string, object>>> QueryAsync(string Query, IDictionary<string, object> Model, CommandBehavior CommandBehavior = CommandBehavior.Default, params WhereStatement[] WhereStatementParameters)
        //{
        //    var command = Connection.CreateCommand();

        //    await command.OpenAsync();

        //    command.CommandText = Query;

        //    AddParameters(command, Model, Model?.Select(x => x.Key).ToArray());

        //    AddWhereStatementParameters(command, WhereStatementParameters);
            
        //    var result = await ExecuteCommandAsync(command, CommandBehavior);

        //    await command.Connection.CloseAsync();

        //    return result;
        //}


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
    }


}
