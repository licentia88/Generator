using Newtonsoft.Json;

namespace Generator.Server.OptionsTemplates;

internal class Connections
{
    [JsonProperty(nameof(Name))]
    public string Name { get; set; }

    [JsonProperty(nameof(ConnectionString))]
    public string ConnectionString { get; set; }

    [JsonProperty(nameof(Provider))]
    public string Provider { get; set; }
}
