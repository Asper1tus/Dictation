namespace Dictation.ViewModels
{
    using Dictation.Models;
    using GalaSoft.MvvmLight.Ioc;

    public class Locator
    {

        public Locator()
        {
            DocumentModel document = new DocumentModel();

            SimpleIoc.Default.Register(() => document);

            SimpleIoc.Default.Register<MenuViewModel>();
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<HelpViewModel>();
            SimpleIoc.Default.Register<OpenViewModel>();
            SimpleIoc.Default.Register<SaveAsViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
            SimpleIoc.Default.Register<FindReplaceViewModel>();
        }

        public MainPageViewModel MainPageViewModel => SimpleIoc.Default.GetInstance<MainPageViewModel>();

        public FindReplaceViewModel FindReplaceViewModel => SimpleIoc.Default.GetInstance<FindReplaceViewModel>();

        public OpenViewModel OpenViewModel => SimpleIoc.Default.GetInstance<OpenViewModel>();

        public MenuViewModel MenuViewModel => SimpleIoc.Default.GetInstance<MenuViewModel>();

        public DocumentModel Document => SimpleIoc.Default.GetInstance<DocumentModel>();

    }
}
