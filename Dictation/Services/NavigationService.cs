namespace Dictation.Services
{
    using System;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media.Animation;
    using Windows.UI.Xaml.Navigation;

    public static class NavigationService
    {
        private static Frame frame;
        private static object lastParamUsed;
        private static Frame contentFrame;

        public static event NavigatedEventHandler Navigated;

        public static event NavigationFailedEventHandler NavigationFailed;

        public static Frame Frame
        {
            get
            {
                if (frame == null)
                {
                    frame = Window.Current.Content as Frame;
                    RegisterFrameEvents();
                }

                return frame;
            }

            set
            {
                UnregisterFrameEvents();
                frame = value;
                RegisterFrameEvents();
            }
        }

        public static Frame ContentFrame
        {
            get
            {
                if (contentFrame == null)
                {
                    contentFrame = Window.Current.Content as Frame;
                    RegisterFrameEvents();
                }

                return contentFrame;
            }

            set
            {
                UnregisterFrameEvents();
                contentFrame = value;
                RegisterFrameEvents();
            }
        }

        public static bool CanGoBack => Frame.CanGoBack;

        public static bool CanGoForward => Frame.CanGoForward;

        public static bool GoBack()
        {
            if (CanGoBack)
            {
                Frame.GoBack();
                return true;
            }

            return false;
        }

        public static void GoForward() => Frame.GoForward();

        public static bool Navigate(Type pageType, object parameter = null, NavigationTransitionInfo infoOverride = null)
        {
            // Don't open the same page multiple times
            if (Frame.Content?.GetType() != pageType || (parameter != null && !parameter.Equals(lastParamUsed)))
            {
                var navigationResult = Frame.Navigate(pageType, parameter, infoOverride);
                if (navigationResult)
                {
                    lastParamUsed = parameter;
                }

                return navigationResult;
            }
            else
            {
                return false;
            }
        }

        public static bool Navigate<T>(object parameter = null, NavigationTransitionInfo infoOverride = null)
            where T : Page
            => Navigate(typeof(T), parameter, infoOverride);

        public static bool NavigateContent(Type pageType, object parameter = null, NavigationTransitionInfo infoOverride = null)
        {
            if (ContentFrame.Content?.GetType() != pageType || (parameter != null && !parameter.Equals(lastParamUsed)))
            {
                var navigationResult = ContentFrame.Navigate(pageType, parameter, infoOverride);
                if (navigationResult)
                {
                    lastParamUsed = parameter;
                }

                return navigationResult;
            }
            else
            {
                return false;
            }
        }

        private static void RegisterFrameEvents()
        {
            if (frame != null)
            {
                frame.Navigated += Frame_Navigated;
                frame.NavigationFailed += Frame_NavigationFailed;
            }
        }

        private static void UnregisterFrameEvents()
        {
            if (frame != null)
            {
                frame.Navigated -= Frame_Navigated;
                frame.NavigationFailed -= Frame_NavigationFailed;
            }
        }

        private static void Frame_NavigationFailed(object sender, NavigationFailedEventArgs e) => NavigationFailed?.Invoke(sender, e);

        private static void Frame_Navigated(object sender, NavigationEventArgs e) => Navigated?.Invoke(sender, e);
    }
}
