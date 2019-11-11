namespace Dictation.ViewModels.Content
{
    using System.Windows.Input;
    using Dictation.Commands;
    using Dictation.Helpers;
    using Dictation.Services;

    public class ToolsViewModel : Observable
    {
        private bool isBold;
        private bool isItalic;
        private bool isUnderline;
        private bool isAlignLeft;
        private bool isAlignRight;
        private bool isAlignCenter;
        private bool isJustify;
        private bool hasBullets;
        private bool hasNumbers;

        private ICommand styleCommand;

        public ToolsViewModel()
        {
            MessageService.NotifyEvent += Update;
        }

        public ICommand StyleCommand => styleCommand ?? (styleCommand = new RelayCommand<string>(MessageService.Send));

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

        public void Update()
        {
            IsBold = RtfTextHelper.IsBold;
            IsItalic = RtfTextHelper.IsItalic;
            IsUnderline = RtfTextHelper.IsUnderline;
            IsAlignLeft = RtfTextHelper.IsAlignLeft;
            IsAlignRight = RtfTextHelper.IsAlignRight;
            IsAlignCenter = RtfTextHelper.IsAlignCenter;
            IsJustify = RtfTextHelper.IsJustify;
            HasBullets = RtfTextHelper.HasBullets;
            HasBullets = RtfTextHelper.HasBullets;
        }
    }
}
