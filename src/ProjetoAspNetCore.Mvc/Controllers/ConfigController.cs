using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoAspNetCore.Aplicacao.Extensions;
using ProjetoAspNetCore.Data.ORM;
using ProjetoAspNetCore.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ProjetoAspNetCore.Mvc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ConfigController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ImportMedicamentos([FromServices] CursoDbContext context)
        {
            string filePath = ImportUtils.GetFilePath("Csv", "Medicamentos", ".CSV");
            ReadWriteFile readWriteFile = new ReadWriteFile();
            if (!await readWriteFile.ReadAndWriteCsvAsync(filePath, context))
            {
                return View("JaTemMedicamento", context.Medicamento.AsNoTracking().OrderBy(o=>o.Codigo));
            }
            return View("Medicamentos", context.Medicamento.AsNoTracking().OrderBy(o => o.Codigo));
        }
    }
}
