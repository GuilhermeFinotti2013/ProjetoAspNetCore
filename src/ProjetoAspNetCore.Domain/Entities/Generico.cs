using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;
using ProjetoAspNetCore.Domain.Entities;

namespace ProjetoAspNetCore.Domain.Entities
{
    public class Generico : EntityBase
    {
        public Generico()
        {

        }

        [Display(Name = "Código")]
        public int Codigo { get; set; }

        public string Nome { get; set; }
    }
}
