using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAspNetCore.Mvc.Configuration
{
    public static class EncodingANSIConfig
    {
        public static IServiceCollection AddCodePageProviderNotSupportedForAnsi(this IServiceCollection services)
        {
            /*
             * Fornece acesso a um provedor de codificação para páginas de código que, de outra forma,
             * estão diponíveis apenas no .Net para Desktop
             */
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            return services;
        }
    }
}
