using Clock11.Data;
using System;
using System.Windows;
using System.Windows.Media;

namespace Clock11
{
    /// <summary>
    /// Interaction logic for ClockWindow.xaml
    /// </summary>
    public partial class ClockWindow : Window
    {
        private double defaultLeft;
        private double defaultTop;

        public ClockWindow()
        {
            InitializeComponent();
            RefreshClock(DateTime.Now);
            Loaded += ClockWindow_Loaded;    
        }

        private void ClockWindow_Loaded(object sender, RoutedEventArgs e)
        {
            defaultLeft = Left;
            defaultTop = Top;

            ApplyTheme(Theme.GetCurrentTheme());
        }

        public void RefreshClock(DateTime now)
        {
            TextClockTime.Text = now.ToString("t");
            TextClockDate.Text = now.ToString("d");
        }

        public void BringToFront()
        {
            Topmost = false;
            Topmost = true;
            Activate();
        }

        public void ApplyTheme(Theme theme)
        {
            if (theme == null)
                return;

            Left = defaultLeft - theme.RightMargin;
            Top = defaultTop - theme.TopMargin;

            TextClockTime.FontFamily =
            TextClockDate.FontFamily = new FontFamily(theme.FontFamily);
            TextClockTime.Foreground =
            TextClockDate.Foreground = new SolidColorBrush(theme.ForegroundColor);
            TextClockTime.FontSize =
            TextClockDate.FontSize = theme.FontSize;

            switch (theme.HorizontalAlignment)
            {
                case Theme.HAlignment.Left:
                    {
                        TextClockTime.HorizontalAlignment = HorizontalAlignment.Left;
                        TextClockDate.HorizontalAlignment = HorizontalAlignment.Left;
                    }
                    break;
                case Theme.HAlignment.Center:
                    {
                        TextClockTime.HorizontalAlignment = HorizontalAlignment.Center;
                        TextClockDate.HorizontalAlignment = HorizontalAlignment.Center;
                    }
                    break;
                case Theme.HAlignment.Right:
                    {
                        TextClockDate.HorizontalAlignment = HorizontalAlignment.Right;
                        TextClockTime.HorizontalAlignment = HorizontalAlignment.Right;
                    }
                    break;
            }

            TextClockDate.Margin = new Thickness(0, theme.DateMargin, 0, 0);
        }
    }
}
