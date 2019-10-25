using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.SpeechRecognition;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace Dictation
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private SpeechRecognizer speechRecognizer;
        private CoreDispatcher dispatcher;
        private StringBuilder dictatedTextBuilder;
        private bool isListening = false;
        public MainPage()
        {
            this.InitializeComponent();
            isListening = true;
            dictatedTextBuilder = new StringBuilder();
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
                    await speechRecognizer.ContinuousRecognitionSession.CancelAsync();
                }
            }
            isListening = !isListening;
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
            this.speechRecognizer = new SpeechRecognizer();
            SpeechRecognitionCompilationResult result =
                await speechRecognizer.CompileConstraintsAsync();
            speechRecognizer.ContinuousRecognitionSession.ResultGenerated +=
        ContinuousRecognitionSession_ResultGenerated;
            speechRecognizer.ContinuousRecognitionSession.Completed +=
      ContinuousRecognitionSession_Completed;
            speechRecognizer.HypothesisGenerated += 
                SpeechRecognizer_HypothesisGenerated;


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
                editor.Document.SetText(Windows.UI.Text.TextSetOptions.None, dictatedTextBuilder.ToString());
            });
        }
    }


}
