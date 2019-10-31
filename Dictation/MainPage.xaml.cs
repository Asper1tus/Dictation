namespace Dictation
{
    using Dictation.ViewModels;
    using Dictation.Views;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media.Animation;
    using Windows.UI.Xaml.Navigation;

    public sealed partial class MainPage : Page
    {
        private Frame rootFrame;

        public MainPageViewModel Logic { get; } = new MainPageViewModel();

        public MainPage()
        {
            InitializeComponent(); 
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Menu), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }

    }
}