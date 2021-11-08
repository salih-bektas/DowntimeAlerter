using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeAlerter.Domain.Entities
{
    public abstract class BaseObject
    {
        [Required]
        [Key]
        public virtual int Id { get; set; }

    }
}
