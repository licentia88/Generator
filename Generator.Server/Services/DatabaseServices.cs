using Generator.Server.DatabaseResolvers;
using Generator.Server.Helpers;
using Generator.Shared.Models;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;
using Mapster;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Generator.Server.Services;

public class DatabaseServices : IDatabaseService
{
    protected readonly IDictionary<string, Func<SqlQueryFactory>> ConnectionFactory;

    public SqlQueryFactory SqlQueryFactory(string connectionName) => ConnectionFactory[connectionName]?.Invoke();

    public DatabaseServices(IServiceProvider provider)
    {

        ConnectionFactory = provider.GetService<IDictionary<string, Func<SqlQueryFactory>>>();
    }

    public Task<RESPONSE_RESULT<List<DATABASE_INFORMATION>>> GetDatabaseList(CallContext context = default)
    {
        return TaskHandler.ExecuteModelAsync(async () =>
        {
            await Task.Delay(0);

            var configuration = ConnectionHelper.GetConfiguration();

            var connections = ConnectionHelper.GetConnectionStrings(configuration);

            var resultData = connections.Select(x => new DATABASE_INFORMATION { DI_DATABASE_NAME = x.Name }).OrderBy(x => x.DI_DATABASE_NAME).ToList();

            return resultData;
        });
    }

    public Task<RESPONSE_RESULT<List<TABLE_INFORMATION>>> GetTableListForConnection(RESPONSE_REQUEST<string> connectionNameRequest, CallContext context = default)
    {
        return TaskHandler.ExecuteModelAsync(async () =>
        {
            var connectionName = connectionNameRequest.RR_DATA;

            var query = " SELECT TABLE_NAME TI_TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY  TABLE_NAME ASC ";

            var queryResult = await SqlQueryFactory(connectionName).QueryAsync(query);

            var adaptedData = queryResult.Adapt<List<TABLE_INFORMATION>>();

            return adaptedData;
        });
    }

    public Task<RESPONSE_RESULT<List<STORED_PROCEDURES>>> GetStoredProcedures(RESPONSE_REQUEST<string> connectionNameRequest, CallContext context = default)
    {
        return TaskHandler.ExecuteModelAsync(async () =>
        {
            var connectionName = connectionNameRequest.RR_DATA;

            var query = " SELECT NAME AS SP_NAME FROM SYS.PROCEDURES WHERE TYPE = 'P' AND IS_MS_SHIPPED = 0 ORDER BY NAME;\n";

            var queryResult = await SqlQueryFactory(connectionName).QueryAsync(query);

            var adaptedData = queryResult.Adapt<List<STORED_PROCEDURES>>();

            return adaptedData;
        });
    }

    //;
    public  Task<RESPONSE_RESULT<List<DISPLAY_FIELD_INFORMATION>>> GetTableFields(RESPONSE_REQUEST<(string connectionName, string TableName)> data, CallContext context = default)
    {
        return TaskHandler.ExecuteModelAsync(async () =>
        {
            var connectionName = data.RR_DATA.connectionName;
            var tableName = data.RR_DATA.TableName;

            var query = " SELECT COLUMN_NAME DFI_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TABLE_NAME ";

            var queryResult = await SqlQueryFactory(connectionName).QueryAsync(query, new WhereStatement("TABLE_NAME", tableName));

            var adaptedData = queryResult.Adapt<List<DISPLAY_FIELD_INFORMATION>>();

            return adaptedData;
        });
       

    }

    public Task<RESPONSE_RESULT<List<DISPLAY_FIELD_INFORMATION>>> GetStoredProcedureFields(RESPONSE_REQUEST<(string connectionName, string StoredProcedure)> data, CallContext context = default)
    {
        return TaskHandler.ExecuteModelAsync(async () =>
        {
            var connectionName = data.RR_DATA.connectionName;
            var storedProcedureName = data.RR_DATA.StoredProcedure;

            var query = $"SELECT NAME DFI_NAME FROM SYS.DM_EXEC_DESCRIBE_FIRST_RESULT_SET('EXEC {storedProcedureName}', NULL, 0)";

            var queryResult = await SqlQueryFactory(connectionName).QueryAsync(query);

            var adaptedData = queryResult.Adapt<List<DISPLAY_FIELD_INFORMATION>>();

            return adaptedData;
        });

       

    }
}
