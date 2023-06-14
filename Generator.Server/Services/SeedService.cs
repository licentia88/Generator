using Generator.Server.Helpers;
using Generator.Server.Services.Base;
using Generator.Shared.Models.ServiceModels;
using Generator.Shared.Services;
using MagicOnion;
using Mapster;

namespace Generator.Server.Services;

public class SeedService : MagicBase<ISeedService, KeyValuePair<string, object>[]>, ISeedService
{
    public SeedService(IServiceProvider provider) : base(provider)
    {
    }

    public UnaryResult<RESPONSE_RESULT<List<KeyValuePair<string, object>[]>>> FillAsync(string Database, string tableName, params KeyValuePair<string, object>[] where)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var result = await GetDatabase(Database).QueryAsync($"SELECT * FROM {tableName}", where);

            var test =  result.Adapt<List<KeyValuePair<string, object>[]>>();

            return test;
        });
    }

    
}
