using KissLog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjetoAspNetCore.Aplicacao.Interfaces;
using ProjetoAspNetCore.Aplicacao.Services;
using ProjetoAspNetCore.CrossCutting.Helpers;
using ProjetoAspNetCore.Data.Repository;
using ProjetoAspNetCore.Domain.Interfaces;
using ProjetoAspNetCore.Domain.Interfaces.Repository;
using ProjetoAspNetCore.Mvc.Extensions.Identity;
using ProjetoAspNetCore.Mvc.Extensions.Identity.Services;
using ProjetoAspNetCore.Mvc.Identity.Services;
using ProjetoAspNetCore.Mvc.Infra;
//using ProjetoAspNetCore.Mvc.Extensions.Filtros;

namespace ProjetoAspNetCore.Mvc.Configuration
{
    public static class DependencyInjectConfig
    {
        public static IServiceCollection AddDependencyInjectConfig(this IServiceCollection services, IConfiguration configuration)
        {
            #region Aplicação
            services.AddScoped<IServicoAplicacaoPaciente, ServicoAplicacaoPaciente>();
            #endregion

            #region Domain -> Service

            #endregion

            #region Domain -> Repository

            #endregion


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped((context) => Logger.Factory.Get());
             //         services.AddScoped<AuditoriaFilter>();
            services.AddTransient<IEmailSender, EmailSender>();

            #region Repositórios
            services.AddScoped<IPacienteRepository, PacienteRepository>();
            #endregion

            services.AddTransient<IUnitOfUpload, UnitOfUpload>();
            #region Mantem o estado do contexto Http por toda a aplicação
            // =====/ Mantem o estado do contexto Http por toda a aplicação === //
          ///  services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // ================================================================ //
           // services.AddScoped<IUsuarioNoContexto, UsuarioAspNet>();
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
