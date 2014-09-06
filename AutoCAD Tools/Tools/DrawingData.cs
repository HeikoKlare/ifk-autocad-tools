using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Globalization;
[assembly: PerDocumentClass(typeof(AutoCADTools.Tools.DrawingData))]


namespace AutoCADTools.Tools
{
    /// <summary>
    /// This class encapsulates the data specified for a drawing like Version, Project, Creation date etc.
    /// </summary>
    public class DrawingData
    {
        #region Attributes

        /// <summary>
        /// The name of the data in the UserData dictionary of the document
        /// </summary>
        public const string DICTIONARY_NAME = "DrawingData";
        private Document document;

        private int version;

        /// <summary>
        /// Gets the version of the drawing.
        /// </summary>
        /// <value>
        /// The version of the drawing.
        /// </value>
        public int Version
        {
            get { return version; }
        }

        private string employer;
        /// <summary>
        /// Gets or sets the employer who ordered this drawing.
        /// </summary>
        /// <value>
        /// The employer who ordered this drawing.
        /// </value>
        public string Employer
        {
            get { return employer; }
            set { employer = value; }
        }

        private string projectNumber;
        /// <summary>
        /// Gets or sets the project number of the project this drawing belongs to.
        /// </summary>
        /// <value>
        /// The project number of the project this drawing belongs to.
        /// </value>
        public string ProjectNumber
        {
            get { return projectNumber; }
            set { projectNumber = value; }
        }

        private string projectDescription1;
        /// <summary>
        /// Gets or sets the first line of the description of the project this drawing belongs to.
        /// </summary>
        /// <value>
        /// The first line of the description of the project this drawing belongs to.
        /// </value>
        public string ProjectDescription1
        {
            get { return projectDescription1; }
            set { projectDescription1 = value; }
        }

        private string projectDescription2;
        /// <summary>
        /// Gets or sets the second line of the description of the project this drawing belongs to.
        /// </summary>
        /// <value>
        /// The second line of the description of the project this drawing belongs to.
        /// </value>
        public string ProjectDescription2
        {
            get { return projectDescription2; }
            set { projectDescription2 = value; }
        }

        private string projectDescription3;
        /// <summary>
        /// Gets or sets the third line of the description of the project this drawing belongs to.
        /// </summary>
        /// <value>
        /// The third line of the description of the project this drawing belongs to.
        /// </value>
        public string ProjectDescription3
        {
            get { return projectDescription3; }
            set { projectDescription3 = value; }
        }

        private string projectDescription4;
        /// <summary>
        /// Gets or sets the forth line of the description of the project this drawing belongs to.
        /// </summary>
        /// <value>
        /// The forth line of the description of the project this drawing belongs to.
        /// </value>
        public string ProjectDescription4
        {
            get { return projectDescription4; }
            set { projectDescription4 = value; }
        }

        private string projectDescriptionShort;
        /// <summary>
        /// Gets or sets the short description of the project this drawing belongs to.
        /// </summary>
        /// <value>
        /// The short description of the project this drawing belongs to.
        /// </value>
        public string ProjectDescriptionShort
        {
            get { return projectDescriptionShort; }
            set { projectDescriptionShort = value; }
        }

        private string drawingDescription;
        /// <summary>
        /// Gets or sets the description of the drawing.
        /// </summary>
        /// <value>
        /// The description of the drawing.
        /// </value>
        public string DrawingDescription
        {
            get { return drawingDescription; }
            set { drawingDescription = value; }
        }

        private string drawingNumber;
        /// <summary>
        /// Gets or sets the drawing number (page number).
        /// </summary>
        /// <value>
        /// The drawing number (page number).
        /// </value>
        public string DrawingNumber
        {
            get { return drawingNumber; }
            set { drawingNumber = value; }
        }

        private DateTime creationDate;
        /// <summary>
        /// Gets or sets the date this drawing was created at.
        /// </summary>
        /// <value>
        /// The date this drawing was created at.
        /// </value>
        public DateTime CreationDate
        {
            get { return creationDate; }
            set { creationDate = value; }
        }

        private int drawingUnit;
        /// <summary>
        /// Gets or sets the drawing unit (number of millimeters a drawing unit represents).
        /// </summary>
        /// <value>
        /// The drawing unit (number of millimeters a drawing unit represents).
        /// </value>
        public int DrawingUnit
        {
            get { return drawingUnit; }
            set { drawingUnit = value; }
        }

