using Newtonsoft.Json;

namespace Generator.Shared.Models;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

public class ConnectionStrings
{
    public string DefaultConnection { get; set; }
}

public class Database
{
    public string DatabaseIdentifier { get; set; }
    public string ConnectionString { get; set; }
}

public class Logging
{
    public LogLevel LogLevel { get; set; }
}

public class LogLevel
{
    public string Default { get; set; }

    [JsonProperty("Microsoft.AspNetCore")]
    public string MicrosoftAspNetCore { get; set; }
}

public class AppSettings
{
    public Logging Logging { get; set; }
    public string AllowedHosts { get; set; }
    public List<Database> Database { get; set; }

    public ConnectionStrings ConnectionStrings { get; set; }
}


