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
        Frame rootFrame;
        public MainPageViewModel Logic { get; } = new MainPageViewModel();

        public MainPage()
        {
            InitializeComponent();
            rootFrame = Window.Current.Content as Frame;
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            rootFrame.Navigate(typeof(Menu), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }

    }
}