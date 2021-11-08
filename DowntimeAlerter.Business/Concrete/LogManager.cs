using DowntimeAlerter.Business.Abstract;
using DowntimeAlerter.DataAccess.Abstract;
using DowntimeAlerter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeAlerter.Business.Concrete
{
    public class LogManager : ILogService
    {
        private readonly ILogDal _logDal;

        public LogManager(ILogDal logDal)
        {
            _logDal = logDal;
        }

        public Task<List<Log>> GetLogs(DateTime since)
        {
            var logs = _logDal.GetListAsync(t => t.TimeStamp > since);
            return logs;
        }
    }
}
