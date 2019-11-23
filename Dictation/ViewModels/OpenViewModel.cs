namespace Dictation.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using Dictation.Commands;
    using Dictation.Helpers;
    using Dictation.Models;
    using Dictation.Services;
    using Windows.UI.Xaml.Media.Animation;

    public class OpenViewModel : Observable
    {
        private ICommand openFileCommand;
        private ICommand openRecentFileCommand;

        public OpenViewModel()
        {
            Items = new List<FileModel>();
            RecentlyFilesAsync();
        }

        public ICommand OpenFileCommand => openFileCommand ?? (openFileCommand = new RelayCommand(OpenFile));

        public ICommand OpenRecentFileCommand => openRecentFileCommand ?? (openRecentFileCommand = new RelayCommand<FileModel>(OpenRecentFile));

        public List<FileModel> Items { get; set; }

        public async void RecentlyFilesAsync()
        {
            Items.Clear();
            var mru = Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList;
            foreach (Windows.Storage.AccessCache.AccessListEntry entry in mru.Entries)
            {
                string mruToken = entry.Token;
                string mruMetadata = entry.Metadata;
                var item = await mru.GetItemAsync(mruToken);
                Items.Add(new FileModel { Name = item.Name, Path = item.Path, IconPath = "ms-appx:///Assets/RtfFileIcon.png" });
            }
        }

        private void OpenFile()
        {
            FileService.OpenAsync();
            RecentlyFilesAsync();
            NavigationService.Navigate(typeof(MainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void OpenRecentFile(FileModel file)
        {
            FileService.OpenAsync(file.Path);
            NavigationService.Navigate(typeof(MainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }
    }
}
