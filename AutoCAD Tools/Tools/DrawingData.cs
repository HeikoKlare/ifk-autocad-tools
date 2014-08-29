using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Globalization;
[assembly: PerDocumentClass(typeof(AutoCADTools.Tools.DrawingData))]


namespace AutoCADTools.Tools
{
    public class DrawingData
    {
        public const string DICTIONARY_NAME = "DrawingData";
        Document document;

        private int version;

        public int Version
        {
            get { return version; }
            set { version = value; }
        }

        private string employer;

        public string Employer
        {
            get { return employer; }
            set { employer = value; }
        }

        private string projectNumber;

        public string ProjectNumber
        {
            get { return projectNumber; }
            set { projectNumber = value; }
        }

        private string projectDescription1;

        public string ProjectDescription1
        {
            get { return projectDescription1; }
            set { projectDescription1 = value; }
        }
        private string projectDescription2;

        public string ProjectDescription2
        {
            get { return projectDescription2; }
            set { projectDescription2 = value; }
        }
        private string projectDescription3;

        public string ProjectDescription3
        {
            get { return projectDescription3; }
            set { projectDescription3 = value; }
        }
        private string projectDescription4;

        public string ProjectDescription4
        {
            get { return projectDescription4; }
            set { projectDescription4 = value; }
        }
        private string projectDescriptionShort;

        public string ProjectDescriptionShort
        {
            get { return projectDescriptionShort; }
            set { projectDescriptionShort = value; }
        }

        private string drawingDescription;

        public string DrawingDescription
        {
            get { return drawingDescription; }
            set { drawingDescription = value; }
        }

        private string drawingNumber;

        public string DrawingNumber
        {
            get { return drawingNumber; }
            set { drawingNumber = value; }
        }

        private DateTime creationTime;

        public DateTime CreationTime
        {
            get { return creationTime; }
            set { creationTime = value; }
        }

        private int drawingUnit;

        public int DrawingUnit
        {
            get { return drawingUnit; }
            set { drawingUnit = value; }
        }

        public DrawingData(Document document)
        {
            this.document = document;
            this.version = 0;
            this.creationTime = DateTime.Now;
            this.drawingUnit = 1000;

            LoadValues();
            if (version == 0)
            {
                ConvertOldTextfields();
                SaveValues();
            }
            if (!document.UserData.ContainsKey(DICTIONARY_NAME)) document.UserData.Add(DICTIONARY_NAME, this);
        }

        public static DrawingData Create(Document document)
        {
            var dd = new DrawingData(document);
            return dd;
        }

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
                    case "Erstellungsdatum": if (!DateTime.TryParseExact(val, "d", CultureInfo.CurrentCulture, DateTimeStyles.None, out creationTime)) creationTime = DateTime.Now; break;
                    case "Zeichnungseinheit": if (!int.TryParse(val, out drawingUnit)) drawingUnit = 1000; break;
                }
            }
            if (counter == 0) version = -1;
        }

        public void ClearChanges()
        {
            LoadValues();
        }

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
            prop.Add("Erstellungsdatum", creationTime.ToShortDateString());
            prop.Add("Statiknummer", projectNumber);
            prop.Add("Bauteil", drawingDescription);
            prop.Add("Plannummer", drawingNumber);
            prop.Add("Zeichnungseinheit", drawingUnit.ToString());

            document.Database.SummaryInfo = dbSumBuilder.ToDatabaseSummaryInfo();
        }

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
    }
}
