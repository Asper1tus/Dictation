namespace Dictation.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dictation.Helpers;
    using Windows.Storage;
    using Windows.Storage.Pickers;
    using Windows.UI.Xaml.Controls;

    public static class FileService
    {
        private static StorageFile file;
        private static string savedText;

        public static async void OpenAsync()
        {
            if (await IsFileSavedAsync())
            {
                var openPicker = new FileOpenPicker
                {
                    ViewMode = PickerViewMode.Thumbnail,
                    SuggestedStartLocation = PickerLocationId.Desktop,
                    CommitButtonText = "Open",
                };
                openPicker.FileTypeFilter.Add(".rtf");

                file = await openPicker.PickSingleFileAsync();
                if (file != null)
                {
                    var stream = await file.OpenAsync(FileAccessMode.ReadWrite);
                    RtfTextHelper.OpenFile(stream);
                    stream.Dispose();
                    savedText = RtfTextHelper.RichText;

                    var mru = Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList;
                    mru.Add(file, "profile pic");
                }
            }
        }

        public static async void OpenAsync(string path)
        {
            if (await IsFileSavedAsync())
            {
                file = await StorageFile.GetFileFromPathAsync(path);

                if (file != null)
                {
                    var stream = await file.OpenAsync(FileAccessMode.ReadWrite);
                    RtfTextHelper.OpenFile(stream);
                    stream.Dispose();
                    savedText = RtfTextHelper.RichText;
                }
            }
        }

        public static async Task<bool> SaveAsync()
        {
            if (file == null)
            {
                SaveAsAsync();
                return false;
            }

            var stream = await file.OpenAsync(FileAccessMode.ReadWrite);
            RtfTextHelper.SaveFile(stream);
            stream.Dispose();
            savedText = RtfTextHelper.RichText;
            return true;
        }

        public static async void SaveAsAsync()
        {
            var savePicker = new FileSavePicker
            {
                SuggestedStartLocation =
                PickerLocationId.DocumentsLibrary,
            };
            savePicker.FileTypeChoices.Add("Rich Text Format", new List<string>() { ".rtf" });
            savePicker.SuggestedFileName = "New Document";

            file = await savePicker.PickSaveFileAsync();

            if (file != null)
            {
                var stream = await file.OpenAsync(FileAccessMode.ReadWrite);
                RtfTextHelper.SaveFile(stream);
                stream.Dispose();
                savedText = RtfTextHelper.RichText;
            }
        }

        public static async void New()
        {
            if (await IsFileSavedAsync())
            {
                Clear();
            }
        }

        private static async Task<bool> IsFileSavedAsync()
        {
            if (RtfTextHelper.RichText != savedText)
            {
                var result = await ContentDialogService.ShowSaveDocumentDialogAsync();

                if (result == ContentDialogResult.None)
                {
                    return false;
                }
                else if (result == ContentDialogResult.Primary)
                {
                    if (!await SaveAsync())
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static void Clear()
        {
            file = null;
            RtfTextHelper.RichText = string.Empty;
            savedText = RtfTextHelper.RichText;
        }
    }
}
