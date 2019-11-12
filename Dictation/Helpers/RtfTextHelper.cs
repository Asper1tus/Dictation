namespace Dictation.Helpers
{
    using Dictation.Extensions;
    using Dictation.Services;
    using Windows.UI;
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

        public static bool IsStrikethrough { get; set; }

        public static bool IsSuperscript { get; set; }

        public static bool IsSubscript { get; set; }

        public static bool HasBullets { get; set; }

        public static bool HasNumbers { get; set; }

        public static string Font { get; set; }

        public static int Size { get; set; }

        public static Color Color { get; set; }

        public static Color Highlight { get; set; }

        public static string GetRichText(RichEditBox richEditBox)
        {
            return (string)richEditBox.GetValue(RichTextProperty);
        }

        public static void SetRichText(RichEditBox richEditBox, string value)
        {
            if (RtfTextHelper.richEditBox == null)
            {
                RtfTextHelper.richEditBox = richEditBox;
                MessageService.StlyeChanged += SelectStyle;
                MessageService.FontChanged += SelectFont;
                MessageService.SizeChanged += SelectSize;
                richEditBox.PointerCaptureLost += Notify;
                richEditBox.KeyUp += Notify;
            }

            richEditBox.SetValue(RichTextProperty, value);
        }

        private static void Notify(object sender, RoutedEventArgs e)
        {
            IsBold = richEditBox.IsBold();
            IsItalic = richEditBox.IsItalic();
            IsUnderline = richEditBox.IsUnderline();
            IsStrikethrough = richEditBox.IsStrikethrough();
            IsSubscript = richEditBox.IsSubscript();
            IsSuperscript = richEditBox.IsSuperscript();
            IsAlignLeft = richEditBox.IsAlignLeft();
            IsAlignRight = richEditBox.IsAlignRight();
            IsAlignCenter = richEditBox.IsAlignCenter();
            IsJustify = richEditBox.IsJustify();
            HasBullets = richEditBox.HasBullets();
            HasNumbers = richEditBox.HasNumbers();
            Font = richEditBox.GetFont();
            Size = (int)richEditBox.GetSize();
            Color = richEditBox.GetForeground();
            Highlight = richEditBox.GetBackground();
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
                "Strikethrough" => richEditBox.Strikethrough(),
                "Subscript" => richEditBox.Subscript(),
                "Superscript" => richEditBox.Superscript(),
            };
        }

        private static void SelectFont(string font)
        {
            richEditBox.SetFont(font);
        }

        private static void SelectSize(int size)
        {
            richEditBox.SetSize(size);
        }

        private static void SelectColor(Color color)
        {
            richEditBox.SetForeground(color);
        }

        private static void SelectHighlight(Color color)
        {
            richEditBox.SetBackground(color);
        }
    }
}
