namespace Dictation.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
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
        }

        public ICommand OpenFileCommand => openFileCommand ?? (openFileCommand = new RelayCommand(OpenFile));

        public ICommand OpenRecentFileCommand => openRecentFileCommand ?? (openRecentFileCommand = new RelayCommand<FileModel>(OpenRecentFile));

        public List<FileModel> Items { get; set; }

        public async Task InitializeAsync()
        {
            Items = new List<FileModel>();
            await RecentlyFilesAsync();
        }

        public async Task RecentlyFilesAsync()
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

            await Task.CompletedTask;
        }

        private void OpenFile()
        {
            FileService.OpenAsync();
            NavigationService.Navigate(typeof(MainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void OpenRecentFile(FileModel file)
        {
            FileService.OpenAsync(file.Path);
            NavigationService.Navigate(typeof(MainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }
    }
}
