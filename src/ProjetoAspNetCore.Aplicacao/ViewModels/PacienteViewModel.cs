using ProjetoAspNetCore.Domain.Enums;
using ProjetoAspNetCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAspNetCore.Aplicacao.ViewModels
{
    public class PacienteViewModel
    {
        public PacienteViewModel()
        {
            Ativo = true;
        }
        
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Estado do paciente")]
        public Guid EstadoPacienteId { get; set; }
        [Display(Name = "Nome do paciente")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [StringLength(80, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres!",
            MinimumLength = 2)]
        public string Nome { get; set; }
        [Display(Name = "Data de nascimento")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [DataType(DataType.DateTime, ErrorMessage = "Data inválida!")]
        public DateTime DataNascimento { get; set; }
        [Display(Name = "Data da internação")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [DataType(DataType.DateTime, ErrorMessage = "Data inválida!")]
        public DateTime DataInternacao { get; set; }
        [Display(Name = "Sexo")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public Sexo Sexo { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email inválido")]
        public string Email { get; set; }
        public bool Ativo { get; set; }
        [Display(Name = "CPF")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [StringLength(11, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres!",
            MinimumLength = 11)]
        public string Cpf { get; set; }
        [Display(Name = "Tipo do paciente")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public TipoPaciente TipoPaciente { get; set; }
        [Display(Name = "Estado do paciente")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public virtual EstadoPaciente EstadoPaciente { get; set; }
        [Display(Name = "RG")]
        [MaxLength(15, ErrorMessage = "O campo {0} não pode ter mais de {1} caracteres")]
        public string Rg { get; set; }
        [Display(Name = "Orgão expedidor")]
        [MaxLength(11, ErrorMessage = "O campo {0} não pode ter mais de {1} caracteres")]
        public string RgOrgao { get; set; }
        [Display(Name = "Data da emissão")]
        public DateTime RgDataEmissao { get; set; }
    }
}
