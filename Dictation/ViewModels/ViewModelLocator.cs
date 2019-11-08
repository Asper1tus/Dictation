namespace Dictation.ViewModels
{
    using GalaSoft.MvvmLight.Ioc;
    using GalaSoft.MvvmLight.Views;

    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            Register<MainViewModel, MainPage>(); 

        }

        public MainViewModel MainViewModel => SimpleIoc.Default.GetInstance<MainViewModel>();

        public void Register<VM, V>()
            where VM : class
        {
            var nav = new NavigationService();

            SimpleIoc.Default.Register<VM>();
            nav.Configure(typeof(VM).FullName, typeof(V));
        }
    }
}