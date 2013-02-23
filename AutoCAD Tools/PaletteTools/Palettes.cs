using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Windows;
using acadDatabase = Autodesk.AutoCAD.DatabaseServices.Database;

namespace AutoCADTools.Tools
{
    public class Palettes
    {
        private static Palettes instance;
        private PaletteSet annotationPalette;
        private PaletteSet bracingPaniclePalette;

        public PaletteSet BracingPaniclePalette
        {
            get { return bracingPaniclePalette; }
        }

        public PaletteSet AnnotationPalette
        {
            get { return annotationPalette; }
        }

        private Palettes()
        {
            this.annotationPalette = new PaletteSet("Annotation", new Guid("22389EAE-3A22-489F-A443-1290E4B0B8F8"));
            AnnotationsControl annotationControl = new AnnotationsControl();
            
            this.annotationPalette.Add("Palette1", annotationControl);

            this.bracingPaniclePalette = new PaletteSet("Panicle", new Guid("3EC65CE2-F08A-4CCC-82D2-172612301C9B"));
            PanicleControl panicleControl = new PanicleControl();
            BracingControl bracingControl = new BracingControl();
            this.bracingPaniclePalette.Add("Panicle", panicleControl);
            this.bracingPaniclePalette.Add("Bracing", bracingControl);
            panicleControl.ParentPalette = this;
        }


        public static Palettes getInstance()
        {
            if (instance == null)
            {
                Palettes.instance = new Palettes();
            }

            return Palettes.instance;
        }

        public void minimizePaniclePalette()
        {
            this.bracingPaniclePalette.WindowState = Window.State.Minimized;
        }
    }
}
