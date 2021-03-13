using ProjetoAspNetCore.Mvc.Infra.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetCore.Mvc.Infra.TOs
{
    public class TOConfiguracaoCampo
    {
        public TOConfiguracaoCampo(string nomePropriedade, TipoCampo tipo, List<TOSelectItem> itensCombo)
        {
            NomePropriedade = nomePropriedade;
            Tipo = tipo;
            ItensCombo = itensCombo;
        }

        public string NomePropriedade { get; set; }
        public TipoCampo Tipo { get; set; }
        public List<TOSelectItem> ItensCombo { get; set; }
    }
}
