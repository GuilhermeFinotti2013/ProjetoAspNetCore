using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace ProjetoAspNetCore.DomainCore.Extensions
{
    public static class GenericEnumExtensionDescription
    {
        public static string ObterDescricao(this Enum _enum)
        {
            Type genericEnumType = _enum.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(_enum.ToString());
            if (memberInfo.Length <= 0)
            {
                return _enum.ToString();
            }

            var attributs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);

            return attributs.Any() ? ((System.ComponentModel.DescriptionAttribute)attributs.ElementAt(0)).Description : _enum.ToString();
        }
    }
}
