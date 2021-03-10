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
                    propriedade.PropertyType.FullName.Contains("ProjetoAspNetCore.Domain.Enums"))
                {
                    output.Content.AppendHtml($"<dt>{propriedade.GetDisplay()}:</dt>");
                    output.Content.AppendHtml($"<dd>{propriedade.GetValue(Dado)}</dd>");
                }
                else if (propriedade.PropertyType.FullName == typeof(Boolean).FullName)
                {
                    string valorBool = (Boolean)propriedade.GetValue(Dado) ? "Sim" : "Não";
                    output.Content.AppendHtml($"<dt>{propriedade.GetDisplay()}:</dt>");
                    output.Content.AppendHtml($"<dd>{valorBool}</dd>");
                }
                else if (propriedade.PropertyType.FullName == typeof(DateTime).FullName)
                {
                    DateTime data = (DateTime)propriedade.GetValue(Dado);
                    output.Content.AppendHtml($"<dt>{propriedade.GetDisplay()}:</dt>");
                    output.Content.AppendHtml($"<dd>{data.ToBrazilianDate()}</dd>");
                }
            }
        }
    }
}
