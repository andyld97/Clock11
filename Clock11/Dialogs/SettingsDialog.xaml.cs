using Clock11.Data;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using static Clock11.Data.Theme;

namespace Clock11.Dialogs
{
    /// <summary>
    /// Interaction logic for SettingsDialog.xaml
    /// </summary>
    public partial class SettingsDialog : Window
    {
        public EventHandler OnClosingSettingsDialog;
        private readonly MainWindow owner;
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
                CmbThemes.SelectedIndex = (Settings.Instance.Theme == Consts.Windows11Theme ? 0 : 1);
            }

            // Apply custom theme settings anyway
            NumFontSize.Value = (int)Settings.Instance.CustomTheme.FontSize;
            CmbFontFamily.Select(new System.Windows.Media.FontFamily(Settings.Instance.CustomTheme.FontFamily));
            CmbColorPicker.SelectedColor = Settings.Instance.CustomTheme.ForegroundColor;
            NumRightMargin.Value = Settings.Instance.CustomTheme.RightMargin;
            NumTopMargin.Value = Settings.Instance.CustomTheme.TopMargin;
            NumDateTimeMargin.Value = Settings.Instance.CustomTheme.DateMargin;
            CmbAlignment.SelectedIndex = (int)Settings.Instance.CustomTheme.HorizontalAlignment;

            ChkShowSeconds.IsChecked = Settings.Instance.ShowSeconds;

            isInitalized = true;
        }

        private void UpdateTheme()
        {
            if (!isInitalized)
                return;

            Settings.Instance.ShowSeconds = ChkShowSeconds.IsChecked.Value;

            if (RadioPredefinedTheme.IsChecked.Value)
            {
                var item = CmbThemes.SelectedItem as ComboBoxItem;
                Settings.Instance.Theme = item.Tag.ToString();
            }
            else
            {
                Settings.Instance.CustomTheme.FontSize = NumFontSize.Value;
                Settings.Instance.CustomTheme.FontFamily = CmbFontFamily.SelectedFontFamily.ToString();
                Settings.Instance.CustomTheme.ForegroundColor = CmbColorPicker.SelectedColor;
                Settings.Instance.CustomTheme.RightMargin = NumRightMargin.Value;
                Settings.Instance.CustomTheme.TopMargin = NumTopMargin.Value;
                Settings.Instance.CustomTheme.DateMargin = NumDateTimeMargin.Value;
                Settings.Instance.CustomTheme.HorizontalAlignment = (HAlignment)CmbAlignment.SelectedIndex;

                Settings.Instance.Theme = string.Empty;
            }

            owner.RefreshTheme();
            Activate();
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

        private void NumFontSize_OnChanged(int oldValue, int newValue)
        {
            UpdateTheme();
        }

        private void CmbFontFamily_OnFontChanged(System.Windows.Media.FontFamily family)
        {
            UpdateTheme();
        }

        private void CmbColorPicker_ColorChanged(System.Windows.Media.Color c)
        {
            UpdateTheme();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            isInitalized = false;
            OnClosingSettingsDialog?.Invoke(this, EventArgs.Empty);
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Settings.Instance = oldSettings;
            owner.RefreshTheme();
            DialogResult = false;
        }

        private void ButtonApply_Click(object sender, RoutedEventArgs e)
        {
            Settings.Instance.Save();
            DialogResult = true;
        }

        private void ChkShowSeconds_Checked(object sender, RoutedEventArgs e)
        {
            UpdateTheme();
        }
    }
}
