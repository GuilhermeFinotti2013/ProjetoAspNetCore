using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetCore.Mvc.Extensions.TagHelpers
{
    public class NossoEmailTagHelper : TagHelper
    {
        public string Dominio { get; set; } = "gmail.com";
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            var prefixo = await output.GetChildContentAsync();
            var email = prefixo.GetContent() + "@" + Dominio;
            output.Attributes.SetAttribute("href", "mailto:" + email);
            output.Content.SetContent(email);
        }
    }
}
