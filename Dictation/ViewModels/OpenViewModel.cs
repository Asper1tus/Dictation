namespace Dictation.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using Dictation.Commands;
    using Dictation.Helpers;
    using Dictation.Services;
    using Windows.Storage;
    using Windows.Storage.Pickers;
    using Windows.UI.Xaml.Media.Animation;

    public class OpenViewModel
    {
        private ICommand openFile;

        public ICommand OpenFileCommand => openFile ?? (openFile = new RelayCommand(OpenFile));

        private async void OpenFile()
        {
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
