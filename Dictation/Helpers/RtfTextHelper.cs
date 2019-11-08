namespace Dictation.Helpers
{
    using Windows.UI.Text;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public class RtfTextHelper
    {
        public static readonly DependencyProperty RichTextProperty =
            DependencyProperty.RegisterAttached("RichText", typeof(string), typeof(RtfTextHelper), new PropertyMetadata(string.Empty, Callback));

        public static string GetRichText(RichEditBox richEditBox)
        {
            return (string)richEditBox.GetValue(RichTextProperty);
        }

        public static void SetRichText(RichEditBox richEditBox, string value)
        {
            richEditBox.SetValue(RichTextProperty, value);
        }

        private static void Callback(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var reb = (RichEditBox)sender;
            reb.Document.SetText(TextSetOptions.FormatRtf, (string)args.NewValue);
        }
    }
}
