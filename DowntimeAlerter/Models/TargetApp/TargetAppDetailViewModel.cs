using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DowntimeAlerter.MVC.UI.Models.TargetApp
{
    public class TargetAppDetailViewModel
    {
        public string Name { get; set; }
        public string Url { get; set; }

        [Display(Name = "Health Check Interval")]
        public int MonitoringInterval { get; set; }

        [Display(Name = "Health Check Results")]
        public IList<HealthCheckResultViewModel> CheckResults { get; set; }
    }
}
