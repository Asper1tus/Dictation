namespace Dictation.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Dictation.Helpers;
    using Windows.Globalization;
    using Windows.Media.SpeechRecognition;
    using Windows.UI.Core;

    internal static class RecognizerService
    {
        private static StringBuilder dictatedTextBuilder;
        private static SpeechRecognizer speechRecognizer;
        private static CoreDispatcher dispatcher;

        private static string richText;
        private static string recognizedText;

        public static void InitializeRecognizerService()
        {
            dictatedTextBuilder = new StringBuilder();
            dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
            Language language = GetLanguageByNativeName(App.RecognitionLanguage);
            InitializeSpeechRecognizer(language);
        }

        public static List<string> GetSupportedLanguagesNativeName()
        {
            var languages = from lang in SpeechRecognizer.SupportedTopicLanguages
                            select lang.NativeName;
            return languages.ToList();
        }

        public static void SetRecognitionLanguage(string languageName)
        {
            var language = GetLanguageByNativeName(languageName);
            InitializeSpeechRecognizer(language);
        }

        public static async void Listening(bool isListening)
        {
            if (isListening)
            {
                if (speechRecognizer.State == SpeechRecognizerState.Idle)
                {
                    await speechRecognizer.ContinuousRecognitionSession.StartAsync();
                    dictatedTextBuilder.Append(RtfTextHelper.Text);
                    richText = RtfTextHelper.RichText;
                }
            }
            else
            {
                if (speechRecognizer.State == SpeechRecognizerState.Idle)
                {
                    await speechRecognizer.ContinuousRecognitionSession.CancelAsync();
                }

                dictatedTextBuilder.Clear();
                RtfTextHelper.RichText = richText;
                RtfTextHelper.AddRtf(recognizedText);
                recognizedText = string.Empty;
            }
        }

        private static Language GetLanguageByNativeName(string name)
        {
            var language = SpeechRecognizer.SupportedTopicLanguages.First((c) => c.NativeName.Equals(name));
            return DefaultSettings.Language;
        }

        private static async void InitializeSpeechRecognizer(Language language)
        {
            speechRecognizer = new SpeechRecognizer(language);
            var grammar = new SpeechRecognitionTopicConstraint(SpeechRecognitionScenario.Dictation, "Dictation");
            speechRecognizer.Constraints.Add(grammar);

            await speechRecognizer.CompileConstraintsAsync();
            SpeechRecognitionCompilationResult result =
                await speechRecognizer.CompileConstraintsAsync();
            speechRecognizer.ContinuousRecognitionSession.ResultGenerated +=
        ContinuousRecognitionSession_ResultGenerated;
            speechRecognizer.ContinuousRecognitionSession.Completed +=
      ContinuousRecognitionSession_Completed;
            speechRecognizer.HypothesisGenerated +=
                SpeechRecognizer_HypothesisGenerated;
        }

        private static async void ContinuousRecognitionSession_ResultGenerated(
      SpeechContinuousRecognitionSession sender,
      SpeechContinuousRecognitionResultGeneratedEventArgs args)
        {
            if (args.Result.Confidence == SpeechRecognitionConfidence.Medium ||
              args.Result.Confidence == SpeechRecognitionConfidence.High)
            {
                string resultGeneratedText = args.Result.Text + " ";
                dictatedTextBuilder.Append(resultGeneratedText);
                recognizedText += resultGeneratedText;
                resultGeneratedText = string.Empty;

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
