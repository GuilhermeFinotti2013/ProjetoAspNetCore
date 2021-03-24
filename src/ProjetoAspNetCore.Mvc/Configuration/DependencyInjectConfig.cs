using KissLog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjetoAspNetCore.CrossCutting.Helpers;
using ProjetoAspNetCore.Domain.Interfaces;
using ProjetoAspNetCore.Mvc.Extensions.Identity.Services;
using ProjetoAspNetCore.Mvc.Identity.Services;
using ProjetoAspNetCore.Mvc.Infra;
using System;
using Microsoft.AspNetCore.Identity;
using ProjetoAspNetCore.Mvc.Extensions.Identity;
//using ProjetoAspNetCore.Mvc.Extensions.Filtros;

namespace ProjetoAspNetCore.Mvc.Configuration
{
    public static class DependencyInjectConfig
    {
        public static IServiceCollection AddDependencyInjectConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped((context) => Logger.Factory.Get());
            //          services.AddScoped<AuditoriaFilter>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IUnitOfUpload, UnitOfUpload>();
            #region Mantem o estado do contexto Http por toda a aplicação
            // =====/ Mantem o estado do contexto Http por toda a aplicação === //
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // ================================================================ //
            ////services.AddScoped<IUsuarioNoContexto, UsuarioAspNet>();
            // ================================================================ //

            // =====/ Adicionar Claims para HttpContext >> toda a Applications ================ //
            //services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, UserClaimsService>();
            // =====
            #endregion
            services.Configure<AuthMessageSenderOptions>(configuration);
            return services;
        }
    }
}
