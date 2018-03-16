using Newtonsoft.Json;

namespace ECommerce.Monitoring.Abstractions
{
    [JsonObject]
    public class RepositorySetting
    {
        [JsonProperty("providerAssembly")]
        public string ProviderAssembly { get; set; }

        [JsonProperty("providerType")]
        public string ProviderType { get; set; }

        [JsonProperty("connectionString")]
        public string ConnectionString { get; set; }

    }
}