        #endregion

        #region Initialisation

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawingData"/> class for the specified document with default values and 
        /// loads values from drawing if available. If the version is outdated the drawing area is updated
        /// </summary>
        /// <param name="document">The document to create the data for.</param>
        public DrawingData(Document document)
        {
            this.document = document;
            this.version = 0;
            this.creationDate = DateTime.Now;
            this.drawingUnit = 1000;

            LoadValues();
            if (version == 0)
            {
                ConvertOldTextfields();
                SaveValues();
            }
            if (!document.UserData.ContainsKey(DICTIONARY_NAME)) document.UserData.Add(DICTIONARY_NAME, this);
        }

        /// <summary>
        /// Creates the data for the specified docoument by calling the constructor.
        /// </summary>
        /// <param name="document">The document to create the data for.</param>
        /// <returns>The created document data object.</returns>
        public static DrawingData Create(Document document)
        {
            var dd = new DrawingData(document);
            return dd;
        }

        #endregion

        #region Load/Save

        /// <summary>
        /// Loads the values from the drawing.
        /// </summary>
        private void LoadValues()
        {
            System.Collections.IDictionaryEnumerator enumer = document.Database.SummaryInfo.CustomProperties;
            int counter = 0;
            while (enumer.MoveNext())
            {
                counter++;
                String val = enumer.Value.ToString().Trim();
                switch (enumer.Key.ToString())
                {
                    case "Version": if (!int.TryParse(val, out version)) version = 1; break;
                    case "Auftraggeber": employer = val; break;
                    case "BV1": projectDescription1 = val; break;
                    case "BV2": projectDescription2 = val; break;
                    case "BV3": projectDescription3 = val; break;
                    case "BV4": projectDescription4 = val; break;
                    case "BVK": projectDescriptionShort = val; break;
                    case "Statiknummer": projectNumber = val; break;
                    case "Bauteil": drawingDescription = val; break;
                    case "Plannummer": drawingNumber = val; break;
                    case "Erstellungsdatum": if (!DateTime.TryParseExact(val, "d", CultureInfo.CurrentCulture, DateTimeStyles.None, out creationDate)) creationDate = DateTime.Now; break;
                    case "Zeichnungseinheit": if (!int.TryParse(val, out drawingUnit)) drawingUnit = 1000; break;
                }
            }
            if (counter == 0) version = -1;
        }

        /// <summary>
        /// Clears the changes made by loading the values saved in the drawing.
        /// </summary>
        public void ClearChanges()
        {
            LoadValues();
        }

        /// <summary>
        /// Saves the values specified in this object in the drawing.
        /// </summary>
        public void SaveValues()
        {
            // If textboxes are empty fill with a whitespace so there is no "---" in the textfields
            if (String.IsNullOrEmpty(employer)) employer = LocalData.EmptyDrawingSetting;
            if (String.IsNullOrEmpty(projectNumber)) projectNumber = LocalData.EmptyDrawingSetting;
            if (String.IsNullOrEmpty(projectDescription1)) projectDescription1 = LocalData.EmptyDrawingSetting;
            if (String.IsNullOrEmpty(projectDescription2)) projectDescription2 = LocalData.EmptyDrawingSetting;
            if (String.IsNullOrEmpty(projectDescription3)) projectDescription3 = LocalData.EmptyDrawingSetting;
            if (String.IsNullOrEmpty(projectDescription4)) projectDescription4 = LocalData.EmptyDrawingSetting;
            if (String.IsNullOrEmpty(projectDescriptionShort)) projectDescriptionShort = LocalData.EmptyDrawingSetting;
            if (String.IsNullOrEmpty(drawingDescription)) drawingDescription = LocalData.EmptyDrawingSetting;
            if (String.IsNullOrEmpty(drawingNumber)) drawingNumber = LocalData.EmptyDrawingSetting;

            // Save data in SummaryInfo
            DatabaseSummaryInfoBuilder dbSumBuilder = new DatabaseSummaryInfoBuilder();
            System.Collections.IDictionary prop = dbSumBuilder.CustomPropertyTable;
            prop.Add("Version", version.ToString());
            prop.Add("Auftraggeber", employer);
            prop.Add("BV1", projectDescription1);
            prop.Add("BV2", projectDescription2);
            prop.Add("BV3", projectDescription3);
            prop.Add("BV4", projectDescription4);
            prop.Add("BVK", projectDescriptionShort);
            prop.Add("Erstellungsdatum", creationDate.ToShortDateString());
            prop.Add("Statiknummer", projectNumber);
            prop.Add("Bauteil", drawingDescription);
            prop.Add("Plannummer", drawingNumber);
            prop.Add("Zeichnungseinheit", drawingUnit.ToString());

            document.Database.SummaryInfo = dbSumBuilder.ToDatabaseSummaryInfo();
        }

