using Newtonsoft.Json;

namespace ECommerce.Monitoring.Abstractions
{
    [JsonObject]
    public class AdapterSettings
    {
        [JsonProperty("adapterAssembly")]
        public string AdapterAssembly { get; set; }
        [JsonProperty("adapterType")]
        public string AdapterType { get; set; }
        [JsonProperty("adapterParameters")]
        public string AdapterParameters { get; set; }
    }
}
