using System.Windows.Forms;
using Autodesk.AutoCAD.Runtime;

[assembly: CommandClass(typeof(AutoCAD_Tools.AutoCAD_Tools))]

namespace AutoCAD_Tools
{
    /// <summary>
    /// The class AutoCAD_Tools handles all the commands that are added to AutoCAD by this library.
    /// </summary>
    public class AutoCAD_Tools
    {
        /// <summary>
        /// NeueRispe is a command to create a new Rispe in the drawing. It opens a UserForm to input a position 
        /// number and description and then lets the user draw Rispen by selecting start and end point. Pressing 
        /// RETURN after drawing one Rispe lets the next one begin at the end of the old one.
        /// </summary>
        [CommandMethod("NeueRispe")]
        public void NeueRispe()
        {
            Form UFRispe = new UFRispe();
            UFRispe.ShowDialog();
            UFRispe.Dispose();
        }

        /// <summary>
        /// VerbandErstellen is a command to create a new bracing. It opend a UserForm to input the needed data and
        /// define the extends.
        /// </summary>
        [CommandMethod("VerbandErstellen")]
        public void VerbandErstellen()
        {
            Form UFVerband = new UFVerband();
            UFVerband.ShowDialog();
            UFVerband.Dispose();
        }

        /// <summary>
        /// Zeichnungseigenschaften opens a UserForm to edit the drawing properties for the active document.
        /// These information are used for the textfields of layouts.
        /// </summary>
        [CommandMethod("Zeichnungseigenschaften")]
        public void Zeichnungseigenschaften()
        {
            Form UFZeichnungseigenschaften = new UFZeichnungseigenschaften();
            UFZeichnungseigenschaften.ShowDialog();
            UFZeichnungseigenschaften.Dispose();
        }

        /// <summary>
        /// Verwaltung opens a UserForm that allows to administer the database for projects and employers.
        /// There is the ability to add, remove and sort the databases,
        /// </summary>
        [CommandMethod("Verwaltung")]
        public void Verwaltung()
        {
            Form UFVerwaltung = new UFVerwaltung();
            UFVerwaltung.ShowDialog();
            UFVerwaltung.Dispose();
        }

        /// <summary>
        /// LayoutErstellen opens a UserForm with options and methods to define a new layout. There is the
        /// possibility to create simple layouts or layouts with textfields and borders using the data
        /// of the drawing properties.
        /// </summary>
        [CommandMethod("LayoutErstellen")]
        public void LayoutErstellen()
        {
            Form UFLayoutErstellen = new UFLayoutErstellen();
            UFLayoutErstellen.ShowDialog();
            UFLayoutErstellen.Dispose();
        }

        /// <summary>
        /// ZeichenbereichA4Hochformat allows to insert a vertical A4 layout in meter-units at a user defined
        /// position.
        /// </summary>
        [CommandMethod("ZeichenbereichA4Hochformat")]
        public void ZeichenbereichA4Hochformat()
        {
            Zeichenbereich.ZeichenbereichErstellen(Zeichenbereich.CA4, false);
        }

        /// <summary>
        /// ZeichenbereichA4Querformat allows to insert a horizontal A4 layout in meter-units at a user defined
        /// position.
        /// </summary>
        [CommandMethod("ZeichenbereichA4Querformat")]
        public void ZeichenbereichA4Querformat()
        {
            Zeichenbereich.ZeichenbereichErstellen(Zeichenbereich.CA4, true);
        }

        /// <summary>
        /// ZeichenbereichA3 allows to insert a A3 layout in meter-units at a user defined position.
        /// </summary>
        [CommandMethod("ZeichenbereichA3")]
        public void ZeichenbereichA3()
        {
            Zeichenbereich.ZeichenbereichErstellen(Zeichenbereich.CA3, true);
        }

        /// <summary>
        /// ZeichenbereichCustom allows to manually create a drawing frame. First the input position is defined
        /// and then the size has to be set interactively. Minumum sizes for the drawing frame are
        /// automatically applied.
        /// </summary>
        [CommandMethod("ZeichenbereichCustom")]
        public static void ZeichenbereichCustom()
        {
            Zeichenbereich.ZeichenbereichErstellen(Zeichenbereich.CAX, true);
        }

        /// <summary>
        /// ZeichenbereichAuto tries to automatically create a drawing frame by analyzing the object currently
        /// in the docuemnt's database and calculating their extends.
        /// </summary>
        [CommandMethod("ZeichenbereichAuto")]
        public static void ZeichenbereichAuto()
        {
            Zeichenbereich.ZeichenbereichAuto();
        }

    }
}