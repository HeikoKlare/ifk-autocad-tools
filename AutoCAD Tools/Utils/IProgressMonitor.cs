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
        /// The current progress of the monitored action between 0 and 1. Setting a value outside of 0 to 1 trims the value to that interval.
        /// </summary>
        double Progress
        {
            get;
            set;
        }

        /// <summary>
        /// Updates the description for the current action of the monitored action.
        /// </summary>
        /// <param name="actionDescription">the description of the current action, must not be <code>null</code></param>
        void SetCurrentActionDescription(string actionDescription);
    }       
}
