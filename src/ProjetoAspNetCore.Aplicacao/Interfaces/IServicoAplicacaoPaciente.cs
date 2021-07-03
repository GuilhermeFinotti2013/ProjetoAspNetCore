using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoAspNetCore.Aplicacao.ViewModels;

namespace ProjetoAspNetCore.Aplicacao.Interfaces
{
    public interface IServicoAplicacaoPaciente
    {
        Task<IEnumerable<PacienteViewModel>> ObterPacientesComEstadoPacienteApp();

    }
}
