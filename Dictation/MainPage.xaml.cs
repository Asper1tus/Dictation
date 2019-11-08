namespace Dictation
{
    using Dictation.ViewModels;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    public sealed partial class MainPage : Page
    {
        MainPageViewModel ViewModel;

        public MainPage()
        {
            InitializeComponent();
            ViewModel = App.Locator.MainPageViewModel;
            NavigationCacheMode = NavigationCacheMode.Enabled;
            ViewModel.Initialize(ContentFrame);
        }
    }
}