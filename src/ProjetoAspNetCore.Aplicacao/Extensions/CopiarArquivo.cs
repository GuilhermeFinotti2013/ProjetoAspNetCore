using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace ProjetoAspNetCore.Aplicacao.Extensions
{
    public static class CopiarArquivo
    {
        public static void Copiar(IFormFile file, string filePath)
        {
            using (FileStream fileStream = File.Create(filePath))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
        }
    }
}
