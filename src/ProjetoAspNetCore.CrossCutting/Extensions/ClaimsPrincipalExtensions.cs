using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace ProjetoAspNetCore.CrossCutting.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetIdUsuario(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }
            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static string GetEmailUsuario(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }
            return principal.FindFirst(x => x.Type == "Email")?.Type;
        }

        public static string GetApelidoUsuario(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }
            return principal.FindFirst(x => x.Type == "Apelido")?.Type;
        }

        public static string GetNomeCompletoUsuario(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }
            return principal.FindFirst(x => x.Type == "NomeCompleto")?.Type;
        }

        public static string GetDataNascUsuario(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }
            return principal.FindFirst(x => x.Type == "DataNascimento")?.Type;
        }

        public static string GetImgProfilePath(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }
            return principal.FindFirst(x => x.Type == "ImgProfilePath")?.Type;
        }
    }
}
