using Clock11.Helper;
using System;
using System.ComponentModel;
using System.Windows;

namespace Clock11.Dialogs
{
    /// <summary>
    /// Interaction logic for AboutDialog.xaml
    /// </summary>
    public partial class AboutDialog : Window
    {
        private readonly MainWindow owner;

        public EventHandler OnClosingSettingsDialog;

        public AboutDialog(MainWindow owner)
        {
            InitializeComponent();
            this.owner = owner;

            TextVersion.Text = typeof(AboutDialog).Assembly.GetName().Version.ToString(3);
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            GeneralHelper.OpenUri(e.Uri);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            OnClosingSettingsDialog?.Invoke(this, EventArgs.Empty);
        }
    }
}
