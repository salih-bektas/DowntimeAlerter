using DowntimeAlerter.DataAccess.Abstract;
using DowntimeAlerter.DataAccess.Repository;
using DowntimeAlerter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeAlerter.DataAccess.Concrete
{
    public class HealthCheckDal : EfEntityRepository<HealthCheckResult>, IHealthCheckDal
    {
        public HealthCheckDal(DbContext context) : base(context) { }
    }
}
