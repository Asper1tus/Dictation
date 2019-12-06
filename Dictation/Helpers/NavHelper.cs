namespace Dictation.Helpers
{
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public static class NavHelper
    {
        public static readonly DependencyProperty NavigateToProperty =
            DependencyProperty.RegisterAttached("NavigateTo", typeof(Type), typeof(NavHelper), new PropertyMetadata(null));

        public static Type GetNavigateTo(NavigationViewItem item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return (Type)item.GetValue(NavigateToProperty);
        }

        public static void SetNavigateTo(NavigationViewItem item, Type value)
        {
            if (item != null)
            {
                item.SetValue(NavigateToProperty, value);
            }
        }
    }
}
