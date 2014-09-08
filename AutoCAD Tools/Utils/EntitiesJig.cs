using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using System.Collections.Generic;

namespace AutoCADTools.Utils
{
    /// <summary>
    /// A generic jig for moving several entites based on a given reference point.
    /// </summary>
    public class EntitiesJig : Autodesk.AutoCAD.EditorInput.DrawJig
    {
        private IList<Entity> entities;
        private Point3d referencePoint;
        private Vector3d movement;
        private string promptMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntitiesJig"/> class for the specified entities,
        /// with the given reference point the jig is based on and the message shown for input.
        /// </summary>
        /// <param name="entities">The entities to move.</param>
        /// <param name="referencePoint">The reference point for movement.</param>
        /// <param name="promptMessage">The prompt message for the user.</param>
        public EntitiesJig(IList<Entity> entities, Point3d referencePoint, string promptMessage)
        {
            this.entities = entities;
            this.referencePoint = referencePoint;
            this.movement = new Vector3d(0, 0, 0);
            this.promptMessage = promptMessage;
        }

        /// <summary>
        /// Transforms and draws the entities.
        /// </summary>
        /// <param name="draw">The draw object.</param>
        /// <returns><c>true</c></returns>
        protected override bool WorldDraw(Autodesk.AutoCAD.GraphicsInterface.WorldDraw draw)
        {
            foreach (Entity ent in entities)
            {
                ent.TransformBy(Matrix3d.Displacement(movement));
                draw.Geometry.Draw(ent);
            }
            
            movement = new Vector3d(0, 0, 0);
            return true;
        }

        /// <summary>
        /// Updates the data based on the current mouse position. If there is enough movement the SamplerStatus is set to OK
        /// and the moved vector is saved for modifying the entities.
        /// </summary>
        /// <param name="prompts">The prompts used.</param>
        /// <returns><c>OK</c> if there is enough movement, <c>NoChange</c> if not enough movement, <c>Cancel</c> if input was canceled</returns>
        protected override Autodesk.AutoCAD.EditorInput.SamplerStatus Sampler(Autodesk.AutoCAD.EditorInput.JigPrompts prompts)
        {
            JigPromptPointOptions jigOpt = new JigPromptPointOptions(promptMessage);
            PromptPointResult res = prompts.AcquirePoint(jigOpt);
            if (res.Status != PromptStatus.OK)
                return SamplerStatus.Cancel;
            if (res.Value.IsEqualTo(referencePoint, new Tolerance(0.0001, 0.0001)))
                return SamplerStatus.NoChange;
            movement = referencePoint.GetVectorTo(res.Value);
            referencePoint = res.Value;
            return SamplerStatus.OK;
        }

    }
}
