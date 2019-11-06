namespace Dictation.Models
{
    public class DocumentInstance
    {
        //private static DocumentInstance document;

        private readonly DocumentModel documentModel;

        //private DocumentInstance(string text, string path, string name)
        //{
        //    documentModel = new DocumentModel();
        //    Text = text;
        //    Path = path;
        //    Name = name;
        //}

        public string Text
        {
            get { return documentModel.Text; }
            set { documentModel.Text = value; }
        }

        public string Path
        {
            get { return documentModel.Path; }
            set { documentModel.Path = value; }
        }

        public string Name
        {
            get { return documentModel.Name; }
            set { documentModel.Name = value; }
        }

        //public static DocumentInstance GetDocument(string text, string path, string name)
        //{
        //    if (document == null)
        //    {
        //        document = new DocumentInstance(text, path, name);
        //    }

        //    return document;
        //}
    }
}
