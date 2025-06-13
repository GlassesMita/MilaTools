using System;
using System.IO;

namespace MilaTools
{
    /// <summary>
    /// Provides methods to convert Linux-style paths to Windows-style paths.
    /// </summary>
    public class ConvertLinuxPathToWindows
    {
        /// <summary>
        /// Converts a Linux path to a Windows path.
        /// </summary>
        /// <param name="linuxPath">The Linux-style path (e.g., ~/Desktop/file.txt or /tmp/test).</param>
        /// <returns>Windows-style path (e.g., C:\Users\Username\Desktop\file.txt).</returns>
        public string Convert(string linuxPath)
        {
            if (string.IsNullOrWhiteSpace(linuxPath))
                throw new ArgumentException("Path must not be null or empty.");

            string windowsPath;

            // Handle home directory (~ or ~/...)
            if (linuxPath.StartsWith("~"))
            {
                string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                if (linuxPath == "~")
                {
                    windowsPath = userProfile;
                }
                else if (linuxPath.StartsWith("~/"))
                {
                    windowsPath = Path.Combine(userProfile, linuxPath.Substring(2).Replace('/', Path.DirectorySeparatorChar));
                }
                else
                {
                    // e.g. ~username/ not supported, fallback
                    throw new NotSupportedException("Only current user's home (~) is supported.");
                }
            }
            // Handle absolute path (/...)
            else if (linuxPath.StartsWith("/"))
            {
                // Map / to system drive root (e.g., C:\)
                string systemDrive = Path.GetPathRoot(Environment.SystemDirectory);
                windowsPath = Path.Combine(systemDrive, linuxPath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
            }
            else
            {
                // Relative path, just replace slashes
                windowsPath = linuxPath.Replace('/', Path.DirectorySeparatorChar);
            }

            return windowsPath;
        }
    }
}
