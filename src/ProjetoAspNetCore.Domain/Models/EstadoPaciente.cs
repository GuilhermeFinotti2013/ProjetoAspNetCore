using ProjetoAspNetCore.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoAspNetCore.Domain.Models
{
    public class EstadoPaciente : EntityBase
    {
        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [StringLength(maximumLength: 20, ErrorMessage = " A {0} deve ter entre {2} e {1} caracteres.",
            MinimumLength = 2)]
        public string Descricao { get; set; }

        public virtual ICollection<Paciente> Paciente { get; set; }
    }
}
