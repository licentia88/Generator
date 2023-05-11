using Generator.Shared.Models;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Models.ServiceModels;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace Generator.Shared.Services;

[Service]
public interface IDatabaseService
{
    public Task<RESPONSE_RESULT<List<DATABASE_INFORMATION>>> GetDatabaseList(CallContext context = default);

    public Task<RESPONSE_RESULT<List<TABLE_INFORMATION>>> GetTableListForConnection(RESPONSE_REQUEST<string> connectionNameRequest, CallContext context = default);

    public Task<RESPONSE_RESULT<List<STORED_PROCEDURES>>> GetStoredProcedures(RESPONSE_REQUEST<string> connectionNameRequest, CallContext context = default);

    public Task<RESPONSE_RESULT<List<DISPLAY_FIELD_INFORMATION>>> GetTableFields(RESPONSE_REQUEST<(string connectionName, string TableName)> data , CallContext context = default);

    public Task<RESPONSE_RESULT<List<DISPLAY_FIELD_INFORMATION>>> GetStoredProcedureFields(RESPONSE_REQUEST<(string connectionName, string StoredProcedure)> data, CallContext context = default);

}



