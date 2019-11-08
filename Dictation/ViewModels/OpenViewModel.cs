namespace Dictation.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using Dictation.Commands;
    using Dictation.Models;
    using Dictation.Services;
    using Windows.Storage;
    using Windows.Storage.Pickers;
    using Windows.UI.Xaml.Media.Animation;

    public class OpenViewModel
    {
        private DocumentModel document;
        private ICommand openFile;

        public OpenViewModel(DocumentModel document)
        {
            this.document = document;
        }

        public ICommand OpenFileCommand => openFile ?? (openFile = new RelayCommand(OpenFile));

        private async void OpenFile()
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            openPicker.CommitButtonText = "Открыть";
            List<string> filters = new List<string>() { ".txt", ".rtf", ".doc", ".docx", ".html", ".htm" };

            AddFilters(openPicker, filters);
            var file = await openPicker.PickSingleFileAsync();

            if (file != null)
            {
                document.Text = await FileIO.ReadTextAsync(file);
                NavigationService.Navigate(typeof(MainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
            }
        }

        private void AddFilters(FileOpenPicker fileOpenPicker, List<string> filters)
        {
            foreach (var filter in filters)
            {
                fileOpenPicker.FileTypeFilter.Add(filter);
            }
        }
    }
}
