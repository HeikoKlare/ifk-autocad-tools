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
        public LayoutPlain(Paperformat paperformat)
            : base(paperformat)
        {

        }

        protected override bool DrawLayoutAdditions(Size margin, BlockTableRecord layoutRecord)
        {
            return true;
        }
    }
}
