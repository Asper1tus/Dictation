namespace Dictation.Helpers
{
    using System;
    using Dictation.Extensions;
    using Dictation.Services;
    using Windows.Storage.Streams;
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
                richEditBox.TextChanged += TextChanged;
                MessageService.StlyeChanged += SelectStyle;
                MessageService.FontChanged += SelectFont;
                MessageService.SizeChanged += SelectSize;
                MessageService.OperationSent += SelectOperation;
                MessageService.FindWord += FindWord;
                MessageService.ReplaceSelectedWord += ReplaceSelectedWord;
                MessageService.ReplaceAllWords += ReplaceAllWords;
                MessageService.ForegroundColorChanged += SelectColor;
                MessageService.BackgroundColorChanged += SelectHighlight;
                MessageService.ImageInsert += InsertImage;
                MessageService.HyperLinkInsert += InsertHyperlink;
                richEditBox.PointerCaptureLost += PointerCaptureLost;
                richEditBox.ProcessKeyboardAccelerators += ProcessKeyboardAccelerators;
            }

            richEditBox.SetValue(RichTextProperty, value);
        }

        public static void AddRtf(string rtf)
        {
            richEditBox.AddRtf(rtf);
        }

        public static void OpenFile(IRandomAccessStream stream)
        {
            richEditBox.Document.LoadFromStream(TextSetOptions.FormatRtf, stream);
        }

        public static void SaveFile(IRandomAccessStream stream)
        {
            richEditBox.Document.SaveToStream(TextGetOptions.FormatRtf, stream);
        }

        private static void TextChanged(object sender, RoutedEventArgs e)
        {
            FileService.IsFileGhanged = true;
        }

        private static void ReplaceSelectedWord(string replacementWord)
        {
            richEditBox.ReplaceSelectedWord(replacementWord);
        }

        private static void ReplaceAllWords(string replacementWord, string searchedWord, bool isMatchCase)
        {
            richEditBox.ReplaceAllWords(replacementWord, searchedWord, isMatchCase);
        }

        private static void FindWord(string word, bool options)
        {
            richEditBox.FindWord(word, options);
        }

        private static void ProcessKeyboardAccelerators(UIElement sender, ProcessKeyboardAcceleratorEventArgs args)
        {
            if (args.Modifiers == Windows.System.VirtualKeyModifiers.Control && args.Key == Windows.System.VirtualKey.S)
            {
                FileService.SaveAsync();
            }

            MessageService.Notify();
        }

        private static void PointerCaptureLost(object sender, PointerRoutedEventArgs e)
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
                case "ToLower":
                    richEditBox.ToLower();
                    break;
                case "ToUpper":
                    richEditBox.ToUpper();
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

        private static void SelectColor(string selectedColor)
        {
            Color color = Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor(selectedColor);
            richEditBox.SetForeground(color);
        }

        private static void SelectHighlight(string selectedColor)
        {
            Color color = Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor(selectedColor);
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

        private static async void InsertImage()
        {
            Windows.Storage.Pickers.FileOpenPicker open = new Windows.Storage.Pickers.FileOpenPicker();
            open.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            open.FileTypeFilter.Add(".png");
            open.FileTypeFilter.Add(".jpg");
            open.FileTypeFilter.Add(".jpeg");
            Windows.Storage.StorageFile file = await open.PickSingleFileAsync();
            if (file != null)
            {
                IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                richEditBox.InsertImage(fileStream);
            }
        }

        private static void InsertHyperlink(string link, string text)
        {
            richEditBox.InsertHyperLink(link, text);
        }
    }
}