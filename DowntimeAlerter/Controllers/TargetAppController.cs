using AutoMapper;
using DowntimeAlerter.Business.Abstract;
using DowntimeAlerter.Domain.Entities;
using DowntimeAlerter.MVC.UI.Models.TargetApp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DowntimeAlerter.MVC.UI.Controllers
{
    [Authorize]
    public class TargetAppController : Controller
    {
        private readonly ITargetAppService _targetAppService;
        private readonly IMapper _mapper;
        private readonly IHealthCheckService _healthCheckService;

        public TargetAppController(ITargetAppService targetAppService, 
            IMapper mapper,
            IHealthCheckService healthCheckService)
        {
            _targetAppService = targetAppService;
            _mapper = mapper;
            _healthCheckService = healthCheckService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TargetAppCreateViewModel createViewModel)
        {
            if (ModelState.IsValid)
            {
                var targetApp = _mapper.Map<TargetApp>(createViewModel);
                
                var result = await _targetAppService.CreateTargetAppAsync(targetApp);

                return RedirectToAction("List");
            }
            return View(createViewModel);
        }

        public async Task<IActionResult> ListAsync()
        {
            var targetAppList = await _targetAppService.GetAllTargetAppsAsync();
            var targetAppViewModelList = new List<TargetAppViewModel>();
            foreach (var item in targetAppList)
            {
                targetAppViewModelList.Add(_mapper.Map<TargetAppViewModel>(item));
            }
            return View(targetAppViewModelList);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var targetApp = await _targetAppService.GetByIdAsync(id);
            var editViewModel = _mapper.Map<TargetAppEditViewModel>(targetApp);
            return View(editViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TargetAppEditViewModel editViewModel)
        {
            if (editViewModel.Id == 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var targetApp = await _targetAppService.GetByIdAsync(editViewModel.Id);
                _mapper.Map(targetApp,editViewModel);
                await _targetAppService.UpdateTargetAppAsync(targetApp);
                return RedirectToAction("List");
            }
            return View(editViewModel);
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            await _targetAppService.DeleteAsync(id);

            return RedirectToAction("List");
        }

        public async Task<IActionResult> Detail(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var targetApp = await _targetAppService.GetByIdAsync(id);
            var targetAppViewModel = _mapper.Map<TargetAppDetailViewModel>(targetApp);

            var healthCheckResult = await _healthCheckService.GetHealthCheckResultsByTargetAppAsync(targetApp.Id);

            var healthCheckResultViewModelList = new List<HealthCheckResultViewModel>();
            foreach(var item in healthCheckResult)
            {
                healthCheckResultViewModelList.Add(_mapper.Map<HealthCheckResultViewModel>(item));
            }

            targetAppViewModel.CheckResults = healthCheckResultViewModelList;

            return View(targetAppViewModel);
        }
    }
}
