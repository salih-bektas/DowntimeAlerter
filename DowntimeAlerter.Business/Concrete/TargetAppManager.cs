using DowntimeAlerter.Business.Abstract;
using DowntimeAlerter.DataAccess.Abstract;
using DowntimeAlerter.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeAlerter.Business.Concrete
{
    public class TargetAppManager : ITargetAppService
    {
        private readonly ITargetAppDal _targetAppDal;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHangfireJobService _hanfireJobManager;

        public TargetAppManager(ITargetAppDal targetAppDal,
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IHangfireJobService hanfireJobManager)
        {
            _targetAppDal = targetAppDal;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _hanfireJobManager = hanfireJobManager;
        }

        public async Task<TargetApp> CreateTargetAppAsync(TargetApp targetApp)
        {

            targetApp.CreatedBy = getCurrentUser().Email;
            targetApp.CreatedDate = DateTime.Now;
            targetApp.LastModifiedDate = DateTime.Now;

            var result = await _targetAppDal.InsertAsync(targetApp);

            if(result.Id > 0)
            {
                _hanfireJobManager.AddOrUpdateHealthCheckJob(result.Id, result.MonitoringInterval, result.CreatedBy);
            }

            return result;
        }

        public Task DeleteAsync(int id)
        {
            var targetApp = _targetAppDal.GetAsync(t => t.Id == id);
            return _targetAppDal.DeleteAsync(targetApp.Result);
        }

        public async Task<List<TargetApp>> GetAllTargetAppsAsync()
        {
            var isAdministrator = await _userManager.IsInRoleAsync(getCurrentUser(), "Administrator");
            if (isAdministrator)
            {
                return await _targetAppDal.GetListAsync();
            }
            else
            {
                var result = await _targetAppDal.GetListAsync(t => t.CreatedBy == getCurrentUser().Id);
                return result;
            }
        }

        public Task<TargetApp> GetByIdAsync(int id)
        {
            return _targetAppDal.GetAsync(t => t.Id == id);
        }

        public async Task<TargetApp> UpdateTargetAppAsync(TargetApp targetApp)
        {
            targetApp.LastModifiedDate = DateTime.Now;

            var result = await _targetAppDal.UpdateAsync(targetApp);

            if (result.Id > 0)
            {
                _hanfireJobManager.AddOrUpdateHealthCheckJob(result.Id, result.MonitoringInterval, result.CreatedBy);
            }

            return result;
        }

        private IdentityUser getCurrentUser()
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            return _userManager.FindByIdAsync(userId).Result;
        }

        //public void Delete(int id)
        //{
        //    var obj = _targetAppDal.Get(t => t.Id == id);
        //    _targetAppDal.Delete(obj);
        //}

        //public TargetApp Get(Expression<Func<TargetApp, bool>> filter)
        //{
        //    return _targetAppDal.Get(filter);
        //}

        //public List<TargetApp> GetAll()
        //{
        //    return _targetAppDal.GetList();
        //}

        //public TargetApp GetById(int id)
        //{
        //    return _targetAppDal.Get(t => t.Id == id);
        //}

        //public List<TargetApp> GetList(Expression<Func<TargetApp, bool>> filter)
        //{
        //    return _targetAppDal.GetList(filter);
        //}

        //public TargetApp Insert(TargetApp obj)
        //{
        //    return _targetAppDal.Add(obj);
        //}

        //public TargetApp Update(TargetApp obj)
        //{
        //    return _targetAppDal.Update(obj);
        //}
    }
}
