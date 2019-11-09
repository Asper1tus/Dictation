namespace Dictation.ViewModels
{
    using System;
    using System.Windows.Input;
    using Dictation.Commands;
    using Dictation.Helpers;
    using Dictation.Models;
    using Windows.UI.Text;
    using Windows.UI.Xaml.Controls;

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
            if (Document.Text.Contains(SearchedWord))
            {
                int index = Document.Text.IndexOf(SearchedWord);

                Document.SelectionStart = index;
                Document.SelectionLenght = SearchedWord.Length;
            }
        }

        public void Replace()
        { 
        }

        public void ReplaceAll()
        {
            throw new NotImplementedException();
        }
    }
}
