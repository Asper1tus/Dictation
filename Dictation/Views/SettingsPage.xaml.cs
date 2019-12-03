﻿namespace Dictation.Views
{
    using Dictation.ViewModels;
    using Windows.UI.Xaml.Controls;

    public sealed partial class SettingsPage : Page
    {
        private readonly SettingsViewModel viewModel;

        public SettingsPage()
        {
            viewModel = App.Locator.SettingsViewModel;
            this.InitializeComponent();
        }
    }
}
