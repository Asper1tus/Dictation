namespace Dictation.ViewModels
{
    using System;
    using System.Linq;
    using System.Windows.Input;
    using Dictation.Commands;
    using Dictation.Helpers;
    using Dictation.Services;
    using Dictation.Views;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media.Animation;
    using Windows.UI.Xaml.Navigation;

    public class MenuViewModel : Observable
    {
        private NavigationView navigationView;
        private NavigationViewItem selected;
        private ICommand itemInvokedCommand;

        public NavigationViewItem Selected
        {
            get => selected;
            set => Set(ref selected, value);
        }

        public ICommand ItemInvokedCommand => itemInvokedCommand ?? (itemInvokedCommand = new RelayCommand<NavigationViewItemInvokedEventArgs>(OnItemInvoked));

        public void Initialize(Frame contentFrame, NavigationView navigationView)
        {
            this.navigationView = navigationView;
            NavigationService.ContentFrame = contentFrame;
            NavigationService.NavigationFailed += Frame_NavigationFailed;
            NavigationService.Navigated += Frame_Navigated;
            NavigationService.NavigateContent(typeof(OpenPage));
        }

        private static bool IsMenuItemForPageType(NavigationViewItem menuItem, Type sourcePageType)
        {
            var pageType = menuItem.GetValue(NavHelper.NavigateToProperty) as Type;
            return pageType == sourcePageType;
        }

        private static async void ChooseItem(string tag)
        {
            switch (tag)
            {
                case "back":
                    NavigationService.Navigate(typeof(MainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
                    break;
                case "new":
                    await FileService.New().ConfigureAwait(true);
                    break;
                case "save":
                    await FileService.SaveAsync().ConfigureAwait(true);
                    break;
                case "saveas":
                    await FileService.SaveAsAsync().ConfigureAwait(true);
                    break;
            }

            NavigationService.Navigate(typeof(MainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void OnItemInvoked(NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                NavigationService.NavigateContent(typeof(SettingsPage));
                return;
            }

            var item = navigationView.MenuItems
                            .OfType<NavigationViewItem>()
                            .First(menuItem => (string)menuItem.Content == (string)args.InvokedItem);

            if (item.Tag != null)
            {
                var tag = item.Tag.ToString();
                ChooseItem(tag);
                return;
            }

            var pageType = item.GetValue(NavHelper.NavigateToProperty) as Type;
            NavigationService.NavigateContent(pageType);
        }

        private void Frame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw e.Exception;
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            var sourcePageType = e.SourcePageType;
            if (sourcePageType == typeof(Menu))
            {
                sourcePageType = typeof(OpenPage);
            }

            Selected = navigationView.MenuItems
                            .OfType<NavigationViewItem>()
                            .FirstOrDefault(menuItem => IsMenuItemForPageType(menuItem, sourcePageType));
        }
    }
}
