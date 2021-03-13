using ProjetoAspNetCore.Mvc.Infra.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetCore.Mvc.Infra.TOs
{
    public class TOConfiguracaoFormulario
    {
        public string Titulo { get; set; }
        public TipoFormulario TipoDoFormulario { get; set; }
        public object Dado { get; set; }
        public List<TOConfiguracaoCampo> ConfiguracaoCampos { get; set; }
        public int TamalhoLabel { get; set; }
    }
}
