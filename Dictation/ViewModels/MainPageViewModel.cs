namespace Dictation.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Dictate.Helpers;
    using Dictation.Commands;
    using Dictation.Views.Content;
    using Windows.UI.Xaml.Controls;

    public class MainPageViewModel : Observable
    {
        private Page currentPage;
        private bool isPanelVisible;
        private string title;

        private readonly List<(string tag, Page page, string title)> pages = new List<(string tag, Page page, string title)>
{
    ("findreplace",  new FindReplace(), "Find and Replace"),
    ("share", new Share(), "Share"),
    ("tools", new Tools(), "Formating Tools"),
    ("vocabulary", new Vocabulary(), "Vocabulary Training"),
};

        public MainPageViewModel()
        {
            IsPanelVisible = false;
            DispalyContent = new ParametersCommand (ChoosePage);
        }

        public Page CurrentPage
        {
            get { return currentPage; }
            set { Set(ref this.currentPage, value); }
        }

        public bool IsPanelVisible
        {
            get { return isPanelVisible; }
            set { Set(ref this.isPanelVisible, value); }
        }

        public string Title
        {
            get { return title; }
            set { Set(ref this.title, value); }
        }

        public ParametersCommand DispalyContent { get; }

        public void Close()
        {
            IsPanelVisible = false;
            CurrentPage = null;
        }

        private void ChoosePage(object tag)
        {
            var item = pages.FirstOrDefault(p => p.tag.Equals((string)tag));
            var page = item.page;
            if (CurrentPage == page)
            {
                IsPanelVisible = false;
                CurrentPage = null;
            }
            else
            {
                CurrentPage = page;
                Title = item.title;
                IsPanelVisible = true;
            }
        }
    }
}
