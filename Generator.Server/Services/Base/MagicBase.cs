using Generator.Server.Database;
using Generator.Server.Helpers;
using Generator.Shared.Services.Base;
using MagicOnion;
using MagicOnion.Server;
using Microsoft.EntityFrameworkCore;
using AQueryMaker;
using Generator.Shared.Models.ComponentModels;
using Generator.Server.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Generator.Server.Services.Base;

public class MagicBase<TService, TModel> : MagicBase<TService, TModel, GeneratorContext>
     where TService : IGenericService<TService, TModel>, IService<TService>
    where TModel : class, new()
{
    public MagicBase(IServiceProvider provider) : base(provider)
    {
      
    }
}

public class MagicBase<TService, TModel,TContext> : ServiceBase<TService>, IGenericService<TService, TModel>
    where TService : IGenericService<TService, TModel>, IService<TService> 
    where TModel : class, new()
    where TContext:DbContext
{

    protected TContext Db;

    private readonly IDictionary<string, Func<SqlQueryFactory>> ConnectionFactory;

    public MemoryContext MemoryDatabase { get; set; }

    [Inject]
    public FastJwtTokenService FastJwtTokenService { get; set; }

    protected SqlQueryFactory SqlQueryFactory(string connectionName) => ConnectionFactory[connectionName]?.Invoke();

    public MagicBase(IServiceProvider provider)
    {
        MemoryDatabase = provider.GetService<MemoryContext>();
        Db = provider.GetService<TContext>();
        FastJwtTokenService = provider.GetService<FastJwtTokenService>();
        ConnectionFactory = provider.GetService<IDictionary<string, Func<SqlQueryFactory>>>();
    }

    /// <summary>
    /// Creates a new instance of the specified model.
    /// </summary>
    /// <param name="model">The model to create.</param>
    /// <returns>A unary result containing the created model.</returns>
    public async UnaryResult<TModel> Create(TModel model)
    {
        Db.Set<TModel>().Add(model);
        await Db.SaveChangesAsync();
        return model;
    }

    /// <summary>
    /// Retrieves a list of models based on the specified request.
    /// </summary>
    /// <param name="request">The request object.</param>
    /// <returns>A unary result containing a list of models.</returns>
    public async UnaryResult<List<TModel>> Read(TModel request)
    {
        return await Db.Set<TModel>().FromSqlRaw($"SELECT * FROM {typeof(TModel).Name}").ToListAsync();
    }

    public async UnaryResult<List<TModel>> FindByParent(int parentId)
    {
        return new List<TModel>();
    }

    /// <summary>
    /// Updates the specified model.
    /// </summary>
    /// <param name="model">The model to update.</param>
    /// <returns>A unary result containing the updated model.</returns>
    public async UnaryResult<TModel> Update(TModel model)
    {
        Db.Set<TModel>().Update(model);
        await Db.SaveChangesAsync();
        return model;
    }

    /// <summary>
    /// Deletes the specified model.
    /// </summary>
    /// <param name="model">The model to delete.</param>
    /// <returns>A unary result containing the deleted model.</returns>
    public async UnaryResult<TModel> Delete(TModel model)
    {
        Db.Set<TModel>().Remove(model);
        await Db.SaveChangesAsync();
        return model;
    }


    /// <summary>
    /// Retrieves all models.
    /// </summary>
    /// <returns>A unary result containing a list of all models.</returns>
    //
    public async UnaryResult<List<TModel>> ReadAll()
    {
        try
        { 

            return await Db.Set<TModel>().ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception();
        }
    }

    
}
