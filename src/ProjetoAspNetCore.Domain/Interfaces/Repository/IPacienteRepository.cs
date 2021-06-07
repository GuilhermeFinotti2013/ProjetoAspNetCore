using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProjetoAspNetCore.Domain.Models;
using ProjetoAspNetCore.DomainCore.Base;

namespace ProjetoAspNetCore.Domain.Interfaces.Repository
{
    public interface IPacienteRepository : IRepository<Paciente, Guid>
    {
        Task<IEnumerable<Paciente>> ListarPacientes();
        Task<IEnumerable<Paciente>> ListarPacientesComEstado();
        List<EstadoPaciente> ListarEstadosPaciente();
        Task<Paciente> ObterPacienteComEstadoPaciente(Guid pacienteId);
        bool TemPaciente(Guid pacienteId);
        Task<IEnumerable<Paciente>> ObterPacientesPorEstadoPaciente(Guid estadoPacienteId);
    }
}
