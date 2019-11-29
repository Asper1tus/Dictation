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
        private List<FileModel> items;
        private ICommand openFileCommand;
        private ICommand openRecentFileCommand;

        public OpenViewModel()
        {
            Items = new List<FileModel>();
        }

        public ICommand OpenFileCommand => openFileCommand ?? (openFileCommand = new RelayCommand(OpenFile));

        public ICommand OpenRecentFileCommand => openRecentFileCommand ?? (openRecentFileCommand = new RelayCommand<FileModel>(OpenRecentFile));

        public List<FileModel> Items
        {
            get { return items; }
            set { Set(ref this.items, value); }
        }

        public async Task InitializeAsync()
        {
            await RecentlyFilesAsync();
        }

        private async Task RecentlyFilesAsync()
        {
            var mru = Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList;
            foreach (Windows.Storage.AccessCache.AccessListEntry entry in mru.Entries)
            {
                string mruToken = entry.Token;
                string mruMetadata = entry.Metadata;
                try
                {
                    var item = await mru.GetItemAsync(mruToken);

                    if (!Items.Exists(x => x.Path == item.Path))
                    {
                        Items.Insert(0, new FileModel { Name = item.Name, Path = item.Path, IconPath = "ms-appx:///Assets/RtfFileIcon.png" });
                        OnPropertyChanged(nameof(Items));
                    }
                }
                catch
                {
                    mru.Remove(mruToken);
                }
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
