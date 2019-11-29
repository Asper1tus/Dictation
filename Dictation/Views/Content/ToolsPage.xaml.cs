namespace Dictation.Views.Content
{
    using Dictation.ViewModels.Content;
    using Windows.UI.Xaml.Controls;

    public sealed partial class ToolsPage : Page
    {
        private readonly ToolsViewModel viewModel;

        public ToolsPage()
        {
            viewModel = App.Locator.ToolsViewModel;
            this.InitializeComponent();
        }
    }
}
