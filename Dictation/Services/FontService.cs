namespace Dictation.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Graphics.Canvas.Text;

    public static class FontService
    {
        public static List<string> Fonts
        {
            get
            {
                return CanvasTextFormat.GetSystemFontFamilies().OrderBy(f => f).ToList();
            }
        }

        public static List<int> Sizes { get; } = new List<int>() { 6, 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
    }
}
