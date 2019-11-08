namespace Dictation.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Dictation.ViewModels;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media.Animation;

    public sealed partial class Menu : Page
    {
        public MenuViewModel menuViewModel;
        public FileViewModel fileViewModel;
        private readonly Frame rootFrame;
        private readonly List<(string Tag, Type Page)> pages = new List<(string Tag, Type Page)>
{
    ("back", typeof(MainPage)),
    ("saveas", typeof(SaveAsPage)),
    ("open", typeof(Open)),
    ("help", typeof(HelpPage)),
    ("settings", typeof(SettingsPage)),
};

        public Menu()
        {
            this.InitializeComponent();
            rootFrame = Window.Current.Content as Frame;
            menuViewModel = new MenuViewModel(rootFrame);
            fileViewModel = new FileViewModel();

        }

        private void NvTopLevelNav_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked == true)
            {
                NavView_Navigate("settings", args.RecommendedNavigationTransitionInfo);
            }
            else if (args.InvokedItemContainer != null)
            {
                var navItemTag = args.InvokedItemContainer.Tag.ToString();
                NavView_Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void NavView_Navigate(string navItemTag, NavigationTransitionInfo transitionInfo)
        {
            Type page = null;
            switch (navItemTag)
            {
                case "back": 
                    rootFrame.Navigate(typeof(MainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
                    break;
                case "new":
                    break;
                case "save":
                    break;
                case "print":
                    break;
                case "close":
                    break;
                default:
                    var item = pages.FirstOrDefault(p => p.Tag.Equals(navItemTag));
                    page = item.Page;
                    break;
            }

            var preNavPageType = ContentFrame.CurrentSourcePageType;

            if (!(page is null) && !Equals(preNavPageType, page))
            {
                ContentFrame.Navigate(page, null, transitionInfo);
            }
        }

        private void NvTopLevelNav_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (NavigationViewItemBase item in NavView.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "open")
                {
                    NavView.SelectedItem = item;
                    break;
                }
            }

            ContentFrame.Navigate(typeof(Open));
        }

    }
}
