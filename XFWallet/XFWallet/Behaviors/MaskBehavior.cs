using Xamarin.Forms;

namespace XFWallet.Behaviors
{
    public class MaskBehavior : Behavior<Entry>
    {
        private string _maskText = "";
        public string MaskText
        {
            get => _maskText;
            set
            {
                _maskText = value;
            }
        }

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            var entry = sender as Entry;
            var text = entry.Text;

            if (!string.IsNullOrWhiteSpace(MaskText))
                if (text.Length == _maskText.Length)
                    entry.MaxLength = _maskText.Length;

            if ((args.OldTextValue == null) || (args.OldTextValue.Length <= args.NewTextValue.Length))
                for (int i = MaskText.Length; i >= text.Length; i--)
                {
                    if (MaskText[(text.Length - 1)] != '#')
                    {
                        text = text.Insert((text.Length - 1), MaskText[(text.Length - 1)].ToString());
                    }
                }

            entry.Text = text;
            entry.Placeholder = MaskText;
        }
    }
}
