using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProjetoAspNetCore.Domain.Models;
using ProjetoAspNetCore.DomainCore.Base;

namespace ProjetoAspNetCore.Domain.Interfaces.Entidades
{
    public interface IRepositoryDomainPaciente : IDomainGenericRepository<Paciente, Guid>
    {
        Task<IEnumerable<Paciente>> ListarPacientes();
        Task<IEnumerable<Paciente>> ListarPacientesComEstado();
    }
}
