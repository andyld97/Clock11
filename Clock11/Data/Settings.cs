using System;
using System.Windows.Media;

namespace Clock11.Data
{
    public class Settings : ICloneable
    {
        public static readonly string SettingsPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Clock11", "Settings.xml");

        public static Settings Instance = Settings.Load();

        public string Theme { get; set; } = "Windows11";

        public Theme CustomTheme { get; set; } = new Theme()
        {
            FontFamily = "Segoe",
            ForegroundColor = Colors.White,
            FontSize = 11,
            HorizontalAlignment = Data.Theme.HAlignment.Right
        };

        public static Settings Load()
        {
            try
            {
                if (System.IO.File.Exists(SettingsPath))
                {
                    var result = Serialization.Serialization.Read<Settings>(SettingsPath, Serialization.Serialization.Mode.Normal);
                    if (result != null)
                        return result;
                }
            }
            catch
            { }

            return new Settings();
        }

        public object Clone()
        {
            return new Settings()
            {
                Theme = this.Theme,
                CustomTheme = (Theme)this.CustomTheme.Clone(),
            };
        }

        public void Save()
        {
            string directoryName = System.IO.Path.GetDirectoryName(SettingsPath);
            if (!System.IO.Directory.Exists(directoryName))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(directoryName);
                }
                catch
                {
                    // ignore
                }
            }

            try
            {
                Serialization.Serialization.Save(SettingsPath, Instance, Serialization.Serialization.Mode.Normal);
            }
            catch
            {
                // ignroe
            }
        }
    }
}