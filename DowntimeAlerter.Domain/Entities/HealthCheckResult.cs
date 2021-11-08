using DowntimeAlerter.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeAlerter.Domain.Entities
{
    public class HealthCheckResult
    {
        public int TargetAppId { get; set; }
        public DateTime HealthCheckTime { get; set; }
        public HealthStatusEnum Result { get; set; }
        public int StatusCode { get; set; }
    }
}
