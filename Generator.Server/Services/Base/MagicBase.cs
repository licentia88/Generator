using AQueryMaker;
using Generator.Server.Database;
using Generator.Server.Helpers;
using Generator.Server.Jwt;
using Generator.Shared.Services.Base;
using MagicOnion;
using MagicOnion.Server;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace Generator.Server.Services.Base;

/// <summary>
/// Base class for magic operations that involve a generic service and model.
/// </summary>
/// <typeparam name="TService">The type of the service.</typeparam>
/// <typeparam name="TModel">The type of the model.</typeparam>
public class MagicBase<TService, TModel> : MagicBase<TService, TModel, GeneratorContext>
    where TService : IGenericService<TService, TModel>, IService<TService>
    where TModel : class 
{
    public MagicBase(IServiceProvider provider) : base(provider)
    {

    }
}

/// <summary>
/// Base class for magic operations that involve a generic service, model, and database context.
/// </summary>
/// <typeparam name="TService">The type of the service.</typeparam>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TContext">The type of the database context.</typeparam>
public class MagicBase<TService, TModel, TContext> : ServiceBase<TService>, IGenericService<TService, TModel>
    where TService : IGenericService<TService, TModel>, IService<TService>
    where TModel : class
    where TContext : DbContext
{
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

    public MagicBase(IServiceProvider provider)
    {
        // MemoryDatabase = provider.GetService<MemoryContext>();
        Db = provider.GetService<TContext>();
        FastJwtTokenService = provider.GetService<FastJwtTokenService>();
        ConnectionFactory = provider.GetService<IDictionary<string, Func<SqlQueryFactory>>>();
    }

    /// <summary>
    /// Creates a new instance of the specified model.
    /// </summary>
    /// <param name="model">The model to create.</param>
    /// <returns>A unary result containing the created model.</returns>
    public virtual UnaryResult<TModel> Create(TModel model)
    {
        return TaskHandler.ExecuteAsyncWithoutResponse(async () =>
        {
            Db.Set<TModel>().Add(model);
            await Db.SaveChangesAsync();
            return model;
        });
    }

    /// <summary>
    /// Retrieves a list of models based on the specified request.
    /// </summary>
    /// <param name="request">The request object.</param>
    /// <returns>A unary result containing a list of models.</returns>
    public virtual UnaryResult<List<TModel>> Read(TModel request)
    {
        return TaskHandler.ExecuteAsyncWithoutResponse(async () =>
        {
            return await Db.Set<TModel>().FromSqlRaw($"SELECT * FROM {typeof(TModel).Name}").ToListAsync();
        });
    }

    /// <summary>
    /// Finds a list of entities of type TModel that are associated with a parent entity based on a foreign key.
    /// </summary>
    /// <param name="parentId">The identifier of the parent entity.</param>
    /// <param name="foreignKey">The foreign key used to associate the entities with the parent entity.</param>
    /// <returns>A <see cref="UnaryResult{List{TModel}}"/> representing the result of the operation, containing a list of entities.</returns>
    public virtual UnaryResult<List<TModel>> FindByParent(string parentId, string foreignKey)
    {
        return TaskHandler.ExecuteAsyncWithoutResponse(async () =>
        {
            return await Db.Set<TModel>().FromSqlRaw($"SELECT * FROM {typeof(TModel).Name} AND {foreignKey} = '{parentId}' ").ToListAsync();
        });
    }

    /// <summary>
    /// Updates the specified model.
    /// </summary>
    /// <param name="model">The model to update.</param>
    /// <returns>A unary result containing the updated model.</returns>
    public virtual UnaryResult<TModel> Update(TModel model)
    {
        return TaskHandler.ExecuteAsyncWithoutResponse(async () =>
        {
            Db.Set<TModel>().Update(model);
            await Db.SaveChangesAsync();
            return model;
        });
    }

    /// <summary>
    /// Deletes the specified model.
    /// </summary>
    /// <param name="model">The model to delete.</param>
    /// <returns>A unary result containing the deleted model.</returns>
    public virtual UnaryResult<TModel> Delete(TModel model)
    {
        return TaskHandler.ExecuteAsyncWithoutResponse(async () =>
        {
            Db.Set<TModel>().Remove(model);
            await Db.SaveChangesAsync();
            return model;
        });
    }

    /// <summary>
    /// Retrieves all models.
    /// </summary>
    /// <returns>A unary result containing a list of all models.</returns>
    public virtual UnaryResult<List<TModel>> ReadAll()
    {
        return TaskHandler.ExecuteAsyncWithoutResponse(async () =>
        {
            return await Db.Set<TModel>().ToListAsync();
        });
    }
}
