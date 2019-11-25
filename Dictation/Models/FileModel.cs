namespace Dictation.Models
{
    public class FileModel
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public string FolderPath
        {
            get
            {
                return Path.Replace(Name, string.Empty);
            }
        }

        public string IconPath { get; set; }
    }
}
