using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCADTools.PrintLayout
{
    public class LayoutPlain : LayoutCreation
    {
        public LayoutPlain(string name, Paperformat paperformat, Point extractLowerRightPoint, PrinterPaperformat printerformat, PaperOrientation orientation, double unit, double scale)
            : base(name, paperformat, extractLowerRightPoint, printerformat, orientation, unit, scale)
        {

        }
        protected override bool DrawLayoutAdditions(Size margin, BlockTableRecord layoutRecord)
        {
            return true;
        }
    }
}
