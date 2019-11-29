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

        public static event Action FileManipulationStarted;

        public static event Action FileManipulationEnded;

        public static bool IsFileGhanged { get; set; }

        public static async void OpenAsync(string path = null)
        {
            if (await IsFileSavedAsync())
            {
                if (path == null)
                {
                    var openPicker = new FileOpenPicker
                    {
                        ViewMode = PickerViewMode.Thumbnail,
                        SuggestedStartLocation = PickerLocationId.Desktop,
                        CommitButtonText = "Open",
                    };
                    openPicker.FileTypeFilter.Add(".rtf");
                    file = await openPicker.PickSingleFileAsync();
                }
                else
                {
                    file = await StorageFile.GetFileFromPathAsync(path);
                }

                if (file != null)
                {
                    FileManipulationStarted();
                    var stream = await file.OpenAsync(FileAccessMode.ReadWrite);
                    RtfTextHelper.OpenFile(stream);
                    stream.Dispose();
                    IsFileGhanged = false;

                    var mru = Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList;
                    mru.Add(file, "profile pic");
                    FileManipulationEnded();
                }
            }
        }

        public static async Task<bool> SaveAsync()
        {
            if (file == null)
            {
                await SaveAsAsync();
                return false;
            }

            await SaveFileAsync();
            return true;
        }

        public static async Task SaveAsAsync()
        {
            var savePicker = new FileSavePicker
            {
                SuggestedStartLocation =
                PickerLocationId.DocumentsLibrary,
            };
            savePicker.FileTypeChoices.Add("Rich Text Format", new List<string>() { ".rtf" });
            savePicker.SuggestedFileName = "New Document";

            file = await savePicker.PickSaveFileAsync();
            await SaveFileAsync();
        }

        public static async void New()
        {
            if (await IsFileSavedAsync())
            {
                CreateFileAsync();
            }
        }

        public static async void ShareFileAsync()
        {
            if (file == null)
            {
                CreateFileAsync();
            }

            await IsFileSavedAsync();
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += DataRequested;
            DataTransferManager.ShowShareUI();
        }

        public static async Task<bool> IsFileSavedAsync()
        {
            if (IsFileGhanged)
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

        private static async Task SaveFileAsync()
        {
            if (file != null)
            {
                FileManipulationStarted();
                var stream = await file.OpenAsync(FileAccessMode.ReadWrite);
                RtfTextHelper.SaveFile(stream);
                stream.Dispose();

                IsFileGhanged = false;
                FileManipulationEnded();
            }
        }

        private static void DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;
            request.Data.SetStorageItems(new[] { file }, false);
            request.Data.Properties.Title = file.Name;
            request.Data.Properties.Description = "Shared from Dictation";
        }

        private static async void CreateFileAsync()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            file = await storageFolder.CreateFileAsync("Document.rtf", CreationCollisionOption.ReplaceExisting);
            var stream = await file.OpenAsync(FileAccessMode.ReadWrite);
            RtfTextHelper.OpenFile(stream);
            stream.Dispose();
            IsFileGhanged = false;
        }
    }
}
