namespace Dictation.ViewModels.Content
{
    using System.Windows.Input;
    using Dictation.Commands;
    using Dictation.Helpers;
    using Dictation.Services;

    public class HyperLinkViewModel : Observable
    {
        private string text;
        private string link;
        private bool isEnabledOkButton;
        private ICommand insertHypelinkCommand;

        public ICommand InsertHypelinkCommand => insertHypelinkCommand ?? (insertHypelinkCommand = new RelayCommand(InsertHyperlink));

        public string Text
        {
            get { return text; }
            set { Set(ref text, value); }
        }

        public string Link
        {
            get { return link; }
            set { Set(ref link, value); }
        }

        public bool IsEnabledOkButton
        {
            get { return isEnabledOkButton; }
            set { Set(ref isEnabledOkButton, value); }
        }

        public void OkButtonSwitch()
        {
            if (Text != null && Link != null)
            {
                IsEnabledOkButton = true;
            }
            else
            {
                isEnabledOkButton = false;
            }
        }

        private void InsertHyperlink()
        {
            MessageService.SendHyperlink(Link, Text);
        }
    }
}
