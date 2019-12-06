namespace Dictation.Views
{
    using Dictation.ViewModels;
    using Windows.UI.Xaml.Controls;

    public sealed partial class Menu : Page
    {
        private MenuViewModel viewModel;

        public Menu()
        {
            viewModel = new MenuViewModel();
            InitializeComponent();
            viewModel.Initialize(ContentFrame, NavView);
        }
    }
}
