namespace Dictation.ViewModels
{
    using System;
    using System.Windows.Input;
    using Dictation.Commands;
    using Dictation.Helpers;
    using Dictation.Models;

    public class FindReplaceViewModel : Observable
    {
        private string searchedWord;
        private string replaceWord;
        private bool isFocused;
        private ICommand findNextCommand;
        private ICommand replaceCommand;
        private ICommand replaceAllCommand;

        public FindReplaceViewModel(DocumentModel document)
        {
            IsFocused = true;
            SearchedWord = string.Empty;
            Document = document;
        }

        public ICommand FindNextCommand => findNextCommand ?? (findNextCommand = new RelayCommand(FindNext));

        public ICommand ReplaceCommand => replaceCommand ?? (replaceCommand = new RelayCommand(Replace));

        public ICommand ReplaceAllCommand => replaceAllCommand ?? (replaceAllCommand = new RelayCommand(ReplaceAll));

        public DocumentModel Document { get; set; }

        public string SearchedWord
        {
            get { return searchedWord; }
            set { Set(ref searchedWord, value); }
        }

        public string ReplaceWord
        {
            get { return replaceWord; }
            set { Set(ref replaceWord, value); }
        }

        public bool IsFocused
        {
            get { return isFocused; }
            set { Set(ref isFocused, value); }
        }

        public bool IsMatchCase { get; set; }

        public void FindNext()
        {
            throw new NotImplementedException();
        }

        public void Replace()
        {
            throw new NotImplementedException();
        }

        public void ReplaceAll()
        {
            throw new NotImplementedException();
        }
    }
}
