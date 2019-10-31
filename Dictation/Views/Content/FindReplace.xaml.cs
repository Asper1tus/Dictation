namespace Dictation.Views.Content
{
    using Dictation.ViewModels.Content;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class FindReplace : Page
    {
        private FindReplaceViewModel FindReplaceLogic { get; } = new FindReplaceViewModel();

        public FindReplace()
        {
            this.InitializeComponent();
        }
    }
}
