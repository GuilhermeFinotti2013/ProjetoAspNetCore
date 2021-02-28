using Microsoft.Extensions.DependencyInjection;
//using ProjetoAspNetCore.Mvc.Extensions.Filtros;

namespace ProjetoAspNetCore.Mvc.Configuration
{
    public static class MvcAndRazorConfig
    {
        public static IServiceCollection AddMvcAndRazor(this IServiceCollection services)
        {
  /*          services.AddMvc(options =>
            {
                options.Filters.Add(typeof(AuditoriaFilter));
            });
  */
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages();
            return services;
        }
    }
}
