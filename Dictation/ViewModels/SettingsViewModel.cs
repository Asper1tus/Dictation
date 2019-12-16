namespace Dictation.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Input;
    using Dictation.Commands;
    using Dictation.Helpers;
    using Dictation.Services;
    using Dictation.Views;
    using Windows.Globalization;
    using Windows.UI.Xaml;

    public class SettingsViewModel : Observable
    {
        private ICommand chooseThemeCommand;
        private ICommand saveCommand;
        private ICommand restoreDefaultCommand;
        private string dictationLanguage;
        private string font;
        private string theme;
        private int size;
        private int minutes;
        private int languageIndex;
        private bool isSaveEnabled;
        private bool isValid;
        private Language language;
        private DispatcherTimer timer;

        public SettingsViewModel()
        {
            var currentTheme = App.RootTheme.ToString();
            Font = App.Font;
            Size = App.FontSize;
            IsSaveEnabled = App.IsSaveEnabled;
            Minutes = App.Minutes;
            Theme = currentTheme;
            DictationLanguage = App.RecognitionLanguage;
            LanguageIndex = Languages.FindIndex((c) => c.LanguageTag.Equals(CultureInfo.CurrentCulture.IetfLanguageTag, StringComparison.InvariantCultureIgnoreCase));
            InitializeTimer();
        }

        public static List<Language> Languages => GetLanguages();

        public static List<string> Fonts => FontService.Fonts;

        public static List<int> Sizes => FontService.Sizes;

        public static List<string> DictationLanguages => RecognizerService.GetSupportedLanguagesNativeName();

        public ICommand ChooseThemeCommand => chooseThemeCommand ?? (chooseThemeCommand = new RelayCommand<string>(ChooseTheme));

        public ICommand SaveCommand => saveCommand ?? (saveCommand = new RelayCommand(SaveSettings));

        public ICommand RestoreDefaultCommand => restoreDefaultCommand ?? (restoreDefaultCommand = new RelayCommand(RestoreDefault));

        public string Theme
        {
            get => theme;
            set => Set(ref this.theme, value);
        }

        public string Font
        {
            get => font;
            set => Set(ref this.font, value);
        }

        public int Size
        {
            get => size;
            set => Set(ref this.size, value);
        }

        public string DictationLanguage
        {
            get => dictationLanguage;
            set => Set(ref this.dictationLanguage, value);
        }

        public Language Language
        {
            get => language;
            set => Set(ref this.language, value);
        }

        public int Minutes
        {
            get => minutes;
            set => Set(ref this.minutes, value);
        }

        public int LanguageIndex
        {
            get => languageIndex;
            set => Set(ref this.languageIndex, value);
        }

        public bool IsSaveEnabled
        {
            get => isSaveEnabled;
            set => Set(ref this.isSaveEnabled, value);
        }

        public bool IsValid
        {
            get => isValid;

            set
            {
                if (!value)
                {
                    Set(ref this.isValid, value);
                    Minutes = 0;
                }
            }
        }

        private static List<Language> GetLanguages()
        {
            List<Language> languages = new List<Language>();

            foreach (var lang in ApplicationLanguages.ManifestLanguages)
            {
                languages.Add(new Language(lang));
            }

            return languages;
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
            App.RecognitionLanguage = DictationLanguage;
            App.Minutes = Minutes;
            App.IsSaveEnabled = IsSaveEnabled;
            App.RootTheme = App.GetEnum<ElementTheme>(Theme);
            RecognizerService.SetRecognitionLanguage(DictationLanguage);
            ChangeLanguage();
            InitializeTimer();
        }

        private async void TimerTickAsync(object sender, object e)
        {
            await FileService.SaveAsync().ConfigureAwait(true);
        }

        private void RestoreDefault()
        {
            Size = DefaultSettings.Size;
            Font = DefaultSettings.Font;
            DictationLanguage = DefaultSettings.Language.NativeName;
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
                timer.Tick += TimerTickAsync;
                timer.Start();
            }
        }

        private void ChangeLanguage()
        {
            ApplicationLanguages.PrimaryLanguageOverride = Language.LanguageTag;
            NavigationService.Frame.CacheSize = 0;
            Windows.ApplicationModel.Resources.Core.ResourceContext.GetForCurrentView().Reset();
            Windows.ApplicationModel.Resources.Core.ResourceContext.GetForViewIndependentUse().Reset();
            NavigationService.Frame.CacheSize = 10;
            NavigationService.Navigate(typeof(MainPage));
        }
    }
}
