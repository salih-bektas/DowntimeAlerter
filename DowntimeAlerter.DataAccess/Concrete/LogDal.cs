using DowntimeAlerter.DataAccess.Abstract;
using DowntimeAlerter.DataAccess.Repository;
using DowntimeAlerter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeAlerter.DataAccess.Concrete
{
    public class LogDal : EfEntityRepository<Log>, ILogDal
    {
        public LogDal(DbContext context) : base(context) { }
    }
}
