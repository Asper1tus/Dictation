namespace Dictation.Converetrs
{
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Media;

    public class ButtonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string selectedPanel, currenPanel;
            if (parameter != null && value != null)
            {
                selectedPanel = value.ToString();
                currenPanel = parameter.ToString();

                if (selectedPanel == currenPanel)
                {
                    if (Application.Current.RequestedTheme == ApplicationTheme.Dark)
                    {
                        return (Application.Current.Resources["SystemControlPageBackgroundChromeLowBrush"] as SolidColorBrush);
                    }
                    else
                    {
                        return (Application.Current.Resources["SystemControlPageTextChromeBlackMediumLowBrush"] as SolidColorBrush);
                    }
                }
            }

            return null;
        }

        // No need to implement converting back on a one-way binding
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
