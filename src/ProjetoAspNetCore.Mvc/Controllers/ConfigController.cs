using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly CursoDbContext _context;

        public ConfigController(CursoDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ImportMedicamentos()
        {
            var k = 0;
            string linha;

            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var csvPath = Path.Combine(outPutDirectory, "Csv\\Medicamentos.CSV");
            string filePath = new Uri(csvPath).LocalPath;
            using (FileStream fs = System.IO.File.OpenRead(filePath))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    while ((linha = reader.ReadLine()) != null)
                    {
                        if (k > 0)
                        {
                            var partes = linha.Split(';');
                            _context.Add(new Medicamento
                            {
                                Codigo = int.Parse(partes[0]),
                                Descricao = partes[1],
                                Generico = partes[2],
                                CodigoGenerico = int.Parse(partes[3])
                            }) ;
                        }
                        k++;
                    }
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("CsvImportado");
        }

        public IActionResult CsvImportado()
        {
            return View();
        }
    }
}
