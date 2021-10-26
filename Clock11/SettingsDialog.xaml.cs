using Clock11.Data;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Clock11
{
    /// <summary>
    /// Interaction logic for SettingsDialog.xaml
    /// </summary>
    public partial class SettingsDialog : Window
    {
        public EventHandler OnClosingSettingsDialog;
        private MainWindow owner;
        private Settings oldSettings;
        private bool isInitalized = false;

        public SettingsDialog(MainWindow owner)
        {
            InitializeComponent();
            this.owner = owner;

            // Clone old settings (in case of cancel)
            oldSettings = (Settings)Settings.Instance.Clone();

            // Apply settings
            if (string.IsNullOrEmpty(Settings.Instance.Theme))
            {
                // Custom settings
                RadioCustomTheme.IsChecked = true;
            }
            else
            {
                // Predefined theme
                RadioPredefinedTheme.IsChecked = true;
                CmbThemes.SelectedIndex = (Settings.Instance.Theme == "Windows11" ? 0 : 1);
            }

            isInitalized = true;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            OnClosingSettingsDialog?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateTheme()
        {
            if (!isInitalized)
                return;

            if (RadioPredefinedTheme.IsChecked.Value)
            {
                var item = CmbThemes.SelectedItem as ComboBoxItem;
                Settings.Instance.Theme = item.Tag.ToString();
            }

            owner.RefreshTheme();
        }

        private void RadioPredefinedTheme_Checked(object sender, RoutedEventArgs e)
        {
            UpdateTheme();
        }

        private void RadioCustomTheme_Checked(object sender, RoutedEventArgs e)
        {
            UpdateTheme();
        }

        private void CmbThemes_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            UpdateTheme();
        }

        private void ButtonApply_Click(object sender, RoutedEventArgs e)
        {
            Settings.Instance.Save();
            DialogResult = true;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Settings.Instance = oldSettings;
            owner.RefreshTheme();
            DialogResult = false;
        }
    }
}
