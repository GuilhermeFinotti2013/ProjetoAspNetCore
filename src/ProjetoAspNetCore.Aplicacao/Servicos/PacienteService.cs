using Microsoft.EntityFrameworkCore;
using ProjetoAspNetCore.Data.ORM;
using ProjetoAspNetCore.Domain.Interfaces.Entidades;
using ProjetoAspNetCore.Domain.Models;
using ProjetoAspNetCore.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAspNetCore.Aplicacao.Servicos
{
    public class PacienteService : RepositorioGenerico<Paciente, Guid>, IRepositoryDomainPaciente
    {
        private readonly CursoDbContext _context;

        public PacienteService(CursoDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Paciente>> ListarPacientes()
        {
            return await _context.Paciente.AsNoTracking().ToArrayAsync();
        }

        public async Task<IEnumerable<Paciente>> ListarPacientesComEstado()
        {
            return await _context.Paciente.Include(e => e.EstadoPaciente).AsNoTracking().ToArrayAsync();
        }
    }
}
