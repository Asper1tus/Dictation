namespace Dictation.ViewModels
{
    using Dictation.ViewModels.Content;
    using GalaSoft.MvvmLight.Ioc;

    public class Locator
    {
        public Locator()
        {
            SimpleIoc.Default.Register<MenuViewModel>();
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<HelpViewModel>();
            SimpleIoc.Default.Register<OpenViewModel>();
            SimpleIoc.Default.Register<SaveAsViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
            SimpleIoc.Default.Register<FindReplaceViewModel>();
            SimpleIoc.Default.Register<ToolsViewModel>();
        }

        public MainPageViewModel MainPageViewModel => SimpleIoc.Default.GetInstance<MainPageViewModel>();

        public ToolsViewModel ToolsViewModel => SimpleIoc.Default.GetInstance<ToolsViewModel>();

        public FindReplaceViewModel FindReplaceViewModel => SimpleIoc.Default.GetInstance<FindReplaceViewModel>();

        public OpenViewModel OpenViewModel => SimpleIoc.Default.GetInstance<OpenViewModel>();

        public MenuViewModel MenuViewModel => SimpleIoc.Default.GetInstance<MenuViewModel>();
    }
}
