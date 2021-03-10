using ProjetoAspNetCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetCore.Mvc.ViewModel
{
    public class PacienteViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        [Display(Name = "Data de nascimento")]
        public DateTime DataNascimento { get; set; }
        [Display(Name = "Data da internação")]
        public DateTime DataInternacao { get; set; }
        public string Email { get; set; }
        public Sexo Sexo { get; set; }
        [Display(Name = "Tipo de paciente")]
        public TipoPaciente TipoPaciente { get; set; }
        [Display(Name = "Estado do pacientw")]
        public string EstdoPaciente { get; set; }
        public bool Ativo { get; set; }
    }
}
