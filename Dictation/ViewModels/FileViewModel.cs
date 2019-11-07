namespace Dictation.ViewModels
{
    using System;
    using System.IO;
    using System.Windows.Input;
    using Dictation.Commands;
    using Dictation.Models;

    public class FileViewModel
    {
        public FileViewModel()
        {
            NewCommand = new RelayCommand(NewFile);
            Document = new DocumentModel();
        }

        public DocumentModel Document { get; set; }

        public ICommand NewCommand { get; }

        public ICommand SaveCommand { get; }

        public ICommand SaveAsCommand { get; }

        public ICommand OpenCommand { get; }

        public void NewFile()
        {
            Document.Text = string.Empty;
            Document.Name = string.Empty;
            Document.Path = string.Empty;
        }

        private void SaveFile()
        {
            File.WriteAllText(Document.Path, Document.Text);
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
