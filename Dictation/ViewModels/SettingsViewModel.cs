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

        public SettingsViewModel()
        {
            var currentTheme = App.RootTheme.ToString();
            Theme = currentTheme;
        }

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

        public string Theme { get; set; }

        public string Font
        {
            get { return App.Font; }
            set { App.Font = value; }
        }

        public int Size
        {
            get { return App.FontSize; }
            set { App.FontSize = value; }
        }

        public ICommand ChooseThemeCommand => chooseThemeCommand ?? (chooseThemeCommand = new RelayCommand<string>(ChooseTheme));

        private void ChooseTheme(string selectedTheme)
        {
            if (selectedTheme != null)
            {
                App.RootTheme = App.GetEnum<ElementTheme>(selectedTheme);
            }
        }
    }
}
