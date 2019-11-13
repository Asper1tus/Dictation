namespace Dictation.Views.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Dictation.ViewModels.Content;
    using Microsoft.Graphics.Canvas.Text;
    using Windows.UI;
    using Windows.UI.Xaml.Controls;

    public sealed partial class ToolsPage : Page
    {
        private readonly ToolsViewModel viewModel;

        public ToolsPage()
        {
            this.InitializeComponent();
            viewModel = App.Locator.ToolsViewModel;
        }

        public List<string> Fonts
        {
            get
            {
                return CanvasTextFormat.GetSystemFontFamilies().OrderBy(f => f).ToList();
            }
        }

        public List<int> Sizes { get; } = new List<int>() { 6, 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };

    }
}
