using Clock11.Data;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImportAttribute("user32.dll")]
        private static extern int SetForegroundWindow(int hWnd);

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
            // Activate the clock
            Show();
            Activate();
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

        public static void BringClockWindowsToFront(List<ClockWindow> windows)
        {
            // Get the foreground window
            var oldWindowHandle = GetForegroundWindow();

            // Bring all windows to the front
            windows.ForEach(w => w.BringToFront());

            // Restore old forgeground window (otherwise you will loose the focus of your current foreground window!)
            SetForegroundWindow(oldWindowHandle.ToInt32());
        }
    }
}
