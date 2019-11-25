namespace Dictation.Services
{
    using System;
    using System.Threading.Tasks;
    using Dictation.Views.ContentDialogs;
    using Windows.UI.Xaml.Controls;

    public static class ContentDialogService
    {
        public static async void ShowHyperLinkDialogAsync()
        {
            HyperLinkContentDialog hyperLink = new HyperLinkContentDialog();
            await hyperLink.ShowAsync();
        }

        public static async Task<ContentDialogResult> ShowSaveDocumentDialogAsync()
        {
            SaveDocumentContentDialog saveDocument = new SaveDocumentContentDialog();
            return await saveDocument.ShowAsync();
        }
    }
}
