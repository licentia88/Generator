using Generator.Server.Helpers;
using Generator.Shared.Models;
using Generator.Shared.Services;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc;

namespace Generator.Server.Services;

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
        return A.ListOf<TModel>(number);
    }

    public ValueTask<RESPONSE_RESULT<List<TModel>>> QueryAsync(RESPONSE_REQUEST<TModel> request, CallContext context = default)
    {
        return Delegates.ExecuteModelAsync(async () =>
        {
            return await Db.Set<TModel>().AsNoTracking().ToListAsync();
        });
    }

    public ValueTask<RESPONSE_RESULT<List<TModel>>> QueryRelationalAsync(RESPONSE_REQUEST<TModel> request, CallContext context = default)
    {
        //TODO:DO Later
        throw new NotImplementedException();
    }

    public ValueTask<RESPONSE_RESULT<TModel>> AddAsync(RESPONSE_REQUEST<TModel> request, CallContext context = default)
    {
        return Delegates.ExecuteModelAsync(async () =>
        {
            await Db.Set<TModel>().AddAsync(request.RR_DATA);

            await Db.SaveChangesAsync();

            return request.RR_DATA;
        });
    }

    public ValueTask<RESPONSE_RESULT<TModel>> Updatesync(RESPONSE_REQUEST<TModel> request, CallContext context = default)
    {
        return Delegates.ExecuteModelAsync(async () =>
        {
             Db.Set<TModel>().Update(request.RR_DATA);

            await Db.SaveChangesAsync();

            return request.RR_DATA;
        });
    }

    public ValueTask<RESPONSE_RESULT<TModel>> RemoveAsync(RESPONSE_REQUEST<TModel> request, CallContext context = default)
    {
        return Delegates.ExecuteModelAsync(async () =>
        {
            Db.Set<TModel>().Remove(request.RR_DATA);

            await Db.SaveChangesAsync();

            return request.RR_DATA;
        });
    }
}
