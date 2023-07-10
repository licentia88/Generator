using AQueryDisassembler;
using Generator.Server.Helpers;
using Generator.Server.Services.Base;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Models.ComponentModels.NonDB;
using Generator.Shared.Models.ServiceModels;
using Generator.Shared.Services;
using Grpc.Core;
using MagicOnion;
using Mapster;

namespace Generator.Server.Services;

// ReSharper disable once UnusedType.Global
public class DatabaseService : MagicBase<IDatabaseService, DATABASE_INFORMATION>, IDatabaseService
{
    private SQLQueryParser SQLQueryParser { get; set; }

    public DatabaseService(IServiceProvider provider) : base(provider)
    {
    }

    public UnaryResult<RESPONSE_RESULT<List<DATABASE_INFORMATION>>> GetDatabaseList()
    {
         return TaskHandler.Execute(() =>
        {
            var configuration = ConnectionHelper.GetConfiguration();

            var connections = ConnectionHelper.GetConnectionStrings(configuration);

            var resultData = connections.Select(x => new DATABASE_INFORMATION { DI_DATABASE_NAME = x.Name }).OrderBy(x => x.DI_DATABASE_NAME).ToList();

            return resultData;
        });

    }



    public UnaryResult<RESPONSE_RESULT<List<DISPLAY_FIELD_INFORMATION>>> GetStoredProcedureFieldsAsync(string connectionName, string StoredProcedure)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var query = $"SELECT NAME DFI_NAME FROM SYS.DM_EXEC_DESCRIBE_FIRST_RESULT_SET('EXEC {StoredProcedure}', NULL, 0)";

            var queryResult = await GetDatabase(connectionName).QueryAsync(query, new KeyValuePair<string, object>());

            var adaptedData = queryResult.Adapt<List<DISPLAY_FIELD_INFORMATION>>();

            return adaptedData;
        });
    }

    public async UnaryResult<RESPONSE_RESULT<List<STORED_PROCEDURES>>> GetStoredProcedures(string connectionName)
    {
        return await TaskHandler.ExecuteAsync(async () =>
        {
            var query = " SELECT NAME AS SP_NAME FROM SYS.PROCEDURES WHERE TYPE = 'P' AND IS_MS_SHIPPED = 0 ORDER BY NAME;\n";

            var queryResult = await GetDatabase(connectionName).QueryAsync(query, new KeyValuePair<string, object>());

            var adaptedData = queryResult.Adapt<List<STORED_PROCEDURES>>();

            return adaptedData;
        });

    }

    public async UnaryResult<RESPONSE_RESULT<List<DISPLAY_FIELD_INFORMATION>>> GetTableFields(string connectionName, string TableName)
    {
        return await TaskHandler.ExecuteAsync(async () =>
        {
            var query = " SELECT COLUMN_NAME DFI_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TABLE_NAME ";

            var queryResult = await GetDatabase(connectionName).QueryAsync(query, ("TABLE_NAME", TableName));

            var adaptedData = queryResult.Adapt<List<DISPLAY_FIELD_INFORMATION>>();

            return adaptedData;
        });
    }

    public async UnaryResult<RESPONSE_RESULT<List<TABLE_INFORMATION>>> GetTableListForConnection(string connectionName)
    {
        return await TaskHandler.ExecuteAsync(async () =>
        {
            var query = " SELECT TABLE_NAME TI_TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY  TABLE_NAME ASC ";

            var queryResult = await GetDatabase(connectionName).QueryAsync(query, new KeyValuePair<string, object>());

            var adaptedData = queryResult.Adapt<List<TABLE_INFORMATION>>();

            return adaptedData;
        });
    }
 

    public UnaryResult<RESPONSE_RESULT<List<DISPLAY_FIELD_INFORMATION>>> GetFieldsUsingQuery(string connectionName, string Query)
    {
        return TaskHandler.Execute(() =>
        {

            var connection = GetDatabase(connectionName).Connection;

            SQLQueryParser = new SQLQueryParser(connection);

            var fieldsList = SQLQueryParser.GetFieldNamesFromQuery(Query);
            return fieldsList.Select(x => new DISPLAY_FIELD_INFORMATION { DFI_NAME = x }).ToList();
        });
    }
}