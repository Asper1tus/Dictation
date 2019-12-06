namespace Dictation.Views.Content
{
    using Dictation.ViewModels;
    using Windows.UI.Xaml.Controls;

    public sealed partial class FindReplacePage : Page
    {
        private readonly FindReplaceViewModel viewModel;

        public FindReplacePage()
        {
            viewModel = new FindReplaceViewModel();
            this.InitializeComponent();
        }
    }
}
