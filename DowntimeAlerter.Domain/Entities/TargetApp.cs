using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeAlerter.Domain.Entities
{
    public class TargetApp: BaseObject
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int MonitoringInterval { get; set; }
        public string CreatedById { get; set; }
        //public virtual ApplicationUser CreatedBy { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }

    }
}
