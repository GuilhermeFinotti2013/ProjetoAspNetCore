using KissLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetCore.Mvc.Controllers
{
    public class LoggerController : Controller
    {
        private readonly ILogger _logger;

        public LoggerController(ILogger logger)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index()
        {
            string nomeUsurio = HttpContext.User.Identity.Name;

            _logger.Trace(String.Format("O usuário {0} fez isso", nomeUsurio));

            try
            {
                throw new Exception("ATENÇÃO. \n UM ERRO (PROPOSITAL) OCORREU. \n CONTATE O ADMINISTRADOR DO SISTEMA.");
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex} - Usuário logado: {nomeUsurio}");
            }

            return View();
        }
    }
}
