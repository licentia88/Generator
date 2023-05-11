using Generator.Shared.Models;
using Generator.Shared.Models.ServiceModels;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace Generator.Shared.Services;

[Service]
public interface IServiceBase
{
    public Task<RESPONSE_RESULT> QueryAsync(RESPONSE_REQUEST request, CallContext context = default);

    public Task<RESPONSE_RESULT> QueryRelationalAsync(RESPONSE_REQUEST request, CallContext context = default);

    public Task<RESPONSE_RESULT> ExecuteNonQuery(CallContext context = default);

    public Task<RESPONSE_RESULT> QueryScalar(CallContext context = default);

    public IAsyncEnumerable<RESPONSE_RESULT> QueryStream(CallContext context = default);

    public Task<RESPONSE_RESULT> AddAsync(RESPONSE_REQUEST request, CallContext context = default);

    public Task<RESPONSE_RESULT> Updatesync(RESPONSE_REQUEST request, CallContext context = default);

    public Task<RESPONSE_RESULT> DeleteAsync(RESPONSE_REQUEST request, CallContext context = default);
}

