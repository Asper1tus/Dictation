namespace Dictation.ViewModels
{
    using System;
    using System.Text;
    using System.Windows.Input;
    using Dictation.Commands;
    using Dictation.Models;
    using Windows.Media.SpeechRecognition;
    using Windows.UI.Core;

    public class RecognizerViewModel
    {

        private readonly StringBuilder dictatedTextBuilder;
        private SpeechRecognizer speechRecognizer;
        private CoreDispatcher dispatcher;

        public RecognizerViewModel(DocumentModel document)
        {
            Document = document;
            Document.Text = string.Empty;
            IsListening = false;
            dictatedTextBuilder = new StringBuilder();
            ListeningCommand = new RelayCommand(Listening);
            dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
            InitializeRecognition();
        }

        public ICommand ListeningCommand { get; }

        public DocumentModel Document { get; set; }

        public bool IsListening { get; set; }

        public async void InitializeRecognition()
        {
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

            if (IsListening)
            {
                if (speechRecognizer.State == SpeechRecognizerState.Idle)
                {
                    dictatedTextBuilder.Append(Document.Text);
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
                    Document.Text = dictatedTextBuilder.ToString();
                });
            }
            else
            {
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    Document.Text = dictatedTextBuilder.ToString();
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
                        Document.Text = dictatedTextBuilder.ToString();
                    });
                }
                else
                {
                    await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        Document.Text = dictatedTextBuilder.ToString();
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
                Document.Text = textboxContent;
            });
        }
    }
}