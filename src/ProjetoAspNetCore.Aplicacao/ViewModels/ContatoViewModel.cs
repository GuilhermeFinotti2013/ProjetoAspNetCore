using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetCore.Aplicacao.ViewModels
{
    public class ContatoViewModel
    {
        public string Assunto { get; set; }
        public string Email { get; set; }
        public string Mensagem { get; set; }
    }
}
