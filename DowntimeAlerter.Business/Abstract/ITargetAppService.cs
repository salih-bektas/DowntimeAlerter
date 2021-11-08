using DowntimeAlerter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeAlerter.Business.Abstract
{
    public interface ITargetAppService
    {
        Task<TargetApp> CreateTargetAppAsync(TargetApp targetApp);
        Task<List<TargetApp>> GetAllTargetAppsAsync();
        Task<TargetApp> GetByIdAsync(int id);
        Task<TargetApp> UpdateTargetAppAsync(TargetApp targetApp);

        Task DeleteAsync(int id);

    }
}
