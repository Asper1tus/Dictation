namespace Dictation
{
    using Dictation.ViewModels;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    public sealed partial class MainPage : Page
    {
        private MainPageViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();
            viewModel = App.Locator.MainPageViewModel;
            NavigationCacheMode = NavigationCacheMode.Enabled;
            viewModel.Initialize(ContentFrame);
        }
    }
}