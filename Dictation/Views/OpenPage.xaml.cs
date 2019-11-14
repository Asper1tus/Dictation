namespace Dictation.Views
{
    using Dictation.ViewModels;
    using Windows.UI.Xaml.Controls;

    public sealed partial class OpenPage : Page
    {
        public OpenViewModel ViewModel;

        public OpenPage()
        {
            ViewModel = App.Locator.OpenViewModel;
            this.InitializeComponent();
        }
    }
}
