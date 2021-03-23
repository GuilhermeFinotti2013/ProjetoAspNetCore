using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ProjetoAspNetCore.Mvc.Data;
using ProjetoAspNetCore.Mvc.Extensions.ExtensionsMethods;

namespace ProjetoAspNetCore.Mvc.Extensions.Identity.Services
{
    public class UserClaimsService : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole> 
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserClaimsService(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) : base(userManager,roleManager,options)
        {
            _applicationDbContext = applicationDbContext;
        }

        public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);

            // Aqui podemos pegar as claims do User no banco. 

            #region Adicionando claims
            ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                new Claim("Apelido", user.Apelido),
                new Claim("NomeCompleto", user.NomeCompleto),
                new Claim("Email", user.Email),
                new Claim("DataNascimento", user.DataNascimento.ToBrazilianDate()),
                new Claim("ImgProfilePath", user.ImgProfilePath)
            });
            #endregion

            return principal;
        }
    }
}
