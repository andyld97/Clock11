using System.Windows.Media;

namespace Clock11.Data
{
    public static class Themes
    {
        public static Theme Windows11Theme { get; set; } = new Theme()
        {
            DateMargin = 3,
            FontFamily = "Segoe",
            FontSize = 11,
            ForegroundColor = Colors.White,
            HorizontalAlignment = Theme.HAlignment.Right,
            IsDefaultTheme = true,
        };

        public static Theme Windows10Theme { get; set; } = new Theme()
        {
            DateMargin = 3,
            FontFamily = "Segoe UI",
            FontSize = 13,
            ForegroundColor = Colors.White,
            RightMargin = 8,
            TopMargin = 5,
            HorizontalAlignment = Theme.HAlignment.Center,
            IsDefaultTheme = true
        };
    }
}
