namespace Dictation.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using Dictation.Commands;
    using Dictation.Helpers;
    using Dictation.Services;
    using Windows.UI.Xaml;

    public class SettingsViewModel : Observable
    {
        private ICommand chooseThemeCommand;
        private ICommand saveCommand;
        private ICommand restoreDefaultCommand;
        private string language;
        private string font;
        private int size;
        private string theme;
        private int minutes;
        private bool isSaveEnabled;
        private bool isValid;
        private DispatcherTimer timer;

        public SettingsViewModel()
        {
            var currentTheme = App.RootTheme.ToString();
            Font = App.Font;
            Size = App.FontSize;
            IsSaveEnabled = App.IsSaveEnabled;
            Minutes = App.Minutes;
            Theme = currentTheme;
            Language = App.RecognitionLanguage;
            InitializeTimer();
        }

        public ICommand ChooseThemeCommand => chooseThemeCommand ?? (chooseThemeCommand = new RelayCommand<string>(ChooseTheme));

        public ICommand SaveCommand => saveCommand ?? (saveCommand = new RelayCommand(SaveSettings));

        public ICommand RestoreDefaultCommand => restoreDefaultCommand ?? (restoreDefaultCommand = new RelayCommand(RestoreDefault));

        public List<string> Fonts
        {
            get
            {
                return FontService.Fonts;
            }
        }

        public List<int> Sizes
        {
            get
            {
                return FontService.Sizes;
            }
        }

        public List<string> Languages
        {
            get
            {
                return RecognizerService.GetSupportedLanguagesNativeName();
            }
        }

        public string Theme
        {
            get { return theme; }
            set { Set(ref this.theme, value); }
        }

        public string Font
        {
            get { return font; }
            set { Set(ref this.font, value); }
        }

        public int Size
        {
            get { return size; }
            set { Set(ref this.size, value); }
        }

        public string Language
        {
            get { return language; }
            set { Set(ref this.language, value); }
        }

        public int Minutes
        {
            get { return minutes; }
            set { Set(ref this.minutes, value); }
        }

        public bool IsSaveEnabled
        {
            get { return isSaveEnabled; }
            set { Set(ref this.isSaveEnabled, value); }
        }

        public bool IsValid
        {
            get
            {
                return isValid;
            }

            set
            {
                if (!value)
                {
                    Set(ref this.isValid, value);
                    Minutes = 0;
                }
            }
        }

        private void ChooseTheme(string selectedTheme)
        {
            if (selectedTheme != null)
            {
                Theme = selectedTheme;
            }
        }

        private void SaveSettings()
        {
            App.FontSize = Size;
            App.Font = Font;
            App.RecognitionLanguage = Language;
            App.Minutes = Minutes;
            App.IsSaveEnabled = IsSaveEnabled;
            App.RootTheme = App.GetEnum<ElementTheme>(Theme);
            RecognizerService.SetRecognitionLanguage(Language);
            InitializeTimer();
        }

        private void Timer_Tick(object sender, object e)
        {
            FileService.SaveAsync();
        }

        private void RestoreDefault()
        {
            Size = DefaultSettings.Size;
            Font = DefaultSettings.Font;
            Language = DefaultSettings.Language.NativeName;
            Theme = DefaultSettings.Theme;
            Minutes = DefaultSettings.Minutes;
            IsSaveEnabled = App.IsSaveEnabled;
            SaveSettings();
        }

        private void InitializeTimer()
        {
            if (IsSaveEnabled)
            {
                int seconds = minutes * 60;
                timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, seconds) };
                timer.Tick += Timer_Tick;
                timer.Start();
            }
        }
    }
}
