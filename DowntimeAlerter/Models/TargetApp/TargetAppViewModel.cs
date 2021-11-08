using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DowntimeAlerter.MVC.UI.Models.TargetApp
{
    public class TargetAppViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        [Display(Name = "Health Check Interval in Minutes")]
        public int MonitoringInterval { get; set; }

        [Display(Name = "Creator")]
        public string CreatedBy { get; set; }

        [Display(Name = "Creation Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Last Update Date")]
        public DateTime LastModifiedDate { get; set; }
    }
}
