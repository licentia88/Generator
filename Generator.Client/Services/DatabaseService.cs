using Generator.Client.Services.Base;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Models.ComponentModels.NonDB;
using Generator.Shared.Models.ServiceModels;
using Generator.Shared.Services;
using MagicOnion;

namespace Generator.Client.Services;

[RegisterSingleton]
public class DatabaseService : ServiceBase<IDatabaseService, DATABASE_INFORMATION>, IDatabaseService
{
    public UnaryResult<RESPONSE_RESULT<List<DATABASE_INFORMATION>>> GetDatabaseList()
    {
        return Client.GetDatabaseList();
    }

    public UnaryResult<RESPONSE_RESULT<List<DISPLAY_FIELD_INFORMATION>>> GetFieldsUsingQuery(string connectionName, string Query)
    {
        return Client.GetFieldsUsingQuery(connectionName, Query);
    }

    public UnaryResult<RESPONSE_RESULT<List<DISPLAY_FIELD_INFORMATION>>> GetStoredProcedureFieldsAsync(string connectionName, string StoredProcedure)
    {
        return Client.GetStoredProcedureFieldsAsync(connectionName, StoredProcedure);
    }

    public UnaryResult<RESPONSE_RESULT<List<DISPLAY_FIELD_INFORMATION>>> GetStoredProcedureParametersAsync(string connectionName, string StoredProcedure)
    {
        return Client.GetStoredProcedureParametersAsync(connectionName, StoredProcedure);
    }

    public UnaryResult<RESPONSE_RESULT<List<STORED_PROCEDURES>>> GetStoredProcedures(string connectionNameRequest)
    {
        return Client.GetStoredProcedures(connectionNameRequest);
    }

    public UnaryResult<RESPONSE_RESULT<List<DISPLAY_FIELD_INFORMATION>>> GetTableFieldsAsync(string connectionName, string TableName)
    {
        return Client.GetTableFieldsAsync(connectionName,TableName);
    }

    public UnaryResult<RESPONSE_RESULT<List<TABLE_INFORMATION>>> GetTableListAsync(string connectionNameRequest)
    {
        return Client.GetTableListAsync(connectionNameRequest);
    }

    
}