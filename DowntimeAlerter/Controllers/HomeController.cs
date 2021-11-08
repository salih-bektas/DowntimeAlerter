using DowntimeAlerter.MVC.UI.Models.Error;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DowntimeAlerter.MVC.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public ActionResult Index()
        {
            return View();
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var error = new ErrorViewModel
            {
                ExceptionPath = exceptionDetails.Path,
                ExceptionMessage = exceptionDetails.Error.Message,
                StackTrace = exceptionDetails.Error.StackTrace
            };
            
            return View(error);
        }
    }
}
