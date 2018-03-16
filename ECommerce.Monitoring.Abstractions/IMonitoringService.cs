using System.Threading.Tasks;

namespace ECommerce.Monitoring.Abstractions
{
    public interface IMonitoringService
    {
        Task<MonitoringItem> WriteEvent(MonitoringItem item);
    }
}