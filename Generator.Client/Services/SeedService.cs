using Generator.Client.Services.Base;
using Generator.Shared.Models.ServiceModels;
using Generator.Shared.Services;
using MagicOnion;

namespace Generator.Client.Services;

[RegisterSingleton]
public class SeedService : ServiceBase<ISeedService, KeyValuePair<string, object>[]>, ISeedService
{
    public UnaryResult<RESPONSE_RESULT<List<KeyValuePair<string, object>[]>>> FillAsync(string Database, string tableName, params KeyValuePair<string, object>[] where)
    {
        return Client.FillAsync(Database, tableName, where);

    }
}
