namespace XFWallet.Models
{
    public class Card
    {
        public string cardName { get; set; }
        public string cardNumber { get; set; }
        public string cardExpirationDate { get; set; }
        public string cardCVV { get; set; }
        public string cardFlag { get; set; }
        public string cardBackground { get; set; }
        public string cardFakeNumber =>
            cardNumber.Length == 16 ?
            $"{cardNumber.Substring(0, 4)}{new string(' ', 8)}****{new string(' ', 8)}****{new string(' ', 8)}{cardNumber.Substring(12, 4)}" :
            $"{cardNumber.Substring(0, 4)}{new string(' ', 8)}****{new string(' ', 8)}****{new string(' ', 8)}{cardNumber.Substring(11, 4)}";
    }
}
