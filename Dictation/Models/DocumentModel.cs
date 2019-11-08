namespace Dictation.Models
{
    using Dictation.Helpers;

    public class DocumentModel : Observable
    {
        private string text;
        private string filePath;
        private string fileName;
        private string selectedText;
        private int selectionStart;
        private int selectionLenght;

        public string Text
        {
            get { return text; }
            set { Set(ref text, value); }
        }

        public string Path
        {
            get { return filePath; }
            set { Set(ref filePath, value); }
        }

        public string Name
        {
            get { return fileName; }
            set { Set(ref fileName, value); }
        }

        public string SelectedText
        {
            get { return selectedText; }
            set { Set(ref selectedText, value); }
        }

        public int SelectionStart
        {
            get { return selectionStart; }
            set { Set(ref selectionStart, value); }
        }

        public int SelectionLenght
        {
            get { return selectionLenght; }
            set { Set(ref selectionLenght, value); }
        }

        public bool IsEmpty
        {
            get
            {
                if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Path))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
