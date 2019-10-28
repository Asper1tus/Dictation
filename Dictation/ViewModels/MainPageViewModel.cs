namespace Dictation.ViewModels
{
    using System;
    using System.Text;
    using Dictate.Helpers;
    using Windows.Media.SpeechRecognition;
    using Windows.UI.Core;

    public class MainPageViewModel : Observable
    {
        private SpeechRecognizer speechRecognizer;
        private CoreDispatcher dispatcher;
        private StringBuilder dictatedTextBuilder;

        public MainPageViewModel()
        {
            IsListening = false;
            dictatedTextBuilder = new StringBuilder();
            InitializeRecognition();
        }

        public bool IsListening { get; set; }

        public string EditorText { get; set; }

        public async void InitializeRecognition()
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

        public async void Listening()
        {
            IsListening = !IsListening;
            OnPropertyChanged(nameof(IsListening));

            if (IsListening)
            {
                if (speechRecognizer.State == SpeechRecognizerState.Idle)
                {
                    dictatedTextBuilder.Append(EditorText);
                    await speechRecognizer.ContinuousRecognitionSession.StartAsync();
                }
            }
            else
            {
                if (speechRecognizer.State != SpeechRecognizerState.Idle)
                {
                    dictatedTextBuilder.Clear();
                    await speechRecognizer.ContinuousRecognitionSession.StopAsync();
                }
            }

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
                    EditorText = dictatedTextBuilder.ToString();
                    OnPropertyChanged(nameof(EditorText));
                });
            }
            else
            {
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    EditorText = dictatedTextBuilder.ToString();
                    OnPropertyChanged(nameof(EditorText));
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
                        EditorText = dictatedTextBuilder.ToString();
                        OnPropertyChanged(nameof(EditorText));
                    });
                }
                else
                {
                    await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        EditorText = dictatedTextBuilder.ToString();
                        OnPropertyChanged(nameof(EditorText));
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
                EditorText = textboxContent;
                OnPropertyChanged(nameof(EditorText));
            });
        }

    }
}
