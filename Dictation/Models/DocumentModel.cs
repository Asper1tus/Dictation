namespace Dictation.Models
{
    using Dictate.Helpers;

    public class DocumentModel : Observable
    {
        private string text;
        private string filePath;
        private string fileName;

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
