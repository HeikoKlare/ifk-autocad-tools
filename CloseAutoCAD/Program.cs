using System.Diagnostics;
using System.Windows.Forms;

namespace CloseAutoCAD
{
    class Program
    {
        static int Main(string[] args)
        {
            if (Process.GetProcessesByName("acad").Length > 0)
            {
                DialogResult res = MessageBox.Show("AutoCAD läuft gerade.\nSchließen oder abbrechen?", "AutoCAD läuft noch", MessageBoxButtons.OKCancel);
                if (res == DialogResult.Cancel)
                {
                    return -1;
                }
                else
                {
                    foreach (Process p in Process.GetProcessesByName("acad"))
                    {
                        if (!p.CloseMainWindow())
                        {
                            return -1;
                        }
                    }
                }
            }
            return 0;
        }
    }
}
