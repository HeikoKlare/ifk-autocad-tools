using System;
using System.Collections.Generic;
using System.Linq;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

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
                Point3d minimum = new Point3d(double.MaxValue, double.MaxValue, 0);
                Point3d maximum = new Point3d(double.MinValue, double.MinValue, 0);
                foreach (SelectedObject obj in selection.Value)
                {
                    handleString += "(handent \"" + obj.ObjectId.Handle + "\")" + "\r";
                    try
                    {
                        Line l = acTrans.GetObject(obj.ObjectId, OpenMode.ForRead) as Line;
                        if (l.StartPoint.X < minimum.X) minimum = new Point3d(l.StartPoint.X, minimum.Y, 0);
                        if (l.StartPoint.X > maximum.X) maximum = new Point3d(l.StartPoint.X, maximum.Y, 0);
                        if (l.EndPoint.X < minimum.X) minimum = new Point3d(l.EndPoint.X, minimum.Y, 0);
                        if (l.EndPoint.X > maximum.X) maximum = new Point3d(l.EndPoint.X, maximum.Y, 0);
                        if (l.StartPoint.Y < minimum.Y) minimum = new Point3d(minimum.X, l.StartPoint.Y, 0);
                        if (l.StartPoint.Y > maximum.Y) maximum = new Point3d(maximum.X, l.StartPoint.Y, 0);
                        if (l.EndPoint.Y < minimum.Y) minimum = new Point3d(minimum.X, l.EndPoint.Y, 0);
                        if (l.EndPoint.Y > maximum.Y) maximum = new Point3d(maximum.X, l.EndPoint.Y, 0);
                    }
                    catch (Exception) { };
                }
                Point3d average = new Point3d((maximum.X + minimum.X) / 2, (maximum.Y + minimum.Y) / 2, 0);

                // Let the user select the reference point
                var referencePoint = acDoc.Editor.GetPoint(Environment.NewLine + LocalData.DimensionReferencePoint);
                if (referencePoint.Status != PromptStatus.OK)
                {
                    acTrans.Abort();
                    return;
                }
                
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
                PromptPointOptions pointOpts = new PromptPointOptions(Environment.NewLine + LocalData.DimensionInsertionPoint);
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
                PromptIntegerOptions intOpts = new PromptIntegerOptions(Environment.NewLine + LocalData.DimensionDecimalPlaces);
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

                // Move reference point a little bit from the center, so it is correctly recognized
                bool horizontal = Math.Abs(insertionPoint.Value.X - referencePoint.Value.X) < Math.Abs(insertionPoint.Value.Y - referencePoint.Value.Y);
                Point3d refPoint = referencePoint.Value;
                Point3d calcRefPoint = referencePoint.Value;
                if (horizontal)
                {
                    refPoint = refPoint.Add(new Vector3d(referencePoint.Value.X < average.X ? -0.001 : 0.001, 0, 0));
                    calcRefPoint = new Point3d(referencePoint.Value.X, referencePoint.Value.Y < average.Y ? minimum.Y : maximum.Y, 0);
                }
                else
                {
                    refPoint = refPoint.Add(new Vector3d(0, referencePoint.Value.Y < average.Y ? -0.001 : 0.001, 0));
                    calcRefPoint = new Point3d(referencePoint.Value.X < average.X ? minimum.X : maximum.X, referencePoint.Value.Y, 0);
                }
                var referencePointString = refPoint.X + "," + refPoint.Y + "," + refPoint.Z;

                // Restore old ortho mode
                acDoc.Database.Orthomode = oldOrtho;

                // Get and activate the style and calculate the base line distance
                double dist = insertionPoint.Value.Subtract(calcRefPoint.GetAsVector()).GetAsVector().Length;
                DimStyleTable dimStyleTable = (DimStyleTable)acTrans.GetObject(acDoc.Database.DimStyleTableId, OpenMode.ForWrite);

                // Save old values
                var oldDimStyle = acDoc.Database.Dimstyle;
                var oldDimStyleData = acDoc.Database.GetDimstyleData();
                var oldDimexo = acDoc.Database.Dimexo;
                var oldDimdec = acDoc.Database.Dimdec;
                var oldDimdli = acDoc.Database.Dimdli;

                if (dimStyleTable.Has(STYLENAME))
                {
                    acDoc.Database.Dimstyle = dimStyleTable[STYLENAME];
                    var rec = acTrans.GetObject(dimStyleTable[STYLENAME], OpenMode.ForRead) as DimStyleTableRecord;
                    acDoc.Database.SetDimstyleData(rec);
                    acDoc.Database.Dimexo = dist * double.Parse(Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("CANNOSCALEVALUE").ToString()) - 0.002;
                }
                else if (dimStyleTable.Has(OLDSTYLENAME))
                {
                    acDoc.Database.Dimstyle = dimStyleTable[OLDSTYLENAME];
                    var rec = acTrans.GetObject(dimStyleTable[OLDSTYLENAME], OpenMode.ForRead) as DimStyleTableRecord;
                    acDoc.Database.SetDimstyleData(rec);
                    acDoc.Database.Dimexo = dist * double.Parse(Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("CANNOSCALEVALUE").ToString()) - 0.002;
                }
                acDoc.Database.Dimdec = decimalPlaces.Value;
                acDoc.Database.Dimdli = 0.0;
                                               
                // Execute command
                acDoc.SendStringToExecute("SBEM " + handleString + " BA P " + referencePointString + "\r" + insertionPointString + " ", true, false, true);
                acTrans.Commit();

                // Restore old values
                acDoc.Database.Dimstyle = oldDimStyle;
                acDoc.Database.SetDimstyleData(oldDimStyleData);
                acDoc.Database.Dimexo = oldDimexo;
                acDoc.Database.Dimdec = oldDimdec;
                acDoc.Database.Dimdli = oldDimdli;
            }
        }
    }
}
