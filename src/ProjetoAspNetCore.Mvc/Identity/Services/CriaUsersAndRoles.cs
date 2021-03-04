using Microsoft.AspNetCore.Identity;
using ProjetoAspNetCore.Mvc.Data;
using ProjetoAspNetCore.Mvc.Extensions.Identity;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjetoAspNetCore.Mvc.Identity.Services
{
    public static class CriaUsersAndRoles
    {
        public static async Task Seed(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            const string nome = "Guilherme Luis Finotti";
            const string apelido = "Gui";
            DateTime dataNascimento = new DateTime(1992, 6, 11);
            const string email = "guilhermelfinotti@gmail.com";
            const string userName = email;
            const string password = "Admin@123";
            const string roleName = "Admin";

            context.Database.Migrate();
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            if (await userManager.FindByNameAsync(userName) == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    Apelido = apelido,
                    NomeCompleto = nome,
                    DataNascimento = dataNascimento,
                    UserName = userName,
                    Email = email,
                    PhoneNumber = "51999026857",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
    }
}
