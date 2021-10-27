using System;
using System.Windows.Media;
using System.Xml.Serialization;

namespace Clock11.Data
{
    public class Theme : ICloneable
    {
        public double FontSize { get; set; }

        public string FontFamily { get; set; }

        [XmlIgnore]
        public Color ForegroundColor { get; set; }

        public string ForegroundColorXML
        {
            get => ForegroundColor.ToString();
            set => ForegroundColor = (Color)ColorConverter.ConvertFromString(value);
        }

        public int RightMargin { get; set; }

        public int TopMargin { get; set; }

        public int DateMargin { get; set; }

        public HAlignment HorizontalAlignment { get; set; } 

        public enum HAlignment
        {
            Left,
            Center,
            Right,
        }

        public static Theme GetCurrentTheme()
        {
            if (string.IsNullOrEmpty(Settings.Instance.Theme))
                return Settings.Instance.CustomTheme;

            if (Settings.Instance.Theme == Consts.Windows11Theme)
                return Themes.Windows11Theme;
            else if (Settings.Instance.Theme == Consts.Windows10Theme)
                return Themes.Windows10Theme;

            return Themes.Windows11Theme; // fallback/default
        }

        public object Clone()
        {
            return new Theme()
            {
                DateMargin = DateMargin,
                FontFamily = FontFamily,
                FontSize = FontSize,
                ForegroundColor = ForegroundColor,
                HorizontalAlignment = HorizontalAlignment,
                RightMargin = RightMargin,
                TopMargin = TopMargin,
            };
        }
    }
}
