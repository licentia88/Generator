using MagicOnion;

namespace Generator.Shared.Hubs;

public interface IHubBase<THub, TReceiver, TModel>: IStreamingHub<THub, TReceiver>
   //where THub : IStreamingHub<THub, TReceiver>
{
    Task ConnectAsync();

    // CRUD operations
    Task CreateAsync(TModel model);
    Task ReadAsync();
    Task UpdateAsync(TModel model);
    Task DeleteAsync(TModel model);
}


