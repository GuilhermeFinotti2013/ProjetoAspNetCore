using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjetoAspNetCore.Mvc.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetCore.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet("fale-conosco")]
        public IActionResult Contato()
        {
            return View();
        }

        [HttpPost]
        [Route("fale-conosco")]
        public IActionResult Contato(ContatoViewModel model)
        {
            return View();
        }
    
        public IActionResult BoxInit()
        {
            return View();
        }

    }
}
