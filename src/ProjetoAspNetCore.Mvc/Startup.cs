using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjetoAspNetCore.Mvc.Configuration;
using ProjetoAspNetCore.Mvc.Data;
using ProjetoAspNetCore.Mvc.Extensions.Identity;
using ProjetoAspNetCore.Mvc.Extensions.Identity.Services;
using ProjetoAspNetCore.Mvc.Identity.Services;
using System;
using System.Diagnostics;
using System.Text;
using ProjetoAspNetCore.Aplicacao.AutoMapper;

namespace ProjetoAspNetCore.Mvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperConfig));
            #region Utilizado métodos de extensão de IServiceCollection;
            services.AddDbContextConfig(Configuration);
            services.AddIdentityConfig(Configuration);
            services.AddMvcAndRazor();
            services.AddDependencyInjectConfig(Configuration);
            // Prove suporte para Code Page (1252) (Windows-1252)
            services.AddCodePageProviderNotSupportedForAnsi();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            #region Configurações de envio de email
            var authMessageSenderOptions = new AuthMessageSenderOptions
            {
                SendGridUser = Configuration["SendGridUser"],
                SendGridKey = Configuration["SendGridKey"]
            };
            #endregion

            app.UseKissLogMiddleware(opt =>
            {
                ConfigureKissLog(opt);
            });

            //CriaUsersAndRoles.Seed(context, userManager, roleManager).Wait();
            //app.UseMiddleware<DefaultUsersAndRolesMiddleware>();
            //app.UseAddUsersAndRoles();

            #region Rotas
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            #endregion

        }

        private void ConfigureKissLog(IOptionsBuilder options)
        {
            // optional KissLog configuration
            options.Options
                .AppendExceptionDetails((Exception ex) =>
                {
                    StringBuilder sb = new StringBuilder();

                    if (ex is System.NullReferenceException nullRefException)
                    {
                        sb.AppendLine("Important: check for null references");
                    }

                    return sb.ToString();
                });

            // KissLog internal logs
            options.InternalLog = (message) =>
            {
                Debug.WriteLine(message);
            };

            // register logs output
            //RegisterKissLogListeners(options);
        }

        private void RegisterKissLogListeners(IOptionsBuilder options)
        {
            // multiple listeners can be registered using options.Listeners.Add() method

            // register KissLog.net cloud listener
            options.Listeners.Add(new RequestLogsApiListener(new Application(
                Configuration["KissLog.OrganizationId"],    //  "02b96cb3-576a-469c-b6b8-f66ba8b86f82"
                Configuration["KissLog.ApplicationId"])     //  "2f5cb2a0-b27d-4a0f-bf5f-592448b442a5"
            )
            {
                ApiUrl = Configuration["KissLog.ApiUrl"]    //  "https://api.kisslog.net"
            });
        }
    }
}
