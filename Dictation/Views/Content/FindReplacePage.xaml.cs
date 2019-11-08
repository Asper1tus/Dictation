namespace Dictation.Views.Content
{
    using Dictation.ViewModels;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class FindReplacePage : Page
    {
        public FindReplaceViewModel ViewModel;
        public FindReplacePage()
        {
            ViewModel = App.Locator.FindReplaceViewModel ;
            this.InitializeComponent();
        }
    }
}
