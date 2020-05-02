using System.Linq;
using System.Text.RegularExpressions;

namespace XFWallet.Helpers
{
    public class CreditCardHelper
    {
        public static Regex VisaRegex = new Regex(@"^4[0-9]{6,}$");
        public static Regex MasterRegex = new Regex(@"^5[1-5][0-9]{14}");
        public static Regex AmexRegex = new Regex(@"^3[47][0-9]{5,}$");

        public static string FindFlag(string numCartao)
        {
            if (string.IsNullOrEmpty(numCartao))
                return string.Empty;

            numCartao = numCartao.RemoveNonNumbers();

            if (VisaRegex.IsMatch(numCartao)) return "VISA";
            if (AmexRegex.IsMatch(numCartao)) return "AMEX";
            if (MasterRegex.IsMatch(numCartao)) return "MASTERCARD";

            return string.Empty;
        }

        public static string FindFlagCard(string numCartao)
        {
            string bandeira = string.Empty;
            if (string.IsNullOrEmpty(numCartao))
                return bandeira;

            var flag = FindFlag(numCartao);

            switch (flag)
            {
                case "VISA": bandeira = "visa.png"; break;
                case "MASTERCARD": bandeira = "mastercard.png"; break;
                case "AMEX": bandeira = "amex.png"; break;
            }

            return bandeira;
        }

        public static bool IsValidLuhnn(string val)
        {

            int currentDigit;
            int valSum = 0;
            int currentProcNum = 0;

            for (int i = val.Length - 1; i >= 0; i--)
            {
                if (!int.TryParse(val.Substring(i, 1), out currentDigit))
                    return false;

                currentProcNum = currentDigit << (1 + i & 1);
                valSum += (currentProcNum > 9 ? currentProcNum - 9 : currentProcNum);
            }

            return (valSum > 0 && valSum % 10 == 0);
        }

        public static bool IsValidCreditCardNumber(string number)
        {
            if (string.IsNullOrEmpty(number))
                return false;

            number = number.RemoveNonNumbers();

            if (number.All(char.IsDigit) == false)
                return false;

            if (12 > number.Length || number.Length > 19)
                return false;

            return IsValidLuhnn(number);
        }
    }
}
