using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace MilaTools
{
    /// <summary>
    /// Provides methods to get operating system version and name information.
    /// </summary>
    public class OSInfo
    {
        /// <summary>
        /// Gets the OS major version as a string.
        /// </summary>
        public static string GetOSMajorVersion()
        {
            return Environment.OSVersion.Version.Major.ToString();
        }

        /// <summary>
        /// Gets the OS minor version as a string.
        /// </summary>
        public static string GetOSMinorVersion()
        {
            return Environment.OSVersion.Version.Minor.ToString();
        }

        /// <summary>
        /// Gets the OS build version as a string.
        /// </summary>
        public static string GetOSBuildVersion()
        {
            return Environment.OSVersion.Version.Build.ToString();
        }

        /// <summary>
        /// Gets the OS version as a string in the format "Major.Minor.Build.Revision".
        /// </summary>
        public static string GetOSVersion()
        {
            var v = Environment.OSVersion.Version;
            return $"{v.Major}.{v.Minor}.{v.Build}.{v.Revision}";
        }

        /// <summary>
        /// Gets the OS platform name (e.g., "Windows").
        /// </summary>
        public static string GetOSPlatformName()
        {
            return Environment.OSVersion.Platform.ToString();
        }

        /// <summary>
        /// Gets the OS full name (e.g., "Microsoft Windows 10 Pro").
        /// </summary>
        public static string GetOSFullName()
        {
            return GetCaptionFromWMI() ?? Environment.OSVersion.VersionString;
        }

        /// <summary>
        /// Gets the OS short name (e.g., "Windows 10").
        /// </summary>
        public static string GetOSShortName()
        {
            string fullName = GetOSFullName();
            if (fullName.StartsWith("Microsoft "))
                fullName = fullName.Substring(10);
            int idx = fullName.IndexOf(" Version");
            if (idx > 0)
                fullName = fullName.Substring(0, idx);
            return fullName.Trim();
        }

        /// <summary>
        /// Gets the OS friendly version, e.g. "Windows 10 22H2" or "Windows 11 23H2".
        /// </summary>
        public static string GetOSFriendlyVersion()
        {
            string productName = GetRegistryValue(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName");
            string displayVersion = GetRegistryValue(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "DisplayVersion");
            string releaseId = GetRegistryValue(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId");

            // Use DisplayVersion first (such as 22H2/23H2) otherwise use ReleaseId (such as 2004/21H1)
            string version = !string.IsNullOrEmpty(displayVersion) ? displayVersion : releaseId;

            if (!string.IsNullOrEmpty(productName) && !string.IsNullOrEmpty(version))
                return $"{productName} {version}";
            if (!string.IsNullOrEmpty(productName))
                return productName;
            return GetOSShortName();
        }

        // Helper: Get OS caption from WMI
        private static string GetCaptionFromWMI()
        {
            try
            {
                Type type = Type.GetTypeFromProgID("WbemScripting.SWbemLocator");
                if (type == null) return null;
                dynamic locator = Activator.CreateInstance(type);
                dynamic service = locator.ConnectServer(".", "root\\cimv2");
                var results = service.ExecQuery("SELECT Caption FROM Win32_OperatingSystem");
                foreach (var os in results)
                {
                    return os.Caption;
                }
            }
            catch { }
            return null;
        }

        // Helper: Get registry value as string
        private static string GetRegistryValue(string subKey, string valueName)
        {
            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey(subKey))
                {
                    if (key != null)
                    {
                        object value = key.GetValue(valueName);
                        if (value != null)
                            return value.ToString();
                    }
                }
            }
            catch { }
            return null;
        }
    }
}
