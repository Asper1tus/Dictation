namespace Dictation
{
    using Dictation.ViewModels;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    public sealed partial class MainPage : Page
    {

        public MainPageViewModel Logic { get; } = new MainPageViewModel(Window.Current.Content as Frame);

        public RecognizerViewModel DocumentLogic { get; } = new RecognizerViewModel();

        public MainPage()
        {
            InitializeComponent(); 
            NavigationCacheMode = NavigationCacheMode.Enabled;
        }
    }
}