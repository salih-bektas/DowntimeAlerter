using DowntimeAlerter.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DowntimeAlerter.MVC.UI.Models.TargetApp
{
    public class HealthCheckResultViewModel
    {
        [Display(Name = "Health Check Time")]
        public DateTime HealthCheckTime { get; set; }
        public HealthStatusEnum Result { get; set; }

        [Display(Name = "Status Code")]
        public int StatusCode { get; set; }
    }
}
