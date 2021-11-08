using DowntimeAlerter.DataAccess.Repository;
using DowntimeAlerter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeAlerter.DataAccess.Abstract
{
    public interface ILogDal : IEntityRepository<Log>
    {
    }
}
