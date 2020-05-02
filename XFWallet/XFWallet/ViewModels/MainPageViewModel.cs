using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XFWallet.Helpers;
using XFWallet.Models;
using XFWallet.ViewModel;
using XFWallet.Views;

namespace XFWallet.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel()
        {
            Cards = new ObservableCollection<Card>();
            Stores = new ObservableCollection<Store>();
            StoresSearch = new ObservableCollection<Store>();
            OpenLinkCommand = new Command<Store>(async (param) => await ExecuteOpenLinkCommand(param));
            SearchCommand = new Command(async () => await ExecuteSearchCommand());
            ClearSearchTextCommand = new Command(async () => await ExecuteClearSearchTextCommand());
            NavigateToAddCreditCardPageCommand = new Command(async () => await ExecuteNavigateToAddCreditCardPageCommand());
            GetCards();
            GetStores();
        }

        public Command OpenLinkCommand { get; }
        public Command SearchCommand { get; }
        public Command NavigateToAddCreditCardPageCommand { get; }
        public Command ClearSearchTextCommand { get; }
        public ObservableCollection<Card> Cards { get; set; }
        public ObservableCollection<Store> Stores { get; set; }
        public ObservableCollection<Store> StoresSearch { get; set; }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    if (string.IsNullOrEmpty(_searchText))
                        SearchCommand.Execute(_searchText);
                }
            }
        }

        private bool _isVisibleBtnClose;
        public bool IsVisibleBtnClose
        {
            get { return _isVisibleBtnClose; }
            set { SetProperty(ref _isVisibleBtnClose, value); }
        }

        void GetCards()
        {
            Cards.Add(new Card()
            {
                cardNumber = "5555666677778884",
                cardName = "ALTEVIR CARDOSO NETO",
                cardFlag = "mastercard.png"
            });

            Cards.Add(new Card()
            {
                cardNumber = "4012001037141112",
                cardName = "CHETAN SINGH",
                cardFlag = "visa.png"
            });

            Cards.Add(new Card()
            {
                cardNumber = "376449047333005",
                cardName = "XAMARIN FORMS",
                cardFlag = "amex.png"
            });

            SetCardBackground();
        }

        void GetStores()
        {
            Stores.Add(new Store()
            {
                name = "McDonalds",
                link = "https://www.mcdonalds.com/us/en-us.html",
                image = "mcdonalds.png"
            });

            Stores.Add(new Store()
            {
                name = "Careem",
                link = "https://www.careem.com/",
                image = "careem.png"
            });

            Stores.Add(new Store()
            {
                name = "Centrepoint",
                link = "https://www.centrepointstores.com/",
                image = "centrepoint.png"
            });

            Stores.Add(new Store()
            {
                name = "Amazon",
                link = "https://www.amazon.com/",
                image = "amazon.png"
            });

            Stores.Add(new Store()
            {
                name = "Starbucks",
                link = "https://www.starbucks.com/",
                image = "starbucks.png"
            });

            foreach (var item in Stores)
                StoresSearch.Add(item);
        }

        private async Task ExecuteOpenLinkCommand(Store param)
        {
            await Browser.OpenAsync(param.link, BrowserLaunchMode.SystemPreferred);
        }

        private async Task ExecuteSearchCommand()
        {
            var stores = await Search(SearchText);
            StoresSearch.Clear();
            foreach (var item in stores)
                StoresSearch.Add(item);
        }

        Task<List<Store>> Search(string text)
        {
            List<Store> stores = new List<Store>();
            IsVisibleBtnClose = false;

            if (string.IsNullOrEmpty(text))
            {
                foreach (var item in Stores)
                    stores.Add(item);
            }
            else
            {
                var result = Stores.Where(p => p.name.ToLower().Contains(text.ToLower()));
                IsVisibleBtnClose = !result.Any();

                if (result.Any())
                    foreach (var item in result)
                        stores.Add(item);
            }

            return Task.FromResult(stores);
        }

        private async Task ExecuteNavigateToAddCreditCardPageCommand()
        {
            await Navigation.PushPopupAsync(new AddCreditCardPage());
        }

        public void SubscribeAddCard()
        {
            MessagingCenter.Subscribe<AddCreditCardPageViewModel, Card>(this, "addCard", (s, param) =>
            {
                Cards.Add(param);
                SetCardBackground();
            });
        }

        void SetCardBackground()
        {
            for (int i = 0; i < Cards.Count(); i++)
            {
                if (Helper.IsOddNumber(i))
                    Cards[i].cardBackground = "mask.png";
                else
                    Cards[i].cardBackground = "mask2.png";
            }
        }

        public void UnsubscribedAddCard()
        {
            MessagingCenter.Unsubscribe<AddCreditCardPageViewModel>(this, "addCard");
        }

        private async Task ExecuteClearSearchTextCommand()
        {
            IsVisibleBtnClose = false;
            SearchText = string.Empty;

            var stores = await Search(SearchText);
            StoresSearch.Clear();
            foreach (var item in stores)
                StoresSearch.Add(item);
        }
    }
}
