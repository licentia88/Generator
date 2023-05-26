using Newtonsoft.Json;

namespace Generator.Server.OptionsTemplates;

internal class ConnectionStrings
{
    [JsonProperty(nameof(Connections))]
    public Connections[] Connections { get; set; }
}
