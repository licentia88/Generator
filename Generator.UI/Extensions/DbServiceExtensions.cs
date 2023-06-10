using System.Collections.Generic;
using Generator.Client;
using Generator.Shared.Models.ComponentModels;
using Mapster;

namespace Generator.UI.Extensions;

public static class DbServiceExtensions
{
    public static void RegisterStaticData(this IServiceCollection collection)
    {
        collection.AddSingleton<List<PERMISSIONS>>();
    }

    public static async Task FillAsync<TModel>(this IServiceProvider provider,string Database, params KeyValuePair<string, object>[] Where)
	{
        ///Wait for server to run
        await Task.Delay(10000);
        using var scope = provider.CreateAsyncScope();

        var tableToFill = scope.ServiceProvider.GetService<List<TModel>>();

        var service = scope.ServiceProvider.GetService<SeedService>();

		var result = await service.FillAsync(Database,typeof(TModel).Name, Where);

        tableToFill.AddRange(result.Data.Adapt<List<TModel>>());

    }	
}
