using Microsoft.AspNetCore.Razor.TagHelpers;
using ProjetoAspNetCore.Mvc.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using ProjetoAspNetCore.Mvc.Extensions.ExtensionsMethods;
using System.Text;

namespace ProjetoAspNetCore.Mvc.Extensions.TagHelpers
{
    public class MontarFormularioTagHelper : TagHelper
    {
        [HtmlAttributeName("Modelo")]
        public object Modelo { get; set; }
        [HtmlAttributeName("Titulo")]
        public string Titulo { get; set; }
        [HtmlAttributeName("Tipo")]
        public TipoFormulario Tipo { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.Add("class", "form-horizontal");
            PropertyInfo[] listaDePropriedades = Modelo.GetType().GetProperties();
            output.Content.AppendHtml($"<h4>{Titulo}</h4>");
            output.Content.AppendHtml("<hr/>");
            if (Tipo == TipoFormulario.Editar)
            {
                output.Content.AppendHtml($"<input data-val=\"true\" id=\"Id\" name=\"Id\" type=\"hidden\" value=\"{GetIdDado()}\">");
            }
            foreach (PropertyInfo propriedade in listaDePropriedades)
            {
                if (propriedade.PropertyType.FullName == typeof(String).FullName ||
                    propriedade.PropertyType.FullName == typeof(Int32).FullName ||
                    propriedade.PropertyType.FullName == typeof(DateTime).FullName ||
                    propriedade.PropertyType.FullName == typeof(Boolean).FullName)
                {
                    AddCampo(output, propriedade);
                }
            }
        }

        private void AddCampo(TagHelperOutput output, PropertyInfo propriedade)
        {
            output.Content.AppendHtml("<div class=\"form-group\">");
            output.Content.AppendHtml($"<label class=\"control-label col-md-2\" for=\"{propriedade.Name}\">{propriedade.GetDisplay()}</label>");
            output.Content.AppendHtml("<div class=\"col-md-10\">");
            output.Content.AppendHtml("<input class=\"form-control text-box single-line\" ");
            output.Content.AppendHtml($"data-val=\"true\" data-val-required=\"O campo {propriedade.GetDisplay()} é obrigatório.\" ");
            output.Content.AppendHtml($"id=\"{propriedade.Name}\" name=\"{propriedade.Name}\" ");
            if (propriedade.PropertyType.FullName == typeof(String).FullName)
            {
                output.Content.AppendHtml("type=\"text\" ");
            }
            else if (propriedade.PropertyType.FullName == typeof(Int16).FullName ||
                    propriedade.PropertyType.FullName == typeof(Int32).FullName ||
                    propriedade.PropertyType.FullName == typeof(Int64).FullName )
            {
                output.Content.AppendHtml("type=\"number\" ");
            }
            if (Tipo == TipoFormulario.Editar)
            {
                output.Content.AppendHtml($"value=\"{propriedade.GetValue(Modelo)}\"");
            }
            else
            {
                output.Content.AppendHtml("value=\"\"");
            }
            output.Content.AppendHtml(">");
            output.Content.AppendHtml("</div>");
            output.Content.AppendHtml("</div>");
        }

        private Guid GetIdDado()
        {
            Guid id = Guid.Empty;
            PropertyInfo[] listaDePropriedades = Modelo.GetType().GetProperties();
            foreach (PropertyInfo propriedade in listaDePropriedades)
            {
                if (propriedade.Name == "Id")
                {
                    id = (Guid)propriedade.GetValue(Modelo);
                }
            }
            return id;
        }
    }
}
