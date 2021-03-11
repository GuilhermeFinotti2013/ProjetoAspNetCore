using Microsoft.AspNetCore.Razor.TagHelpers;
using ProjetoAspNetCore.Mvc.Infra.Enums;
using ProjetoAspNetCore.Mvc.Infra.TOs;
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
        [HtmlAttributeName("configuracao")]
        public TOConfiguracaoFormulario ConfiguracaoFormulario { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.Add("class", "form-horizontal");

            if (ConfiguracaoFormulario == null || ConfiguracaoFormulario.Dado == null ||
                ConfiguracaoFormulario.TipoDoFormulario == 0 || ConfiguracaoFormulario.Titulo == null)
            {
                output.Content.AppendHtml("<h1 style=\"color: red;\">Falha ao configurar o formulário!</h1>");
            }
            else
            {
                output.Content.AppendHtml($"<h4>{ConfiguracaoFormulario.Titulo}</h4>");
                output.Content.AppendHtml("<hr/>");
                if (ConfiguracaoFormulario.TipoDoFormulario == TipoFormulario.Editar ||
                     ConfiguracaoFormulario.TipoDoFormulario == TipoFormulario.Inserir)
                {
                    MontarFormularioSimples(output);
                }
                else if (ConfiguracaoFormulario.TipoDoFormulario == TipoFormulario.EditarEspecializado ||
                         ConfiguracaoFormulario.TipoDoFormulario == TipoFormulario.InserirEspecializado)
                {

                }
            }
            
            
            
            
        }

        private void MontarFormularioSimples(TagHelperOutput output)
        {
            PropertyInfo[] listaDePropriedades = ConfiguracaoFormulario.Dado.GetType().GetProperties();

            if (ConfiguracaoFormulario.TipoDoFormulario == TipoFormulario.Editar)
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
                    AddCampoEditavel(output, propriedade);
                }
            }
        }

        private void AddCampoEditavel(TagHelperOutput output, PropertyInfo propriedade)
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
            if (ConfiguracaoFormulario.TipoDoFormulario == TipoFormulario.Editar)
            {
                output.Content.AppendHtml($"value=\"{propriedade.GetValue(ConfiguracaoFormulario.Dado)}\"");
            }
            else if (propriedade.PropertyType.FullName == typeof(DateTime).FullName)
            {
                output.Content.AppendHtml("type=\"datetime-local\" style=\"width: 200px\" ");
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
            PropertyInfo[] listaDePropriedades = ConfiguracaoFormulario.Dado.GetType().GetProperties();
            foreach (PropertyInfo propriedade in listaDePropriedades)
            {
                if (propriedade.Name == "Id")
                {
                    id = (Guid)propriedade.GetValue(ConfiguracaoFormulario.Dado);
                }
            }
            return id;
        }
    }
}
