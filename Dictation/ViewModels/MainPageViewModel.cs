namespace Dictation.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using Dictation.Commands;
    using Dictation.Helpers;
    using Dictation.Models;
    using Dictation.Services;
    using Dictation.Views;
    using Dictation.Views.Content;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media.Animation;

    public class MainPageViewModel : Observable
    {
        private bool isPanelVisible;
        private bool isListeningVisible;
        private string title;
        private ICommand listeningCommand;
        private ICommand dispalyContent;
        private ICommand closeCommand;
        private ICommand goToMenuCommand;

        private readonly List<(string tag, Type page, string title)> pages = new List<(string tag, Type page, string title)>
{
    ("findreplace",  typeof(FindReplacePage), "Find and Replace"),
    ("share", typeof(SharePage), "Share"),
    ("tools", typeof(ToolsPage), "Formating Tools"),
    ("vocabulary", typeof(VocabularyPage), "Vocabulary Training"),
};

        public MainPageViewModel(DocumentModel document)
        {
            Document = document;
            RecognizerService.Document = document;
            RecognizerService.InitializeRecognition();
            IsPanelVisible = false;
        }

        public ICommand ListeningCommand => listeningCommand ?? (listeningCommand = new RelayCommand(Listening));

        public ICommand DispalyContent => dispalyContent ?? (dispalyContent = new RelayCommand<string>(ChoosePage));

        public ICommand CloseCommand => closeCommand ?? (closeCommand = new RelayCommand(Close));

        public ICommand GoToMenuCommand => goToMenuCommand ?? (goToMenuCommand = new RelayCommand(GoToMenu));

        public DocumentModel Document { get; set; }

        public bool IsListening
        {
            get { return isListeningVisible; }
            set { Set(ref this.isListeningVisible, value); }
        }

        public bool IsPanelVisible
        {
            get { return isPanelVisible; }
            set { Set(ref this.isPanelVisible, value); }
        }

        public string Title
        {
            get { return title; }
            set { Set(ref this.title, value); }
        }

        public void Initialize(Frame contentframe)
        {
            NavigationService.ContentFrame = contentframe;
        }

        private void Close()
        {
            IsPanelVisible = false;
        }

        private async void Listening()
        {
            if (!await AudioCapturePermissionsService.RequestMicrophonePermission())
            {
                IsListening = false;
            }

            RecognizerService.Listening(IsListening);
        }

        private void ChoosePage(object tag)
        {
            IsPanelVisible = true;
            var item = pages.FirstOrDefault(p => p.tag.Equals((string)tag));
            var page = item.page;
            NavigationService.NavigateContent(page, null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
            Title = item.title;
        }

        private void GoToMenu()
        {
            NavigationService.Navigate(typeof(Menu), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }
    }
}
