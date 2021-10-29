using Clock11.Data;
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
        private SettingsDialog settingsDialog = null;

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

            // Register hotkeys
            GlobalHotKey.RegisterHotKey("Ctrl + U", () => ClockWindow.BringClockWindowsToFront(clockWindows));
            GlobalHotKey.RegisterHotKey("Ctrl + H", () => clockWindows.ForEach(w => w.Hide()));

            // Create a context menu for the notifyicon
            var menu = new System.Windows.Forms.ContextMenuStrip();

            var closeButton = new System.Windows.Forms.ToolStripMenuItem() { Text = Clock11.Resources.strExit };
            closeButton.Click += CloseButton_Click;

            var settingsButton = new System.Windows.Forms.ToolStripMenuItem() { Text = Clock11.Resources.strSettings };
            settingsButton.Click += SettingsButton_Click;

            var aboutButton = new System.Windows.Forms.ToolStripMenuItem() { Text = Clock11.Resources.strAbout};
            aboutButton.Click += AboutButton_Click;

            menu.Items.AddRange(new System.Windows.Forms.ToolStripMenuItem[] { aboutButton, settingsButton, closeButton });

            // Initalize notify icon
            System.Windows.Forms.NotifyIcon notifyIcon = new System.Windows.Forms.NotifyIcon
            {
                Icon = Clock11.Resources.clock,
                Visible = true,
                BalloonTipTitle = Clock11.Resources.strAppTitle,
                Text = Clock11.Resources.strAppTitle,
                ContextMenuStrip = menu,
            };
        }

        #region Tray Icon Buttons

        private void AboutButton_Click(object? sender, EventArgs e)
        {
 
        }

        private void SettingsButton_Click(object? sender, EventArgs e)
        {
            if (settingsDialog == null)
            {
                settingsDialog = new SettingsDialog(this);
                settingsDialog.OnClosingSettingsDialog += delegate (object? sender, EventArgs e) { settingsDialog = null; };
                settingsDialog.ShowDialog();
            }
            else
            {
                settingsDialog.Activate();
                settingsDialog.Show();
            }
        }

        private void CloseButton_Click(object? sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        #endregion

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

        public void RefreshTheme()
        {
            var theme = Theme.GetCurrentTheme();
            clockWindows.ForEach(w => w.ApplyTheme(theme));
            ClockWindow.BringClockWindowsToFront(clockWindows);
        }

        private void ClockTimer_Tick(object? sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            clockWindows.ForEach(w => w.RefreshClock(now));
        }
    }
}