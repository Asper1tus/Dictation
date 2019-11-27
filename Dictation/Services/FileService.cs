namespace Dictation.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dictation.Helpers;
    using Windows.ApplicationModel.DataTransfer;
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
                CreateFileAsync();
            }
        }

        public static void ShareFile()
        {
            if (file == null)
            {
                CreateFileAsync();
            }

            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += DataRequested;
            DataTransferManager.ShowShareUI();
        }

        private static void DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;
            request.Data.SetStorageItems(new[] { file }, false);
            request.Data.Properties.Title = file.Name;
            request.Data.Properties.Description = "Shared from Dictation";

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
                };
            }

            return true;
        }

        private static async void CreateFileAsync()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            file = await storageFolder.CreateFileAsync("Document.rtf", CreationCollisionOption.ReplaceExisting);
            var stream = await file.OpenAsync(FileAccessMode.ReadWrite);
            RtfTextHelper.OpenFile(stream);
            stream.Dispose();
        }
    }
}
