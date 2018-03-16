using System.Threading.Tasks;
using Ecommerce.Data.RepositoryStore;
using ECommerce.Monitoring.Abstractions;
using ECommerce.Monitoring.Data;

namespace ECommerce.Monitoring.Services
{
    public class MonitoringService : IMonitoringService
    {
        private readonly MonitoringItemRepository _repositoryStore;

        public MonitoringService(MonitoringItemRepository repositoryStore)
        {
            _repositoryStore = repositoryStore;
        }

        public async Task<MonitoringItem> WriteEvent(MonitoringItem item)
        {
            var result = await _repositoryStore.AddAsync(item);
            return result.IsSuccessful ? result.Result : null;
        }
    }
}