namespace Dictation
{
    using System;
    using System.Text;
    using Windows.Media.SpeechRecognition;
    using Windows.UI.Core;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    public sealed partial class MainPage : Page
    {
        private SpeechRecognizer speechRecognizer;
        private CoreDispatcher dispatcher;
        private StringBuilder dictatedTextBuilder;
        private bool isListening = false;
        private bool isVisible = false;

        public MainPage()
        {
            InitializeComponent();
            isListening = true;
            dictatedTextBuilder = new StringBuilder();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
            speechRecognizer = new SpeechRecognizer();
            SpeechRecognitionCompilationResult result =
                await speechRecognizer.CompileConstraintsAsync();
            speechRecognizer.ContinuousRecognitionSession.ResultGenerated +=
        ContinuousRecognitionSession_ResultGenerated;
            speechRecognizer.ContinuousRecognitionSession.Completed +=
      ContinuousRecognitionSession_Completed;
            speechRecognizer.HypothesisGenerated +=
                SpeechRecognizer_HypothesisGenerated;
        }

        private async void AppBarToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (isListening)
            {
                if (speechRecognizer.State == SpeechRecognizerState.Idle)
                {
                    await speechRecognizer.ContinuousRecognitionSession.StartAsync();
                }
            }
            else
            {
                if (speechRecognizer.State != SpeechRecognizerState.Idle)
                {
                    await speechRecognizer.ContinuousRecognitionSession.StopAsync();
                }
            }

            isListening = !isListening;
        }

        private async void ContinuousRecognitionSession_ResultGenerated(
      SpeechContinuousRecognitionSession sender,
      SpeechContinuousRecognitionResultGeneratedEventArgs args)
        {
            if (args.Result.Confidence == SpeechRecognitionConfidence.Medium ||
              args.Result.Confidence == SpeechRecognitionConfidence.High)
            {
                dictatedTextBuilder.Append(args.Result.Text + " ");

                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    editor.Document.SetText(Windows.UI.Text.TextSetOptions.None, dictatedTextBuilder.ToString());
                });
            }
            else
            {
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    editor.Document.SetText(Windows.UI.Text.TextSetOptions.None, dictatedTextBuilder.ToString());
                });
            }
        }

        private async void ContinuousRecognitionSession_Completed(
      SpeechContinuousRecognitionSession sender,
      SpeechContinuousRecognitionCompletedEventArgs args)
        {
            if (args.Status != SpeechRecognitionResultStatus.Success)
            {
                if (args.Status == SpeechRecognitionResultStatus.TimeoutExceeded)
                {
                    await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        editor.Document.SetText(Windows.UI.Text.TextSetOptions.None, dictatedTextBuilder.ToString());
                    });
                }
                else
                {
                    await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        editor.Document.SetText(Windows.UI.Text.TextSetOptions.None, dictatedTextBuilder.ToString());
                    });
                }
            }
        }

        private async void SpeechRecognizer_HypothesisGenerated(
  SpeechRecognizer sender,
  SpeechRecognitionHypothesisGeneratedEventArgs args)
        {
            string hypothesis = args.Hypothesis.Text;
            string textboxContent = dictatedTextBuilder.ToString() + " " + hypothesis + " ...";

            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                editor.Document.SetText(Windows.UI.Text.TextSetOptions.None, textboxContent);
            });
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (isVisible)
            {
                FindAndReplaceBox.Visibility = Visibility.Visible;
            }
            else
            {
                FindAndReplaceBox.Visibility = Visibility.Collapsed;
            }

            isVisible = !isVisible;
        }
    }
}