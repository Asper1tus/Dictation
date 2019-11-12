namespace Dictation.Services
{
    public static class MessageService
    {
        public delegate void UpdateDelegate();

        public delegate void MethodContainer<T>(T fontFormat);

        public static event UpdateDelegate NotifyEvent;

        public static event MethodContainer<string> StlyeChanged;

        public static event MethodContainer<string> FontChanged;

        public static event MethodContainer<int> SizeChanged;

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
