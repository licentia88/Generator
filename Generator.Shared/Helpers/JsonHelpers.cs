using System.Text.Json;
using Generator.Shared.Models;

namespace Generator.Shared.Helpers;


public class JsonHelpers
{

    private static async ValueTask<AppSettings> WriteJson(AppSettings settings)
    {
        var jsonSerialised = JsonSerializer.Serialize(settings);

        var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

        await File.WriteAllTextAsync(path, jsonSerialised);

        return await ReadAppSettingsAsync();
    }

	public static async ValueTask<AppSettings> ReadAppSettingsAsync()
	{
        var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

        var json = await File.ReadAllTextAsync(path);

        AppSettings appsettingsModel = JsonSerializer.Deserialize<AppSettings>(json);

        return appsettingsModel;
    }

    public static async ValueTask<AppSettings> AddDatabase(Database database)
    {
        var jsonObject = await ReadAppSettingsAsync();

        jsonObject.Database.Add(database);

        return await WriteJson(jsonObject);
    }

    public static async ValueTask<AppSettings> Update(Database database)
    {
        var jsonObject = await ReadAppSettingsAsync();

        var itemToUpdate = jsonObject.Database.FirstOrDefault(x => x.DatabaseIdentifier == database.DatabaseIdentifier);

        var index = jsonObject.Database.IndexOf(itemToUpdate);

        jsonObject.Database[index] = database;

        return await WriteJson(jsonObject);
    }

    public static async ValueTask<AppSettings> Remove(Database database)
    {
        var jsonObject = await ReadAppSettingsAsync();

        jsonObject.Database.Remove(database);

        return await WriteJson(jsonObject);
    }
}

