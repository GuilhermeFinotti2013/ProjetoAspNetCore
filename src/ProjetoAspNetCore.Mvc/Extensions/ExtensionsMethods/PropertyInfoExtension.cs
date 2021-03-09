using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ProjetoAspNetCore.Mvc.Extensions.ExtensionsMethods
{
    public static class PropertyInfoExtension
    {
        /// <summary>
        /// Se propriedade conter a dataAnnotation Display, retorna seu conteúdo, caso contrário, retorna seu nome.
        /// </summary>
        /// <param name="propridade">Propridade</param>
        /// <returns>Display do atributo</returns>
        public static string GetDisplay(this PropertyInfo propridade)
        {
            string descricao = String.Empty;
            IEnumerable<Attribute> listaAtributos = propridade.GetCustomAttributes();
            bool temDisplayAttribute = false;
            if (listaAtributos.Count() > 0)
            {
                DisplayAttribute displayAttribute;
                foreach (Attribute atributo in listaAtributos)
                {
                    if (atributo.GetType().FullName == typeof(DisplayAttribute).FullName)
                    {
                        displayAttribute = (DisplayAttribute)atributo;
                        descricao = displayAttribute.Name;
                        temDisplayAttribute = true;
                    }
                }
            }

            if (!temDisplayAttribute)
            {
                descricao = propridade.Name;
            }
            return descricao;
        }
    }
}
