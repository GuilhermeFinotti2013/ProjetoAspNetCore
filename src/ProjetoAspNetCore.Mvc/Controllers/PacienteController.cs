using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoAspNetCore.Data.ORM;
using ProjetoAspNetCore.Domain.Models;
using ProjetoAspNetCore.Mvc.Infra.Enums;
using ProjetoAspNetCore.Mvc.ViewModel;
using ProjetoAspNetCore.Mvc.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoAspNetCore.Mvc.Infra.TOs;

namespace ProjetoAspNetCore.Mvc.Controllers
{
    //    [Authorize(Roles = "Admin")]
    public class PacienteController : Controller
    {
        private readonly CursoDbContext _context;

        public PacienteController(CursoDbContext context)
        {
            _context = context;
        }

        // GET: Paciente
        public async Task<IActionResult> Index()
        {
            var registrosObtidos = _context.Paciente.Include(e => e.EstadoPaciente).ToList();
            List<PacienteViewModel> listaExibicao = ConverterParaViewModels(registrosObtidos);
            
            return View(listaExibicao);
        }

        // GET: Paciente/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente.Include(x => x.EstadoPaciente)
                .AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // GET: Paciente/Create
        public IActionResult Create()
        {
            TOConfiguracaoFormulario configuracaoFormulario = new TOConfiguracaoFormulario();
            configuracaoFormulario.Titulo = "Adicionar novo paciente";
            configuracaoFormulario.TipoDoFormulario = TipoFormulario.InserirEspecializado;
            configuracaoFormulario.Dado = new Paciente();
            configuracaoFormulario.ConfiguracaoCampos = new List<TOConfiguracaoCampo>();
            configuracaoFormulario.ConfiguracaoCampos.Add(new TOConfiguracaoCampo("Ativo", TipoCampo.SimNao, null));
            configuracaoFormulario.ConfiguracaoCampos.Add(new TOConfiguracaoCampo("Sexo", TipoCampo.Combo,
                ConversorDeEnuns.EnumParaSelectItem(TipoEnunsConvertiveis.Sexo)));
            configuracaoFormulario.ConfiguracaoCampos.Add(new TOConfiguracaoCampo("TipoPaciente", TipoCampo.Combo,
                            ConversorDeEnuns.EnumParaSelectItem(TipoEnunsConvertiveis.TipoPaciente)));
            configuracaoFormulario.ConfiguracaoCampos.Add(new TOConfiguracaoCampo("EstadoPacienteId", TipoCampo.Combo,
                            EstadoPacientesParaSelectItens(_context.EstadoPaciente.ToList())));
            configuracaoFormulario.TamalhoLabel = 3;

            return View(configuracaoFormulario);
        }

        // POST: Paciente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.EstadoPaciente = new SelectList(_context.EstadoPaciente, "Id", "Descricao",
                paciente.EstadoPacienteId);
            return View(paciente);
        }

        // GET: Paciente/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            ViewBag.EstadoPaciente = new SelectList(_context.EstadoPaciente, "Id", "Descricao",
                paciente.EstadoPacienteId);
            return View(paciente);
        }

        // POST: Paciente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Paciente paciente)
        {
            if (id != paciente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paciente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.Id))
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
            ViewBag.EstadoPaciente = new SelectList(_context.EstadoPaciente, "Id", "Descricao",
                paciente.EstadoPacienteId);
            return View(paciente);
        }

        // GET: Paciente/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente.Include(x => x.EstadoPaciente).AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: Paciente/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var paciente = await _context.Paciente.FindAsync(id);
            _context.Paciente.Remove(paciente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(Guid id)
        {
            return _context.Paciente.Any(e => e.Id == id);
        }

        #region Métodos auxiliares
        private List<PacienteViewModel> ConverterParaViewModels(List<Paciente> registros)
        {
            List<PacienteViewModel> lista = new List<PacienteViewModel>();
            PacienteViewModel viewModel;
            foreach (Paciente paciente in registros)
            {
                viewModel = new PacienteViewModel();
                viewModel.Id = paciente.Id;
                viewModel.Nome = paciente.Nome;
                viewModel.DataNascimento = paciente.DataNascimento;
                viewModel.DataInternacao = paciente.DataInternacao;
                viewModel.Email = paciente.Email;
                viewModel.Sexo = paciente.Sexo;
                viewModel.TipoPaciente = paciente.TipoPaciente;
                viewModel.EstdoPaciente = paciente.EstadoPaciente.Descricao;
                viewModel.Ativo = paciente.Ativo;
                lista.Add(viewModel);
            }
            return lista;
        }

        private List<TOSelectItem> EstadoPacientesParaSelectItens(List<EstadoPaciente> registros)
        {
            List<TOSelectItem> lista = new List<TOSelectItem>();
            foreach (EstadoPaciente estado in registros)
            {
                lista.Add(new TOSelectItem
                {
                    Valor = estado.Id,
                    Descricao = estado.Descricao
                });
            }
            return lista;
        }

        #endregion
    }
}
