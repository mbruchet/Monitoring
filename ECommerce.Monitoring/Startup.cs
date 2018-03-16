using System.Diagnostics;
using Ecommerce.Data.RepositoryStore;
using ECommerce.Core;
using ECommerce.Monitoring.Abstractions;
using ECommerce.Monitoring.Data;
using ECommerce.Monitoring.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ECommerce.Monitoring
{
    public class Startup
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly DiagnosticSource _diagnosticSource;
        private readonly RepositorySetting _monitoringRuleRepositorySettings;
        private readonly RepositorySetting _monitoringItemRepositorySettings;
        private readonly AdapterSettings _adapterSettings;

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory, DiagnosticSource diagnosticSource)
        {
            _loggerFactory = loggerFactory;
            _diagnosticSource = diagnosticSource;
            Configuration = configuration;

            _monitoringRuleRepositorySettings = new RepositorySetting();
            configuration.GetSection("MonitoringRuleRepository").Bind(_monitoringRuleRepositorySettings);

            _monitoringItemRepositorySettings = new RepositorySetting();
            configuration.GetSection("MonitoringItemRepositorySettings").Bind(_monitoringRuleRepositorySettings);

            _adapterSettings = new AdapterSettings();
            configuration.GetSection("AdapterSettings").Bind(_monitoringRuleRepositorySettings);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new MonitoringRuleRepository(_monitoringRuleRepositorySettings.ProviderAssembly, new ConnectionOptions
            {
                Provider = _monitoringRuleRepositorySettings.ProviderType,
                ConnectionString = _monitoringRuleRepositorySettings.ConnectionString,
                IntervalRetry = 30,
                RetryCount = 3
            }, _loggerFactory, _diagnosticSource));

            services.AddSingleton(new MonitoringItemRepository(_monitoringItemRepositorySettings.ProviderAssembly, new ConnectionOptions
            {
                Provider = _monitoringItemRepositorySettings.ProviderType,
                ConnectionString = _monitoringItemRepositorySettings.ConnectionString,
                IntervalRetry = 30,
                RetryCount = 3
            }, _loggerFactory, _diagnosticSource));

            services.AddSingleton(provider => PluginContainer.GetInstance<IMonitoringAdapter>(_adapterSettings.AdapterAssembly, 
                _adapterSettings.AdapterParameters));

            services.AddSingleton<IMonitoringRuleService, IMonitoringRuleService>();
            services.AddSingleton<IMonitoringService, MonitoringService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
