namespace Dictation.Views.HelpPages
{
    using Dictation.ViewModels.HelpPages;
    using Windows.UI.Xaml.Controls;

    public sealed partial class AboutPage : Page
    {
        private AboutViewModel viewModel;

        public AboutPage()
        {
            viewModel = App.Locator.AboutViewModel;
            this.InitializeComponent();
        }
    }
}
