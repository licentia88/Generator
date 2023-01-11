using System;
using Generator.Examples.Shared;
using Microsoft.EntityFrameworkCore;
using ProtoBuf.Grpc;

namespace Generator.Services.Services
{
	public class OrdersMService:IOrdersMService
	{
        public TestContext Db { get; set; }

        public OrdersMService(IServiceProvider provider)
		{
            Db = provider.GetService<TestContext>();
        }

        public async ValueTask<ORDERS_M> CreateAsync(RESPONSE_REQUEST<ORDERS_M> request, CallContext context = default)
        {
            Db.ORDERS_M.Add(request.Data);

            await Db.SaveChangesAsync();

            return request.Data;
        }

        public async ValueTask<List<ORDERS_M>> ReadAsync(RESPONSE_REQUEST<int> request, CallContext context = default)
        {
            return await Db.ORDERS_M.AsNoTracking().Where(x=> x.OM_USER_REFNO == request.Data).ToListAsync();

        }

        public async ValueTask<ORDERS_M> UpdateAsync(RESPONSE_REQUEST<ORDERS_M> request, CallContext context = default)
        {
            Db.ORDERS_M.Update(request.Data);

            await Db.SaveChangesAsync();

            return request.Data;
        }

        public async ValueTask<ORDERS_M> DeleteAsync(RESPONSE_REQUEST<ORDERS_M> request, CallContext context = default)
        {
            Db.ORDERS_M.Remove(request.Data);

            await Db.SaveChangesAsync();

            return request.Data;
        }

        
    }
}

