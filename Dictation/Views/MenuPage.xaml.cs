namespace Dictation.Views
{
    using Dictation.ViewModels;
    using Windows.UI.Xaml.Controls;

    public sealed partial class MenuPage : Page
    {
        private MenuViewModel viewModel;

        public MenuPage()
        {
            viewModel = new MenuViewModel();
            InitializeComponent();
            viewModel.Initialize(ContentFrame, NavView);
        }
    }
}
