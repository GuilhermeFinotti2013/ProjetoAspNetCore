using ProjetoAspNetCore.Data.ORM;
using ProjetoAspNetCore.Domain.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetCore.Aplicacao.Extensions
{
    public class ReadWriteFile
    {
        // Responsável por ler e gravar arquivo importado
        public async Task<bool> ReadAndWriteCsvAsync(string filePath, CursoDbContext context)
        {
            int k = 0;
            string linha;
            using (FileStream fs = System.IO.File.OpenRead(filePath))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    while ((linha = reader.ReadLine()) != null)
                    {
                        if (k > 0)
                        {
                            var partes = linha.Split(';');
                            if (JaTemMedicamento(partes[0], context))
                            {
                                return false;
                            }
                            context.Add(new Medicamento
                            {
                                Codigo = int.Parse(partes[0]),
                                Descricao = partes[1],
                                Generico = partes[2],
                                CodigoGenerico = int.Parse(partes[3])
                            });
                        }
                        k++;
                    }
                }
                await context.SaveChangesAsync();
            }
            return true;
        }

        private bool JaTemMedicamento(string codigoMedicamento, CursoDbContext context)
        {
            return context.Medicamento.Any(e => e.Codigo == int.Parse(codigoMedicamento));
        }
    }
}
