namespace Dictation.Views
{
    using Dictation.ViewModels;
    using Windows.UI.Xaml.Controls;

    public sealed partial class Menu : Page
    {
        private MenuViewModel viewModel;

        public Menu()
        {
            InitializeComponent();
            viewModel = App.Locator.MenuViewModel;
            viewModel.Initialize(ContentFrame, NavView);
        }
    }
}
