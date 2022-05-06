using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCADTools.Utils
{
    /// <summary>
    /// A monitor for the progress of an action. Can be used as a callback to be given to an action whose progress can be monitored.
    /// </summary>
    interface IProgressMonitor : IDisposable
    {
        /// <summary>
        /// Updates the description for the current action of the monitored action.
        /// </summary>
        /// <param name="actionDescription">the description of the current action, must not be <code>null</code></param>
        void SetCurrentActionDescription(string actionDescription);

        /// <summary>
        /// Updates the progress of the monitored action. The given value will be trimmed to the valid interval
        /// between 0 and 1.
        /// </summary>
        /// <param name="value">the current progress value between 0 and 1</param>
        void SetProgress(double value);

        /// <summary>
        /// Indicates that the monitored action has been finished.
        /// </summary>
        void Finish();
    }
}
