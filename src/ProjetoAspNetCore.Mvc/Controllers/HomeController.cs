using Microsoft.AspNetCore.Identity.UI.Services;
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
        private readonly IEmailSender _emailSender;

        public HomeController(ILogger<HomeController> logger, IEmailSender emailSender)
        {
            _logger = logger;
            _emailSender = emailSender;
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
        public async Task<IActionResult> Contato(ContatoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _emailSender.SendEmailAsync(model.Email, model.Assunto, model.Mensagem);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return View();
        }
    
        public IActionResult BoxInit()
        {
            return View();
        }

    }
}
