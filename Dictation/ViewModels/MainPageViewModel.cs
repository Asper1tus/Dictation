namespace Dictation.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Dictate.Helpers;
    using Dictation.Views.Content;
    using Windows.Media.SpeechRecognition;
    using Windows.UI.Core;
    using Windows.UI.Xaml.Controls;

    public class MainPageViewModel : Observable
    {
        private SpeechRecognizer speechRecognizer;
        private CoreDispatcher dispatcher;
        private StringBuilder dictatedTextBuilder;
        private Page currentPage;
        private bool isPanelVisible;

        private readonly List<(string tag, Page page)> pages = new List<(string tag, Page page)>
{
    ("findReplace",  new FindReplace()),
    ("share", new Share()),
    ("tools", new Tools()),
    ("vocabulary", new Vocabulary()),
};

        public MainPageViewModel()
        {
            IsListening = false;
            dictatedTextBuilder = new StringBuilder();
            InitializeRecognition();
            IsPanelVisible = false;
        }

        public bool IsListening { get; set; }

        public Page CurrentPage
        {
            get { return currentPage; }
            set { Set(ref this.currentPage, value); }
        }

        public bool IsPanelVisible
        {
            get { return isPanelVisible; }
            set { Set(ref this.isPanelVisible, value); }
        }

        public string EditorText { get; set; }

        public void DisplayFindReplace()
        {
            ChoosePage("findReplace");
        }

        public void DisplayShare()
        {
            ChoosePage("share");
        }

        public void DisplayTools()
        {
            ChoosePage("tools");
        }

        public void DisplayVocabulary()
        {
            ChoosePage("vocabulary");
        }

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

        private void ChoosePage(string tag)
        {
            var item = pages.FirstOrDefault(p => p.tag.Equals(tag));
            var page = item.page;
            if (CurrentPage == page)
            {
                IsPanelVisible = false;
                CurrentPage = null;
            }
            else
            {

                CurrentPage = page;
                IsPanelVisible = true;
            }
        }
    }
}
