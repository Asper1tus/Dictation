namespace Dictation.Converetrs
{
    using System;
    using Windows.UI.Xaml.Data;

    public class ZoomToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            float zoomValue = (float)value;
            string zoomPersents = (int)(zoomValue * 100) + "%";
            return zoomPersents;
        }

        // No need to implement converting back on a one-way binding
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
