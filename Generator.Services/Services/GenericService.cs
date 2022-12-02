using Grpc.Core;
using Generator.Service;
using Generator.Shared.Services;
using Generator.Shared.Models;
using ProtoBuf.Grpc;
using Generator.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System;
using Generator.Service.Helpers;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using System.Text;
using System.Xml.Linq;
using MsgPack.Serialization;
using Generator.Service.Extensions;
using Mapster;
using Generator.Services;
using Generator.Shared.TEST_WILL_DELETE_LATER;

namespace Generator.Service.Services;

public class GenericService : IGenericService
{
    TestContext Db { get; set; }

 
    public GenericService(IServiceProvider provider)
    {
        Db = provider.GetService<TestContext>()!;         
    }

 
    public async ValueTask<RESPONSE_RESULT> QueryAsync(CallContext context = default)
    {
        return await Delegates.ExecuteAsync(async () =>
        {
            var result = await Db.Database.GetDbConnection().QueryAsync($"SELECT * FROM TEST_TABLE");

            return result;
        });
    }

    public async ValueTask<RESPONSE_RESULT> AddAsync(RESPONSE_REQUEST request, CallContext context = default)
    {
        return await Delegates.ExecuteAsync(async () =>
        {
            var data = request.RR_DATA.Deserialize<List<Dictionary<string, object>>>();

            var adaptedType = data.Adapt<List<TEST_TABLE>>();
            var data2 = request.RR_DATA.Deserialize<List<TEST_TABLE>>();
           
            await  Db.AddAsync(adaptedType);

            await Db.SaveChangesAsync();

            return request.RR_DATA;
        });
    }
}
