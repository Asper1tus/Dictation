namespace Dictation.Services
{
    using System;
    using Dictation.Views.Content;

    public static class ContentDialogService
    {
        public static async void ShowHyperLinkDialogAsync()
        {
            HyperLinkContentDialog hyperLink = new HyperLinkContentDialog();
            await hyperLink.ShowAsync();
        }
    }
}
