using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetoAspNetCore.Mvc.Extensions.Identity;
using ProjetoAspNetCore.Mvc.Extensions.ExtensionsMethods;

namespace ProjetoAspNetCore.Mvc.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //    builder.AddDefaultUserAndRole();
            //  builder.AddGenericos();
           // builder.AddCid();
            base.OnModelCreating(builder);
        }
    }
}
