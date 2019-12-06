namespace Dictation.Extensions
{
    using System;
    using System.Globalization;
    using Windows.Storage.Streams;
    using Windows.UI;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media.Imaging;

    public static class RichEditBoxExtension
    {
        public static int GetLenght(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            richEditBox.Document.GetText(Windows.UI.Text.TextGetOptions.None, out string value);
            return value.Length;
        }

        public static void SetRtf(this RichEditBox richEditBox, string rtf)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            if (rtf != null)
            {
                richEditBox.Document.SetText(Windows.UI.Text.TextSetOptions.FormatRtf, rtf);
            }
        }

        public static string GetRtf(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            richEditBox.Document.GetText(Windows.UI.Text.TextGetOptions.FormatRtf, out string rtf);
            return rtf;
        }

        public static void AddRtf(this RichEditBox richEditBox, string rtf)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            int lenght = richEditBox.GetLenght();
            richEditBox.Document.Selection.StartPosition = lenght;
            if (rtf != null && !string.IsNullOrEmpty(rtf))
            {
                richEditBox.Document.Selection.TypeText(rtf);
            }
        }

        public static void ClearUndo(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            uint limit = richEditBox.Document.UndoLimit;
            richEditBox.Document.UndoLimit = 0;
            richEditBox.Document.UndoLimit = limit;
        }

        public static bool CanRedo(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            return richEditBox.Document.CanRedo();
        }

        public static void Redo(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            if (richEditBox.Document.CanRedo())
            {
                richEditBox.Document.Redo();
            }

            Focus(richEditBox);
        }

        public static bool CanUndo(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            return richEditBox.Document.CanUndo();
        }

        public static void Undo(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            if (richEditBox.Document.CanUndo())
            {
                richEditBox.Document.Undo();
            }

            Focus(richEditBox);
        }

        public static bool CanCopy(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            return richEditBox.Document.CanCopy();
        }

        public static void Copy(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            if (richEditBox.Document.CanCopy())
            {
                richEditBox.Document.Selection.Copy();
            }

            Focus(richEditBox);
        }

        public static void Cut(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            if (richEditBox.Document.CanCopy())
            {
                richEditBox.Document.Selection.Cut();
            }

            Focus(richEditBox);
        }

        public static bool CanPaste(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            return richEditBox.Document.Selection.CanPaste(0);
        }

        public static void PasteIn(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            if (richEditBox.Document.CanPaste())
            {
                richEditBox.Document.Selection.Paste(0);
            }

            Focus(richEditBox);
        }

        public static bool IsBold(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            return richEditBox.Document.Selection.CharacterFormat.Bold == Windows.UI.Text.FormatEffect.On;
        }

        public static void Bold(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            richEditBox.Document.Selection.CharacterFormat.Bold = Windows.UI.Text.FormatEffect.Toggle;
            Focus(richEditBox);
        }

        public static bool IsItalic(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            return richEditBox.Document.Selection.CharacterFormat.Italic == Windows.UI.Text.FormatEffect.On;
        }

        public static void Italic(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            richEditBox.Document.Selection.CharacterFormat.Italic = Windows.UI.Text.FormatEffect.Toggle;
            Focus(richEditBox);
        }

        public static bool IsUnderline(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            return richEditBox.Document.Selection.CharacterFormat.Underline != Windows.UI.Text.UnderlineType.None;
        }

        public static void Underline(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            richEditBox.Document.Selection.CharacterFormat.Underline = IsUnderline(richEditBox) ? Windows.UI.Text.UnderlineType.None : Windows.UI.Text.UnderlineType.Single;
            Focus(richEditBox);
        }

        public static bool IsStrikethrough(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            return richEditBox.Document.Selection.CharacterFormat.Strikethrough == Windows.UI.Text.FormatEffect.On;
        }

        public static void Strikethrough(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            richEditBox.Document.Selection.CharacterFormat.Strikethrough = Windows.UI.Text.FormatEffect.Toggle;
            Focus(richEditBox);
        }

        public static bool IsSuperscript(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            return richEditBox.Document.Selection.CharacterFormat.Superscript == Windows.UI.Text.FormatEffect.On;
        }

        public static void Superscript(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            richEditBox.Document.Selection.CharacterFormat.Superscript = Windows.UI.Text.FormatEffect.Toggle;
            Focus(richEditBox);
        }

        public static bool IsSubscript(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            return richEditBox.Document.Selection.CharacterFormat.Subscript == Windows.UI.Text.FormatEffect.On;
        }

        public static void Subscript(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            richEditBox.Document.Selection.CharacterFormat.Subscript = Windows.UI.Text.FormatEffect.Toggle;
            Focus(richEditBox);
        }

        public static bool IsAlignLeft(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            return richEditBox.Document.Selection.ParagraphFormat.Alignment == Windows.UI.Text.ParagraphAlignment.Left;
        }

        public static void AlignLeft(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            richEditBox.Document.Selection.ParagraphFormat.Alignment = IsAlignLeft(richEditBox) ? Windows.UI.Text.ParagraphAlignment.Undefined : Windows.UI.Text.ParagraphAlignment.Left;
            Focus(richEditBox);
        }

        public static bool IsAlignRight(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            return richEditBox.Document.Selection.ParagraphFormat.Alignment == Windows.UI.Text.ParagraphAlignment.Right;
        }

        public static void AlignRight(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            richEditBox.Document.Selection.ParagraphFormat.Alignment = IsAlignRight(richEditBox) ? Windows.UI.Text.ParagraphAlignment.Undefined : Windows.UI.Text.ParagraphAlignment.Right;
            Focus(richEditBox);
        }

        public static bool IsAlignCenter(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            return richEditBox.Document.Selection.ParagraphFormat.Alignment == Windows.UI.Text.ParagraphAlignment.Center;
        }

        public static void AlignCenter(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            richEditBox.Document.Selection.ParagraphFormat.Alignment = IsAlignCenter(richEditBox) ? Windows.UI.Text.ParagraphAlignment.Undefined : Windows.UI.Text.ParagraphAlignment.Center;
            Focus(richEditBox);
        }

        public static bool IsJustify(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            return richEditBox.Document.Selection.ParagraphFormat.Alignment == Windows.UI.Text.ParagraphAlignment.Justify;
        }

        public static void Justify(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            richEditBox.Document.Selection.ParagraphFormat.Alignment = IsJustify(richEditBox) ? Windows.UI.Text.ParagraphAlignment.Undefined : Windows.UI.Text.ParagraphAlignment.Justify;
            Focus(richEditBox);
        }

        public static bool HasBullets(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            return richEditBox.Document.Selection.ParagraphFormat.ListType == Windows.UI.Text.MarkerType.Bullet;
        }

        public static void Bullets(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            richEditBox.Document.Selection.ParagraphFormat.ListType = HasBullets(richEditBox) ? Windows.UI.Text.MarkerType.None : Windows.UI.Text.MarkerType.Bullet;
            richEditBox.Document.Selection.ParagraphFormat.ListStyle = Windows.UI.Text.MarkerStyle.Plain;
            Focus(richEditBox);
        }

        public static bool HasNumbers(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            return richEditBox.Document.Selection.ParagraphFormat.ListType == Windows.UI.Text.MarkerType.Arabic;
        }

        public static void Numbers(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            richEditBox.Document.Selection.ParagraphFormat.ListType = HasNumbers(richEditBox) ? Windows.UI.Text.MarkerType.None : Windows.UI.Text.MarkerType.Arabic;
            richEditBox.Document.Selection.ParagraphFormat.ListStart = 1;
            richEditBox.Document.Selection.ParagraphFormat.ListStyle = Windows.UI.Text.MarkerStyle.Parenthesis;
            Focus(richEditBox);
        }

        public static float GetSize(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            return richEditBox.Document.Selection.CharacterFormat.Size;
        }

        public static void SetSize(this RichEditBox richEditBox, int size)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            richEditBox.Document.Selection.CharacterFormat.Size = size;
            Focus(richEditBox);
        }

        public static Color GetForeground(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            return richEditBox.Document.Selection.CharacterFormat.ForegroundColor;
        }

        public static void SetForeground(this RichEditBox richEditBox, Color color)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            richEditBox.Document.Selection.CharacterFormat.ForegroundColor = color;
            Focus(richEditBox);
        }

        public static Color GetBackground(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            return richEditBox.Document.Selection.CharacterFormat.BackgroundColor;
        }

        public static void SetBackground(this RichEditBox richEditBox, Color color)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            richEditBox.Document.Selection.CharacterFormat.BackgroundColor = color;
            Focus(richEditBox);
        }

        public static string GetFont(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            return richEditBox.Document.Selection.CharacterFormat.Name;
        }

        public static void SetFont(this RichEditBox richEditBox, string font)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            if (font != null)
            {
                richEditBox.Document.Selection.CharacterFormat.Name = font;
                Focus(richEditBox);
            }
        }

        public static void Focus(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            richEditBox.Focus(Windows.UI.Xaml.FocusState.Keyboard);
        }

        public static void FindWord(this RichEditBox richEditBox, string searchedWord, bool isMatchCase)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            Windows.UI.Text.FindOptions options;
            if (isMatchCase)
            {
                options = Windows.UI.Text.FindOptions.Case;
            }
            else
            {
                options = Windows.UI.Text.FindOptions.Word;
            }

            // To highlight the searched word in turn
            if (string.Compare(richEditBox.Document.Selection.Text, searchedWord, true, CultureInfo.CurrentCulture) != 0)
            {
                // Set selection to start of document
                richEditBox.Document.Selection.SetRange(0, 0);
            }

            int lenght = richEditBox.GetLenght();
            richEditBox.Document.Selection.FindText(searchedWord, lenght, options);

            Focus(richEditBox);
        }

        public static void ReplaceSelectedWord(this RichEditBox richEditBox, string replacementWord)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            if (!string.IsNullOrEmpty(richEditBox.Document.Selection.Text))
            {
                richEditBox.Document.Selection.Text = replacementWord;
            }

            Focus(richEditBox);
        }

        public static void ReplaceAllWords(this RichEditBox richEditBox, string replacementWord, string searchedWord, bool isMatchCase)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            // Avoids infinite lool
            if (replacementWord != searchedWord && !string.IsNullOrEmpty(searchedWord))
            {
                richEditBox.FindWord(searchedWord, isMatchCase);
                do
                {
                    richEditBox.ReplaceSelectedWord(replacementWord);
                    richEditBox.FindWord(searchedWord, isMatchCase);
                }
                while (string.Compare(richEditBox.Document.Selection.Text, searchedWord, isMatchCase, CultureInfo.CurrentCulture) == 0);
            }
        }

        public static void ToUpper(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            richEditBox.Document.Selection.ChangeCase(Windows.UI.Text.LetterCase.Upper);
            Focus(richEditBox);
        }

        public static void ToLower(this RichEditBox richEditBox)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            richEditBox.Document.Selection.ChangeCase(Windows.UI.Text.LetterCase.Lower);
            Focus(richEditBox);
        }

        public static async void InsertImage(this RichEditBox richEditBox, IRandomAccessStream stream)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            BitmapImage image = new BitmapImage();
            await image.SetSourceAsync(stream);
            richEditBox.Document.Selection.InsertImage(image.PixelWidth, image.PixelHeight, 0, Windows.UI.Text.VerticalCharacterAlignment.Baseline, "img", stream);
        }

        public static void InsertHyperLink(this RichEditBox richEditBox, string link, string text)
        {
            if (richEditBox is null)
            {
                throw new ArgumentNullException(nameof(richEditBox));
            }

            if (link is null)
            {
                throw new ArgumentNullException(nameof(link));
            }

            richEditBox.Document.Selection.Text = text;
            string hyperlink = link;

            if (!link.StartsWith("http://", StringComparison.CurrentCultureIgnoreCase))
            {
                hyperlink = "http://" + hyperlink;
            }

            hyperlink = '\u0022' + hyperlink + '\u0022';

            richEditBox.Document.Selection.Link = hyperlink;
        }
    }
}