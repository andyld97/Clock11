using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

namespace Clock11
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer clockTimer = new DispatcherTimer();
        private readonly List<ClockWindow> clockWindows = new List<ClockWindow>();
        
        private const int LEFT_MARGIN = 60;
        private const int TOP_MARGIN = 10;
        private const int CLOCK_TIMER_REFRESH_INTERVAL = 100; // [ms.]

        public MainWindow()
        {
            InitializeComponent();
            InitalizeClockWindows();
   
            // Initalize clock timer
            clockTimer.Interval = TimeSpan.FromMilliseconds(CLOCK_TIMER_REFRESH_INTERVAL);
            clockTimer.Tick += ClockTimer_Tick;
            clockTimer.Start();

            // Register hotkey
            GlobalHotKey.RegisterHotKey("Ctrl + U", () => clockWindows.ForEach(w => w.BringToFront()));
        }

        private void InitalizeClockWindows()
        {
            clockWindows.Clear();

            foreach (var screen in WpfScreen.AllScreens())
            {
                if (screen.IsPrimary)
                    continue;

                var area = screen.WorkingArea;
                ClockWindow clockWindow = new ClockWindow
                {
                    Left = area.Left + area.Width - LEFT_MARGIN,
                    Top = area.Top + area.Height + TOP_MARGIN,
                };

                clockWindows.Add(clockWindow);
                clockWindow.Show();
            }
        }

        private void ClockTimer_Tick(object? sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            clockWindows.ForEach(w => w.RefreshClock(now));
        }
    }
}