using System.Threading.Tasks;

namespace ECommerce.Monitoring.Abstractions
{
    public interface IMonitoringAdapter
    {
        Task WriteEvent(MonitoringRule rule, MonitoringItem monitoringItem);
    }
}
