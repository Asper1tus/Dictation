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
            if (RtfTextHelper.RichText != savedText)
            {
                var result = await ContentDialogService.ShowSaveDocumentDialogAsync();

                if (result == ContentDialogResult.None)
                {
                    return;
                }
                else if (result == ContentDialogResult.Primary)
                {
                    if (!await SaveAsync())
                    {
                        return;
                    }
                }
            }

            Clear();
        }

        private static void Clear()
        {
            file = null;
            RtfTextHelper.RichText = string.Empty;
            savedText = RtfTextHelper.RichText;
        }
    }
}
