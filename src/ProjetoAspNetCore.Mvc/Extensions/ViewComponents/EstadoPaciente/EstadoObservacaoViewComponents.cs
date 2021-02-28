using Microsoft.AspNetCore.Mvc;
using ProjetoAspNetCore.Data.ORM;
using ProjetoAspNetCore.Mvc.Extensions.ViewComponents.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetCore.Mvc.Extensions.ViewComponents.EstadoPaciente
{
    public class EstadoObservacaoViewComponents : ViewComponent
    {
        private readonly CursoDbContext _contexto;
        public EstadoObservacaoViewComponents(CursoDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int totalGeral = Util.TotReg(_contexto);
            decimal totalEstado = Util.GetNumRegEstado(_contexto, "Observação");

            decimal percentual = totalEstado * 100 / totalGeral;
            var percentualFormatado = percentual.ToString("F1");

            ContadorEstadoPaciente model = new ContadorEstadoPaciente()
            {
                Titulo = "Pacientes em observação",
                Parcial = (int)totalEstado,
                Percentual = percentualFormatado,
                ClasseContainer = "panel panel-default tile panelClose panelRefresh",
                IconeLg = "l-banknote",
                IconeSm = "fa fa-arrow-circle-o-down s20 mr5 pull-left",
                Progress = percentual
            };

            return View(model);
        }
    }
}
