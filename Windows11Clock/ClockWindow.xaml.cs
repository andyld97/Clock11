using System;
using System.Windows;

namespace Windows11Clock
{
    /// <summary>
    /// Interaction logic for ClockWindow.xaml
    /// </summary>
    public partial class ClockWindow : Window
    {
        public ClockWindow()
        {
            InitializeComponent();
            RefreshClock(DateTime.Now);
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
    }
}
