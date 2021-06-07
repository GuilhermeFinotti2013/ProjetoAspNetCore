using Microsoft.EntityFrameworkCore;
using ProjetoAspNetCore.Data.ORM;
using ProjetoAspNetCore.Domain.Interfaces.Repository;
using ProjetoAspNetCore.Domain.Models;
using ProjetoAspNetCore.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetCore.Aplicacao.Repository
{
    public class PacienteRepository : RepositoryGeneric<Paciente, Guid>, IPacienteRepository
    {
        private readonly CursoDbContext _context;

        public PacienteRepository(CursoDbContext context) : base(context)
        {
            _context = context;
        }

        public List<EstadoPaciente> ListarEstadosPaciente()
        {
            return _context.EstadoPaciente.AsNoTracking().ToListAsync().Result;
        }

        public async Task<IEnumerable<Paciente>> ListarPacientes()
        {
            return await _context.Paciente.AsNoTracking().ToArrayAsync();
        }

        public async Task<IEnumerable<Paciente>> ListarPacientesComEstado()
        {
            return await _context.Paciente.Include(e => e.EstadoPaciente).AsNoTracking().ToArrayAsync();
        }

        public async Task<Paciente> ObterPacienteComEstadoPaciente(Guid pacienteId)
        {
            return await _context.Paciente.Include(e => e.EstadoPaciente).AsNoTracking().FirstOrDefaultAsync(x => x.Id == pacienteId);
        }

        public async Task<IEnumerable<Paciente>> ObterPacientesPorEstadoPaciente(Guid estadoPacienteId)
        {
            return await _context.Paciente.Include(ep => ep.EstadoPaciente).AsNoTracking()
                .Where(x => x.EstadoPaciente.Id == estadoPacienteId).OrderBy(ordem => ordem.Nome).ToListAsync();
        }

        public bool TemPaciente(Guid pacienteId)
        {
            return _context.Paciente.Any(x => x.Id == pacienteId);
        }
    }
}
