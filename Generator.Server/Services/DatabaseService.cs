using System.Text.RegularExpressions;
using AQueryDisassembler;
using Generator.Server.Helpers;
using Generator.Server.Services.Base;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Models.ComponentModels.NonDB;
using Generator.Shared.Models.ServiceModels;
using Generator.Shared.Services;
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
            var procedureFields = await GetDatabase(connectionName).GetStoredProcedureFieldsAsync(StoredProcedure);

            var adaptedData = procedureFields.Select(x => new DISPLAY_FIELD_INFORMATION { DFI_NAME = x["NAME"].ToString() }).ToList();

            return adaptedData;
        });
    }

    public async UnaryResult<RESPONSE_RESULT<List<STORED_PROCEDURES>>> GetStoredProcedures(string connectionName)
    {
        return await TaskHandler.ExecuteAsync(async () =>
        {
            var procedureFields = await GetDatabase(connectionName).GetStoredProcedures();

            var adaptedData = procedureFields.Select(x => new STORED_PROCEDURES { SP_NAME = x["SP_NAME"].ToString() }).ToList();

            return adaptedData;
        });

    }

    public async UnaryResult<RESPONSE_RESULT<List<DISPLAY_FIELD_INFORMATION>>> GetTableFieldsAsync(string connectionName, string TableName)
    {
        return await TaskHandler.ExecuteAsync(async () =>
        {
            var queryResult = await GetDatabase(connectionName).GetTableFieldsAsync(TableName);

            var adaptedData = queryResult.Select(x => new DISPLAY_FIELD_INFORMATION { DFI_NAME = x["COLUMN_NAME"].ToString() }).ToList();

            return adaptedData;
        });
    }

    public async UnaryResult<RESPONSE_RESULT<List<TABLE_INFORMATION>>> GetTableListAsync(string connectionName)
    {
        return await TaskHandler.ExecuteAsync(async () =>
        {
            var queryResult = await GetDatabase(connectionName).GetTableListAsync();

            var adaptedData = queryResult.Select(x => new TABLE_INFORMATION { TI_TABLE_NAME = x["TABLE_NAME"].ToString() }).ToList();

            return adaptedData;
        });
    }
 
    public UnaryResult<RESPONSE_RESULT<List<DISPLAY_FIELD_INFORMATION>>> GetFieldsUsingQuery(string connectionName, string Query)
    {
        return TaskHandler.Execute(() =>
        {
            var connection = GetDatabase(connectionName).Connection;

            SQLQueryParser = new SQLQueryParser(connection);

            var queryParams = GetParametersFromQuery(Query);

            var fieldsList = SQLQueryParser.GetFieldNamesFromQuery(Query);
 
            return fieldsList.Select(x => new DISPLAY_FIELD_INFORMATION { DFI_NAME = x , DFI_IS_SEARCH_FIELD = queryParams.Contains(x) }).ToList();
        });
    }

    private List<string> GetParametersFromQuery(string query)
    {     
        string pattern = @"@\w+";

        Regex regex = new(pattern);

        MatchCollection matches = regex.Matches(query);

        return matches.Select(x => x.Value.Substring(1)).ToList();
        
    }

    

    public async UnaryResult<RESPONSE_RESULT<List<DISPLAY_FIELD_INFORMATION>>> GetStoredProcedureParametersAsync(string connectionName, string StoredProcedure)
    {
        return await TaskHandler.ExecuteAsync(async () =>
        {
            var queryResult = await GetDatabase(connectionName).GetStoredProcedureParametersAsync(StoredProcedure);

            return queryResult.Select(x => new DISPLAY_FIELD_INFORMATION { DFI_NAME = x["PARAMETER_NAME"].ToString(), DFI_IS_SEARCH_FIELD = true }).ToList();

            
        });
      
    }
}