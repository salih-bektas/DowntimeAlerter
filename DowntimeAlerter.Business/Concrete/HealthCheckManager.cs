using DowntimeAlerter.Business.Abstract;
using DowntimeAlerter.Business.Notification;
using DowntimeAlerter.DataAccess.Abstract;
using DowntimeAlerter.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeAlerter.Business.Concrete
{
    public class HealthCheckManager : IHealthCheckService
    {
        private readonly ITargetAppDal _targetAppDal;
        private readonly IHttpClientFactory _clientFactory;
        private readonly INotificationSender _notificationSender;
        private readonly IHealthCheckDal _healthCheckDal;

        public HealthCheckManager(ITargetAppDal targetAppDal, 
            IHttpClientFactory clientFactory,
            INotificationSender notificationSender,
            IHealthCheckDal healthCheckDal)
        {
            _targetAppDal = targetAppDal;
            _clientFactory = clientFactory;
            _notificationSender = notificationSender;
            _healthCheckDal = healthCheckDal;
        }

        public async Task CheckHealth(int targetAppId, string userMail)
        {
            var target = await _targetAppDal.GetAsync(t => t.Id == targetAppId);
            var check = new Domain.Entities.HealthCheckResult
            {
                TargetAppId = targetAppId,
                StatusCode = 0,
                HealthCheckTime = DateTime.Now,
                Result = target == null ? HealthStatusEnum.NotFound : HealthStatusEnum.Error
            };
            try
            {
                if (target != null)
                {
                    using (var client = _clientFactory.CreateClient())
                    {

                        using (var response = await client.GetAsync(target.Url))
                        {
                            check.StatusCode = (int)response.StatusCode;
                            check.Result = response.IsSuccessStatusCode ? HealthStatusEnum.Success : HealthStatusEnum.UnSuccess;

                            if (!response.IsSuccessStatusCode)
                            {
                                var subject = $"Downtime Alerter : {target.Name} service is down";
                                var message = $"<p>{target.Url} could not be reached at {check.HealthCheckTime}</p>";
                                await _notificationSender.SendNotificationAsync(userMail, subject, message);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                var subject = $"Downtime Alerter : {target.Name} is not reached";
                var message = $"<p>Error received during check url {target.Url} at {check.HealthCheckTime}</p><p>{ex.Message}</p> ";
                await _notificationSender.SendNotificationAsync(userMail, subject, message);
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                await _healthCheckDal.InsertAsync(check);
            }
        }

        public Task<List<Domain.Entities.HealthCheckResult>> GetHealthCheckResultsByTargetAppAsync(int targetAppId)
        {
            var healthChecks = _healthCheckDal.GetListAsync(t => t.TargetAppId == targetAppId);
            return healthChecks;
        }
    }
}
