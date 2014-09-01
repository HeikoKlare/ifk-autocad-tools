using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// Represents a printer with its name and paperformats.
    /// </summary>
    public class Printer
    {
        private String name;

        /// <summary>
        /// Gets the name of the printer.
        /// </summary>
        /// <value>
        /// The name of the printer.
        /// </value>
        public String Name
        {
            get { return name; }
        }

        private List<PrinterPaperformat> unoptimizedPaperformats;
        private List<PrinterPaperformat> optimizedPaperformats;
        
        /// <summary>
        /// Gets the optimized or unoptimized paperformats for this printer, depending on the specified parameter. Formats are optimized if their margins
        /// are all nearly 4 mm.
        /// </summary>
        /// <param name="optimized">if set to <c>true</c> optimized formats are returned.</param>
        /// <returns>A list the (un)optmized paperformats for this printer</returns>
        public IReadOnlyList<PrinterPaperformat> GetPaperformats(bool optimized)
        {
            return optimized ? optimizedPaperformats.AsReadOnly() : unoptimizedPaperformats.AsReadOnly();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Printer"/> class with the specified name.
        /// <see cref="Printer.InitializePaperformats()"/> has to be called afterwards to load the available paperformats.
        /// </summary>
        /// <param name="name">The name of the printer.</param>
        /// <exception cref="System.ArgumentException">Is thrown if the name is null, empty or if the specified printer does not exist</exception>
        public Printer(String name)
        {
            PlotSettingsValidator psv = PlotSettingsValidator.Current;
            if (String.IsNullOrEmpty(name) || !psv.GetPlotDeviceList().Contains(name + ".pc3"))
            {
                throw new System.ArgumentException(LocalData.PrinterNameException + name);
            }

            this.name = name;
            this.unoptimizedPaperformats = new List<PrinterPaperformat>();
            this.optimizedPaperformats = new List<PrinterPaperformat>();
        }

        /// <summary>
        /// Initializes the paperformats. They are seperated into optimized and unoptimized formats.
        /// </summary>
        public void InitializePaperformats()
        {
            bool found;
            bool optFound;

            PlotSettingsValidator psv = PlotSettingsValidator.Current;

            using (PlotSettings acPlSet = new PlotSettings(true))
            {
                psv.SetPlotConfigurationName(acPlSet, name + ".pc3", null);
                psv.RefreshLists(acPlSet);

                var paperFormats = psv.GetCanonicalMediaNameList(acPlSet);

                for (int i = 0; i <= 4; i++)
                {
                    // Look if the paperformat is optimized or if its usable and not optimed
                    found = false;
                    optFound = false;
                    String savedFormat = "";

                    var formats = paperFormats;

                    for (int k = 0; k < formats.Count && !optFound; k++)
                    {
                        // Add the PNG formats
                        if (i == 0)
                        {
                            if (formats[k].ToString() == "UserDefinedRaster (2100.00 x 2970.00Pixel)")
                            {
                                unoptimizedPaperformats.Add(new PrinterPaperformat("A4", formats[k].ToString(), this));
                                optimizedPaperformats.Add(new PrinterPaperformat("A4", formats[k].ToString(), this));

                            }
                            else if (formats[k].ToString() == "UserDefinedRaster (2970.00 x 4200.00Pixel)")
                            {
                                unoptimizedPaperformats.Add(new PrinterPaperformat("A3", formats[k].ToString(), this));
                                optimizedPaperformats.Add(new PrinterPaperformat("A3", formats[k].ToString(), this));

                            }
                        }

                        if (psv.GetLocaleMediaName(acPlSet, formats[k]).Contains("A" + i.ToString()) && !optFound)
                        {
                            psv.SetCanonicalMediaName(acPlSet, formats[k]);

                            found = true;
                            Extents2d margins = acPlSet.PlotPaperMargins;
                            if (Math.Abs(margins.MinPoint.X) < 4.2 && Math.Abs(margins.MinPoint.Y) < 4.2
                                && Math.Abs(margins.MaxPoint.X) < 4.2 && Math.Abs(margins.MaxPoint.Y) < 4.2
                                && Math.Abs(margins.MinPoint.X) > 3.8 && Math.Abs(margins.MinPoint.Y) > 3.8
                                && Math.Abs(margins.MaxPoint.X) > 3.8 && Math.Abs(margins.MaxPoint.Y) > 3.8)
                            {
                                optFound = true;
                            }
                            savedFormat = formats[k].ToString();
                        }

                    }
                    // If format (optimal) found, add it to formats
                    if (found)
                    {
                        unoptimizedPaperformats.Add(new PrinterPaperformat("A" + i.ToString(), savedFormat, this));

                        if (optFound)
                        {
                            optimizedPaperformats.Add(new PrinterPaperformat("A" + i.ToString(), savedFormat, this));

                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the list of printer names.
        /// </summary>
        /// <value>
        /// The printer names list.
        /// </value>
        public static List<string> PrinterNamesList
        {
            get
            {
                PlotSettingsValidator psv = PlotSettingsValidator.Current;
                var devicelist = psv.GetPlotDeviceList();
                List<string> result = new List<string>();
                foreach (string device in devicelist)
                {
                    if (!device.Contains("Default")
                       && device.Length > 4 && device.Substring(device.Length - 4, 4) == ".pc3")
                    {
                        result.Add(device.Substring(0, device.Length - 4));
                    }
                }
                return result;
            }
        }
    }

    /// <summary>
    /// Defines a paperformat for a printer, defined by a readable name and the format name.
    /// </summary>
    public class PrinterPaperformat
    {
        private String name;

        /// <summary>
        /// Gets the readable name of the format.
        /// </summary>
        /// <value>
        /// The readable name of the format.
        /// </value>
        public String Name
        {
            get { return name; }
        }

        private String formatName;

        /// <summary>
        /// Gets the name of the format as it is used to adress the format.
        /// </summary>
        /// <value>
        /// The original name of the format.
        /// </value>
        public String FormatName
        {
            get { return formatName; }
        }

        private Printer printer;

        /// <summary>
        /// Gets the printer this format belongs to.
        /// </summary>
        /// <value>
        /// The printer.
        /// </value>
        public Printer Printer
        {
            get { return printer; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrinterPaperformat"/> class with the specified readable name and the original format name.
        /// </summary>
        /// <param name="name">The readable name.</param>
        /// <param name="formatName">Original name of the format.</param>
        /// <param name="printer">The printer this format belongs to.</param>
        public PrinterPaperformat(String name, String formatName, Printer printer)
        {
            this.name = name;
            this.formatName = formatName;
            this.printer = printer;
        }
    }

}
