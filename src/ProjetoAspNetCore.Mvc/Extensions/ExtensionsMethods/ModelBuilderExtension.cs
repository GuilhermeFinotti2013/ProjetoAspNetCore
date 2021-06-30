using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjetoAspNetCore.Domain.Entities;
using ProjetoAspNetCore.Mvc.Extensions.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ProjetoAspNetCore.Mvc.Extensions.ExtensionsMethods
{
    public static class ModelBuilderExtension
    {
        #region Constantes
        const string NOMECOMPLETO = "Guilherme Luis Finotti";
        const string APELIDO = "Gui";
        const string EMAIL = "guilhermelfinotti@gmail.com";
        const string USERNAME = EMAIL;
        const string PASSWORD = "Admin@123";
        const string ROLERNAME = "Admin";

        const string USERID = "F6F2A61B-4B5A-4C9C-88C9-42A473B7958D";
        const string ROLEID = "3EE387F4-ADBD-42BF-A068-022D48E99021";
        #endregion

        public static ModelBuilder AddDefaultUserAndRole(this ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = ROLEID,
                    Name = ROLERNAME,
                    NormalizedName = ROLERNAME.ToUpper()
                }
            );

            var phash = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = USERID,
                    Apelido = APELIDO,
                    NomeCompleto = NOMECOMPLETO,
                    DataNascimento = new DateTime(1992, 6, 11),
                    Email = EMAIL,
                    NormalizedEmail = EMAIL.ToUpper(),
                    UserName = USERNAME,
                    NormalizedUserName = USERNAME.ToUpper(),
                    PasswordHash = phash.HashPassword(null, PASSWORD),
                    EmailConfirmed = true
                });

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = ROLEID,
                    UserId = USERID
                });

            return builder;
        }

        public static ModelBuilder AddGenericos(this ModelBuilder builder)
        {
            var k = 0;
            string linha;

            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var csvPath = Path.Combine(outPutDirectory, "Csv\\Generico.CSV");
            string filePath = new Uri(csvPath).LocalPath;

            using (var fs = File.OpenRead(filePath))
            {
                using (var reader = new StreamReader(fs))
                {
                    while ((linha = reader.ReadLine()) != null)
                    {
                        if (k > 0)
                        {
                            var partes = linha.Split(';');
                            builder.Entity<Generico>().HasData(
                                new Generico
                                {
                                    Codigo = Convert.ToInt32(partes[0]),
                                    Nome = partes[1]
                                });
                        }
                        k++;
                    }
                }
            }

            return builder;
        }
        public static ModelBuilder AddCid(this ModelBuilder builder)
        {
            var k = 0;
            string linha;

            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var csvPath = Path.Combine(outPutDirectory, "Csv\\Cid.CSV");
            string filePath = new Uri(csvPath).LocalPath;

            using (var fs = File.OpenRead(filePath))
            {
                using (var reader = new StreamReader(fs))
                {
                    while ((linha = reader.ReadLine()) != null)
                    {
                        if (k > 0)
                        {
                            var partes = linha.Split(';');
                            builder.Entity<Cid>().HasData(
                                new Cid
                                {
                                    CidInternalId = Convert.ToInt32(partes[0]),
                                    Codigo = partes[1],
                                    Diagnostico = partes[2]
                                });
                        }
                        k++;
                    }
                }
            }

            return builder;
        }
    }
}
