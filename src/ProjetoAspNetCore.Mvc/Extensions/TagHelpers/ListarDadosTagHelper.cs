using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

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
            var props = GetItemProperties();
            AddTableHeader(output, props);
            AddTableBody(output, props);
            AddTableFooter(output, props);
        }
   
        private void AddTableHeader(TagHelperOutput output, PropertyInfo[] props)
        {
            output.Content.AppendHtml("<thead>");
            output.Content.AppendHtml("<tr>");
            foreach (var prop in props)
            {
                if (!prop.PropertyType.ToString().Contains("System.Collection"))
                {
                    var name = GetPropertyName(prop);
                    output.Content.AppendHtml(!name.Equals("Id") ? $"<th>{name}</th>" : $"<th class=\"text-center\">Ação</th>");
                }
            }
            output.Content.AppendHtml("</tr>");
            output.Content.AppendHtml("</thead>");
        }

        private void AddTableBody(TagHelperOutput output, PropertyInfo[] props)
        {
            string model = String.Empty;
            StringBuilder htmlBotoes;
            output.Content.AppendHtml("<tbody>");
            foreach (var item in Items)
            {
                output.Content.AppendHtml("<tr>");
                foreach (var prop in props)
                {
                    var value = GetPropertyValue(prop, item);
                    if (prop.Name.Equals("Id"))
                    {
                        model = prop.ReflectedType.Name;
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
                        //   output.Content.AppendHtml($"<td><a href='/{model}/Details/{value}' ><span class='fa fa-search fa-2x' title='Detalhes'></a>");
                    }
                    else
                    {
                        if (!prop.PropertyType.ToString().Contains("Collection"))
                            output.Content.AppendHtml($"<td>{value}</td>");
                    }
                }
                output.Content.AppendHtml("</tr>");

                // <span class="fa fa-pencil-square fa-2x" title="Editar Paciente"></span>
            }
            output.Content.AppendHtml("</tbody>");
        }

        private void AddTableFooter(TagHelperOutput output, PropertyInfo[] props)
        {
            output.Content.AppendHtml("<tfoot>");
            output.Content.AppendHtml("<tr>");
            foreach (var prop in props)
            {
                if (!prop.PropertyType.ToString().Contains("System.Collection"))
                {
                    var name = GetPropertyName(prop);
                    output.Content.AppendHtml(!name.Equals("Id") ? $"<th>{name}</th>" : $"<th class=\"text-center\">Ação</th>");
                }
            }
            output.Content.AppendHtml("</tr>");
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

        /// <summary>
        /// ------------------------------------------------------------------ //
        /// //Funciona com essa notação: [DisplayName(displayName:"Descrição")]
        /// var attribute = property.GetCustomAttribute<DisplayNameAttribute>();
        /// if (attribute != null)
        /// {
        ///     return attribute.DisplayName; // Muda aqui
        /// }
        /// return property.Name;
        /// ------------------------------------------------------------------ // 
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>

        private string GetPropertyName(PropertyInfo property)
        {


            // Funciona com essa notação: [Display(Name = "Estado do Paciente")]
            var attribute = property.GetCustomAttribute<DisplayAttribute>();
            if (attribute != null)
            {
                return attribute.Name; // Muda aqui
            }
            return property.Name;


        }
        private object GetPropertyValue(PropertyInfo property, object instance)
        {
            return property.GetValue(instance);
        }
    }
}