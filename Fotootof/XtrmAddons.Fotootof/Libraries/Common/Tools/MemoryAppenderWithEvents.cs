using log4net.Appender;
using System;

namespace XtrmAddons.Fotootof.Libraries.Common.Tools
{
    /// <summary>
    /// 
    /// </summary>
    public class MemoryAppenderWithEvents : MemoryAppender
    {
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler Updated;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggingEvent"></param>
        protected override void Append(log4net.Core.LoggingEvent loggingEvent)
        {
            // Append the event as usual
            base.Append(loggingEvent);

            // Then alert the Updated event that an event has occurred
            Updated?.Invoke(this, new EventArgs());
        }
    }
}
