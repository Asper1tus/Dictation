namespace Dictation.ViewModels
{
    using Dictation.Models;

    public class MainViewModel
    {
        public MainViewModel()
        {
            Document = new DocumentModel();
            File = new FileViewModel(Document);
            MainPage = new MainPageViewModel(Document);
        }

        public DocumentModel Document { get; set; }

        public FileViewModel File { get; set; }

        public MainPageViewModel MainPage { get; set; }
    }
}
