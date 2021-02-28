using Microsoft.AspNetCore.Mvc;
using ProjetoAspNetCore.Data.ORM;
using ProjetoAspNetCore.Mvc.Extensions.ViewComponents.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetCore.Mvc.Extensions.ViewComponents.EstadoPaciente
{
    [ViewComponent(Name = "EstadoCritico")]
    public class EstadoCriticoViewComponents : ViewComponent
    {
        private readonly CursoDbContext _contexto;
        public EstadoCriticoViewComponents(CursoDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int totalGeral = Util.TotReg(_contexto);
            decimal totalEstado = Util.GetNumRegEstado(_contexto, "Critíco");

            decimal percentual = totalEstado * 100 / totalGeral;
            var percentualFormatado = percentual.ToString("F1");

            ContadorEstadoPaciente model = new ContadorEstadoPaciente()
            {
                Titulo = "Pacientes critícos",
                Parcial = (int)totalEstado,
                Percentual = percentualFormatado,
                ClasseContainer = "panel panel-success tile panelClose panelRefresh",
                IconeLg = "l-basic-geolocalize-05",
                IconeSm = "fa fa-arrow-circle-o-up s20 mr5 pull-left",
                Progress = percentual
            };

            return View(model);
        }
    }
}
