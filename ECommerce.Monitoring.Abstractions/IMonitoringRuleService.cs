namespace ECommerce.Monitoring.Abstractions
{
    public interface IMonitoringRuleService
    {
        void Push(MonitoringItem monitoringItem);
    }
}