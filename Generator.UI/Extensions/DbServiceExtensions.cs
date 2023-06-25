using Generator.Client.Services;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Models.ComponentModels.Abstracts;
using Generator.Shared.Services;
using Generator.Shared.Services.Base;
using Mapster;

namespace Generator.UI.Extensions;

public static class DbServiceExtensions
{
    public static void RegisterStaticData(this IServiceCollection collection)
    {
        collection.AddSingleton<List<GRID_M>>();
        collection.AddSingleton<List<GRID_D>>();
        collection.AddSingleton<List<PERMISSIONS>>();
        collection.AddSingleton<List<COMPONENTS_BASE>>();
        collection.AddSingleton<List<ROLES>>();

    }


    //public static async Task FillComponentsAndPermissions(this IServiceProvider provider)
    //{
    //    await Task.Delay(5000);

    //    using var scope = provider.CreateAsyncScope();

    //    var componentsTable = scope.ServiceProvider.GetService<List<COMPONENTS_BASE>>();

    //    var permissionsTable = scope.ServiceProvider.GetService<List<PERMISSIONS>>();

    //    var service = scope.ServiceProvider.GetService<ComponentsBaseService>();

    //    var permissionsList = await service.FillPermissions();

    //    var result = await service.FillComponentAndPermissions();
    //     componentsTable.AddRange(result);

    //    var permissions = result.SelectMany(x => x.PERMISSIONS).ToList();

    //    permissionsTable.AddRange(permissions);
    //}


    public static async Task FillAsync<TModel, TIService>(this IServiceProvider provider)
        where TIService : IGenericService<TIService, TModel>
    {
        //Wait for server to run
        await using var scope = provider.CreateAsyncScope();

        var tableToFill = scope.ServiceProvider.GetService<List<TModel>>();

        var service = scope.ServiceProvider.GetService<TIService>();

        var result = await service.ReadAll();

        tableToFill.AddRange(result);

    }

     

    public static async Task FillAsync<TModel>(this IServiceProvider provider,string Database, params KeyValuePair<string, object>[] Where)
	{
        //Wait for server to run
        await Task.Delay(10000);
        await using var scope = provider.CreateAsyncScope();

        var tableToFill = scope.ServiceProvider.GetService<List<TModel>>();

        var service = scope.ServiceProvider.GetService<SeedService>();

		var result = await service.FillAsync(Database,typeof(TModel).Name, Where);

        tableToFill.AddRange(result.Data.Adapt<List<TModel>>());

    }	
}
