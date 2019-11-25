namespace Dictation.Helpers
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public class ScrollViewerHelper
    {
        public static readonly DependencyProperty ZoomValueProperty =
               DependencyProperty.RegisterAttached("ZoomValue", typeof(float), typeof(ScrollViewerHelper), new PropertyMetadata(0, Callback));

        public static float GetZoomValue(ScrollViewer scrollViewer)
        {
            return (float)scrollViewer.GetValue(ZoomValueProperty);
        }

        public static void SetZoomValue(ScrollViewer scrollViewer, float value)
        {
            scrollViewer.SetValue(ZoomValueProperty, value);
        }

        private static void Callback(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var scrollViewer = (ScrollViewer)sender;
            scrollViewer.ChangeView(null, null, (float)args.NewValue);
        }
    }
}
