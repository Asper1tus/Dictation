namespace Dictation.Helpers
{
    using Dictation.Extensions;
    using Dictation.Services;
    using Windows.UI;
    using Windows.UI.Text;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;

    public class RtfTextHelper
    {
        public static readonly DependencyProperty RichTextProperty =
            DependencyProperty.RegisterAttached("RichText", typeof(string), typeof(RtfTextHelper), new PropertyMetadata(string.Empty, Callback));

        private static RichEditBox richEditBox;

        public static string RichText
        {
            get { return richEditBox.GetRtf(); }
            set { richEditBox.SetRtf(value); }
        }

        public static string Text
        {
            get
            {
                richEditBox.Document.GetText(TextGetOptions.None, out string text);
                return text;
            }

            set
            {
                richEditBox.Document.SetText(TextSetOptions.None, value);
            }
        }

        public static bool IsBold => richEditBox.IsBold();

        public static bool IsItalic => richEditBox.IsItalic();

        public static bool IsUnderline => richEditBox.IsUnderline();

        public static bool IsAlignLeft => richEditBox.IsAlignLeft();

        public static bool IsAlignRight => richEditBox.IsAlignRight();

        public static bool IsAlignCenter => richEditBox.IsAlignCenter();

        public static bool IsJustify => richEditBox.IsJustify();

        public static bool IsStrikethrough => richEditBox.IsStrikethrough();

        public static bool IsSuperscript => richEditBox.IsSuperscript();

        public static bool IsSubscript => richEditBox.IsSubscript();

        public static bool HasBullets => richEditBox.HasBullets();

        public static bool HasNumbers => richEditBox.HasNumbers();

        public static string Font => richEditBox.GetFont();

        public static int Size => (int)richEditBox.GetSize();

        public static Color Color => richEditBox.GetForeground();

        public static Color Highlight => richEditBox.GetBackground();

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
                MessageService.OperationSent += SelectOperation;
                MessageService.FindWord += FindWord;
                MessageService.ReplaceSelectedWord += ReplaceSelectedWord;
                MessageService.ReplaceAllWords += ReplaceAllWords; 
                richEditBox.PointerCaptureLost += PointerCaptureLost;

                // richEditBox.ProcessKeyboardAccelerators += ProcessKeyboardAccelerators;
            }

            richEditBox.SetValue(RichTextProperty, value);
        }

        private static void ReplaceSelectedWord(string replacementWord)
        {
            richEditBox.ReplaceSelectedWord(replacementWord);
        }

        private static void ReplaceAllWords(string replacementWord, string searchedWord, bool isMatchCase)
        {
            richEditBox.ReplaceAllWords(replacementWord, searchedWord, isMatchCase);
        }

        public static void AddRtf(string rtf)
        {
            richEditBox.AddRtf(rtf);
        }

        private static void FindWord(string word, bool options)
        {
            richEditBox.FindWord(word, options);
        }

        private static void PointerCaptureLost(object sender, PointerRoutedEventArgs e)
        {
            MessageService.Notify();
        }

        private static void ProcessKeyboardAccelerators(UIElement sender, ProcessKeyboardAcceleratorEventArgs args)
        {
            MessageService.Notify();
        }

        private static void Callback(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var reb = (RichEditBox)sender;
            reb.SetRtf((string)args.NewValue);
        }

        private static void SelectStyle(string style)
        {
            switch (style)
            {
                case "Bold":
                    richEditBox.Bold();
                    break;
                case "Italic":
                    richEditBox.Italic();
                    break;
                case "Underline":
                    richEditBox.Underline();
                    break;
                case "AlignLeft":
                    richEditBox.AlignLeft();
                    break;
                case "AlignRight":
                    richEditBox.AlignRight();
                    break;
                case "AlignCenter":
                    richEditBox.AlignCenter();
                    break;
                case "Justify":
                    richEditBox.Justify();
                    break;
                case "Bullets":
                    richEditBox.Bullets();
                    break;
                case "Numbers":
                    richEditBox.Numbers();
                    break;
                case "Strikethrough":
                    richEditBox.Strikethrough();
                    break;
                case "Subscript":
                    richEditBox.Subscript();
                    break;
                case "Superscript":
                    richEditBox.Superscript();
                    break;
            }
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

        private static void SelectOperation(string operation)
        {
            switch (operation)
            {
                case "Undo":
                    richEditBox.Undo();
                    break;
                case "Redo":
                    richEditBox.Redo();
                    break;
                case "Copy":
                    richEditBox.Copy();
                    break;
                case "Cut":
                    richEditBox.Cut();
                    break;
                case "Paste":
                    richEditBox.PasteIn();
                    break;
            }
        }
    }
}
