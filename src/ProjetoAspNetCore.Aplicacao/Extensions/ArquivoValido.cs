using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjetoAspNetCore.Aplicacao.Extensions
{
    public static class ArquivoValido
    {
        public static bool EhValido(IFormFile file, string nomeArquivo)
        {
            return file != null && !string.IsNullOrEmpty(file.FileName) && (nomeArquivo.ToUpper() == file.FileName.ToUpper());
        }
    }
}
