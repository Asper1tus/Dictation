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

        public FindReplaceViewModel(DocumentModel document)
        {
            IsFocused = true;
            SearchedWord = string.Empty;
            Document = document;
            FindNextCommand = new RelayCommand(FindNext);
            ReplaceAllCommand = new RelayCommand(Replace);
            ReplaceAllCommand = new RelayCommand(ReplaceAll);
        }

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

        public ICommand FindNextCommand { get; }

        public ICommand ReplaceCommand { get; }

        public ICommand ReplaceAllCommand { get; }

        public void FindNext()
        {
            if (Document.Text.Contains(SearchedWord))
            {
                int index = Document.Text.IndexOf(SearchedWord);

                Document.SelectionStart = index;
                Document.SelectionLenght = SearchedWord.Length;
            }
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
