using Generator.Shared.Models.ServiceModels;
using Generator.Shared.Services;
using MagicOnion;

namespace Generator.Client;

public class SeedService : ServiceBase<ISeedService, IDictionary<string, object>>, ISeedService
{
    

    public UnaryResult<RESPONSE_RESULT<List<IDictionary<string, object>>>> FillAsync(string Database, string tableName, params KeyValuePair<string, object>[] where)
    {
        return Client.FillAsync(Database, tableName, where);
    }
}
