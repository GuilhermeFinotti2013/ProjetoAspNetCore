using ProjetoAspNetCore.Domain.Entities;
using ProjetoAspNetCore.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoAspNetCore.Domain.Models
{
    public class Paciente : EntityBase
    {
        public Paciente()
        {
            this.Ativo = true;
        }

        [ForeignKey("EstadoPaciente")]
        [Display(Name = "Estado do paciente")]
        public Guid EstadoPacienteId { get; set; }
        public virtual EstadoPaciente EstadoPaciente { get; set; }
        public string Nome { get; set; }
        [Display(Name = "Data de nascimento")]
        public DateTime DataNascimento { get; set; }
        [Display(Name = "Data da internação")]
        public DateTime DataInternacao { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        [Display(Name = "CPF")]
        public string Cpf { get; set; }
        [Display(Name = "Tipo do paciente")]
        public TipoPaciente TipoPaciente { get; set; }
        public Sexo Sexo { get; set; }
        [Display(Name = "RG")]
        public string Rg { get; set; }
        [Display(Name = "Orgão expedidor")]
        public string RgOrgao { get; set; }
        [Display(Name = "Data da emissão")]
        public DateTime RgDataEmissao { get; set; }
    }
}
