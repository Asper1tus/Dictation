namespace Dictation.Helpers
{
    using Dictation.Services;
    using LemonLib.Extensions;
    using Windows.UI.Text;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public class RtfTextHelper
    {
        public static readonly DependencyProperty RichTextProperty =
            DependencyProperty.RegisterAttached("RichText", typeof(string), typeof(RtfTextHelper), new PropertyMetadata(string.Empty, Callback));

        private static RichEditBox richEditBox;

        public static bool IsBold { get; set; }

        public static bool IsItalic { get; set; }

        public static bool IsUnderline { get; set; }

        public static bool IsAlignLeft { get; set; }

        public static bool IsAlignRight { get; set; }

        public static bool IsAlignCenter { get; set; }

        public static bool IsJustify { get; set; }

        public static bool HasBullets { get; set; }

        public static bool HasNumbers { get; set; }

        public static string GetRichText(RichEditBox richEditBox)
        {
            return (string)richEditBox.GetValue(RichTextProperty);
        }

        public static void SetRichText(RichEditBox richEditBox, string value)
        {
            if (RtfTextHelper.richEditBox == null)
            {
                RtfTextHelper.richEditBox = richEditBox;
                MessageService.MessageRecived += SelectStyle;
                richEditBox.SelectionChanged += Notify;
            }

            richEditBox.SetValue(RichTextProperty, value);
        }

        private static void Notify(object sender, RoutedEventArgs e)
        {
            IsBold = richEditBox.IsBold();
            IsItalic = richEditBox.IsItalic();
            IsUnderline = richEditBox.IsUnderline();
            IsAlignLeft = richEditBox.IsAlignLeft();
            IsAlignRight = richEditBox.IsAlignRight();
            IsAlignCenter = richEditBox.IsAlignCenter();
            IsJustify = richEditBox.IsJustify();
            HasBullets = richEditBox.HasBullets();
            HasNumbers = richEditBox.HasNumbers();
            MessageService.Notify();

        }

        private static void Callback(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var reb = (RichEditBox)sender;
            reb.Document.SetText(TextSetOptions.FormatRtf, (string)args.NewValue);
        }

        private static void SelectStyle(string style)
        {
            _ = style switch
            {
                "Bold" => richEditBox.Bold(),
                "Italic" => richEditBox.Italic(),
                "Underline" => richEditBox.Underline(),
                "AlignLeft" => richEditBox.AlignLeft(),
                "AlignRight" => richEditBox.AlignRight(),
                "AlignCenter" => richEditBox.AlignCenter(),
                "Justify" => richEditBox.Justify(),
                "Bullets" => richEditBox.Bullets(),
                "Numbers" => richEditBox.Numbers(),
            };
        }

    }
}
