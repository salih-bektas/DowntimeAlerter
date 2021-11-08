using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DowntimeAlerter.MVC.UI.Models.TargetApp
{
    public class TargetAppEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Url]
        public string Url { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Health Check Interval in Minutes")]
        public int MonitoringInterval { get; set; }
    }
}
