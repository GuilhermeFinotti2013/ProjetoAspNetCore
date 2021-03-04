﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjetoAspNetCore.Data.ORM;

namespace ProjetoAspNetCore.Data.Migrations
{
    [DbContext(typeof(CursoDbContext))]
    [Migration("20210304215508_MigrationClear")]
    partial class MigrationClear
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjetoAspNetCore.Domain.Models.Cid", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CidInternalId")
                        .HasColumnName("CidInternalId")
                        .HasColumnType("int");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnName("Codigo")
                        .HasColumnType("varchar(6)")
                        .HasMaxLength(6);

                    b.Property<string>("Diagnostico")
                        .IsRequired()
                        .HasColumnName("Diagnostico")
                        .HasColumnType("nvarchar(4000)")
                        .HasMaxLength(4000);

                    b.HasKey("Id");

                    b.ToTable("Cid");
                });

            modelBuilder.Entity("ProjetoAspNetCore.Domain.Models.EstadoPaciente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnName("Descricao")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("EstadoPaciente");
                });

            modelBuilder.Entity("ProjetoAspNetCore.Domain.Models.Generico", b =>
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

            modelBuilder.Entity("ProjetoAspNetCore.Domain.Models.Paciente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Cpf")
                        .HasColumnName("Cpf")
                        .HasColumnType("varchar(11)");

                    b.Property<DateTime>("DataInternacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnName("Email")
                        .HasColumnType("varchar(150)");

                    b.Property<Guid>("EstadoPacienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("Nome")
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Rg")
                        .HasColumnName("Rg")
                        .HasColumnType("varchar(15)");

                    b.Property<DateTime>("RgDataEmissao")
                        .HasColumnType("datetime2");

                    b.Property<string>("RgOrgao")
                        .HasColumnName("RgOrgao")
                        .HasColumnType("varchar(10)");

                    b.Property<int>("Sexo")
                        .HasColumnType("int");

                    b.Property<int>("TipoPaciente")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EstadoPacienteId");

                    b.ToTable("Paciente");
                });

            modelBuilder.Entity("ProjetoAspNetCore.Domain.Models.Paciente", b =>
                {
                    b.HasOne("ProjetoAspNetCore.Domain.Models.EstadoPaciente", "EstadoPaciente")
                        .WithMany("Paciente")
                        .HasForeignKey("EstadoPacienteId")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
