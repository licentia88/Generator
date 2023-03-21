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

            var queryParameters = new Dictionary<string, object> { { nameof(TableName), TableName } };

            var isAutoIncrementResult = await QueryAsync(isAutoInrementQuery, queryParameters, CommandBehavior);

            string primaryKeyName = isAutoIncrementResult.First()["PrimaryKeyName"].ToString();

            bool IsIdentity = (bool)(isAutoIncrementResult.First()["IS_IDENTITY"].CastTo<bool>());

            if (IsIdentity)
                Model.Remove(primaryKeyName);

            var insertStatement = CreateInsertStatement(TableName, Model,primaryKeyName,IsIdentity);

            var insertResult = await QueryAsync(insertStatement, Model);
 
            return insertResult.FirstOrDefault();
        }

        public async Task<IDictionary<string, object>> UpdateAsync(string TableName, IDictionary<string, object> Model, CommandBehavior CommandBehavior = CommandBehavior.Default)
        {
            var isAutoInrementQuery = IsAutoIncrementStatement(TableName);

            var queryParameters = new Dictionary<string, object> { { nameof(TableName), TableName } };

            var isAutoIncrementResult = await QueryAsync(isAutoInrementQuery, queryParameters, CommandBehavior);

            string primaryKeyName = isAutoIncrementResult.First()["PrimaryKeyName"].ToString();

            bool IsIdentity = (bool)(isAutoIncrementResult.First()["IS_IDENTITY"].CastTo<bool>());

            var updateStatement = CreateUpdateStatement(TableName, Model, primaryKeyName);

            var updateResult = await QueryAsync(updateStatement, Model);

            return updateResult.FirstOrDefault();


        }

        public async Task<List<IDictionary<string,object>>> QueryAsync(string Query, IDictionary<string, object> Model, CommandBehavior CommandBehavior = CommandBehavior.Default)
        {
            var command = Connection.CreateCommand();

            await command.OpenAsync();

            command.CommandText = Query;

            AddParameters(command, Model, Model?.Select(x => x.Key).ToArray());

            var result = await ExecuteCommandAsync(command, CommandBehavior);

            await command.Connection.CloseAsync();

            return result;
        }
    }


}
