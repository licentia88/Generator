using System.Text.RegularExpressions;
using AQueryMaker;
using Generator.Server.Database;
using Generator.Server.Helpers;
using Generator.Server.Jwt;
using Generator.Shared.Hubs;
using MagicOnion;
using MagicOnion.Server.Hubs;
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
    IGroup Room;

    public List<TModel> Collection { get; set; }

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

    public async Task DeleteAsync(TModel model)
    {
        await TaskHandler.ExecuteAsync(async () =>
        {
            Db.Set<TModel>().Remove(model);

            await Db.SaveChangesAsync();

            Collection.Remove(model);

            Broadcast(Room).OnDelete(model);
        });
    }

    public async Task ReadAsync()
    {
        await TaskHandler.ExecuteAsync(async () =>
        {
            var result = await Db.Set<TModel>().AsNoTracking().ToListAsync();

            Collection.AddRange(result);

            Broadcast(Room).OnRead(result);
        });
    }

    public async Task UpdateAsync(TModel model)
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
}


