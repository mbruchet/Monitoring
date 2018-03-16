using System.Threading.Tasks;
using ECommerce.Monitoring.Abstractions;
using ECommerce.Monitoring.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Monitoring.Controllers
{
    [Route("api/[controller]")]
    public class MonitoringController : Controller
    {
        private readonly IMonitoringService _monitoringService;

        public MonitoringController(IMonitoringService monitoringService)
        {
            _monitoringService = monitoringService;
        }

        [HttpPost]
        public async Task<IActionResult> WriteEvent(MonitoringItem monitoringItem)
        {
            var item = await _monitoringService.WriteEvent(monitoringItem);
            return Ok(item);
        }
    }
}