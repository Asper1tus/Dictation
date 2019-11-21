namespace Dictation.Helpers
{
    using Windows.Globalization;
    using Windows.Media.SpeechRecognition;

    public static class DefaultSettings
    {
        public static int Size { get; } = 14;

        public static string Font { get; } = "Segoe UI";

        public static Language Language { get; } = SpeechRecognizer.SystemSpeechLanguage;
    }
}
