using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoAspNetCore.Domain.Entities;
using ProjetoAspNetCore.Mvc.Infra.Enums;
using ProjetoAspNetCore.Aplicacao.ViewModels;
using ProjetoAspNetCore.Mvc.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoAspNetCore.Mvc.Infra.TOs;
using ProjetoAspNetCore.Domain.Interfaces.Repository;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProjetoAspNetCore.Aplicacao.Interfaces;

namespace ProjetoAspNetCore.Mvc.Controllers
{
    //    [Authorize(Roles = "Admin")]
    public class PacienteController : Controller
    {
        private readonly IServicoAplicacaoPaciente _serviceApp;
        private readonly IPacienteRepository _repositorioPaciente;

        public PacienteController(IPacienteRepository repositorioPaciente, IServicoAplicacaoPaciente serviceApp)
        {
            _repositorioPaciente = repositorioPaciente;
            _serviceApp = serviceApp;
        }

        // GET: Paciente
        public async Task<IActionResult> Index()
        {
            return View(await _serviceApp.ObterPacientesComEstadoPacienteApp());
        }

        // GET: Paciente/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var paciente = await _repositorioPaciente.ObterPacienteComEstadoPaciente(id);
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
                            EstadoPacientesParaSelectItens(_repositorioPaciente.ListarEstadosPaciente())));
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
                await _repositorioPaciente.Inserir(paciente);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                StringBuilder sb = new StringBuilder();

                foreach (var modelState in ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        if (error.ErrorMessage == "The value '' is invalid.")
                        {
                            sb.AppendLine($"O valor informado para o campo é inválido!");
                        }
                        else
                        {
                            sb.Append(error.ErrorMessage + "\n");
                        }
                    }
                }
                TempData["Errors"] = sb.ToString();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Paciente/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            var paciente = await _repositorioPaciente.SelecionarPorId(id.Value); 
            if (paciente == null)
            {
                return NotFound();
            }
            ViewBag.EstadoPaciente = new SelectList(_repositorioPaciente.ListarEstadosPaciente(), "Id", "Descricao",
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
                    await _repositorioPaciente.Atualizar(paciente);
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
            ViewBag.EstadoPaciente = new SelectList(_repositorioPaciente.ListarEstadosPaciente(), "Id", "Descricao",
                paciente.EstadoPacienteId);
            return View(paciente);
        }

        // GET: Paciente/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var paciente = await _repositorioPaciente.ObterPacienteComEstadoPaciente(id);
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
            await _repositorioPaciente.ExcluirPorId(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ReportForEstadoPaciente(Guid? id)
        {
            if (id.Value == null)
            {
                return NotFound();
            }

            var pacientesPorEstado = await _repositorioPaciente.ObterPacientesPorEstadoPaciente(id.Value);
            if (pacientesPorEstado == null)
            {
                return NotFound();
            }
            return View(pacientesPorEstado);
        }

        private bool PacienteExists(Guid id)
        {
            return _repositorioPaciente.TemPaciente(id);
        }

        #region Métodos auxiliares
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
