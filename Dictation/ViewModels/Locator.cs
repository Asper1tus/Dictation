namespace Dictation.ViewModels
{
    using Dictation.Models;
    using Dictation.Views;
    using GalaSoft.MvvmLight.Ioc;
    using GalaSoft.MvvmLight.Views;

    public class Locator
    {
        public Locator()
        {
            Register<MenuViewModel, Menu>();
            Register<MainPageViewModel, MainPage>();
            Register<HelpViewModel, HelpPage>();
            Register<OpenViewModel, Open>();
            Register <SaveAsViewModel, SaveAsPage>();
            Register<SettingsViewModel, SettingsPage>();

        }

        public MainViewModel MainViewModel => SimpleIoc.Default.GetInstance<MainViewModel>();

        public DocumentModel Document => SimpleIoc.Default.GetInstance<DocumentModel>();

        public void Register<VM, V>()
            where VM : class
        {
            SimpleIoc.Default.Register<VM>();
            var nav = new NavigationService();
            nav.Configure(typeof(VM).FullName, typeof(V));
        }
    }
}
