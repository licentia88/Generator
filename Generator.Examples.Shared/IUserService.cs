using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace Generator.Examples.Shared;

[Service]
public interface IUserService
{
    public ValueTask<USER> CreateAsync(RESPONSE_REQUEST<USER> request, CallContext context = default);

    public ValueTask<List<USER>> ReadAsync(CallContext context = default);

    public ValueTask<USER> UpdateAsync(RESPONSE_REQUEST<USER> request, CallContext context = default);

    public ValueTask<USER> DeleteAsync(RESPONSE_REQUEST<USER> request, CallContext context = default);

}
