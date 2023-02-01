using Generator.Shared.Models;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace Generator.Shared.Services;

[Service]
public interface IGenericServiceBase<TModel> where TModel:class, new()
{
    public Task<RESPONSE_RESULT<List<TModel>>> QueryAsync(RESPONSE_REQUEST<TModel> request, CallContext context = default);

    public Task<RESPONSE_RESULT<List<TModel>>> QueryRelationalAsync(RESPONSE_REQUEST<TModel> request, CallContext context = default);

    public Task<RESPONSE_RESULT<TModel>> AddAsync(RESPONSE_REQUEST<TModel> request, CallContext context = default);

    public Task<RESPONSE_RESULT<TModel>> Updatesync(RESPONSE_REQUEST<TModel> request, CallContext context = default);

    public Task<RESPONSE_RESULT<TModel>> RemoveAsync(RESPONSE_REQUEST<TModel> request, CallContext context = default);
}
