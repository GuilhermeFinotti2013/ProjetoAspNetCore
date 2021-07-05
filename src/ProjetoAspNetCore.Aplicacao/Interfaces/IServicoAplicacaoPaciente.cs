using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoAspNetCore.Aplicacao.ViewModels;
using ProjetoAspNetCore.Domain.Entities;

namespace ProjetoAspNetCore.Aplicacao.Interfaces
{
    public interface IServicoAplicacaoPaciente
    {
        Task<IEnumerable<PacienteViewModel>> ObterPacientesComEstadoPacienteApp();
        Task<PacienteViewModel> ObterPacienteComEstadoPacienteApp(Guid pacienteId);
        Task<IEnumerable<PacienteViewModel>> ObterPacientesPorEstadoPacienteApp(Guid estadoPacienteId);
        Task<List<EstadoPaciente>> ListarEstadoPacienteApp();
        bool TemPaciente(Guid pacienteId);
    }
}
