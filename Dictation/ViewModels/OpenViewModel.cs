namespace Dictation.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using Dictation.Commands;
    using Dictation.Services;
    using Windows.Storage;
    using Windows.UI.Xaml.Media.Animation;

    public class OpenViewModel
    {
        public OpenViewModel()
        {
            Items = new List<IStorageItem>();
            RecentlyFilesAsync();
        }

        private ICommand openFile;

        public ICommand OpenFileCommand => openFile ?? (openFile = new RelayCommand(OpenFile));

        private void OpenFile()
        {
            FileService.OpenAsync();
            NavigationService.Navigate(typeof(MainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });

        }

        public async void RecentlyFilesAsync()
        {
            var mru = Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList;
            foreach (Windows.Storage.AccessCache.AccessListEntry entry in mru.Entries)
            {
                string mruToken = entry.Token;
                string mruMetadata = entry.Metadata;
                Items.Add(await mru.GetItemAsync(mruToken));
            }
        }

        public List<IStorageItem> Items { get; set; }
    }
}
