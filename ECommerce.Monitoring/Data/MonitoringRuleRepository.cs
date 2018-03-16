using System.Diagnostics;
using Ecommerce.Data.RepositoryStore;
using ECommerce.Monitoring.Abstractions;
using Microsoft.Extensions.Logging;

namespace ECommerce.Monitoring.Data
{
    public class MonitoringRuleRepository:RepositoryStoreFactory<MonitoringRule>
    {
        public MonitoringRuleRepository(string assembly, ConnectionOptions connectionOptions, ILoggerFactory loggerFactory, DiagnosticSource diagnosticSource) : 
            base(assembly, connectionOptions, loggerFactory, diagnosticSource)
        {
        }
    }
}
