using Generator.Shared.Models;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace Generator.Shared.Services;

[Service]
public interface IServiceBase
{
    public ValueTask<RESPONSE_RESULT> QueryAsync(RESPONSE_REQUEST request, CallContext context = default);

    public ValueTask<RESPONSE_RESULT> QueryRelationalAsync(RESPONSE_REQUEST request, CallContext context = default);

    public ValueTask<RESPONSE_RESULT> ExecuteNonQuery(CallContext context = default);

    public ValueTask<RESPONSE_RESULT> QueryScalar(CallContext context = default);

    public IAsyncEnumerable<RESPONSE_RESULT> QueryStream(CallContext context = default);

    public ValueTask<RESPONSE_RESULT> AddAsync(RESPONSE_REQUEST request, CallContext context = default);

    public ValueTask<RESPONSE_RESULT> Updatesync(RESPONSE_REQUEST request, CallContext context = default);

    public ValueTask<RESPONSE_RESULT> DeleteAsync(RESPONSE_REQUEST request, CallContext context = default);
}

