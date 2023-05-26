using Generator.Examples.Shared;
using Generator.Examples.Shared.Models;
using Generator.Shared.Models.ServiceModels;
using Microsoft.EntityFrameworkCore;
using ProtoBuf.Grpc;

namespace Generator.Services
{
	public class UserService:IUserService
	{
        public TestContext Db { get; set; }

        public UserService(IServiceProvider provider)
		{
            Db = provider.GetService<TestContext>();
        }

        public async Task<USER> CreateAsync(RESPONSE_REQUEST<USER> request, CallContext context = default)
        {
            Db.USER.Add(request.RR_DATA);

            await Db.SaveChangesAsync();

            return request.RR_DATA;
        }

        public async Task<List<USER>> ReadAsync(CallContext context = default)
        {
            return await Db.USER.AsNoTracking().ToListAsync();
        }

        public async Task<USER> UpdateAsync(RESPONSE_REQUEST<USER> request, CallContext context = default)
        {
            Db.USER.Update(request.RR_DATA);

            await Db.SaveChangesAsync();

            return request.RR_DATA;
        }

        public async Task<USER> DeleteAsync(RESPONSE_REQUEST<USER> request, CallContext context = default)
        {
            Db.USER.Remove(request.RR_DATA);

            await Db.SaveChangesAsync();

            return request.RR_DATA;
        }
    }
}

