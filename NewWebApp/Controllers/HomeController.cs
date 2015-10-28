using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using NewWebApp.ViewModels.Home;

namespace NewWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Hello World!!!";

            var serverInfoVM = new ServerInfoViewModel()
            {
                Name = Environment.MachineName,
                Software = Environment.OSVersion.ToString(),
                LocalAddress = "127.0.0.1"
            };


            return View(serverInfoVM);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
