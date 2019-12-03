namespace Dictation.Views
{
    using Dictation.Views.HelpPages;
    using Windows.UI.Xaml.Controls;

    public sealed partial class HelpPage : Page
    {
        public HelpPage()
        {
            this.InitializeComponent();

            FAQ.Navigate(typeof(FaqPage));
            Shortcut.Navigate(typeof(ShortcutKeysPage));
            About.Navigate(typeof(AboutPage));
        }
    }
}
