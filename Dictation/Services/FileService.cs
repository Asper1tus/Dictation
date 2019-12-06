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

        private static StorageFile tempFile;

        public static event Action FileManipulationStarted;

        public static event Action FileManipulationEnded;

        public static string FileName
        {
            get
            {
                if (file != null)
                {
                    return file.Name;
                }

                return "New Document";
            }
        }

        public static bool IsFileGhanged { get; set; }

        public static async void OpenAsync(string path = null)
        {
            if (await IsFileSavedAsync().ConfigureAwait(true))
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

                if (file == null)
                {
                    file = tempFile;
                }
                else
                {
                    FileManipulationStarted();

                    var stream = await file.OpenAsync(FileAccessMode.ReadWrite);
                    RtfTextHelper.OpenFile(stream);
                    stream.Dispose();

                    var mru = Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList;
                    mru.Add(file, "profile pic");

                    IsFileGhanged = false;
                    FileManipulationEnded();
                }
            }
        }

        public static async Task<bool> SaveAsync()
        {
            if (file == tempFile)
            {
                await SaveAsAsync().ConfigureAwait(true);
                return false;
            }

            await SaveFileAsync().ConfigureAwait(true);
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
            await SaveFileAsync().ConfigureAwait(true);
        }

        public static async Task New()
        {
            bool isFileSaved = await IsFileSavedAsync().ConfigureAwait(true);
            if (isFileSaved)
            {
                FileManipulationStarted();
                await CreateTempFileAsync().ConfigureAwait(true);
                file = tempFile;
                FileManipulationEnded();
            }
        }

        public static async void ShareAsync()
        {
            if (file == tempFile)
            {
                await CreateTempFileAsync().ConfigureAwait(true);
            }

            await IsFileSavedAsync().ConfigureAwait(true);
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += DataRequested;
            DataTransferManager.ShowShareUI();
        }

        public static async Task<bool> IsFileSavedAsync()
        {
            if (IsFileGhanged)
            {
                var result = await ContentDialogService.ShowSaveDocumentDialogAsync().ConfigureAwait(true);

                if (result == ContentDialogResult.None)
                {
                    return false;
                }
                else if (result == ContentDialogResult.Primary)
                {
                    bool isFileSaved = await SaveAsync().ConfigureAwait(true);
                    return isFileSaved;
                }
            }

            return true;
        }

        private static async Task SaveFileAsync()
        {
            FileManipulationStarted();
            var stream = await file.OpenAsync(FileAccessMode.ReadWrite);
            RtfTextHelper.SaveFile(stream);
            stream.Dispose();
            IsFileGhanged = false;
            FileManipulationEnded();
        }

        private static void DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;
            request.Data.SetStorageItems(new[] { file }, false);
            request.Data.Properties.Title = file.Name;
            request.Data.Properties.Description = "Shared from Dictation";
        }

        private static async Task CreateTempFileAsync()
        {
            StorageFolder storageFolder = ApplicationData.Current.TemporaryFolder;
            tempFile = await storageFolder.CreateFileAsync("Document.rtf", CreationCollisionOption.ReplaceExisting);
            var stream = await tempFile.OpenAsync(FileAccessMode.ReadWrite);
            RtfTextHelper.OpenFile(stream);
            stream.Dispose();
            IsFileGhanged = false;
        }
    }
}
