using log4net.Appender;
using log4net.Core;
using System;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems.Log4net
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Libraries Base Log4net Memory Appender With Events.</para>
    /// </summary>
    /// <example>
    /// In the App.config file, use something like this :
    /// 
    /// appender name="MemoryAppender" type="XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems.Log4net.MemoryAppenderWithEvents, XtrmAddons.Fotootof.Lib.Base"
    ///     layout type = "log4net.Layout.PatternLayout" 
    ///         conversionPattern value="%date %level : %message%newline" /
    ///     /layout
    /// /appender
    /// 
    /// </example>
    public class MemoryAppenderWithEvents : MemoryAppender
    {
        /// <summary>
        /// Update Event handler.
        /// </summary>
        public event EventHandler Updated;

        /// <summary>
        /// Method to append a logging event.
        /// </summary>
        /// <param name="loggingEvent">A logging event to append.</param>
        protected override void Append(LoggingEvent loggingEvent)
        {
            // Append the event as usual.
            base.Append(loggingEvent);

            // Then alert the Updated event that an event has occurred.
            Updated?.Invoke(this, new EventArgs());
        }
    }
}
