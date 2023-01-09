using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace Generator.Examples.Shared;

[Service]
public interface IOrdersDService
{
    public ValueTask<ORDERS_D> CreateAsync(RESPONSE_REQUEST<ORDERS_D> request, CallContext context = default);

    public ValueTask<List<ORDERS_D>> ReadAsync(RESPONSE_REQUEST<int> request, CallContext context = default);

    public ValueTask<ORDERS_D> UpdateAsync(RESPONSE_REQUEST<ORDERS_D> request, CallContext context = default);

    public ValueTask<ORDERS_D> DeleteAsync(RESPONSE_REQUEST<ORDERS_D> request, CallContext context = default);

}
