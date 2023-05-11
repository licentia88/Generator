using System;
using Newtonsoft.Json;

namespace Generator.Services.OptionsTemplates;

internal class ConnectionStrings
{
    [JsonProperty(nameof(Connections))]
    public Connections[] Connections { get; set; }
}
