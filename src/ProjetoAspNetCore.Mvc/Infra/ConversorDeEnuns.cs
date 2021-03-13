using ProjetoAspNetCore.Domain.Enums;
using ProjetoAspNetCore.Mvc.Infra.Enums;
using ProjetoAspNetCore.Mvc.Infra.TOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetCore.Mvc.Infra
{
    public static class ConversorDeEnuns
    {
        public static List<TOSelectItem> EnumParaSelectItem(TipoEnunsConvertiveis tipoEnum)
        {
            List<TOSelectItem> lista = null;
            
            switch (tipoEnum)
            {
                case TipoEnunsConvertiveis.Sexo:
                    lista = Enum.GetValues(typeof(Sexo)).Cast<Sexo>()
                        .Select(t => new TOSelectItem
                        {
                            Valor = ((int)t),
                            Descricao = t.ToString()
                        }).ToList();
                    break;
                case TipoEnunsConvertiveis.TipoEntradaPaciente:
                    lista = Enum.GetValues(typeof(TipoMovimentoPaciente)).Cast<TipoMovimentoPaciente>()
                        .Select(t => new TOSelectItem
                        {
                            Valor = ((int)t),
                            Descricao = t.ToString()
                        }).ToList();
                    break;
                case TipoEnunsConvertiveis.TipoMovimentoPaciente:
                    lista = Enum.GetValues(typeof(TipoMovimentoPaciente)).Cast<TipoMovimentoPaciente>()
                        .Select(t => new TOSelectItem
                        {
                            Valor = ((int)t),
                            Descricao = t.ToString()
                        }).ToList();
                    break;
                case TipoEnunsConvertiveis.TipoPaciente:
                    lista = Enum.GetValues(typeof(TipoPaciente)).Cast<TipoPaciente>()
                        .Select(t => new TOSelectItem
                        {
                            Valor = ((int)t),
                            Descricao = t.ToString()
                        }).ToList();
                    break;
                case TipoEnunsConvertiveis.TipoSaidaPaciente:
                    lista = Enum.GetValues(typeof(TipoSaidaPaciente)).Cast<TipoSaidaPaciente>()
                        .Select(t => new TOSelectItem
                        {
                            Valor = ((int)t),
                            Descricao = t.ToString()
                        }).ToList();
                    break;
                default:
                    break;
            }

            return lista;
        }
    }
}
