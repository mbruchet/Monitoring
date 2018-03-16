using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using ECommerce.Monitoring.Abstractions;
using ECommerce.Monitoring.Data;
using Microsoft.Extensions.Logging;

namespace ECommerce.Monitoring.Services
{
    public class MonitoringRuleService : IMonitoringRuleService
    {
        private readonly MonitoringRuleRepository _monitoringRuleRepository;
        private readonly IMonitoringAdapter _monitoringAdapter;
        private ConcurrentBag<MonitoringRule> _rules;

        public MonitoringRuleService(MonitoringRuleRepository monitoringRuleRepository, IMonitoringAdapter monitoringAdapter)
        {
            _monitoringRuleRepository = monitoringRuleRepository;
            _monitoringAdapter = monitoringAdapter;

            _monitoringRuleRepository.AfterInsert = rule => _rules.Add(rule);

            _monitoringRuleRepository.AfterUpdate = rule =>
                _rules = new ConcurrentBag<MonitoringRule>(_rules.Where(x => x.Id != rule.Id));

            _monitoringRuleRepository.AfterDelete = rule => _rules = new ConcurrentBag<MonitoringRule>(_rules.Where(x => x.Id != rule.Id));
        }

        public void Push(MonitoringItem monitoringItem)
        {
            if (_rules == null)
            {
                var searchRules = _monitoringRuleRepository.SearchAsync(x => x.IsEnabled).Result;

                if (searchRules != null)
                    _rules = new ConcurrentBag<MonitoringRule>(searchRules.Result);
            }

            if (_rules == null) return;

            foreach (var rule in _rules)
            {
                if (Evaluate(rule, monitoringItem))
                {
                    _monitoringAdapter.WriteEvent(rule, monitoringItem).Wait();
                }
            }
        }

        private bool Evaluate(MonitoringRule rule, MonitoringItem loggingItem)
        {
            var lambdaParser = new NReco.Linq.LambdaParser();
            var varContext = new Dictionary<string, object> { ["LoggingItem"] = loggingItem, ["Warning"] = LogLevel.Warning, ["Error"] = LogLevel.Error, ["Information"] = LogLevel.Information };
            return (bool)lambdaParser.Eval(rule.Rule, varContext);
        }
    }
}
