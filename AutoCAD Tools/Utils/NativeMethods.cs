using System.Runtime.InteropServices;

namespace AutoCADTools.Utils
{
    public static class NativeMethods
    {
        /// <summary>
        /// This method provided by the COM-API allows to set the window with the given handle active
        /// </summary>
        /// <param name="hWnd">the handle of the window to set active</param>
        /// <returns>something</returns>
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int SetForegroundWindow(int hWnd);
    }
}
