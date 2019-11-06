namespace Dictation.Views.Content
{
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class FindReplace : Page
    {

        public FindReplace()
        {
            this.InitializeComponent();
            MainPage mainPage = new MainPage();
        }
    }
}
