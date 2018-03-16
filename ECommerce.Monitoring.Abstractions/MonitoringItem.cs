using System;

namespace ECommerce.Monitoring.Abstractions
{
    public class MonitoringItem
    {
        public Guid Id { get; set; }
        public string Application { get; set; }
        public string Service { get; set; }
        public string ServerName { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }
}