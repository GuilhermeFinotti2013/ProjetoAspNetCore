using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ProjetoAspNetCore.Domain.Entities;
using ProjetoAspNetCore.Domain.Models;
using System.Linq;

namespace ProjetoAspNetCore.Data.ORM
{
    public class CursoDbContext : DbContext
    {
        public CursoDbContext(DbContextOptions<CursoDbContext> options)
            : base(options)
        {

        }

        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<EstadoPaciente> EstadoPaciente { get; set; }
        public DbSet<Generico> Generico { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // onde não tiver setado varchar e a propriedade for do tipo string fica valendo varchar(valor)
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(90)");
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CursoDbContext).Assembly);
            //remover delete cascade
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            
            base.OnModelCreating(modelBuilder);

        }
    }
}
