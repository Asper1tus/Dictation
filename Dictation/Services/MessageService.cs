namespace Dictation.Services
{
    using System;

    public static class MessageService
    {
        public static event Action NotifyEvent;

        public static event Action ImageInserting;

        public static event Action<int> SizeChanged;

        public static event Action<string> FIleManipulating;

        public static event Action<string> StlyeChanged;

        public static event Action<string> FontChanged;

        public static event Action<string> TextActionSending;

        public static event Action<string> ForegroundColorChanged;

        public static event Action<string> BackgroundColorChanged;

        public static event Action<string> ReplaceingSelectedWord;

        public static event Action<float> ZoomFactorChanged;

        public static event Action<string, bool> WordFinding;

        public static event Action<string, string> HyperLinkInserting;

        public static event Action<string, string, bool> AllWordsReplacing;

        public static void Notify()
        {
            NotifyEvent();
        }

        public static void SendImage()
        {
            ImageInserting();
        }

        public static void SendSize(int size)
        {
            SizeChanged(size);
        }

        public static void SendManipulationFile(string manipulationType)
        {
            FIleManipulating(manipulationType);
        }

        public static void SendStyle(string style)
        {
            StlyeChanged(style);
        }

        public static void SendFont(string font)
        {
            FontChanged(font);
        }

        public static void SendOperation(string operation)
        {
            TextActionSending(operation);
        }

        public static void SendForegroundColor(string color)
        {
            ForegroundColorChanged(color);
        }

        public static void SendBackgroundColor(string color)
        {
            BackgroundColorChanged(color);
        }

        public static void SendSelectedWord(string replacementWord)
        {
            ReplaceingSelectedWord(replacementWord);
        }

        public static void SendZoomFactor(float zoomFactor)
        {
            ZoomFactorChanged(zoomFactor);
        }

        public static void SendWord(string searhedWord, bool isMatchCase)
        {
            WordFinding(searhedWord, isMatchCase);
        }

        public static void SendHyperlink(string link, string text)
        {
            HyperLinkInserting(link, text);
        }

        public static void SendAllWords(string replacementWord, string searhedWord, bool isMatchCase)
        {
            AllWordsReplacing(replacementWord, searhedWord, isMatchCase);
        }
    }
}
