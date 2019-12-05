namespace Dictation.ViewModels
{
    using System.Windows.Input;
    using Dictation.Commands;
    using Dictation.Helpers;
    using Dictation.Services;

    public class FindReplaceViewModel : Observable
    {
        private string searchedWord;
        private string replacementWord;
        private ICommand findNextCommand;
        private ICommand replaceCommand;
        private ICommand replaceAllCommand;

        public FindReplaceViewModel()
        {
            SearchedWord = string.Empty;
        }

        public ICommand FindNextCommand => findNextCommand ?? (findNextCommand = new RelayCommand(FindNext));

        public ICommand ReplaceCommand => replaceCommand ?? (replaceCommand = new RelayCommand(Replace));

        public ICommand ReplaceAllCommand => replaceAllCommand ?? (replaceAllCommand = new RelayCommand(ReplaceAll));

        public string SearchedWord
        {
            get => searchedWord;
            set => Set(ref searchedWord, value);
        }

        public string ReplacementWord
        {
            get => replacementWord;
            set => Set(ref replacementWord, value);
        }

        public bool IsMatchCase { get; set; }

        public void FindNext()
        {
            MessageService.SendWord(SearchedWord, IsMatchCase);
        }

        public void Replace()
        {
            MessageService.SendSelectedWord(ReplacementWord);
        }

        public void ReplaceAll()
        {
            MessageService.SendAllWords(ReplacementWord, SearchedWord, IsMatchCase);
        }
    }
}
