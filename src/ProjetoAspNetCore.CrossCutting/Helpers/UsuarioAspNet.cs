using Microsoft.AspNetCore.Http;
using ProjetoAspNetCore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using ProjetoAspNetCore.CrossCutting.Extensions;

namespace ProjetoAspNetCore.CrossCutting.Helpers
{
    public class UsuarioAspNet : IUsuarioNoContexto
    {
        private readonly HttpContextAccessor _httpContextAccessor;

        public UsuarioAspNet(HttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Nome => _httpContextAccessor.HttpContext.User.Identity.Name;

        public Guid GetIdUsuario()
        {
            return EstaAutenticado() ? Guid.Parse(_httpContextAccessor.HttpContext.User.GetIdUsuario()) : Guid.Empty;
        }

        public IEnumerable<Claim> GetClaimIdentity()
        {
            return _httpContextAccessor.HttpContext.User.Claims;
        }

        public string GetEmailUsuario()
        {
            return EstaAutenticado() ? _httpContextAccessor.HttpContext.User.GetEmailUsuario() : String.Empty;
        }

        public string GetApelidoUsuario()
        {
            return EstaAutenticado() ? _httpContextAccessor.HttpContext.User.GetApelidoUsuario() : String.Empty;
        }

        public string GetDataNascUsuario()
        {
            return EstaAutenticado() ? _httpContextAccessor.HttpContext.User.GetDataNascUsuario() : String.Empty;
        }

        public string GetImgProfilePath()
        {
            return EstaAutenticado() ? _httpContextAccessor.HttpContext.User.GetImgProfilePath() : String.Empty;
        }

        public string GetNomeCompletoUsuario()
        {
            return EstaAutenticado() ? _httpContextAccessor.HttpContext.User.GetNomeCompletoUsuario() : String.Empty;
        }

        public bool EstaAutenticado()
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public bool EstaNaRole(string role)
        {
            return _httpContextAccessor.HttpContext.User.IsInRole(role);
        }
    }
}
