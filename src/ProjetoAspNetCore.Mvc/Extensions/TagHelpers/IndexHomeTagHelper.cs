using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetCore.Mvc.Extensions.TagHelpers
{
    public class IndexHomeTagHelper : TagHelper
    {
        [HtmlAttributeName("imagem")]
        public string Imagem { get; set; }
        [HtmlAttributeName("titulo")]
        public string Titulo { get; set; }
        [HtmlAttributeName("controlador")]
        public string Controldor { get; set; }
        [HtmlAttributeName("visualizacao")]
        public string Visualizacao { get; set; }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "session";
            output.Content.AppendHtml("<div class=\"col-md-3 col-sm-3 col-sx-12\" style=\"padding-bottom: 15px; \">");
            output.Content.AppendHtml("<div class=\"card card-inverse text-md-center text-sm-center text-xs-center\">");
            output.Content.AppendHtml($"<a href=\"/{Controldor}/{Visualizacao}\" style=\"text-decoration: none; color: #ed5353; font-size: 16px;\">");
            output.Content.AppendHtml($"<img src=\"/images/{Imagem}\" class=\"card-img-top\" style=\"width: 100%; height: auto\" />");
            output.Content.AppendHtml("</a>");
            output.Content.AppendHtml("<div class=\"card-img-overlay\">");
            output.Content.AppendHtml("<div style=\"text-align: center\">");
            output.Content.AppendHtml($"<a href=\"/{Controldor}/{Visualizacao}\" style=\"text-decoration: none; color: #ed5353; font-size: 16px;\">");
            output.Content.AppendHtml($"<h4 class=\"card-title\">{Titulo}</h4>");
            output.Content.AppendHtml("</a>");
            output.Content.AppendHtml("</div>");
            output.Content.AppendHtml("</div>");
            output.Content.AppendHtml("</div>");
            output.Content.AppendHtml("</div>");
            return base.ProcessAsync(context, output);
        }
        
    }
}
