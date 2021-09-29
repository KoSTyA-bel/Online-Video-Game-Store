using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GameStore.Models;
using Microsoft.AspNetCore.Authorization;

namespace GameStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            string name = "Anonymous";

            if (User.Identity.IsAuthenticated)
            {
                name = User.Identity.Name;
            }

            ViewBag.Nick = name;
            
            return View();
        }
    }
}
