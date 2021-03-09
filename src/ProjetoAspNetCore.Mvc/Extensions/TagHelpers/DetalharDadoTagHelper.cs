using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Reflection;
using ProjetoAspNetCore.Mvc.Extensions.ExtensionsMethods;

namespace ProjetoAspNetCore.Mvc.Extensions.TagHelpers
{
    public class DetalharDadoTagHelper : TagHelper
    {
        [HtmlAttributeName("Dado")]
        public object Dado { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "dl";
            output.Attributes.Add("class", "dl-horizontal");
            AddDetalhes(output);
        }

        private void AddDetalhes(TagHelperOutput output)
        {
            PropertyInfo[] listaDePropriedades = Dado.GetType().GetProperties();
            foreach (PropertyInfo propriedade in listaDePropriedades)
            {
                if (propriedade.PropertyType.FullName == typeof(String).FullName ||
                    propriedade.PropertyType.FullName == typeof(Int32).FullName ||
                    propriedade.PropertyType.FullName == typeof(DateTime).FullName ||
                    propriedade.PropertyType.FullName == typeof(Boolean).FullName)
                {
                    output.Content.AppendHtml($"<dt>{propriedade.GetDisplay()}:</dt>");
                    output.Content.AppendHtml($"<dd>{propriedade.GetValue(Dado)}</dd>");
                }
            }
        }
    }
}
