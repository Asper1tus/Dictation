namespace Dictation.ViewModels
{
    using System;
    using System.Windows.Input;
    using Dictation.Commands;
    using Dictation.Services;
    using Windows.UI.Xaml.Media.Animation;

    public class OpenViewModel
    {
        private ICommand openFile;

        public ICommand OpenFileCommand => openFile ?? (openFile = new RelayCommand(OpenFile));

        private void OpenFile()
        {
            FileService.OpenAsync();
            NavigationService.Navigate(typeof(MainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }
    }
}
