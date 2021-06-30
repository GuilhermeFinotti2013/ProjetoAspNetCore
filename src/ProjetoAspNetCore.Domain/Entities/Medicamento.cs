using ProjetoAspNetCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjetoAspNetCore.Domain.Entities
{
    public class Medicamento : EntityBase
    {
        public Medicamento() { }

        [Display(Name = "Código")]
        public int Codigo { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Display(Name = "Genérico")]
        public string Generico { get; set; }
        [Display(Name = "Código do genérico")]
        public int CodigoGenerico { get; set; }


    }
}
