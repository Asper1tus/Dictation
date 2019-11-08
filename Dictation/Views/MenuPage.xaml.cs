namespace Dictation.Views
{
    using Dictation.ViewModels;
    using Windows.UI.Xaml.Controls;

    public sealed partial class Menu : Page
    {
        public MenuViewModel ViewModel;

        public Menu()
        {
            InitializeComponent();
            ViewModel = App.Locator.MenuViewModel;
            ViewModel.Initialize(ContentFrame, NavView);
        }
    }
}
