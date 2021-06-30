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
        private int tamanhoColunaCampo = 10;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.Add("class", "form-horizontal");

            if (ConfiguracaoFormulario.TamalhoLabel == 0 || ConfiguracaoFormulario.TamalhoLabel == 3)
            {
                tamanhoColunaCampo = 8;
            }

            if (ConfiguracaoFormulario == null || ConfiguracaoFormulario.Dado == null ||
                ConfiguracaoFormulario.TipoDoFormulario == 0 || ConfiguracaoFormulario.Titulo == null)
            {
                output.Content.AppendHtml("<h1 style=\"color: red;\">Falha ao configurar o formulário!</h1>");
            }
            else
            {
                output.Content.AppendHtml($"<h4>{ConfiguracaoFormulario.Titulo}</h4>");
                output.Content.AppendHtml("<hr/>");

                #region Se for edição, monta input hidden
                if (ConfiguracaoFormulario.TipoDoFormulario == TipoFormulario.Editar ||
                    ConfiguracaoFormulario.TipoDoFormulario == TipoFormulario.EditarEspecializado)
                {
                    output.Content.AppendHtml($"<input data-val=\"true\" id=\"Id\" name=\"Id\" type=\"hidden\" value=\"{GetIdDado()}\">");
                }
                #endregion

                if (ConfiguracaoFormulario.TipoDoFormulario == TipoFormulario.Editar ||
                     ConfiguracaoFormulario.TipoDoFormulario == TipoFormulario.Inserir)
                {
                    MontarFormularioSimples(output);
                }
                else if (ConfiguracaoFormulario.TipoDoFormulario == TipoFormulario.EditarEspecializado ||
                         ConfiguracaoFormulario.TipoDoFormulario == TipoFormulario.InserirEspecializado)
                {
                    MontarFormularioEspecializado(output);
                }
            }
        }

        private void MontarFormularioSimples(TagHelperOutput output)
        {
            PropertyInfo[] listaDePropriedades = ConfiguracaoFormulario.Dado.GetType().GetProperties();
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

        private void MontarFormularioEspecializado(TagHelperOutput output)
        {
            PropertyInfo[] listaDePropriedades = ConfiguracaoFormulario.Dado.GetType().GetProperties();
            TOConfiguracaoCampo configuracaoCampo = null;
            foreach (PropertyInfo propriedade in listaDePropriedades)
            {
                if (!propriedade.Name.Equals("Id") || propriedade.PropertyType.FullName.Contains("ProjetoAspNetCore.Domain.Entities."))
                {
                    configuracaoCampo = VerificarComplexidadeDoCampo(propriedade.Name);
                    if (configuracaoCampo != null)
                    {
                        switch (configuracaoCampo.Tipo)
                        {
                            case TipoCampo.Email:
                                break;
                            case TipoCampo.SimNao:
                                AddCampoSimNao(output, propriedade);
                                break;
                            case TipoCampo.Combo:
                                AddCampoCombo(output, propriedade, configuracaoCampo);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        AddCampoEditavel(output, propriedade);
                    }
                }
            }
        }

        private void AddCampoEditavel(TagHelperOutput output, PropertyInfo propriedade)
        {
            output.Content.AppendHtml("<div class=\"form-group\">");
            output.Content.AppendHtml($"<label class=\"control-label col-md-{ConfiguracaoFormulario.TamalhoLabel}\" for=\"{propriedade.Name}\">{propriedade.GetDisplay()}</label>");
            output.Content.AppendHtml($"<div class=\"col-md-{tamanhoColunaCampo}\">");
            output.Content.AppendHtml("<input class=\"form-control text-box single-line\" ");
            output.Content.AppendHtml($"data-val=\"true\" data-val-required=\"O campo {propriedade.GetDisplay()} é obrigatório.\" ");
            output.Content.AppendHtml($"id=\"{propriedade.Name}\" name=\"{propriedade.Name}\" ");
            if (propriedade.PropertyType.FullName == typeof(String).FullName)
            {
                output.Content.AppendHtml("type=\"text\" ");
            }
            else if (propriedade.PropertyType.FullName == typeof(Int16).FullName ||
                    propriedade.PropertyType.FullName == typeof(Int32).FullName ||
                    propriedade.PropertyType.FullName == typeof(Int64).FullName)
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

        private void AddCampoCombo(TagHelperOutput output, PropertyInfo propriedade, TOConfiguracaoCampo configuracaoCampo)
        {
            output.Content.AppendHtml("<div class=\"form-group\">");
            output.Content.AppendHtml($"<label class=\"control-label col-md-{ConfiguracaoFormulario.TamalhoLabel}\" for=\"{propriedade.Name}\">{propriedade.GetDisplay()}</label>");
            output.Content.AppendHtml($"<div class=\"col-md-{tamanhoColunaCampo}\">");
            output.Content.AppendHtml($"<select class=\"form-control\" data-val=\"true\" data-val-required=\"O campo {propriedade.GetDisplay()} é obrigatório.\" ");
            output.Content.AppendHtml($"id=\"{propriedade.Name}\" name=\"{propriedade.Name}\" >");
            if (ConfiguracaoFormulario.TipoDoFormulario == TipoFormulario.InserirEspecializado)
            {
                output.Content.AppendHtml("<option value=\"\" selected=\"selected\">Selecione uma opção</option>");
            }
            else
            {
                output.Content.AppendHtml("<option value=\"\">Selecione uma opção</option>");
            }
            foreach (TOSelectItem item in configuracaoCampo.ItensCombo)
            {
                output.Content.AppendHtml($"<option value=\"{item.Valor}\"");
                if (propriedade.GetValue(ConfiguracaoFormulario.Dado) == item.Valor)
                {
                    output.Content.AppendHtml("selected=\"selected\" ");
                }
                output.Content.AppendHtml($">{item.Descricao}</option>");
            }
            output.Content.AppendHtml("</select>");
            output.Content.AppendHtml("</div>");
            output.Content.AppendHtml("</div>");
        }

        private void AddCampoSimNao(TagHelperOutput output, PropertyInfo propriedade)
        {
            output.Content.AppendHtml("<div class=\"form-group\">");
            output.Content.AppendHtml($"<label class=\"control-label col-md-{ConfiguracaoFormulario.TamalhoLabel}\" for=\"{propriedade.Name}\">{propriedade.GetDisplay()}</label>");
            output.Content.AppendHtml($"<div class=\"col-md-{tamanhoColunaCampo}\">");
            output.Content.AppendHtml("<div class=\"toggle-custom\">");
            output.Content.AppendHtml("<label class=\"toggle\" data-on=\"Sim\" data-off=\"Não\">");
            output.Content.AppendHtml($"<input type=\"checkbox\" data-val=\"true\" id=\"{propriedade.Name}\" name=\"{propriedade.Name}\" value=\"\">");
            output.Content.AppendHtml("<span class=\"button-checkbox\"></span>");
            output.Content.AppendHtml("</label>");
            output.Content.AppendHtml("</div>");
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

        private TOConfiguracaoCampo VerificarComplexidadeDoCampo(string nomeCampo)
        {
            TOConfiguracaoCampo configuracaoCampo = null;
            foreach (TOConfiguracaoCampo campo in ConfiguracaoFormulario.ConfiguracaoCampos)
            {
                if (configuracaoCampo == null && campo.NomePropriedade.Equals(nomeCampo))
                {
                    configuracaoCampo = campo;
                }
            }
            return configuracaoCampo;
        }
    }
}
