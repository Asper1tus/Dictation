namespace Dictation.Converetrs
{
    using System;
    using Windows.UI.Xaml.Data;

    public class ThemeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string selectedTheme, currentheme;
            if (parameter != null)
            {
                selectedTheme = value.ToString();
                currentheme = parameter.ToString();

                if (selectedTheme == currentheme)
                {
                    return true;
                }
            }

            return false;
        }

        // No need to implement converting back on a one-way binding
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
