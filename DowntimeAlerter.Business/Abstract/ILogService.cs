using DowntimeAlerter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeAlerter.Business.Abstract
{
    public interface ILogService
    {
        Task<List<Log>> GetLogs(DateTime since);
    }
}