namespace Dictation
{
    using System;
    using System.Reflection;
    using Dictation.Helpers;
    using Dictation.ViewModels;
    using Windows.ApplicationModel;
    using Windows.ApplicationModel.Activation;
    using Windows.Globalization;
    using Windows.Storage;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// Обеспечивает зависящее от конкретного приложения поведение, дополняющее класс Application по умолчанию.
    /// </summary>
    public sealed partial class App : Application
    {
        private const string SelectedAppThemeKey = "SelectedAppTheme";
        private static Locator locator;

        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        public static Locator Locator => locator ?? (locator = new Locator());

        public static string RecognitionLanguage
        {
            get
            {
                if (ApplicationData.Current.LocalSettings.Values["RecognitionLanguage"] != null)
                {
                    return ApplicationData.Current.LocalSettings.Values["RecognitionLanguage"].ToString();
                }

                return DefaultSettings.Language.NativeName;
            }

            set
            {
                ApplicationData.Current.LocalSettings.Values["RecognitionLanguage"] = value;
            }
        }

        public static int FontSize
        {
            get
            {
                if (ApplicationData.Current.LocalSettings.Values["FontSize"] != null)
                {
                    return (int)ApplicationData.Current.LocalSettings.Values["FontSize"];
                }

                return DefaultSettings.Size;
            }

            set
            {
                ApplicationData.Current.LocalSettings.Values["FontSize"] = value;
            }
        }

        public static string Font
        {
            get
            {
                if (ApplicationData.Current.LocalSettings.Values["Font"] != null)
                {
                    return ApplicationData.Current.LocalSettings.Values["Font"].ToString();
                }

                return DefaultSettings.Font;
            }

            set
            {
                ApplicationData.Current.LocalSettings.Values["Font"] = value.ToString();
            }
        }

        public static int Minutes
        {
            get
            {
                if (ApplicationData.Current.LocalSettings.Values["Minutes"] != null)
                {
                    return (int)ApplicationData.Current.LocalSettings.Values["Minutes"];
                }

                return DefaultSettings.Minutes;
            }

            set
            {
                ApplicationData.Current.LocalSettings.Values["Minutes"] = value;
            }
        }

        public static bool IsSaveEnabled
        {
            get
            {
                if (ApplicationData.Current.LocalSettings.Values["IsSaveEnabled"] != null)
                {
                    return (bool)ApplicationData.Current.LocalSettings.Values["IsSaveEnabled"];
                }

                return DefaultSettings.IsSaveEnabled;
            }

            set
            {
                ApplicationData.Current.LocalSettings.Values["IsSaveEnabled"] = value;
            }
        }

        public static ElementTheme RootTheme
        {
            get
            {
                if (Window.Current.Content is FrameworkElement rootElement)
                {
                    return rootElement.RequestedTheme;
                }

                return ElementTheme.Default;
            }

            set
            {
                if (Window.Current.Content is FrameworkElement rootElement)
                {
                    rootElement.RequestedTheme = value;
                }

                ApplicationData.Current.LocalSettings.Values[SelectedAppThemeKey] = value.ToString();
            }
        }

        public static TEnum GetEnum<TEnum>(string text)
            where TEnum : struct
        {
            if (!typeof(TEnum).GetTypeInfo().IsEnum)
            {
                throw new InvalidOperationException("Generic parameter 'TEnum' must be an enum.");
            }

            return (TEnum)Enum.Parse(typeof(TEnum), text);
        }

        /// <summary>
        /// Вызывается при обычном запуске приложения пользователем. Будут использоваться другие точки входа,
        /// например, если приложение запускается для открытия конкретного файла.
        /// </summary>
        /// <param name="e">Сведения о запросе и обработке запуска.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Не повторяйте инициализацию приложения, если в окне уже имеется содержимое,
            // только обеспечьте активность окна
            if (rootFrame == null)
            {
                // Создание фрейма, который станет контекстом навигации, и переход к первой странице
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: Загрузить состояние из ранее приостановленного приложения
                }

                // Размещение фрейма в текущем окне
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // Если стек навигации не восстанавливается для перехода к первой странице,
                    // настройка новой страницы путем передачи необходимой информации в качестве параметра
                    // навигации
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }

                // Обеспечение активности текущего окна
                Window.Current.Activate();
            }

            string savedTheme = ApplicationData.Current.LocalSettings.Values[SelectedAppThemeKey]?.ToString();

            if (savedTheme != null)
            {
                RootTheme = GetEnum<ElementTheme>(savedTheme);
            }
        }

        /// <summary>
        /// Вызывается в случае сбоя навигации на определенную страницу.
        /// </summary>
        /// <param name="sender">Фрейм, для которого произошел сбой навигации.</param>
        /// <param name="e">Сведения о сбое навигации.</param>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Вызывается при приостановке выполнения приложения.  Состояние приложения сохраняется
        /// без учета информации о том, будет ли оно завершено или возобновлено с неизменным
        /// содержимым памяти.
        /// </summary>
        /// <param name="sender">Источник запроса приостановки.</param>
        /// <param name="e">Сведения о запросе приостановки.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            // TODO: Сохранить состояние приложения и остановить все фоновые операции
            deferral.Complete();
        }
    }
}
