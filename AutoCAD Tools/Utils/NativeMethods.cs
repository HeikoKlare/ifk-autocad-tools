using System.Runtime.InteropServices;

namespace AutoCADTools.Utils
{
    /// <summary>
    /// Collection a extern native method definition.
    /// </summary>
    public static class NativeMethods
    {
        /// <summary>
        /// This method provided by the COM-API allows to set the window with the given handle active
        /// </summary>
        /// <param name="hWnd">the handle of the window to set active</param>
        public static void SetWindowsToForeground(int hWnd)
        {
            SetForegroundWindow(hWnd);
        }

        /// <summary>
        /// This method provided by the COM-API allows to set the window with the given handle active
        /// </summary>
        /// <param name="hWnd">the handle of the window to set active</param>
        /// <returns>something</returns>
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        private static extern int SetForegroundWindow(int hWnd);
    }
}
