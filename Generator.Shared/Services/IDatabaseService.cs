using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Models.ComponentModels.NonDB;
using Generator.Shared.Models.ServiceModels;
using Generator.Shared.Services.Base;
using MagicOnion;


namespace Generator.Shared.Services;

public interface IDatabaseService:IGenericService<IDatabaseService,DATABASE_INFORMATION>
{
    public UnaryResult<RESPONSE_RESULT<List<DATABASE_INFORMATION>>> GetDatabaseList();

    public UnaryResult<RESPONSE_RESULT<List<TABLE_INFORMATION>>> GetTableListAsync(string connectionName);

    public UnaryResult<RESPONSE_RESULT<List<STORED_PROCEDURES>>> GetStoredProcedures(string connectionName);

    public UnaryResult<RESPONSE_RESULT<List<DISPLAY_FIELD_INFORMATION>>> GetTableFieldsAsync(string connectionName, string TableName );

    public UnaryResult<RESPONSE_RESULT<List<DISPLAY_FIELD_INFORMATION>>> GetFieldsUsingQuery(string connectionName, string Query);

    public UnaryResult<RESPONSE_RESULT<List<DISPLAY_FIELD_INFORMATION>>> GetStoredProcedureFieldsAsync(string connectionName, string StoredProcedure);

    public UnaryResult<RESPONSE_RESULT<List<DISPLAY_FIELD_INFORMATION>>> GetStoredProcedureParametersAsync(string connectionName, string StoredProcedure);

    
}