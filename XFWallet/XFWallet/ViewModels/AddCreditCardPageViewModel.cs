using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XFWallet.Helpers;
using XFWallet.Models;
using XFWallet.ViewModel;

namespace XFWallet.ViewModels
{
    public class AddCreditCardPageViewModel : BaseViewModel
    {
        public AddCreditCardPageViewModel()
        {
            AddCardCommand = new Command(async () => await ExecuteAddCardCommand());
            ClosePopUpCommand = new Command(async () => await ExecuteClosePopUpCommand());
        }

        public Command AddCardCommand { get; }
        public Command ClosePopUpCommand { get; }

        private string _cardName;
        public string CardName
        {
            get { return _cardName; }
            set { SetProperty(ref _cardName, value); }
        }

        private string _cardNumber;
        public string CardNumber
        {
            get { return _cardNumber; }
            set
            {
                if (SetProperty(ref _cardNumber, value))
                    CardFlag = CreditCardHelper.FindFlagCard(_cardNumber);
            }
        }

        private string _cardExpirationDate;
        public string CardExpirationDate
        {
            get { return _cardExpirationDate; }
            set { SetProperty(ref _cardExpirationDate, value); }
        }

        private string _cardCVV;
        public string CardCVV
        {
            get { return _cardCVV; }
            set { SetProperty(ref _cardCVV, value); }
        }

        private string _cardFlag;
        public string CardFlag
        {
            get { return _cardFlag; }
            set { SetProperty(ref _cardFlag, value); }
        }

        private async Task ExecuteAddCardCommand()
        {
            if (string.IsNullOrEmpty(CardName.DefaultString()))
            {
                await DisplayAlert("Information", "Enter the name!", "OK");
                return;
            }

            if (string.IsNullOrEmpty(CardNumber.DefaultString()))
            {
                await DisplayAlert("Information", "Enter the card number!", "OK");
                return;
            }

            if (CardNumber.DefaultString().Length < 16 ||
                CardNumber.DefaultString().Length < 15)
            {
                await DisplayAlert("Information", "Incomplete card number!", "OK");
                return;
            }

            if (!CreditCardHelper.IsValidCreditCardNumber(CardNumber))
            {
                await DisplayAlert("Information", "Card number is invalid!", "OK");
                return;
            }

            if (string.IsNullOrEmpty(CardExpirationDate.DefaultString()))
            {
                await DisplayAlert("Information", "Enter the expiration date!", "OK");
                return;
            }

            if (string.IsNullOrEmpty(CardCVV.DefaultString()))
            {
                await DisplayAlert("Information", "Enter the secuity code!", "OK");
                return;
            }

            var confirm = await DisplayAlert("Confirmation", "Confirm registration?", "YES", "NO");
            if (confirm)
            {
                var card = new Card()
                {
                    cardName = CardName,
                    cardNumber = CardNumber.RemoveNonNumbers(),
                    cardExpirationDate = CardExpirationDate,
                    cardCVV = CardCVV,
                    cardFlag = CardFlag
                };

                card.cardFlag = CreditCardHelper.FindFlagCard(card.cardNumber);
                MessagingCenter.Send(this, "addCard", card);
                await PopupNavigation.Instance.PopAsync(true);
            }
        }

        private async Task ExecuteClosePopUpCommand()
        {
            await PopupNavigation.Instance.PopAsync(true);
        }
    }
}
