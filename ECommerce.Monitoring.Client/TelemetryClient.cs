using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace ECommerce.Monitoring.Client
{
    public class TelemetryClient
    {
        private readonly HttpClient _client;

        public TelemetryClient(string remoteUrl, HttpClient client = null)
        {
            _client = client ?? new HttpClient { BaseAddress = new Uri(remoteUrl) };
        }

        public void RecordEvent(string name, int value)
        {
            _client.PostAsync("api/monitoring", new StringContent(JsonConvert.SerializeObject(new {name, value}), Encoding.UTF8, "application/json"));
        }
    }
}
