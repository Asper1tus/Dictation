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

        public static async Task<ContentDialogResult> ShowRecognitionSettingsDialog()
        {
            RecognitionSettingsDialog recognitionSettings = new RecognitionSettingsDialog();
            return await recognitionSettings.ShowAsync();
        }

        public static async void ShowRecognitionPrivacyDialog()
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "The speech privacy policy was not accepted",
                Content = "You need to enable network voice recognition in Settings",
                PrimaryButtonText = "Go to Settings",
                CloseButtonText = "Cancel",
            };
            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
            {
                const string uriToLaunch = "ms-settings:privacy-speech";
                var uri = new Uri(uriToLaunch);

                var success = await Windows.System.Launcher.LaunchUriAsync(uri);

                if (!success)
                {
                    _ = await new ContentDialog
                    {
                        Title = "Oops! Something went wrong...",
                        Content = "The settings app could not be opened.",
                        CloseButtonText = "Cancel",
                    }.ShowAsync();
                }
            }
        }
    }
}