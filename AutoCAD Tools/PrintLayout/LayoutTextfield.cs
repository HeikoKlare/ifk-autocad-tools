using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Drawing;

namespace AutoCADTools.PrintLayout
{
    /// <summary>
    /// Implements the layout creation process for a layout with textfield and borders.
    /// </summary>
    public class LayoutTextfield : LayoutCreation
    {
        #region Attributes

        private readonly PaperformatTextfield paperformat;
        /// <summary>
        /// Gets the paperformat specified in the constructor.
        /// </summary>
        /// <value>
        /// The paperformat.
        /// </value>
        public new PaperformatTextfield Paperformat
        {
            get { return paperformat; }
        }

        #endregion

        #region Initialisation

        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutTextfield"/> class for the specified paperformat.
        /// </summary>
        /// <param name="paperformat">The paperformat to create the layout for.</param>
        public LayoutTextfield(PaperformatTextfield paperformat)
            : base(paperformat)
        {
            this.paperformat = paperformat;
        }

        #endregion

        #region Creation

        /// <summary>
        /// Draws additional parts in the layout: textfield, borders and fold lines.
        /// </summary>
        /// <param name="margin">The margin of the used paper.</param>
        /// <param name="layoutRecord">The layout record of the created layout.</param>
        /// <returns></returns>
        protected override bool DrawLayoutAdditions(Size margin, BlockTableRecord layoutRecord)
        {
            using (Transaction trans = Document.Database.TransactionManager.StartTransaction())
            {
                BlockTable blockTable = trans.GetObject(Document.Database.BlockTableId, OpenMode.ForRead) as BlockTable;

                if (!blockTable.Has(paperformat.TextfieldBlockName))
                {
                    trans.Abort();
                    return false;
                }

                // Create polyline and add the vertices
                Polyline pBorder = new Polyline
                {
                    Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.Black)
                };

                pBorder.AddVertexAt(pBorder.NumberOfVertices,
                    new Point2d(paperformat.BorderBasePoint.X / DrawingUnit - margin.Width,
                        paperformat.BorderBasePoint.Y / DrawingUnit - margin.Height), 0, 0, 0);
                pBorder.AddVertexAt(pBorder.NumberOfVertices,
                    new Point2d((paperformat.BorderBasePoint.X + paperformat.BorderSize.Width) / DrawingUnit - margin.Width,
                        paperformat.BorderBasePoint.Y / DrawingUnit - margin.Height), 0, 0, 0);
                pBorder.AddVertexAt(pBorder.NumberOfVertices,
                    new Point2d((paperformat.BorderBasePoint.X + paperformat.BorderSize.Width) / DrawingUnit - margin.Width,
                        (paperformat.BorderBasePoint.Y + paperformat.BorderSize.Height) / DrawingUnit - margin.Height), 0, 0, 0);
                pBorder.AddVertexAt(pBorder.NumberOfVertices,
                    new Point2d(paperformat.BorderBasePoint.X / DrawingUnit - margin.Width,
                    (paperformat.BorderBasePoint.Y + paperformat.BorderSize.Height) / DrawingUnit - margin.Height), 0, 0, 0);
                pBorder.Closed = true;

                // Append polyline and tell transaction about it
                layoutRecord.AppendEntity(pBorder);
                trans.AddNewlyCreatedDBObject(pBorder, true);
                pBorder.Dispose();

                // Create a line for the nippel and set start- and endpoint depending on format
                Line halfSizeMark = new Line
                {
                    Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.Black),
                    LayerId = Document.Database.LayerZero
                };

                if (paperformat is PaperformatTextfieldA4)
                {
                    halfSizeMark.StartPoint = new Point3d((paperformat.BorderBasePoint.X - 10.0) / DrawingUnit - margin.Width,
                        (paperformat.BorderBasePoint.Y + paperformat.BorderSize.Height / 2) / DrawingUnit - margin.Height, 0);
                }
                else
                {
                    double remainingHeight = paperformat.BorderSize.Height;
                    int counter = 0;
                    while (remainingHeight > PaperformatTextfieldCustom.FoldPeriod.Y)
                    {
                        counter++;
                        using (Line foldLine = new Line())
                        {
                            foldLine.Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.Black);
                            foldLine.LayerId = Document.Database.LayerZero;
                            foldLine.StartPoint = new Point3d(paperformat.BorderBasePoint.X / DrawingUnit - margin.Width,
                                    (paperformat.BorderBasePoint.Y + counter * PaperformatTextfieldCustom.FoldPeriod.Y) / DrawingUnit - margin.Height, 0);
                            foldLine.EndPoint = foldLine.StartPoint.Add(new Vector3d((paperformat.ViewportBasePoint.X - paperformat.BorderBasePoint.X) / DrawingUnit, 0, 0));
                            layoutRecord.AppendEntity(foldLine);
                            trans.AddNewlyCreatedDBObject(foldLine, true);
                        }
                        remainingHeight -= PaperformatTextfieldCustom.FoldPeriod.Y;
                    }
                    halfSizeMark.StartPoint = new Point3d((paperformat.ViewportBasePoint.X - 10.0) / DrawingUnit - margin.Width,
                        (paperformat.BorderBasePoint.Y + PaperformatTextfieldCustom.FoldPeriod.Y / 2) / DrawingUnit - margin.Height, 0);
                }

                halfSizeMark.EndPoint = halfSizeMark.StartPoint.Add(new Vector3d(10.0 / DrawingUnit, 0, 0));

                // Append nippel polyline and tell transaction about it
                layoutRecord.AppendEntity(halfSizeMark);
                trans.AddNewlyCreatedDBObject(halfSizeMark, true);
                halfSizeMark.Dispose();

                // Create a new BlockReference of the right textfield and get the record of the block
                BlockTableRecord textfieldBlock = trans.GetObject(blockTable[paperformat.TextfieldBlockName], OpenMode.ForRead) as BlockTableRecord;
                BlockReference textfield = new BlockReference(new Point3d(paperformat.TextfieldBasePoint.X / DrawingUnit - margin.Width,
                    paperformat.TextfieldBasePoint.Y / DrawingUnit - margin.Height, 0), blockTable[paperformat.TextfieldBlockName])
                {
                    Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color.Black),
                    LayerId = Document.Database.LayerZero
                };

                // Set right scalefactor (should be 0.01)
                textfield.ScaleFactors = new Scale3d(textfield.UnitFactor);
                if (paperformat.OldTextfieldSize)
                {
                    textfield.ScaleFactors = new Scale3d(10 / DrawingUnit);
                    textfield.Position = textfield.Position.Add(new Vector3d(paperformat.TextfieldSize.Width / DrawingUnit, -paperformat.TextfieldSize.Height / DrawingUnit, 0));
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
                                attRef.TextString = "1:" + Math.Round(1.0 / Scale).ToString();
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

        #endregion

    }
}
