using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoAspNetCore.Domain.Models;

namespace ProjetoAspNetCore.Data.Mapping
{
    public class EstadoPacienteMap : IEntityTypeConfiguration<EstadoPaciente>
    {
        public void Configure(EntityTypeBuilder<EstadoPaciente> builder)
        {
            builder.HasKey(pk => pk.Id);
            builder.Property(p => p.Descricao).HasColumnType("varchar(30)")
                .HasColumnName("Descricao");
            builder.ToTable("EstadoPaciente");
        }
    }
}
