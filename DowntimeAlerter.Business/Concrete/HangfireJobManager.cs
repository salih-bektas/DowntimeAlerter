using DowntimeAlerter.Business.Abstract;
using Hangfire;
using Hangfire.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeAlerter.Business.Concrete
{
    public class HangfireJobManager : IHangfireJobService
    {
        private readonly IRecurringJobManager _jobManager;
        private readonly IHealthCheckService _healthCheckManager;

        public HangfireJobManager(IRecurringJobManager jobManager,
            IHealthCheckService healthCheckManager)
        {
            _jobManager = jobManager;
            _healthCheckManager = healthCheckManager;
        }

        public void AddOrUpdateHealthCheckJob(int id, int intervalInMinutes, string userMail)
        {
            var job = Job.FromExpression(() => HealthCheckJob(id, userMail));
            _jobManager.AddOrUpdate(id.ToString(), job, cronExpression: $"*/{intervalInMinutes} * * * *",
               new RecurringJobOptions { TimeZone = TimeZoneInfo.Utc, QueueName = "default" });
        }

        public void RemoveHealthCheckJob(int id)
        {
            _jobManager.RemoveIfExists(id.ToString());
        }

        [AutomaticRetry(Attempts = 0)]
        public async Task HealthCheckJob(int id, string userMail)
        {
            await _healthCheckManager.CheckHealth(id, userMail);
        }
    }
}
