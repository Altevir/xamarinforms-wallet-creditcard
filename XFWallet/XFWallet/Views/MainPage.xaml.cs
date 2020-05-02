using System.ComponentModel;
using Xamarin.Forms;
using XFWallet.ViewModels;

namespace XFWallet
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        MainPageViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new MainPageViewModel();
        }

        protected override void OnAppearing()
        {
            viewModel.SubscribeAddCard();
        }

        protected override void OnDisappearing()
        {
            viewModel.UnsubscribedAddCard();
        }
    }
}
