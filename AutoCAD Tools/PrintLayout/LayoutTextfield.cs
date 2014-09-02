using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Drawing;

namespace AutoCADTools.PrintLayout
{
    public class LayoutTextfield : LayoutCreation
    {
        private PaperformatTextfield paperformat;

        public LayoutTextfield(string name, PaperformatTextfield paperformat, Point extractLowerRightPoint, PrinterPaperformat printerformat, PaperOrientation orientation, double unit, double scale)
            : base(name, paperformat, extractLowerRightPoint, printerformat, orientation, unit, scale)
        {
            this.paperformat = paperformat;
        }

        protected override bool DrawLayoutAdditions(Size margin, BlockTableRecord layoutRecord)
        {
            using (Transaction trans = document.Database.TransactionManager.StartTransaction())
            {
                BlockTable blockTable = trans.GetObject(document.Database.BlockTableId, OpenMode.ForRead) as BlockTable;

                if (!blockTable.Has(paperformat.TextfieldBlockName))
                {
                    trans.Abort();
                    return false;
                }

                // Create polyline and add the vertices
                Polyline pBorder = new Polyline();
                pBorder.Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.Black);

                pBorder.AddVertexAt(pBorder.NumberOfVertices,
                    new Point2d(paperformat.BorderBasePoint.X / unit - margin.Width,
                        paperformat.BorderBasePoint.Y / unit - margin.Height), 0, 0, 0);
                pBorder.AddVertexAt(pBorder.NumberOfVertices,
                    new Point2d((paperformat.BorderBasePoint.X + paperformat.BorderSize.Width) / unit - margin.Width,
                        paperformat.BorderBasePoint.Y / unit - margin.Height), 0, 0, 0);
                pBorder.AddVertexAt(pBorder.NumberOfVertices,
                    new Point2d((paperformat.BorderBasePoint.X + paperformat.BorderSize.Width) / unit - margin.Width,
                        (paperformat.BorderBasePoint.Y + paperformat.BorderSize.Height) / unit - margin.Height), 0, 0, 0);
                pBorder.AddVertexAt(pBorder.NumberOfVertices,
                    new Point2d(paperformat.BorderBasePoint.X / unit - margin.Width,
                    (paperformat.BorderBasePoint.Y + paperformat.BorderSize.Height) / unit - margin.Height), 0, 0, 0);
                pBorder.Closed = true;

                // Append polyline and tell transaction about it
                layoutRecord.AppendEntity(pBorder);
                trans.AddNewlyCreatedDBObject(pBorder, true);
                pBorder.Dispose();

                // Create a line for the nippel and set start- and endpoint depending on format
                Line halfSizeMark = new Line();
                halfSizeMark.Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.Black);
                halfSizeMark.LayerId = document.Database.LayerZero;

                if (paperformat is PaperformatTextfieldA4)
                {
                    halfSizeMark.StartPoint = new Point3d((paperformat.BorderBasePoint.X - 10.0) / unit - margin.Width,
                        (paperformat.BorderBasePoint.Y + paperformat.BorderSize.Height / 2) / unit - margin.Height, 0);
                }
                else
                {
                    double remainingHeight = paperformat.BorderSize.Height;
                    int counter = 0;
                    while (remainingHeight > PaperformatTextfieldCustom.foldPeriod.Y)
                    {
                        counter++;
                        using (Line foldLine = new Line())
                        {
                            foldLine.Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.Black);
                            foldLine.LayerId = document.Database.LayerZero;
                            foldLine.StartPoint = new Point3d(paperformat.BorderBasePoint.X / unit - margin.Width,
                                    (paperformat.BorderBasePoint.Y + counter * PaperformatTextfieldCustom.foldPeriod.Y) / unit - margin.Height, 0);
                            foldLine.EndPoint = foldLine.StartPoint.Add(new Vector3d((paperformat.ViewportBasePoint.X - paperformat.BorderBasePoint.X) / unit, 0, 0));
                            layoutRecord.AppendEntity(foldLine);
                            trans.AddNewlyCreatedDBObject(foldLine, true);
                        }
                        remainingHeight -= PaperformatTextfieldCustom.foldPeriod.Y;
                    }
                    halfSizeMark.StartPoint = new Point3d((paperformat.ViewportBasePoint.X - 10.0) / unit - margin.Width,
                        (paperformat.BorderBasePoint.Y + PaperformatTextfieldCustom.foldPeriod.Y / 2) / unit - margin.Height, 0);
                }

                halfSizeMark.EndPoint = halfSizeMark.StartPoint.Add(new Vector3d(10.0 / unit, 0, 0));

                // Append nippel polyline and tell transaction about it
                layoutRecord.AppendEntity(halfSizeMark);
                trans.AddNewlyCreatedDBObject(halfSizeMark, true);
                halfSizeMark.Dispose();

                // Create a new BlockReference of the right textfield and get the record of the block
                BlockTableRecord textfieldBlock = trans.GetObject(blockTable[paperformat.TextfieldBlockName], OpenMode.ForRead) as BlockTableRecord;
                BlockReference textfield = new BlockReference(new Point3d(paperformat.TextfieldBasePoint.X / unit - margin.Width, 
                    paperformat.TextfieldBasePoint.Y / unit - margin.Height, 0), blockTable[paperformat.TextfieldBlockName]);
                textfield.Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.Black);
                textfield.LayerId = document.Database.LayerZero;

                // Set right scalefactor (should be 0.01)
                textfield.ScaleFactors = new Scale3d(textfield.UnitFactor);
                if (paperformat.OldTextfieldSize)
                {
                    textfield.ScaleFactors = new Scale3d(10 / unit);
                    textfield.Position = textfield.Position.Add(new Vector3d(paperformat.TextfieldSize.Width / unit, - paperformat.TextfieldSize.Height / unit, 0));
                }

                // Append the textfield and tell transaction about it
                layoutRecord.AppendEntity(textfield);
                trans.AddNewlyCreatedDBObject(textfield, true);

                // Go through object in record and add a reference of the attributes to the BlockReference
                foreach (ObjectId id in textfieldBlock)
                {
                    AttributeDefinition attDef = trans.GetObject(id, OpenMode.ForRead) as AttributeDefinition;
                    if (attDef != null)
                    {
                        using (AttributeReference attRef = new AttributeReference())
                        {
                            // Position the attribute reference in the block reference
                            attRef.SetAttributeFromBlock(attDef, textfield.BlockTransform);
                            attRef.Position = attDef.Position.TransformBy(textfield.BlockTransform);

                            // Set scale or date in the textfield
                            if (attRef.Tag == "MAßSTÄBE")
                            {
                                attRef.TextString = "1:" + Math.Round(1.0 / scale).ToString();
                            }

                            // Append the attribute and tell transaction about it
                            textfield.AttributeCollection.AppendAttribute(attRef);
                            trans.AddNewlyCreatedDBObject(attRef, true);
                        }

                        attDef.Dispose();
                    }
                }

                textfield.Dispose();
                trans.Commit();
            }
            return true;
        }
    }
}
