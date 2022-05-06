using Autodesk.AutoCAD.ApplicationServices;
using System;
using Forms = System.Windows.Forms;

namespace AutoCADTools.Utils
{
    class AutocadMainWindow : Forms.IWin32Window
    {
        public static AutocadMainWindow Instance { get; } = new AutocadMainWindow();

        private AutocadMainWindow() { }

        public IntPtr Handle
        {
            get { return Application.MainWindow.Handle; }
        }
    }

}
