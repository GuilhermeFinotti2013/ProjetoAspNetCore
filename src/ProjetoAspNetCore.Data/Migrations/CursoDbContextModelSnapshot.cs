﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjetoAspNetCore.Data.ORM;

namespace ProjetoAspNetCore.Data.Migrations
{
    [DbContext(typeof(CursoDbContext))]
    partial class CursoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjetoAspNetCore.Domain.Entities.Cid", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CidInternalId")
                        .HasColumnType("int")
                        .HasColumnName("CidInternalId");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("varchar(6)")
                        .HasColumnName("Codigo");

                    b.Property<string>("Diagnostico")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)")
                        .HasColumnName("Diagnostico");

                    b.HasKey("Id");

                    b.ToTable("Cid");
                });

            modelBuilder.Entity("ProjetoAspNetCore.Domain.Entities.EstadoPaciente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("Descricao");

                    b.HasKey("Id");

                    b.ToTable("EstadoPaciente");
                });

            modelBuilder.Entity("ProjetoAspNetCore.Domain.Entities.Generico", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Codigo")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(90)");

                    b.HasKey("Id");

                    b.ToTable("Generico");
                });

            modelBuilder.Entity("ProjetoAspNetCore.Domain.Entities.Medicamento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Codigo")
                        .HasColumnType("int");

                    b.Property<int>("CodigoGenerico")
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)")
                        .HasColumnName("Descricao");

                    b.Property<string>("Generico")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)")
                        .HasColumnName("Generico");

                    b.HasKey("Id");

                    b.ToTable("Medicamento");
                });

            modelBuilder.Entity("ProjetoAspNetCore.Domain.Entities.Paciente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Cpf")
                        .HasColumnType("varchar(11)")
                        .HasColumnName("Cpf");

                    b.Property<DateTime>("DataInternacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(150)")
                        .HasColumnName("Email");

                    b.Property<Guid>("EstadoPacienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Motivo")
                        .HasColumnType("varchar(90)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(80)")
                        .HasColumnName("Nome");

                    b.Property<string>("Rg")
                        .HasColumnType("varchar(15)")
                        .HasColumnName("Rg");

                    b.Property<DateTime>("RgDataEmissao")
                        .HasColumnType("datetime2");

                    b.Property<string>("RgOrgao")
                        .HasColumnType("varchar(10)")
                        .HasColumnName("RgOrgao");

                    b.Property<int>("Sexo")
                        .HasColumnType("int");

                    b.Property<int>("TipoPaciente")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EstadoPacienteId");

                    b.ToTable("Paciente");
                });

            modelBuilder.Entity("ProjetoAspNetCore.Domain.Entities.Paciente", b =>
                {
                    b.HasOne("ProjetoAspNetCore.Domain.Entities.EstadoPaciente", "EstadoPaciente")
                        .WithMany("Paciente")
                        .HasForeignKey("EstadoPacienteId")
                        .IsRequired();

                    b.Navigation("EstadoPaciente");
                });

            modelBuilder.Entity("ProjetoAspNetCore.Domain.Entities.EstadoPaciente", b =>
                {
                    b.Navigation("Paciente");
                });
#pragma warning restore 612, 618
        }
    }
}
