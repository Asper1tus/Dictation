namespace Dictation.ViewModels.Content
{
    internal class FindReplaceViewModel
    {
        public string SearchedWord { get; set; }

        public string ReplaceWord { get; set; }

        public bool IsMatchCase { get; set; }

        public string EditorText { get; set; }

        public void FindNext()
        {

        }

        public void Replace()
        {
        }

        public void ReplaceAll()
        {
            EditorText = EditorText.Replace(SearchedWord, ReplaceWord);
        }
    }
}
