using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ProjetoAspNetCore.Aplicacao.Extensions;
using ProjetoAspNetCore.Domain.Models;
using ProjetoAspNetCore.Data.ORM;
using System.Text;

namespace ProjetoAspNetCore.Mvc.Controllers
{
    public class CidController : Controller
    {
        private readonly CursoDbContext _context;

        public CidController(CursoDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Cid.AsNoTracking().Where(c => c.CidInternalId < 101).OrderBy(o => o.CidInternalId).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> ImportCid(IFormFile file, [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            #region Validando o arquivo
            if (!ArquivoValido.EhValido(file, "Cid.csv"))
            {
                return RedirectToAction("ArquivoInvalido");
            }
            #endregion

            string filePath = $"{webHostEnvironment.WebRootPath}\\importFiles\\{file.FileName}";
            CopiarArquivo.Copiar(file, filePath);

            #region Gravação dos registros na base
            int k = 0;
            string linha;
            List<Cid> listaDeCids = new List<Cid>();
            using (FileStream fileStream = System.IO.File.OpenRead(filePath))
            {
                using (StreamReader streamReader = new StreamReader(fileStream, encoding: Encoding.GetEncoding(1252), false))
                {
                    while ((linha = streamReader.ReadLine()) != null)
                    {
                        if (k > 0)
                        {
                            var partes = linha.Split(';');
                            if (!_context.Cid.Any(e => e.CidInternalId == int.Parse(partes[0])))
                            {
                                listaDeCids.Add(new Cid {
                                    CidInternalId = int.Parse(partes[0]),
                                    Codigo = partes[1],
                                    Diagnostico = partes[2]
                                });
                            }
                        }
                        k++;
                    }
                    if (listaDeCids.Any())
                    {
                        await _context.AddRangeAsync(listaDeCids);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            #endregion

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ArquivoInvalido()
        {
            TempData["arquivoInvalido"] = "O arquivo não é válido";
            return View();
        }

    }
}