        #endregion

        #region Old Conversion

        /// <summary>
        /// Converts the old textfields by removing date field from A4 textfield and changes from bigger textfield.
        /// </summary>
        private void ConvertOldTextfields()
        {
            version = 1;
            using (document.LockDocument())
            {
                using (Transaction trans = document.Database.TransactionManager.StartTransaction())
                {
                    // Delete from block definition
                    BlockTable table = trans.GetObject(document.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                    if (table.Has("Textfeld A4"))
                    {
                        BlockTableRecord textfieldRecord = trans.GetObject(table["Textfeld A4"], OpenMode.ForWrite) as BlockTableRecord;
                        foreach (ObjectId id in textfieldRecord)
                        {
                            var attribute = trans.GetObject(id, OpenMode.ForRead) as AttributeDefinition;
                            if (attribute != null && attribute.Tag.ToUpper() == "DATUM")
                            {
                                attribute.UpgradeOpen();
                                attribute.Erase();
                            }
                        }
                    }
                    if (table.Has("Textfeld A3+"))
                    {
                        BlockTableRecord textfieldRecord = trans.GetObject(table["Textfeld A3+"], OpenMode.ForWrite) as BlockTableRecord;
                        foreach (ObjectId id in textfieldRecord)
                        {
                            var attribute = trans.GetObject(id, OpenMode.ForRead) as AttributeDefinition;
                            if (attribute != null)
                            {
                                attribute.UpgradeOpen();
                                switch (attribute.Tag.ToUpper())
                                {
                                    case "Ä1NAME": attribute.Erase(); break;
                                    case "Ä1DATUM": attribute.Erase(); break;
                                    case "Ä1VERMERK": attribute.Erase(); break;
                                    case "Ä2NAME": attribute.Erase(); break;
                                    case "Ä2DATUM": attribute.Erase(); break;
                                    case "Ä2VERMERK": attribute.Erase(); break;
                                }
                            }
                        }
                    }

                    // Delete from layouts
                    var layouts = trans.GetObject(document.Database.LayoutDictionaryId, OpenMode.ForRead) as DBDictionary;
                    foreach (DBDictionaryEntry layout in layouts)
                    {
                        BlockTableRecord record = trans.GetObject((trans.GetObject(layouts.GetAt(layout.Key), OpenMode.ForRead) as Layout).BlockTableRecordId, OpenMode.ForRead) as BlockTableRecord;

                        if (record != null)
                        {
                            foreach (ObjectId objId in record)
                            {

                                BlockReference block = trans.GetObject(objId, OpenMode.ForWrite) as BlockReference;
                                if (block != null && block.Name == "Textfeld A4")
                                {
                                    foreach (ObjectId attId in block.AttributeCollection)
                                    {
                                        var attribute = trans.GetObject(attId, OpenMode.ForRead) as AttributeReference;
                                        if (attribute != null && attribute.Tag.ToUpper() == "DATUM")
                                        {
                                            attribute.UpgradeOpen();
                                            attribute.Erase();
                                        }
                                    }
                                }
                                else if (block != null && block.Name == "Textfeld A3+")
                                {
                                    foreach (ObjectId attId in block.AttributeCollection)
                                    {
                                        var attribute = trans.GetObject(attId, OpenMode.ForRead) as AttributeReference;
                                        if (attribute != null)
                                        {
                                            attribute.UpgradeOpen();
                                            switch (attribute.Tag.ToUpper())
                                            {
                                                case "Ä1NAME": attribute.Erase(); break;
                                                case "Ä1DATUM": attribute.Erase(); break;
                                                case "Ä1VERMERK": attribute.Erase(); break;
                                                case "Ä2NAME": attribute.Erase(); break;
                                                case "Ä2DATUM": attribute.Erase(); break;
                                                case "Ä2VERMERK": attribute.Erase(); break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    trans.Commit();
                }
            }

        }

        #endregion

    }
}
