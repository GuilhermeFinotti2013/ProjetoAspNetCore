using Microsoft.AspNetCore.Mvc;
using ProjetoAspNetCore.Data.ORM;
using ProjetoAspNetCore.Mvc.Extensions.ViewComponents.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetCore.Mvc.Extensions.ViewComponents.EstadoPaciente
{
    [ViewComponent(Name = "EstadoGrave")]
    public class EstadoGraveViewComponents : ViewComponent
    {
        private readonly CursoDbContext _contexto;
        public EstadoGraveViewComponents(CursoDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int totalGeral = Util.TotReg(_contexto);
            decimal totalEstado = Util.GetNumRegEstado(_contexto, "Grave");

            decimal percentual = totalEstado * 100 / totalGeral;
            var percentualFormatado = percentual.ToString("F1");

            ContadorEstadoPaciente model = new ContadorEstadoPaciente()
            {
                Titulo = "Pacientes graves",
                Parcial = (int)totalEstado,
                Percentual = percentualFormatado,
                ClasseContainer = "panel panel-danger tile panelClose panelRefresh",
                IconeLg = "l-basic-life-buoy",
                IconeSm = "fa fa-arrow-circle-o-down s20 mr5 pull-left",
                Progress = percentual
            };

            return View(model);
        }
    }
}
