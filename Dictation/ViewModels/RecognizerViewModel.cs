namespace Dictation.ViewModels
{
    using System;
    using System.Text;
    using Dictate.Helpers;
    using Dictation.Models;
    using Windows.Media.SpeechRecognition;
    using Windows.UI.Core;

    public class RecognizerViewModel : Observable
    {
        private SpeechRecognizer speechRecognizer;
        private CoreDispatcher dispatcher;
        private StringBuilder dictatedTextBuilder; 

        public RecognizerViewModel()
        {
            Document = DocumentModel.GetDocument();
            Document.Text = string.Empty;
            IsListening = false;
            dictatedTextBuilder = new StringBuilder();
            InitializeRecognition();
        }

        public DocumentModel Document;

        public bool IsListening { get; set; }

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