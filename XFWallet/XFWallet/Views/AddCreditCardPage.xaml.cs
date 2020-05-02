using Rg.Plugins.Popup.Pages;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFWallet.ViewModels;

namespace XFWallet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCreditCardPage : PopupPage
    {
        public AddCreditCardPage()
        {
            InitializeComponent();
            BindingContext = new AddCreditCardPageViewModel();
            entryCardName.TextChanged += (sender, e) =>
            {
                var entry = (Entry)sender;
                entry.Text = Regex.Replace(e.NewTextValue.ToLower(), @"(?<=\b)[a-zA-Z]", m => m.Value.ToUpper());
            };
        }
    }
}