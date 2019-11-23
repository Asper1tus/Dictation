namespace Dictation.Helpers
{
    using Windows.Globalization;
    using Windows.Media.SpeechRecognition;

    public static class DefaultSettings
    {
        public const int Size = 14;

        public const string Font = "Segoe UI";

        public const int Minutes = 10;

        public const bool IsSaveEnabled = false;

        public const string Theme = "Default";

        public static readonly Language Language = SpeechRecognizer.SystemSpeechLanguage;
    }
}
