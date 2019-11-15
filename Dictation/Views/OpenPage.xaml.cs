namespace Dictation.Views
{
    using Dictation.ViewModels;
    using Windows.UI.Xaml.Controls;

    public sealed partial class OpenPage : Page
    {
        private OpenViewModel viewModel;

        public OpenPage()
        {
            viewModel = App.Locator.OpenViewModel;
            this.InitializeComponent();
        }
    }
}
