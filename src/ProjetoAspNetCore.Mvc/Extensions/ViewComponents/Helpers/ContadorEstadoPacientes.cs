using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetCore.Mvc.Extensions.ViewComponents.Helpers
{
    public class ContadorEstadoPaciente
    {
        public int Parcial { get; set; }
        public string Percentual { get; set; }
        public string ClasseContainer { get; set; }
        public string Titulo { get; set; }
        public decimal Progress { get; set; }
        public string IconeLg { get; set; }
        public string IconeSm { get; set; }
    }
}
