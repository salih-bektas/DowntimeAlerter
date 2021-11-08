using AutoMapper;
using DowntimeAlerter.Business.Abstract;
using DowntimeAlerter.MVC.UI.Models.Log;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DowntimeAlerter.MVC.UI.Controllers
{
    public class LogController : Controller
    {
        private readonly ILogService _logService;
        private readonly IMapper _mapper;

        public LogController(ILogService logService,
            IMapper mapper)
        {
            _logService = logService;
            _mapper = mapper;
        }

        public async Task<IActionResult> ListAsync()
        {
            var list = (await _logService.GetLogs(DateTime.Now.AddDays(-5))).OrderByDescending(t => t.TimeStamp);
            var logViewModelList = new List<LogViewModel>();
            foreach(var item in list)
            {
                logViewModelList.Add(_mapper.Map<LogViewModel>(item));
            }
            return View(logViewModelList);
        }
    }
}
