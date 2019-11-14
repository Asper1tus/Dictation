namespace Dictation.Services
{
    using System;
    using System.Text;
    using Dictation.Helpers;
    using Windows.Media.SpeechRecognition;
    using Windows.UI.Core;

    internal static class RecognizerService
    {
        private static StringBuilder dictatedTextBuilder;
        private static SpeechRecognizer speechRecognizer;
        private static CoreDispatcher dispatcher;

        public static async void InitializeRecognition()
        {
            dictatedTextBuilder = new StringBuilder();
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

        public static async void Listening(bool isListening)
        {
            if (isListening)
            {
                if (speechRecognizer.State == SpeechRecognizerState.Idle)
                {
                    dictatedTextBuilder.Append(RtfTextHelper.Text);
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

        private static async void ContinuousRecognitionSession_ResultGenerated(
      SpeechContinuousRecognitionSession sender,
      SpeechContinuousRecognitionResultGeneratedEventArgs args)
        {
            if (args.Result.Confidence == SpeechRecognitionConfidence.Medium ||
              args.Result.Confidence == SpeechRecognitionConfidence.High)
            {
                dictatedTextBuilder.Append(args.Result.Text + " ");

                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    RtfTextHelper.Text = dictatedTextBuilder.ToString();
                });
            }
            else
            {
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    RtfTextHelper.Text = dictatedTextBuilder.ToString();
                });
            }
        }

        private static async void ContinuousRecognitionSession_Completed(
      SpeechContinuousRecognitionSession sender,
      SpeechContinuousRecognitionCompletedEventArgs args)
        {
            if (args.Status != SpeechRecognitionResultStatus.Success)
            {
                if (args.Status == SpeechRecognitionResultStatus.TimeoutExceeded)
                {
                    await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        RtfTextHelper.Text = dictatedTextBuilder.ToString();
                    });
                }
                else
                {
                    await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        RtfTextHelper.Text = dictatedTextBuilder.ToString();
                    });
                }
            }
        }

        private static async void SpeechRecognizer_HypothesisGenerated(
  SpeechRecognizer sender,
  SpeechRecognitionHypothesisGeneratedEventArgs args)
        {
            string hypothesis = args.Hypothesis.Text;
            string textboxContent = dictatedTextBuilder.ToString() + " " + hypothesis + " ...";

            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                RtfTextHelper.Text = textboxContent;
            });
        }
    }
}
