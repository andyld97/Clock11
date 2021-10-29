using System;
using System.Threading;
using System.Windows;

namespace Clock11
{
    public class SingleInstanceManager
    {
        private static Mutex AppMutex = new Mutex(false, Consts.AppMutexGuid);

        // Entry Point: https://stackoverflow.com/a/6156665/6237448
        // Mutex:       https://stackoverflow.com/a/19165/6237448
        [STAThread]
        public static void Main(string[] args)
        {
            // Check if mutex is aquired
            if (!AppMutex.WaitOne(TimeSpan.FromSeconds(1), false))
            {
                MessageBox.Show(Resources.strSingleInstance_Message_AlreadyRunning, Resources.strSingleInstance_Message_AlreadyRunning_Title, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Run app
            var app = new App();
            app.InitializeComponent();
            app.Run();

            // Release app mutex
            AppMutex.ReleaseMutex();
        }
    }
}