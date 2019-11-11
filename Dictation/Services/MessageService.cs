namespace Dictation.Services
{
    public static class MessageService
    {
        public delegate void UpdateDelegate();

        public delegate void MethodContainer(string style);

        public static event UpdateDelegate NotifyEvent;

        public static event MethodContainer MessageRecived;

        public static void Send(string style)
        {
            MessageRecived(style);
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
