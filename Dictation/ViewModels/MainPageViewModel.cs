﻿namespace Dictation.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using Dictation.Commands;
    using Dictation.Helpers;
    using Dictation.Services;
    using Dictation.Views;
    using Dictation.Views.Content;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media.Animation;

    public class MainPageViewModel : Observable
    {
        private readonly List<(string tag, Type page, string title)> pages = new List<(string tag, Type page, string title)>
{
    ("findreplace",  typeof(FindReplacePage), "Find and Replace"),
    ("share", typeof(SharePage), "Share"),
    ("tools", typeof(ToolsPage), "Formating Tools"),
    ("vocabulary", typeof(VocabularyPage), "Vocabulary Training"),
};

        private Frame contentFrame;
        private bool isPanelVisible;
        private bool isListeningVisible;
        private string title;
        private float zoomFactor;
        private ICommand listeningCommand;
        private ICommand dispalyContentCommand;
        private ICommand closeCommand;
        private ICommand goToMenuCommand;
        private ICommand operationCommand;
        private ICommand changeZoomCommand;

        public MainPageViewModel()
        {
            RecognizerService.InitializeRecognizerService();
            IsPanelVisible = false;
            ZoomFactor = 1f;
            MessageService.ZoomFactorChanged += ZoomFactorChanged;
        }

        public ICommand ListeningCommand => listeningCommand ?? (listeningCommand = new RelayCommand(Listening));

        public ICommand DispalyContentCommand => dispalyContentCommand ?? (dispalyContentCommand = new RelayCommand<string>(ChoosePage));

        public ICommand CloseCommand => closeCommand ?? (closeCommand = new RelayCommand(Close));

        public ICommand GoToMenuCommand => goToMenuCommand ?? (goToMenuCommand = new RelayCommand(GoToMenu));

        public ICommand ChangeZoomCommand => changeZoomCommand ?? (changeZoomCommand = new RelayCommand<string>(ChangeZoom));

        public ICommand OperationCommand => operationCommand ?? (operationCommand = new RelayCommand<string>(MessageService.SendOperation));

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

        public float ZoomFactor
        {
            get { return zoomFactor; }
            set { Set(ref this.zoomFactor, value); }
        }

        public int FontSize
        {
            get
            {
                // FontSize in RichEditBox 4 sizes smaller
                return App.FontSize + 4;
            }
        }

        public string Font
        {
            get
            {
                return App.Font;
            }
        }

        public void Initialize(Frame contentFrame)
        {
            this.contentFrame = contentFrame;
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

            try
            {
                RecognizerService.Listening(IsListening);
            }
            catch
            {
                IsListening = false;
            }
        }

        private void ChoosePage(object tag)
        {
            NavigationService.ContentFrame = contentFrame;
            var item = pages.FirstOrDefault(p => p.tag.Equals((string)tag));
            var page = item.page;

            if (NavigationService.ContentFrame.CurrentSourcePageType != page)
            {
                IsPanelVisible = true;
                NavigationService.NavigateContent(page, null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
                Title = item.title;
            }
            else
            {
                IsPanelVisible = !IsPanelVisible;
                NavigationService.ContentFrame = null;
            }
        }

        private void GoToMenu()
        {
            NavigationService.Navigate(typeof(Menu), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }

        private void ChangeZoom(string operation)
        {
            if (operation == "Plus")
            {
                ZoomFactor += 0.1f;

                if (ZoomFactor > 5)
                {
                    ZoomFactor = 5;
                }
            }
            else
            {
                ZoomFactor -= 0.1f;

                if (ZoomFactor < 0.1f)
                {
                    ZoomFactor = 0.1f;
                }
            }
        }

        private void ZoomFactorChanged(float zoomFactor)
        {
            ZoomFactor = zoomFactor;
        }
    }
}
