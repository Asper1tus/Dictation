namespace Dictation.ViewModels
{
    using System.Collections.Generic;
    using System.Windows.Input;
    using Dictation.Commands;
    using Dictation.Services;
    using Windows.Storage.Pickers;
    using Windows.UI.Xaml.Media.Animation;

    public class OpenViewModel
    {
        private ICommand openFile;

        public ICommand OpenFileCommand => openFile ?? (openFile = new RelayCommand(OpenFile));

        private void OpenFile()
        {
            // TODO: OpenFile
            //  openPicker = new FileOpenPicker
            // {
            //    ViewMode = PickerViewMode.Thumbnail,
            //    SuggestedStartLocation = PickerLocationId.Desktop,
            //    CommitButtonText = "Open",
            // };
            // List<string> filters = new List<string>() { ".txt", ".rtf", ".doc", ".docx", ".html", ".htm" };

            // AddFilters(openPicker, filters);
            // var file = await openPicker.PickSingleFileAsync();

            // if (file != null)
            // {
            //    //document.Text = await FileIO.ReadTextAsync(file);
            // }

            NavigationService.Navigate(typeof(MainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
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
