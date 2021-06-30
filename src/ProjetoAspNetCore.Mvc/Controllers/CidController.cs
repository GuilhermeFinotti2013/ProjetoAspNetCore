using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ProjetoAspNetCore.Aplicacao.Extensions;
using ProjetoAspNetCore.Domain.Entities;
using ProjetoAspNetCore.Data.ORM;
using System.Text;
using X.PagedList;
using System;

namespace ProjetoAspNetCore.Mvc.Controllers
{
    public class CidController : Controller
    {
        private readonly CursoDbContext _context;

        public CidController(CursoDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? pagina, string ordenacao, string stringBusca)
        {
            const int itensPorPagina = 8;
            int numeroPagina = (pagina ?? 1);

            ViewData["ordenacao"] = ordenacao;
            ViewData["filtroAtual"] = stringBusca;

            var cids = from c in _context.Cid select c;

            #region Filtro
            if (!string.IsNullOrEmpty(stringBusca))
            {
                cids = cids.Where(s => s.Codigo.Contains(stringBusca) || s.Diagnostico.Contains(stringBusca));
            }
            #endregion

            #region Ordenação
            ViewData["OrderByInternalId"] = string.IsNullOrEmpty(ordenacao) ? "CidInternalId_desc" : "";
            ViewData["OrderByCodigo"] = ordenacao == "Codigo" ? "Codigo_desc" : "Codigo";
            ViewData["OrderByDiagnostico"] = ordenacao == "Diagnostico" ? "Diagnostico_desc" : "Diagnostico";

            if (string.IsNullOrEmpty(ordenacao))
            {
                ordenacao = "CidInternalId";
            }
            if (ordenacao.EndsWith("_desc"))
            {
                ordenacao = ordenacao.Substring(0, ordenacao.Length - 5);
                cids = cids.OrderByDescending(x => EF.Property<object>(x, ordenacao));
            }
            else
            {
                cids = cids.OrderBy(x => EF.Property<object>(x, ordenacao));
            }
            #endregion
            return View(await cids.AsNoTracking().ToPagedListAsync(numeroPagina, itensPorPagina));
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
        // CRUD Aqui
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {

            var cid = await _context.Cid.FirstOrDefaultAsync(m => m.Id == id);
            if (cid == null) return NotFound();

            return View(cid);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cid cid)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(cid);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cid);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var cid = await _context.Cid.FindAsync(id);
            if (cid == null) return NotFound();

            return View(cid);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Cid cid)
        {
            if (id != cid.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cid);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CidExists(cid.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cid);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var cid = await _context.Cid.FirstOrDefaultAsync(m => m.Id == id);
            if (cid == null) NotFound();

            return View(cid);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cid = await _context.Cid.FindAsync(id);
            _context.Cid.Remove(cid);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CidExists(Guid id)
        {
            return _context.Cid.Any(x => x.Id == id);
        }
    }
}
