using System;
using System.Collections.Generic;
using System.Linq;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;

namespace AutoCADTools.Tools
{
    class ConsecutiveDimension
    {
        private readonly static string[] LAYERS_TO_SHOW = { "Binder (Normal)", "Dachhaut" };
        private readonly static string STYLENAME = "Laufendes Maß";
        private readonly static string OLDSTYLENAME = "2NK Fortlaufend";

        public static void Execute() {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Dictionary<LayerTableRecord, bool> layerActivation = new Dictionary<LayerTableRecord, bool>();

            using (Transaction acTrans = acDoc.TransactionManager.StartTransaction())
            {
                // Save all layers status and turn most of them off
                LayerTable layerTable = acTrans.GetObject(acDoc.Database.LayerTableId, OpenMode.ForWrite) as LayerTable;
                foreach (ObjectId layerId in layerTable)
                {
                    LayerTableRecord current = acTrans.GetObject(layerId, OpenMode.ForWrite) as LayerTableRecord;
                    layerActivation.Add(current, current.IsOff);
                    Boolean True = new Boolean();
                    True = true;
                    Boolean False = new Boolean();
                    False = false;
                    if (!LAYERS_TO_SHOW.Contains(current.Name) && acDoc.Database.Clayer != layerId)
                    {
                        current.IsOff = True;
                    }
                    else
                    {
                        current.IsOff = False;
                    }
                }
                
                // Let the user select the objects
                var selection = acDoc.Editor.GetSelection();
                if (selection.Status != PromptStatus.OK)
                {
                    acTrans.Abort();
                    return;
                }
                // Create a handle string for the objects
                string handleString = "";
                foreach (SelectedObject obj in selection.Value)
                {
                    handleString += "(handent \"" + obj.ObjectId.Handle + "\")" + "\r";
                }

                // Let the user select the reference point
                var referencePoint = acDoc.Editor.GetPoint("\n" + LocalData.DimensionReferencePoint + ": ");
                if (referencePoint.Status != PromptStatus.OK)
                {
                    acTrans.Abort();
                    return;
                }
                var referencePointString = referencePoint.Value.X + "," + referencePoint.Value.Y + "," + referencePoint.Value.Z;

                // Turn ortho mode on for input point and turn on the layers again
                object oldAutoSnap = Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("AUTOSNAP");
                bool oldOrtho = acDoc.Database.Orthomode;
                acDoc.Database.Orthomode = true;
                foreach (ObjectId layerId in layerTable)
                {
                    LayerTableRecord current = acTrans.GetObject(layerId, OpenMode.ForWrite) as LayerTableRecord;
                    current.IsOff = layerActivation[current];
                }

                // Let the user select the insertion point
                PromptPointOptions pointOpts = new PromptPointOptions("\n" + LocalData.DimensionInsertionPoint + ": ");
                pointOpts.UseBasePoint = true;
                pointOpts.BasePoint = referencePoint.Value;
                var insertionPoint = acDoc.Editor.GetPoint(pointOpts);
                Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("AUTOSNAP", oldAutoSnap);
                if (insertionPoint.Status != PromptStatus.OK)
                {
                    acTrans.Abort();
                    return;
                }
                var insertionPointString = insertionPoint.Value.X + "," + insertionPoint.Value.Y + "," + insertionPoint.Value.Z;

                // Let the user define the number of decimal places
                PromptIntegerOptions intOpts = new PromptIntegerOptions("\n" + LocalData.DimensionDecimalPlaces + ": ");
                intOpts.DefaultValue = 2;
                intOpts.LowerLimit = 1;
                intOpts.UpperLimit = 4;
                intOpts.UseDefaultValue = true;
                var decimalPlaces = acDoc.Editor.GetInteger(intOpts);
                if (decimalPlaces.Status != PromptStatus.OK) 
                {
                    acTrans.Abort();
                    return;
                }

                // Restore old ortho mode
                acDoc.Database.Orthomode = oldOrtho;

                // Get and activate the style and calculate the base line distance
                double dist = insertionPoint.Value.Subtract(referencePoint.Value.GetAsVector()).GetAsVector().Length;
                DimStyleTable dimStyleTable = (DimStyleTable)acTrans.GetObject(acDoc.Database.DimStyleTableId, OpenMode.ForWrite);
                if (dimStyleTable.Has(STYLENAME))
                {
                    acDoc.Database.Dimstyle = dimStyleTable[STYLENAME];
                    var rec = acTrans.GetObject(dimStyleTable[STYLENAME], OpenMode.ForRead) as DimStyleTableRecord;
                    acDoc.Database.SetDimstyleData(rec);
                    acDoc.Database.Dimexo = dist * 0.01 - 0.002;
                }
                else if (dimStyleTable.Has(OLDSTYLENAME))
                {
                    acDoc.Database.Dimstyle = dimStyleTable[OLDSTYLENAME];
                    var rec = acTrans.GetObject(dimStyleTable[OLDSTYLENAME], OpenMode.ForRead) as DimStyleTableRecord;
                    acDoc.Database.SetDimstyleData(rec);
                    acDoc.Database.Dimexo = dist * 0.01 - 0.002;
                }
                acDoc.Database.Dimdec = decimalPlaces.Value;
                acDoc.Database.Dimdli = 0.0;

                // Execute command
                acDoc.SendStringToExecute("SBEM " + handleString + "\r BA \r P " + referencePointString + "\r" + insertionPointString + "\r", true, false, false);
                acTrans.Commit();
            }
        }
    }
}
