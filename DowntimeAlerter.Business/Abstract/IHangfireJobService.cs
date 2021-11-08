using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeAlerter.Business.Abstract
{
    public interface IHangfireJobService
    {
        void AddOrUpdateHealthCheckJob(int id, int intervalInMinutes, string userMail);
        void RemoveHealthCheckJob(int id);
        Task HealthCheckJob(int id, string userMail);
    }
}
