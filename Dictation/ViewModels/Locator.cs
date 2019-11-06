namespace Dictation.ViewModels
{
    using Dictation.Views;
    using Dictation.Views.Content;
    using GalaSoft.MvvmLight.Ioc;
    using GalaSoft.MvvmLight.Views;

    public class Locator
    {
        public Locator()
        {
            Register<MainViewModel, Menu>();
        }

        public MainViewModel MainViewModel => SimpleIoc.Default.GetInstance<MainViewModel>();

        public void Register<VM, V>()
            where VM : class
        {
            SimpleIoc.Default.Register<VM>();
            var nav = new NavigationService();
            nav.Configure(typeof(VM).FullName, typeof(V));
        }
    }
}
