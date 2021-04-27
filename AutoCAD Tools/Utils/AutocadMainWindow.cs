using System;
using Forms = System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;

namespace AutoCADTools.Utils
{
    class AutocadMainWindow : Forms.IWin32Window
    {
        private static AutocadMainWindow instance = new AutocadMainWindow();

        public static AutocadMainWindow Instance
        {
            get { return instance; }
        }

        private AutocadMainWindow() { }

        public IntPtr Handle
        {
            get { return Application.MainWindow.Handle; }
        }
    }

}
