namespace Dictation.Views.ContentDialogs
{
    using Dictation.ViewModels.ContentDialogs;
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
