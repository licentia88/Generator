using Generator.Shared.Models.ServiceModels;
using MagicOnion;

namespace Generator.Shared.Hubs;

public interface IHubBase<THub, TReceiver, TModel>: IStreamingHub<THub, TReceiver>
{
    Task ConnectAsync();

    Task CreateAsync(TModel model);

    Task<RESPONSE_RESULT<List<TModel>>> ReadAsync();

    Task StreamReadAsync(int batchSize);

    Task UpdateAsync(TModel model);

    Task DeleteAsync(TModel model);

    Task CollectionChanged();
}


