using Microsoft.Win32;
using System;
using System.Globalization;
using System.Management;
using System.Security.Principal;

namespace Clock11.Helper
{
    /// <summary>
    /// https://engy.us/blog/2018/10/20/dark-theme-in-wpf/
    /// </summary>
    public static class ThemeHelper
    {
		private const string RegistryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";

		private const string RegistryValueName = "AppsUseLightTheme";

		public enum WindowsTheme
		{
			Light,
			Dark
		}

		public static ManagementEventWatcher RegisterThemeWatcher()
		{
			var currentUser = WindowsIdentity.GetCurrent();
			string query = string.Format(
				CultureInfo.InvariantCulture,
				@"SELECT * FROM RegistryValueChangeEvent WHERE Hive = 'HKEY_USERS' AND KeyPath = '{0}\\{1}' AND ValueName = '{2}'",
				currentUser.User.Value,
				RegistryKeyPath.Replace(@"\", @"\\"),
				RegistryValueName);

			try
			{
				var watcher = new ManagementEventWatcher(query);

				// Start listening for events
				watcher.Start();

				return watcher;
			}
			catch (Exception)
			{
				// This can fail on Windows 7
			}

			return null;
		}

		public static WindowsTheme GetWindowsTheme()
		{
			using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath))
			{
				object registryValueObject = key?.GetValue(RegistryValueName);
				if (registryValueObject == null)
					return WindowsTheme.Light;

				int registryValue = (int)registryValueObject;
				return registryValue > 0 ? WindowsTheme.Light : WindowsTheme.Dark;
			}
		}
	}
}
