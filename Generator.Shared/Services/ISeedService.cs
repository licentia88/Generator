using Generator.Shared.Models.ServiceModels;
using Generator.Shared.Services.Base;
using MagicOnion;


namespace Generator.Shared.Services;

public interface ISeedService : IGenericService<ISeedService, KeyValuePair<string, object>[]>
{
    public UnaryResult<RESPONSE_RESULT<List<KeyValuePair<string, object>[]>>> FillAsync(string Database,string tableName, params KeyValuePair<string, object>[] where);
}
