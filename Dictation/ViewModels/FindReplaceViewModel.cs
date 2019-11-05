using System;

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