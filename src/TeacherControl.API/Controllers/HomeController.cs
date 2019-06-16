using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TeacherControl.Infraestructure.Logger;
using TeacherControl.Common.Enums;

namespace TeacherControl.API.Controllers
{
    [Route("api")]
    public class HomeController : Controller
    {
        public ILogger<Controller> _Logger { get; set; }

        public HomeController(ILogger<Controller> logger)
        {
            _Logger = logger;
        }
        public IActionResult Index()
        {
            string message = "The Api is Currently Running...";
            _Logger.Emit(LoggerEventStatus.GET_ITEM, "The Api is Currently Running...");

            return Json(message);
        }
    }
}