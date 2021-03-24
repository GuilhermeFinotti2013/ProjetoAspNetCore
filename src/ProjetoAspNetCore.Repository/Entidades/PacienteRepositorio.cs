using ProjetoAspNetCore.Data.ORM;
using ProjetoAspNetCore.Domain.Interfaces.Entidades;
using ProjetoAspNetCore.Domain.Models;
using ProjetoAspNetCore.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjetoAspNetCore.Repository.Entidades
{
    public class PacienteRepositorio : RepositorioGenerico<Paciente, Guid>, IRepositoryDomainPaciente
    {
        private readonly CursoDbContext _context;

        public PacienteRepositorio(CursoDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Paciente>> ListarPacientes()
        {
            return await _context.Paciente.AsNoTracking().ToArrayAsync();
        }

        public async Task<IEnumerable<Paciente>> ListarPacientesComEstado()
        {
            return await _context.Paciente.Include(e=>e.EstadoPaciente).AsNoTracking().ToArrayAsync();
        }
    }
}
