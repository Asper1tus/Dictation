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
        private string foregroundColor;
        private string backgroundColor;
        private int size;

        private ICommand changeStyleCommand;
        private ICommand changeFontCommand;
        private ICommand changeSizeCommand;
        private ICommand selectForegroundColorCommand;
        private ICommand selectBackgroundColorCommand;
        private ICommand changeForegroundColorCommand;
        private ICommand changeBackgroundColorCommand;
        private ICommand insertImageCommand;
        private ICommand insertHyperlinkCommand;

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

        public ICommand InsertImageCommand => insertImageCommand ?? (insertImageCommand = new RelayCommand(MessageService.SendImage));

        public ICommand InsertHyperlinkCommand => insertHyperlinkCommand ?? (insertHyperlinkCommand = new RelayCommand(ContentDialogService.ShowHyperLinkDialogAsync));

        public List<string> Fonts => FontService.Fonts;

        public List<int> Sizes => FontService.Sizes;

        public bool IsBold
        {
            get => isBold;
            set => Set(ref this.isBold, value);
        }

        public bool IsItalic
        {
            get => isItalic;
            set => Set(ref this.isItalic, value);
        }

        public bool IsUnderline
        {
            get => isUnderline;
            set => Set(ref this.isUnderline, value);
        }

        public bool IsStrikethrough
        {
            get => isStrikethrough;
            set => Set(ref this.isStrikethrough, value);
        }

        public bool IsSuperscript
        {
            get => isSuperscript;
            set => Set(ref this.isSuperscript, value);
        }

        public bool IsSubscript
        {
            get => isSubscript;
            set => Set(ref this.isSubscript, value);
        }

        public bool IsAlignLeft
        {
            get => isAlignLeft;
            set => Set(ref this.isAlignLeft, value);
        }

        public bool IsAlignRight
        {
            get => isAlignRight;
            set => Set(ref this.isAlignRight, value);
        }

        public bool IsAlignCenter
        {
            get => isAlignCenter;
            set => Set(ref this.isAlignCenter, value);
        }

        public bool IsJustify
        {
            get => isJustify;
            set => Set(ref this.isJustify, value);
        }

        public bool HasBullets
        {
            get => hasBullets;
            set => Set(ref this.hasBullets, value);
        }

        public bool HasNumbers
        {
            get => hasNumbers;
            set => Set(ref this.hasNumbers, value);
        }

        public string Font
        {
            get => font;
            set => Set(ref this.font, value);
        }

        public int Size
        {
            get => size;
            set => Set(ref this.size, value);
        }

        public string ForegroundColor
        {
            get => foregroundColor;
            set => Set(ref this.foregroundColor, value);
        }

        public string BackgroundColor
        {
            get => backgroundColor;
            set => Set(ref this.backgroundColor, value);
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
