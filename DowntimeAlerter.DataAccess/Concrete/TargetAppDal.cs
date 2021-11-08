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
    public class TargetAppDal : EfEntityRepository<TargetApp>, ITargetAppDal
    {
        public TargetAppDal(DbContext context) : base(context) { }
    }
}
