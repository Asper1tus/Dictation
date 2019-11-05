namespace Dictation.Views.Content
{
    using Dictation.ViewModels.Content;
    using Windows.UI.Xaml.Controls;

    public sealed partial class FindReplace : Page
    {
        FindReplaceViewModel FindReplaceLogic;
        public FindReplace()
        {
            FindReplaceLogic = new FindReplaceViewModel();
            this.InitializeComponent();
        }
    }
}
