using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MilaTools
{
    /// <summary>
    /// Provides methods to set the transparency of a window by its Form instance or by its executable name and window title.
    /// </summary>
    public class WindowTransparency
    {
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_LAYERED = 0x80000;
        private const int LWA_ALPHA = 0x2;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        /// <summary>
        /// Sets the transparency of the main window of the specified executable.
        /// </summary>
        /// <param name="executableName">The process name or executable file name (without .exe or with .exe).</param>
        /// <param name="percent">Transparency percentage (0-100, 100 is fully opaque).</param>
        public void SetTransparency(string executableName, int percent)
        {
            if (string.IsNullOrWhiteSpace(executableName))
                throw new ArgumentNullException(nameof(executableName));
            if (percent < 0 || percent > 100)
                throw new ArgumentOutOfRangeException(nameof(percent), "Percent must be between 0 and 100.");

            string procName = executableName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase)
                ? executableName.Substring(0, executableName.Length - 4)
                : executableName;

            Process[] processes = Process.GetProcessesByName(procName);
            if (processes.Length == 0)
                throw new InvalidOperationException($"No process found with name '{executableName}'.");

            // Use the main window handle of the first found process
            IntPtr hWnd = processes[0].MainWindowHandle;
            if (hWnd == IntPtr.Zero)
                throw new InvalidOperationException("Main window handle not found for the specified process.");

            int exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
            SetWindowLong(hWnd, GWL_EXSTYLE, exStyle | WS_EX_LAYERED);

            byte alpha = (byte)(255 * percent / 100);
            if (!SetLayeredWindowAttributes(hWnd, 0, alpha, LWA_ALPHA))
                throw new InvalidOperationException("Failed to set window transparency.");
        }
    }
}
