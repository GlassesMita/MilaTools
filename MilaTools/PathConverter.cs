using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MilaTools
{
    public static class PathConverter
    {
        /// <summary>
        /// Converts a Linux path to a Windows path.
        /// </summary>
        /// <param name="linuxPath">The Linux-style path (e.g., ~/Desktop/file.txt or /tmp/test).</param>
        /// <returns>Windows-style path (e.g., C:\Users\Username\Desktop\file.txt).</returns>
        public static string LinuxToWindows(string linuxPath)
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

        /// <summary>
        /// Converts a Windows path to a Linux path.
        /// </summary>
        /// <param name="windowsPath">The Windows-style path (e.g., C:\Users\Username\Desktop\file.txt).</param>
        /// <returns>Linux-style path (e.g., /home/username/Desktop/file.txt).</returns>
        public static string WindowsToLinux(string windowsPath)
        {
            if (string.IsNullOrWhiteSpace(windowsPath))
                throw new ArgumentException("Path must not be null or empty.");

            string linuxPath = windowsPath.Replace('\\', '/');

            // Handle user profile
            string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile).Replace('\\', '/');
            if (linuxPath.StartsWith(userProfile, StringComparison.OrdinalIgnoreCase))
            {
                linuxPath = "~" + linuxPath.Substring(userProfile.Length);
            }
            // Handle drive letter (e.g., C:/)
            else if (linuxPath.Length > 2 && char.IsLetter(linuxPath[0]) && linuxPath[1] == ':' && linuxPath[2] == '/')
            {
                // Convert C:/path to /c/path
                linuxPath = "/" + char.ToLower(linuxPath[0]) + linuxPath.Substring(2);
            }

            return linuxPath;
        }

        /// <summary>
        /// Converts a Windows path to a UNC path.
        /// </summary>
        /// <param name="windowsPath">The Windows-style path (e.g., C:\Users\Username\Desktop\file.txt).</param>
        /// <returns>UNC path (e.g., \\localhost\C$\Users\Username\Desktop\file.txt).</returns>
        public static string WindowsToUNC(string windowsPath)
        {
            if (string.IsNullOrWhiteSpace(windowsPath))
                throw new ArgumentException("Path must not be null or empty.");

            string fullPath = Path.GetFullPath(windowsPath);
            string root = Path.GetPathRoot(fullPath)?.TrimEnd('\\');
            if (string.IsNullOrEmpty(root) || root.Length < 2)
                throw new ArgumentException("Invalid Windows path.");

            // Extract drive letter
            string driveLetter = root.Substring(0, 1).ToUpper();
            string unc = @"\\localhost\" + driveLetter + "$" + fullPath.Substring(2).Replace(Path.DirectorySeparatorChar, '\\');
            return unc;
        }

        /// <summary>
        /// Converts a Linux path to a UNC path.
        /// </summary>
        public static string LinuxToUNC(string linuxPath)
        {
            string winPath = LinuxToWindows(linuxPath);
            return WindowsToUNC(winPath);
        }

        /// <summary>
        /// Converts a UNC path to a Windows path.
        /// </summary>
        public static string UNCToWindows(string uncPath)
        {
            if (string.IsNullOrWhiteSpace(uncPath))
                throw new ArgumentException("Path must not be null or empty.");

            // Example: \\localhost\C$\Users\Username\Desktop\file.txt
            if (!uncPath.StartsWith(@"\\", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Not a valid UNC path.");

            string path = uncPath.TrimStart('\\');
            string[] parts = path.Split(new[] { '\\' }, 3);
            if (parts.Length < 3)
                throw new ArgumentException("Not a valid UNC path.");

            // parts[1] like "C$"
            string driveLetter = parts[1].TrimEnd('$');
            string rest = parts[2];
            string winPath = driveLetter + @":\" + rest.Replace('/', '\\');
            return winPath;
        }

        /// <summary>
        /// Converts a UNC path to a Linux path.
        /// </summary>
        public static string UNCToLinux(string uncPath)
        {
            string winPath = UNCToWindows(uncPath);
            return WindowsToLinux(winPath);
        }

        /// <summary>
        /// Converts a Unix path to a Windows path.
        /// </summary>
        public static string UnixToWindows(string unixPath)
        {
            // Unix path's process is the same as Linux path
            return LinuxToWindows(unixPath);
        }

        /// <summary>
        /// Converts a Windows path to a Unix path.
        /// </summary>
        public static string WindowsToUnix(string windowsPath)
        {
            // Unix path's process is the same as Linux path
            return WindowsToLinux(windowsPath);
        }

        /// <summary>
        /// Converts a Unix path to a UNC path.
        /// </summary>
        public static string UnixToUNC(string unixPath)
        {
            return LinuxToUNC(unixPath);
        }

        /// <summary>
        /// Converts a UNC path to a Unix path.
        /// </summary>
        public static string UNCToUnix(string uncPath)
        {
            return UNCToLinux(uncPath);
        }
    }
}
