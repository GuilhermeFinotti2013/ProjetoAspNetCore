using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ProjetoAspNetCore.Mvc.Extensions.ExtensionsMethods;

namespace ProjetoAspNetCore.Mvc.Extensions.TagHelpers
{
    public class ListarDadosTagHelper : TagHelper
    {
        [HtmlAttributeName("Items")]
        public IEnumerable<object> Items { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "table";
            output.Attributes.Add("id", "myTable");
            output.Attributes.Add("class", "table table-striped table-bordered table-hover");
            output.Attributes.Add("style", "width:100%");
            output.Attributes.Add("cellspacing", "0");
            output.Attributes.Add("width", "100%");
            var listaDeProriedades = GetItemProperties();
            AddTableHeader(output, listaDeProriedades);
            AddTableBody(output, listaDeProriedades);
            AddTableFooter(output, listaDeProriedades);
        }

        private void AddTableHeader(TagHelperOutput output, PropertyInfo[] listaDeProriedades)
        {
            output.Content.AppendHtml("<thead>");
            output.Content.AppendHtml("<tr>");
            foreach (var propriedade in listaDeProriedades)
            {
                if (!propriedade.Name.Equals("Id") &&
                    !propriedade.PropertyType.ToString().Contains("System.Collection"))
                {
                    output.Content.AppendHtml($"<th>{propriedade.GetDisplay()}</th>");
                }
            }
            output.Content.AppendHtml("<th class=\"text-center\">Ação</th>");
            output.Content.AppendHtml("</tr>");
            output.Content.AppendHtml("</thead>");
        }

        private void AddTableBody(TagHelperOutput output, PropertyInfo[] listaDeProriedades)
        {
            output.Content.AppendHtml("<tbody>");
            foreach (var item in Items)
            {
                output.Content.AppendHtml("<tr>");
                foreach (var propriedade in listaDeProriedades)
                {
                    if (!propriedade.Name.Equals("Id") && !propriedade.PropertyType.ToString().Contains("Collection"))
                    {
                        var valor = propriedade.GetValue(item);
                        if (propriedade.PropertyType.FullName == typeof(Boolean).FullName)
                        {
                            String texto = (Boolean)valor == true ? "Sim" : "Não";
                            output.Content.AppendHtml($"<td>{texto}</td>");
                        }
                        else if (propriedade.PropertyType.FullName == typeof(DateTime).FullName)
                        {
                            DateTime data = (DateTime)valor;
                            output.Content.AppendHtml($"<td>{data.ToBrazilianDate()}</td>");
                        }
                        else
                        {
                            output.Content.AppendHtml($"<td>{valor}</td>");
                        }
                    }
                }
                AddColunaAcoes(output, item);
            }
            output.Content.AppendHtml("</tr>");
            output.Content.AppendHtml("</tbody>");
        }

        private void AddColunaAcoes(TagHelperOutput output, object dado)
        {
            PropertyInfo[] listaDePropriedades = GetItemProperties();
            string model = String.Empty;
            StringBuilder htmlBotoes;

            foreach (PropertyInfo propriedade in listaDePropriedades)
            {
                if (propriedade.Name.Equals("Id"))
                {
                    var value = propriedade.GetValue(dado);
                    model = propriedade.ReflectedType.Name;
                    htmlBotoes = new StringBuilder();
                    htmlBotoes.Append("<td>");
                    htmlBotoes.Append($"<button class=\"btn btn-default details\" data-id=\"{value}\" data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"Detalhes\" data-original-title=\"Detalhes\">");
                    htmlBotoes.Append("<i class=\"glyphicon glyphicon-file\"></i>");
                    htmlBotoes.Append("</button>");
                    htmlBotoes.Append($"<button class=\"btn btn-primary edit\" data-id=\"{value}\" data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"Editar\" data-original-title=\"Editar\">");
                    htmlBotoes.Append("<i class=\"glyphicon glyphicon-edit\"></i>");
                    htmlBotoes.Append("</button>");
                    htmlBotoes.Append($"<button class=\"btn btn-danger delete\" data-id=\"{value}\" data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"Excluir\" data-original-title=\"Excluir\">");
                    htmlBotoes.Append("<i class=\"glyphicon glyphicon-trash\"></i>");
                    htmlBotoes.Append("</button>");
                    htmlBotoes.Append("</td>");
                    output.Content.AppendHtml(htmlBotoes.ToString());
                }
            }
        }

        private void AddTableFooter(TagHelperOutput output, PropertyInfo[] listaDeProriedades)
        {
            output.Content.AppendHtml("<tfoot>");
            output.Content.AppendHtml("<tr>"); 
            foreach (var propriedade in listaDeProriedades)
            {
                if (!propriedade.Name.Equals("Id") &&
                    !propriedade.PropertyType.ToString().Contains("System.Collection"))
                {
                    output.Content.AppendHtml($"<th>{propriedade.GetDisplay()}</th>");
                }
            }
            output.Content.AppendHtml("<th class=\"text-center\">Ação</th>");
            output.Content.AppendHtml("</tfoot>");
        }

        private PropertyInfo[] GetItemProperties()
        {
            var listType = Items.GetType();
            if (listType.IsGenericType)
            {
                Type itemType = listType.GetGenericArguments().First();
                return itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            }
            return new PropertyInfo[] { };
        }

    }
}