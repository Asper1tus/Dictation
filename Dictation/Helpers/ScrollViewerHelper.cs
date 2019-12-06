namespace Dictation.Helpers
{
    using Dictation.Services;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public static class ScrollViewerHelper
    {
        public static readonly DependencyProperty ZoomValueProperty =
               DependencyProperty.RegisterAttached("ZoomValue", typeof(float), typeof(ScrollViewerHelper), new PropertyMetadata(0, Callback));

        private static ScrollViewer viewer;

        public static float GetZoomValue(ScrollViewer scrollViewer)
        {
            return (float)scrollViewer.GetValue(ZoomValueProperty);
        }

        public static void SetZoomValue(ScrollViewer scrollViewer, float value)
        {
            if (scrollViewer != null)
            {
                viewer = scrollViewer;
                viewer.ViewChanging += ViewChanging;

                scrollViewer.SetValue(ZoomValueProperty, value);
            }
        }

        private static void ViewChanging(object sender, ScrollViewerViewChangingEventArgs e)
        {
            MessageService.SendZoomFactor(viewer.ZoomFactor);
        }

        private static void Callback(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var scrollViewer = (ScrollViewer)sender;
            scrollViewer.ChangeView(null, null, (float)args.NewValue);
        }
    }
}
