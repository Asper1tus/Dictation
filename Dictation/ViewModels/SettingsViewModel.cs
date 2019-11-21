namespace Dictation.ViewModels
{
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

        public SettingsViewModel()
        {
            var currentTheme = App.RootTheme.ToString();
            Font = App.Font;
            Size = App.FontSize;
            Theme = currentTheme;
            Language = App.RecognitionLanguage;
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

        private void ChooseTheme(string selectedTheme)
        {
            if (selectedTheme != null)
            {
                App.RootTheme = App.GetEnum<ElementTheme>(selectedTheme);
            }
        }

        private void SaveSettings()
        {
            App.FontSize = Size;
            App.Font = Font;
            App.RecognitionLanguage = Language;
            RecognizerService.SetRecognitionLanguage(Language);
        }

        private void RestoreDefault()
        {
            Size = DefaultSettings.Size;
            Font = DefaultSettings.Font;
            Language = DefaultSettings.Language.NativeName;
        }
    }
}
