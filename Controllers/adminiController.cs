using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AppMuebles.Controllers
{
    
    public class adminiController : Controller
    {
        private readonly ILogger<adminiController> _logger;

        public adminiController(ILogger<adminiController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}