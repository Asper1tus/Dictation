namespace Dictation.ViewModels
{
    using Dictate.Helpers;
    using Windows.UI.Xaml.Controls;

    public class MenuViewModel : Observable
    {
        private Frame frame;
        private NavigationViewItem selected;

        public MenuViewModel(Frame frame)
        {
            this.frame = frame;
        }

        public NavigationViewItem Selected
        {
            get { return selected; }
            set { Set(ref selected, value); }
        }

    }
}
