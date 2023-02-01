using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace Generator.Examples.Shared;

[Service]
public interface IUserService
{
    public Task<USER> CreateAsync(RESPONSE_REQUEST<USER> request, CallContext context = default);

    public Task<List<USER>> ReadAsync(CallContext context = default);

    public Task<USER> UpdateAsync(RESPONSE_REQUEST<USER> request, CallContext context = default);

    public Task<USER> DeleteAsync(RESPONSE_REQUEST<USER> request, CallContext context = default);

}
