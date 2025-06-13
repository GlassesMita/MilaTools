using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MilaTools
{
    /// <summary>
    /// Provides an interface to control the Windows taskbar progress bar.
    /// </summary>
    public class TaskbarProgress
    {
        private static readonly Guid CLSID_TaskbarList = new Guid("56FDF344-FD6D-11d0-958A-006097C9A090");
        private static readonly Guid IID_ITaskbarList3 = new Guid("EA1AFB91-9E28-4B86-90E9-9E9F8A5EEA84");

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("EA1AFB91-9E28-4B86-90E9-9E9F8A5EEA84")]
        private interface ITaskbarList3
        {
            // ITaskbarList
            void HrInit();
            void AddTab(IntPtr hwnd);
            void DeleteTab(IntPtr hwnd);
            void ActivateTab(IntPtr hwnd);
            void SetActiveAlt(IntPtr hwnd);

            // ITaskbarList2
            void MarkFullscreenWindow(IntPtr hwnd, [MarshalAs(UnmanagedType.Bool)] bool fFullscreen);

            // ITaskbarList3
            void SetProgressValue(IntPtr hwnd, ulong ullCompleted, ulong ullTotal);
            void SetProgressState(IntPtr hwnd, TaskbarProgressState state);
        }

        [ComImport]
        [Guid("56FDF344-FD6D-11d0-958A-006097C9A090")]
        [ClassInterface(ClassInterfaceType.None)]
        private class CTaskbarList { }
        private readonly ITaskbarList3 _taskbarList;
        private readonly IntPtr _windowHandle;

        /// <summary>
        /// Initializes a new instance for the specified window handle.
        /// </summary>
        /// <param name="windowHandle">The handle of the window to show progress for.</param>
        public TaskbarProgress(IntPtr windowHandle)
        {
            _windowHandle = windowHandle;
            _taskbarList = (ITaskbarList3)new CTaskbarList();
            _taskbarList.HrInit();
        }

        /// <summary>
        /// Sets the progress value on the taskbar.
        /// </summary>
        /// <param name="completed">The completed value.</param>
        /// <param name="total">The total value.</param>
        public void SetProgressValue(ulong completed, ulong total)
        {
            _taskbarList.SetProgressValue(_windowHandle, completed, total);
        }

        /// <summary>
        /// Sets the progress state on the taskbar.
        /// </summary>
        /// <param name="state">The progress state.</param>
        public void SetProgressState(TaskbarProgressState state)
        {
            _taskbarList.SetProgressState(_windowHandle, state);
        }
    }
}
