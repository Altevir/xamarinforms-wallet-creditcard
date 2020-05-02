using System;
using System.Text.RegularExpressions;

namespace XFWallet.Helpers
{
    public static class ExtensionMethods
    {
        public static string DefaultString(this object value)
        {
            if (string.IsNullOrEmpty(Convert.ToString(value)))
            {
                return string.Empty;
            }
            return Convert.ToString(value).Trim();
        }

        public static string RemoveNonNumbers(this string texto)
        {
            if (string.IsNullOrEmpty(texto.DefaultString()))
                return string.Empty;

            var regex = new Regex(@"[^\d]");
            return regex.Replace(texto, "");
        }

        public static string FirstCharToUpper(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return input.Substring(0, 1).ToUpper() + string.Join("", input.Substring(1, input.Length - 1));
            }

            return null;
        }

        public static string RemoverAcentos(this string texto)
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (int a = 0; a < comAcentos.Length; a++)
            {
                texto = texto.Replace(comAcentos[a].ToString(), semAcentos[a].ToString());
            }

            return texto;
        }
    }
}
