using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ECommerce.Monitoring.Abstractions
{
    public class MonitoringRule
    {
        [Key]
        public string Id { get; set; }

        [JsonProperty("rule")]
        public string Rule { get; set; }

        [JsonProperty("isEnabled")]
        public bool IsEnabled { get; set; } = true;
    }
}
