using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ProjetoAspNetCore.Domain.Interfaces
{
    public interface IUsuarioNoContexto
    {
        string Nome { get; }
        Guid GetIdUsuario();
        string GetEmailUsuario();
        bool EstaAutenticado();
        bool EstaNaRole(string role);
        IEnumerable<Claim> GetClaimIdentity();
        string GetApelidoUsuario();
        string GetNomeCompletoUsuario();
        string GetDataNascUsuario();
        string GetImgProfilePath();
    }
}
