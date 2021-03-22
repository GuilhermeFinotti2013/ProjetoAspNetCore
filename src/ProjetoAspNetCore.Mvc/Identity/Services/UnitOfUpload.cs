using Microsoft.AspNetCore.Http;
using ProjetoAspNetCore.Mvc.Infra;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace ProjetoAspNetCore.Mvc.Identity.Services
{
    public class UnitOfUpload : IUnitOfUpload
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UnitOfUpload(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async void UploadImagem(IFormFile file)
        {
            long totalBytes = file.Length;
            string fileName = file.FileName.Trim('"');
            fileName = fileName.Contains("\\") ? fileName.Substring(fileName.LastIndexOf("\\" + 1)) : fileName;

            byte[] buffer = new byte[3 * 1024];
            using (FileStream output = System.IO.File.Create(ObterCaminhoCompletoArquivo(fileName)))
            {
                using (Stream input = file.OpenReadStream())
                {
                    int readBytes;
                    while ((readBytes = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        await output.WriteAsync(buffer, 0, readBytes);
                        totalBytes += readBytes;
                    }
                }
            }
        }

        private string ObterCaminhoCompletoArquivo(string fileName)
        {
            string path = $@"{_webHostEnvironment.WebRootPath}\upload\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path + fileName;
        }
    }
}
