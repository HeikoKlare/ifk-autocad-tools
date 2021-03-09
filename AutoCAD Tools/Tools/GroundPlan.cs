using AutoCADTools.Utils;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AutoCADTools.Tools
{
    /// <summary>
    /// This class provides the functionality to specifiy a simple rectangular ground plan through user input.
    /// </summary>
    public static class GroundPlan
    {
        private static double savedWidth = 0.0;
        private static double savedLength = 0.0;
        private static double savedWallThickness = 0.32;
        private static double savedEaveOverhang = 0.0;
        private static double savedVergeOverhang = 0.0;
        private const string WallLayer = "Wände";
        private const string RoofCladdingLayer = "Dachhaut";

        /// <summary>
        /// Executes the command and takes user input to create and place a ground plan.
        /// </summary>
        public static void Execute()
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            if (!GetValue(acDoc.Editor, LocalData.GroundPlanLengthPrompt, savedLength, out double length))
            {
                return;
            }
            savedLength = length;
            if (!GetValue(acDoc.Editor, LocalData.GroundPlanWidthPrompt, savedWidth, out double width))
            {
                return;
            }
            savedWidth = width;
            if (!GetValue(acDoc.Editor, LocalData.GroundPlanWallThicknessPrompt, savedWallThickness, out double wallThickness))
            {
                return;
            }
            savedWallThickness = wallThickness;
            if (!GetValue(acDoc.Editor, LocalData.GroundPlanEaveOverhangPrompt, savedEaveOverhang, out double eaveOverhang))
            {
                return;
            }
            savedEaveOverhang = eaveOverhang;
            if (!GetValue(acDoc.Editor, LocalData.GroundPlanVergeOverhangPrompt, savedVergeOverhang, out double vergeOverhang))
            {
                return;
            }
            savedVergeOverhang = vergeOverhang;

            using (Transaction acTrans = acDoc.TransactionManager.StartTransaction())
            {
                var entities = new List<Entity>();
                LayerTable layerTable = acTrans.GetObject(acDoc.Database.LayerTableId, OpenMode.ForRead) as LayerTable;

                ObjectId currentLayer = acDoc.Database.LayerZero;
                // Outer wall
                if (layerTable.Has(WallLayer))
                {
                    currentLayer = layerTable[WallLayer];
                }
                Line line = new Line(new Point3d(vergeOverhang, eaveOverhang, 0),
                    new Point3d(vergeOverhang + length, eaveOverhang, 0));
                entities.Add(line);
                line.SetDatabaseDefaults();
                line.SetLayerId(currentLayer, true);
                line = new Line(new Point3d(vergeOverhang + length, eaveOverhang, 0),
                    new Point3d(vergeOverhang + length, eaveOverhang + width, 0));
                entities.Add(line);
                line.SetDatabaseDefaults();
                line.SetLayerId(currentLayer, true);
                line = new Line(new Point3d(vergeOverhang + length, eaveOverhang + width, 0),
                    new Point3d(vergeOverhang, eaveOverhang + width, 0));
                entities.Add(line);
                line.SetDatabaseDefaults();
                line.SetLayerId(currentLayer, true);
                line = new Line(new Point3d(vergeOverhang, eaveOverhang + width, 0),
                    new Point3d(vergeOverhang, eaveOverhang, 0));
                entities.Add(line);
                line.SetDatabaseDefaults();
                line.SetLayerId(currentLayer, true);
                // Inner wall
                line = new Line(new Point3d(vergeOverhang + wallThickness, eaveOverhang + wallThickness, 0),
                    new Point3d(vergeOverhang + length - wallThickness, eaveOverhang + wallThickness, 0));
                entities.Add(line);
                line.SetDatabaseDefaults();
                line.SetLayerId(currentLayer, true);
                line = new Line(new Point3d(vergeOverhang + length - wallThickness, eaveOverhang + wallThickness, 0),
                    new Point3d(vergeOverhang + length - wallThickness, eaveOverhang + width - wallThickness, 0));
                entities.Add(line);
                line.SetDatabaseDefaults();
                line.SetLayerId(currentLayer, true);
                line = new Line(new Point3d(vergeOverhang + length - wallThickness, eaveOverhang + width - wallThickness, 0),
                    new Point3d(vergeOverhang + wallThickness, eaveOverhang + width - wallThickness, 0));
                entities.Add(line);
                line.SetDatabaseDefaults();
                line.SetLayerId(currentLayer, true);
                line = new Line(new Point3d(vergeOverhang + wallThickness, eaveOverhang + width - wallThickness, 0),
                    new Point3d(vergeOverhang + wallThickness, eaveOverhang + wallThickness, 0));
                entities.Add(line);
                line.SetDatabaseDefaults();
                line.SetLayerId(currentLayer, true);
                // Roof cladding
                if (layerTable.Has(RoofCladdingLayer))
                {
                    currentLayer = layerTable[RoofCladdingLayer];
                }
                line = new Line(new Point3d(0, 0, 0),
                    new Point3d(2 * vergeOverhang + length, 0, 0));
                entities.Add(line);
                line.SetDatabaseDefaults();
                line.SetLayerId(currentLayer, true);
                line = new Line(new Point3d(2 * vergeOverhang + length, 0, 0),
                    new Point3d(2 * vergeOverhang + length, 2 * eaveOverhang + width, 0));
                entities.Add(line);
                line.SetDatabaseDefaults();
                line.SetLayerId(currentLayer, true);
                line = new Line(new Point3d(2 * vergeOverhang + length, 2 * eaveOverhang + width, 0),
                    new Point3d(0, 2 * eaveOverhang + width, 0));
                entities.Add(line);
                line.SetDatabaseDefaults();
                line.SetLayerId(currentLayer, true);
                line = new Line(new Point3d(0, 2 * eaveOverhang + width, 0),
                    new Point3d(0, 0, 0));
                entities.Add(line);
                line.SetDatabaseDefaults();
                line.SetLayerId(currentLayer, true);
                line = new Line(new Point3d(0, eaveOverhang + width / 2, 0),
                    new Point3d(2 * vergeOverhang + length, eaveOverhang + width / 2, 0));
                entities.Add(line);
                line.SetDatabaseDefaults();
                line.SetLayerId(currentLayer, true);

                BlockTable acBlkTbl = acTrans.GetObject(acDoc.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                foreach (Entity ent in entities)
                {
                    acBlkTblRec.AppendEntity(ent);
                }

                EntitiesJig jig = new EntitiesJig(entities, Point3d.Origin, LocalData.InsertionPoint);

                if (acDoc.Editor.Drag(jig).Status != PromptStatus.OK)
                {
                    acTrans.Abort();
                    return;
                }

                foreach (Entity ent in entities)
                {
                    acTrans.AddNewlyCreatedDBObject(ent, true);
                    ent.Dispose();
                }

                acTrans.Commit();
            }

        }

        private static bool GetValue(Editor ed, string prompt, double defaultValue, out double result)
        {
            PromptDoubleOptions opts = new PromptDoubleOptions(Environment.NewLine + prompt)
            {
                UseDefaultValue = true,
                DefaultValue = defaultValue
            };
            PromptDoubleResult res = ed.GetDouble(opts);
            result = res.Value;
            return res.Status == PromptStatus.OK;
        }
    }
}
