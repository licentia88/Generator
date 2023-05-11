using System;
using Generator.Examples.Shared;
using Generator.Examples.Shared.Models;
using Generator.Shared.Models.ServiceModels;
using Microsoft.EntityFrameworkCore;
using ProtoBuf.Grpc;

namespace Generator.Services
{
	public class OrdersMService:IOrdersMService
	{
        public TestContext Db { get; set; }

        public OrdersMService(IServiceProvider provider)
		{
            Db = provider.GetService<TestContext>();
        }

        public async Task<ORDERS_M> CreateAsync(RESPONSE_REQUEST<ORDERS_M> request, CallContext context = default)
        {
            Db.ORDERS_M.Add(request.RR_DATA);

            await Db.SaveChangesAsync();

            return request.RR_DATA;
        }

        public async Task<List<ORDERS_M>> ReadAsync(RESPONSE_REQUEST<int> request, CallContext context = default)
        {
            return await Db.ORDERS_M.AsNoTracking().Where(x=> x.OM_USER_REFNO == request.RR_DATA).ToListAsync();

        }

        public async Task<ORDERS_M> UpdateAsync(RESPONSE_REQUEST<ORDERS_M> request, CallContext context = default)
        {
            Db.ORDERS_M.Update(request.RR_DATA);

            await Db.SaveChangesAsync();

            return request.RR_DATA;
        }

        public async Task<ORDERS_M> DeleteAsync(RESPONSE_REQUEST<ORDERS_M> request, CallContext context = default)
        {
            Db.ORDERS_M.Remove(request.RR_DATA);

            await Db.SaveChangesAsync();

            return request.RR_DATA;
        }

        
    }
}

