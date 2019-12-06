namespace Dictation.Views
{
    using Dictation.ViewModels;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    public sealed partial class OpenPage : Page
    {
        private readonly OpenViewModel viewModel;

        public OpenPage()
        {
            viewModel = App.Locator.OpenViewModel;
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await viewModel.InitializeAsync().ConfigureAwait(true);
        }
    }
}
