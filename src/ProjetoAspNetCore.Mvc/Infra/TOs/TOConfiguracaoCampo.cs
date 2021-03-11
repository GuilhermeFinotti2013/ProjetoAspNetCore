using ProjetoAspNetCore.Mvc.Infra.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetCore.Mvc.Infra.TOs
{
    public class TOConfiguracaoCampo
    {
        public int Posicao { get; set; }
        public TipoCampo Tipo { get; set; }
        public List<object> ItensCombo { get; set; }
    }
}
