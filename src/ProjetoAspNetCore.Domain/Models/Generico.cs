using System;
using System.Collections.Generic;
using System.Text;
using ProjetoAspNetCore.Domain.Entities;

namespace ProjetoAspNetCore.Domain.Models
{
    public class Generico : EntityBase
    {
        public Generico()
        {

        }

        public int Codigo { get; set; }
        public string Nome { get; set; }
    }
}
