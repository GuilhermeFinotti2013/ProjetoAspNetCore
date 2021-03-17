using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ProjetoAspNetCore.Aplicacao.Extensions
{
    public static class ImportUtils
    {
        public static string GetFilePath(string raiz, string nomeArquivo, string extencao)
        {
            string outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            string csvPath = Path.Combine(outPutDirectory, $"{raiz}\\{nomeArquivo}{extencao}");
            return new Uri(csvPath).LocalPath;
        }
    }
}
