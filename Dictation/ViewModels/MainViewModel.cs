namespace Dictation.ViewModels
{
    using Dictation.Models;

    public class MainViewModel
    {
        public MainViewModel()
        {
            File = new FileViewModel();
            MainPage = new MainPageViewModel();
            Find = new FindReplaceViewModel();
            File.NewFile();
        }


        public FileViewModel File { get; set; }

        public MainPageViewModel MainPage { get; set; }

        public FindReplaceViewModel Find { get; set; }
    }
}
