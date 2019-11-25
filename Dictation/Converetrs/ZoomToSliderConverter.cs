namespace Dictation.Converetrs
{
    using System;
    using Windows.UI.Xaml.Data;

    public class ZoomToSliderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            float zoomValue = (float)value;
            double sliderValue = zoomValue * 100;
            return sliderValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            double sliderValue = (double)value;

            float zoomValue = (float)(sliderValue / 100);

            return zoomValue;
        }
    }
}
