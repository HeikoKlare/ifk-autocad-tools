using System;

namespace AutoCADTools.Utils
{
    /// <summary>
    /// A monitor for the progress of an action. Can be used as a callback to be given to an action whose progress can be monitored.
    /// </summary>
    public interface IProgressMonitor : IDisposable
    {
        /// <summary>
        /// The current progress of the monitored action between 0 and 1. Setting a value outside of 0 to 1 trims the value to that interval.
        /// </summary>
        double Progress { get; set; }

        /// <summary>
        /// The title of the progress monitor, e.g., the title of a dialog window.
        /// </summary>
        string Title { set; }

        /// <summary>
        /// The main text for the progress monitor. Should not change during the process.
        /// </summary>
        string MainText { set; }

        /// <summary>
        /// The description of the current action. May change during the process.
        /// </summary>
        string CurrentActionDescription { set; }

        /// <summary>
        /// Indicates that the monitored process is started.
        /// </summary>
        void Start();
    }

    class NullProgressMonitor : IProgressMonitor
    {
        private double _progress;
        public double Progress { get => _progress; set => _progress = value < 0 ? 0 : value > 1 ? 1 : value; }
        public string Title { set { } }
        public string MainText { set { } }
        public string CurrentActionDescription { set { } }

        public void Dispose()
        {
            // Do nothing
        }

        public void Start()
        {
            // Do nothing
        }
    }
}
