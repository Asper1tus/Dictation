namespace Dictation.ViewModels
{
    using Dictation.Models;

    public class MainViewModel
    {
        public MainViewModel()
        {
            Document = new DocumentModel();
            MainPage = new MainPageViewModel();
            File = new FileViewModel(Document);
        }

        public void RecognizerInit()
        {
            Recognizer = new RecognizerViewModel(Document);
        }

        public DocumentModel Document { get; set; }

        public FileViewModel File { get; set; }

        public RecognizerViewModel Recognizer { get; set; }

        public MainPageViewModel MainPage { get; set; }
    }
}
