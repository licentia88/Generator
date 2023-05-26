using Generator.Services.Helpers;
using Generator.Shared.Models.ServiceModels;
using Microsoft.EntityFrameworkCore;
using ProtoBuf.Grpc;

namespace Generator.Services;

public abstract class GenericServiceBase<TContext,TModel> :IGenericServiceBase<TModel> where TModel: class,new() where TContext: DbContext
{
    protected TContext Db { get; set; }

    public IServiceProvider Provider { get; set; }

    public GenericServiceBase(IServiceProvider provider)
    {
        Db = provider.GetService<TContext>();

        Provider = provider;
    }


    protected List<TModel> CreateFakeData(int number)
    {
        return GenFu.GenFu.ListOf<TModel>(number);
    }

    public Task<RESPONSE_RESULT<List<TModel>>> ReadAsync(CallContext context = default)
    {
        return TaskHandler.ExecuteModelAsync(async () =>
        {
            return await Db.Set<TModel>().AsNoTracking().ToListAsync();
        });
    }

    public Task<RESPONSE_RESULT<List<TModel>>> QueryAsync(RESPONSE_REQUEST<TModel> request, CallContext context = default)
    {
        return TaskHandler.ExecuteModelAsync(async () =>
        {
            return await Db.Set<TModel>().AsNoTracking().ToListAsync();
        });
    }

    public Task<RESPONSE_RESULT<List<TModel>>> ReadByParentAsync(RESPONSE_REQUEST<TModel> request, CallContext context = default) =>
        //TODO:DO Later
        throw new NotImplementedException();

    public Task<RESPONSE_RESULT<TModel>> CreateAsync(RESPONSE_REQUEST<TModel> request, CallContext context = default)
    {
        return TaskHandler.ExecuteModelAsync(async () =>
        {
            await Db.Set<TModel>().AddAsync(request.RR_DATA);

            await Db.SaveChangesAsync();

            return request.RR_DATA;
        });
    }

    public Task<RESPONSE_RESULT<TModel>> UpdateAsync(RESPONSE_REQUEST<TModel> request, CallContext context = default)
    {
        return TaskHandler.ExecuteModelAsync(async () =>
        {
             Db.Set<TModel>().Update(request.RR_DATA);

            await Db.SaveChangesAsync();

            return request.RR_DATA;
        });
    }

    public Task<RESPONSE_RESULT<TModel>> DeleteAsync(RESPONSE_REQUEST<TModel> request, CallContext context = default)
    {
        return TaskHandler.ExecuteModelAsync(async () =>
        {
            Db.Set<TModel>().Remove(request.RR_DATA);

            await Db.SaveChangesAsync();

            return request.RR_DATA;
        });
    }

   
}
