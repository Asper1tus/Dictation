namespace Dictation.ViewModels
{
    using Dictation.Models;

    public class MainViewModel
    {
        public MainViewModel()
        {
            Document = new DocumentModel();
            File = new FileViewModel(Document);
            MainPage = new MainPageViewModel();
        }

        public DocumentModel Document { get; set; }

        public FileViewModel File { get; set; }

        public RecognizerViewModel Recognizer { get; set; }

        public MainPageViewModel MainPage { get; set; }
    }
}
