using Generator.Shared.Models.ServiceModels;
using MagicOnion;

namespace Generator.Shared.Hubs.Base;

public interface IHubBase<THub, TReceiver, TModel>: IStreamingHub<THub, TReceiver>
{
    Task ConnectAsync();

    Task<RESPONSE_RESULT<TModel>> CreateAsync(TModel model);

    Task<RESPONSE_RESULT<List<TModel>>> ReadAsync();

    Task StreamReadAsync(int batchSize);

    Task<RESPONSE_RESULT<TModel>> UpdateAsync(TModel model);

    Task<RESPONSE_RESULT<TModel>> DeleteAsync(TModel model);

    Task CollectionChanged();
}


