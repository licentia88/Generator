using AQueryMaker;
using Generator.Server.Database;
using Generator.Server.Helpers;
using Generator.Server.Jwt;
using Generator.Shared.Enums;
using Generator.Shared.Hubs;
using Generator.Shared.Models.ServiceModels;
using MagicOnion;
using MagicOnion.Server.Hubs;
using MessagePipe;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace Generator.Server.Hubs;

public class MagicHubBase<THub, TReceiver, TModel> : MagicHubBase<THub, TReceiver, TModel, GeneratorContext>//, IHubBase<THub, TReceiver, TModel>
    where THub : IStreamingHub<THub, TReceiver>
    where TReceiver : IHubReceiverBase<TModel>
    where TModel:class
{
    public MagicHubBase(IServiceProvider provider) : base(provider)
    {
    }
}

public class MagicHubBase<THub, TReceiver, TModel, TContext> : StreamingHubBase<THub, TReceiver>, IHubBase<THub, TReceiver, TModel>
    where THub : IStreamingHub<THub, TReceiver>
    where TReceiver : IHubReceiverBase<TModel>
    where TContext : DbContext
    where TModel : class
{
    protected IGroup Room;


    //IInMemoryStorage<List<TModel>> storage;

    protected List<TModel> Collection { get; set; }

    protected ISubscriber<Operation,TModel> Subscriber { get; set; }

    protected TContext Db;

    private readonly IDictionary<string, Func<SqlQueryFactory>> ConnectionFactory;

    /// <summary>
    /// Gets or sets the instance of FastJwtTokenService.
    /// </summary>
    [Inject]
    public FastJwtTokenService FastJwtTokenService { get; set; }

    /// <summary>
    /// Retrieves the database connection based on the specified connection name.
    /// </summary>
    /// <param name="connectionName">The name of the connection.</param>
    /// <returns>An instance of SqlQueryFactory.</returns>
    protected SqlQueryFactory GetDatabase(string connectionName) => ConnectionFactory[connectionName]?.Invoke();
    public MagicHubBase(IServiceProvider provider)
    {
        Db = provider.GetService<TContext>();
        FastJwtTokenService = provider.GetService<FastJwtTokenService>();
        ConnectionFactory = provider.GetService<IDictionary<string, Func<SqlQueryFactory>>>();
        Collection = provider.GetService<List<TModel>>();
        Subscriber = provider.GetService<ISubscriber<Operation,TModel>>();
    }

    public async Task ConnectAsync()
    {
        Room = await Group.AddAsync(typeof(TModel).Name);

    }

    public virtual async Task CreateAsync(TModel model)
    {
        await TaskHandler.ExecuteAsync(async () =>
        {
            Db.Set<TModel>().Add(model);

            await Db.SaveChangesAsync();

            Collection.Add(model);

            Broadcast(Room).OnCreate(model);
        });
    }

    public virtual async Task DeleteAsync(TModel model)
    {
        await TaskHandler.ExecuteAsync(async () =>
        {
            Db.Set<TModel>().Remove(model);

            await Db.SaveChangesAsync();

            Collection.Remove(model);

            Broadcast(Room).OnDelete(model);
        });
    }

    public virtual async Task<RESPONSE_RESULT<List<TModel>>> ReadAsync()
    {
        return await TaskHandler.ExecuteAsync(async () =>
        {
            var result = await Db.Set<TModel>().AsNoTracking().ToListAsync();

            Collection.AddRange(result);

            Broadcast(Room).OnRead(result);

            return Collection;
        });
    }

    public async Task StreamReadAsync(int batchSize)
    {
        await foreach (var data in FetchStreamAsync(batchSize))
        {
            Collection.AddRange(data);

            Broadcast(Room).OnStreamRead(data);
        }
    }

    public virtual async Task UpdateAsync(TModel model)
    {
        await TaskHandler.ExecuteAsync(async () =>
        {
            Db.Set<TModel>().Attach(model);
            Db.Set<TModel>().Update(model);

            await Db.SaveChangesAsync();

            //burada biseyler yap
            //Collection.Add(model);

            Broadcast(Room).OnUpdate(model);
        });
    }

    public virtual async Task CollectionChanged()
    {
        var newCollection = await Db.Set<TModel>().AsNoTracking().ToListAsync();

        Collection.Clear();
        Collection.AddRange(newCollection);

        Broadcast(Room).OnCollectionChanged(Collection);
    }


   


    private async IAsyncEnumerable<List<TModel>> FetchStreamAsync(int batchSize = 2)
    {
        var count = await Db.Set<TModel>().CountAsync().ConfigureAwait(false);
        var batches = (int)Math.Ceiling((double)count / batchSize);

        for (var i = 0; i < batches; i++)
        {
            var skip = i * batchSize;
            var take = Math.Min(batchSize, count - skip);
            var entities = await FetchStream(Db, skip, take).ConfigureAwait(false);
            yield return entities;
        }

    }

  

    private static Func<TContext, int, int, Task<List<TModel>>> FetchStream =
        EF.CompileAsyncQuery(
            (TContext context, int skip, int take) =>
                context.Set<TModel>().AsNoTracking().Skip(skip).Take(take).ToList());
}


