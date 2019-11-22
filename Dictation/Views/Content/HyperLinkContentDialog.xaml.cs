namespace Dictation.Views.Content
{
    using Dictation.ViewModels.Content;
    using Windows.UI.Xaml.Controls;

    public sealed partial class HyperLinkContentDialog : ContentDialog
    {
        private readonly HyperLinkViewModel viewModel;

        public HyperLinkContentDialog()
        {
            viewModel = new HyperLinkViewModel();
            this.InitializeComponent();
        }
    }
}
