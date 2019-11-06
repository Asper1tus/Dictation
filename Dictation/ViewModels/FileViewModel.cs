namespace Dictation.ViewModels
{
    using System;
    using System.IO;
    using System.Windows.Input;
    using Dictation.Commands;
    using Dictation.Models;

    public class FileViewModel
    {
        public FileViewModel(DocumentModel document)
        {
            NewCommand = new RelayCommand(NewFile);
            Document = document;
        }

        public DocumentModel Document { get; set; }

        public ICommand NewCommand { get; }

        public ICommand SaveCommand { get; }

        public ICommand SaveAsCommand { get; }

        public ICommand OpenCommand { get; }

        private void NewFile()
        {
            Document.Text = string.Empty;
            Document.FileName = string.Empty;
            Document.FilePath = string.Empty;
        }

        private void SaveFile()
        {
            File.WriteAllText(Document.FilePath, Document.Text);
        }

        private void SaveAsFile()
        {
            throw new NotImplementedException();
        }

        private void OpenFile()
        {
            throw new NotImplementedException();
        }
    }
}
