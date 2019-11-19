namespace Dictation.ViewModels.Content
{
    using System.Collections.Generic;
    using System.Windows.Input;
    using Dictation.Commands;
    using Dictation.Helpers;
    using Dictation.Services;

    public class ToolsViewModel : Observable
    {
        private bool isBold;
        private bool isItalic;
        private bool isUnderline;
        private bool isStrikethrough;
        private bool isSuperscript;
        private bool isSubscript;
        private bool isAlignLeft;
        private bool isAlignRight;
        private bool isAlignCenter;
        private bool isJustify;
        private bool hasBullets;
        private bool hasNumbers;
        private string font;
        private int size;
        private string foregroundColor;
        private string backgroundColor;

        private ICommand changeStyleCommand;
        private ICommand changeFontCommand;
        private ICommand changeSizeCommand;
        private ICommand selectForegroundColorCommand;
        private ICommand selectBackgroundColorCommand;
        private ICommand changeForegroundColorCommand;
        private ICommand changeBackgroundColorCommand;

        public ToolsViewModel()
        {
            Font = App.Font;
            Size = App.FontSize;
            ForegroundColor = "Black";
            BackgroundColor = "White";
            MessageService.NotifyEvent += Update;
        }

        public ICommand ChangeStyleCommand => changeStyleCommand ?? (changeStyleCommand = new RelayCommand<string>(MessageService.SendStyle));

        public ICommand ChangeFontCommand => changeFontCommand ?? (changeFontCommand = new RelayCommand<string>(MessageService.SendFont));

        public ICommand ChangeSizeCommand => changeSizeCommand ?? (changeSizeCommand = new RelayCommand<int>(MessageService.SendSize));

        public ICommand SelectForegroundColorCommand => selectForegroundColorCommand ?? (selectForegroundColorCommand = new RelayCommand<string>(SelectForegroundColor));

        public ICommand SelectBackgroundColorCommand => selectBackgroundColorCommand ?? (selectBackgroundColorCommand = new RelayCommand<string>(SelectBackgroundColor));

        public ICommand ChangeForegroundColorCommand => changeForegroundColorCommand ?? (changeForegroundColorCommand = new RelayCommand<string>(MessageService.SendForegroundColor));

        public ICommand ChangeBackgroundColorCommand => changeBackgroundColorCommand ?? (changeBackgroundColorCommand = new RelayCommand<string>(MessageService.SendBackgroundColor));

        public List<string> Fonts
        {
            get
            {
                return FontService.Fonts;
            }
        }

        public List<int> Sizes
        {
            get
            {
                return FontService.Sizes;
            }
        }

        public bool IsBold
        {
            get { return isBold; }
            set { Set(ref this.isBold, value); }
        }

        public bool IsItalic
        {
            get { return isItalic; }
            set { Set(ref this.isItalic, value); }
        }

        public bool IsUnderline
        {
            get { return isUnderline; }
            set { Set(ref this.isUnderline, value); }
        }

        public bool IsStrikethrough
        {
            get { return isStrikethrough; }
            set { Set(ref this.isStrikethrough, value); }
        }

        public bool IsSuperscript
        {
            get { return isSuperscript; }
            set { Set(ref this.isSuperscript, value); }
        }

        public bool IsSubscript
        {
            get { return isSubscript; }
            set { Set(ref this.isSubscript, value); }
        }

        public bool IsAlignLeft
        {
            get { return isAlignLeft; }
            set { Set(ref this.isAlignLeft, value); }
        }

        public bool IsAlignRight
        {
            get { return isAlignRight; }
            set { Set(ref this.isAlignRight, value); }
        }

        public bool IsAlignCenter
        {
            get { return isAlignCenter; }
            set { Set(ref this.isAlignCenter, value); }
        }

        public bool IsJustify
        {
            get { return isJustify; }
            set { Set(ref this.isJustify, value); }
        }

        public bool HasBullets
        {
            get { return hasBullets; }
            set { Set(ref this.hasBullets, value); }
        }

        public bool HasNumbers
        {
            get { return hasNumbers; }
            set { Set(ref this.hasNumbers, value); }
        }

        public string Font
        {
            get { return font; }
            set { Set(ref this.font, value); }
        }

        public int Size
        {
            get { return size; }
            set { Set(ref this.size, value); }
        }

        public string ForegroundColor
        {
            get { return foregroundColor; }
            set { Set(ref this.foregroundColor, value); }
        }

        public string BackgroundColor
        {
            get { return backgroundColor; }
            set { Set(ref this.backgroundColor, value); }
        }

        public void Update()
        {
            IsBold = RtfTextHelper.IsBold;
            IsItalic = RtfTextHelper.IsItalic;
            IsUnderline = RtfTextHelper.IsUnderline;
            IsStrikethrough = RtfTextHelper.IsStrikethrough;
            IsSuperscript = RtfTextHelper.IsSuperscript;
            IsSubscript = RtfTextHelper.IsSubscript;
            IsAlignLeft = RtfTextHelper.IsAlignLeft;
            IsAlignRight = RtfTextHelper.IsAlignRight;
            IsAlignCenter = RtfTextHelper.IsAlignCenter;
            IsJustify = RtfTextHelper.IsJustify;
            HasBullets = RtfTextHelper.HasBullets;
            HasBullets = RtfTextHelper.HasBullets;
            Font = RtfTextHelper.Font;
            Size = RtfTextHelper.Size;
        }

        private void SelectForegroundColor(string color)
        {
            ForegroundColor = color;
        }

        private void SelectBackgroundColor(string color)
        {
            BackgroundColor = color;
        }
    }
}
