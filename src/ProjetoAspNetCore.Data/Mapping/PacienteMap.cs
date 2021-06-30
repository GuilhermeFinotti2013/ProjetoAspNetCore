using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoAspNetCore.Domain.Entities;

namespace ProjetoAspNetCore.Data.Mapping
{
    public class PacienteMap : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.HasKey(pk => pk.Id);
            builder.Property(c => c.Nome).IsRequired().HasColumnName("Nome").HasColumnType("varchar(80)");
            builder.Property(c => c.Email).HasColumnName("Email").HasColumnType("varchar(150)");
            builder.Property(c => c.Cpf).HasColumnName("Cpf").HasColumnType("varchar(11)");
            builder.Property(c => c.Rg).HasColumnName("Rg").HasColumnType("varchar(15)");
            builder.Property(c => c.RgOrgao).HasColumnName("RgOrgao").HasColumnType("varchar(10)");

            builder.HasOne(p => p.EstadoPaciente).WithMany(prop => prop.Paciente)
                .HasForeignKey(prop => prop.EstadoPacienteId).HasPrincipalKey(prop => prop.Id);

            builder.ToTable("Paciente");
        }
    }
}
