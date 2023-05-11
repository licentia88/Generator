using Generator.Shared.Models;
using Generator.Shared.Models.ServiceModels;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace Generator.Shared.Services;

[Service]
public interface IGenericServiceBase<TModel> where TModel: new()
{
    public Task<RESPONSE_RESULT<List<TModel>>> ReadByParentAsync(RESPONSE_REQUEST<TModel> request, CallContext context = default);

    public Task<RESPONSE_RESULT<List<TModel>>> ReadAsync(CallContext context = default);

    public Task<RESPONSE_RESULT<TModel>> CreateAsync(RESPONSE_REQUEST<TModel> request, CallContext context = default);

    public Task<RESPONSE_RESULT<TModel>> UpdateAsync(RESPONSE_REQUEST<TModel> request, CallContext context = default);

    public Task<RESPONSE_RESULT<TModel>> DeleteAsync(RESPONSE_REQUEST<TModel> request, CallContext context = default);
}
