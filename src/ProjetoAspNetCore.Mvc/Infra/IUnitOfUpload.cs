using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetCore.Mvc.Infra
{
    public interface IUnitOfUpload
    {
        void UploadImagem(IFormFile file);
    }
}
