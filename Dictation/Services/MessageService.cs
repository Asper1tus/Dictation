namespace Dictation.Services
{
    using System;
    using Windows.UI.Text;

    public static class MessageService
    {
        public static event Action NotifyEvent;

        public static event Action<string> OpenSaveFIle;

        public static event Action<string> StlyeChanged;

        public static event Action<string> FontChanged;

        public static event Action<int> SizeChanged;

        public static event Action<string> OperationSent;

        public static event Action<string, bool> FindWord;

        public static event Action<string> ReplaceSelectedWord;

        public static event Action<string, string, bool> ReplaceAllWords;

        public static void SendStyle(string style)
        {
            StlyeChanged(style);
        }

        public static void SendFont(string font)
        {
            FontChanged(font);
        }

        public static void SendSize(int size)
        {
            SizeChanged(size);
        }

        public static void SendOperation(string operation)
        {
            OperationSent(operation);
        }

        public static void SendOpenSaveFile(string openOrRead)
        {
            OpenSaveFIle(openOrRead);
        }

        public static void SendWord(string searhedWord, bool isMatchCase)
        {
            FindWord(searhedWord, isMatchCase);
        }

        public static void SendSelectedWord(string replacementWord)
        {
            ReplaceSelectedWord(replacementWord);
        }

        public static void SendAllWords(string replacementWord, string searhedWord, bool isMatchCase)
        {
            ReplaceAllWords(replacementWord, searhedWord, isMatchCase);
        }

        public static void Notify()
        {
            // Throws an NullReferenceException at startup
            try
            {
                NotifyEvent();
            }
            catch
            {
            }
        }
    }
}
