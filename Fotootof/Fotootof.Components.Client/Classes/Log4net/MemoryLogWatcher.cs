using log4net;
using log4net.Appender;
using log4net.Core;
using System;
using System.Diagnostics;
using System.Text;

namespace Fotootof.Components.Client.Log4net
{
    /// <summary>
    /// <para>Class Fotootof Libraries Log Watcher.</para>
    /// </summary>
    public class MemoryLogWatcher
    {
        #region Variables

        /// <summary>
        /// Variable to store the memory appender configurator <see cref="MemoryAppenderWithEvents"/>.
        /// </summary>
        private MemoryAppenderWithEvents memoryAppender;

        #endregion



        #region Event Handler

        /// <summary>
        /// Event handler <see cref="EventHandler"/> to handle updated memory appender log event.
        /// </summary>
        public event EventHandler Updated;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the log content.
        /// </summary>
        public string LogContent { get; set; }

        #endregion



        #region Methods

        /// <summary>
        /// Method to whatch <see cref="MemoryAppenderWithEvents"/> logs events.
        /// </summary>
        public MemoryLogWatcher()
        {
            // Get the memory appender
            memoryAppender = (MemoryAppenderWithEvents)Array.Find(LogManager.GetRepository().GetAppenders(), IsMemoryAppender);
            if (memoryAppender == null)
            {
                throw new NullReferenceException("Log4net memory appender not found !");
            }

            // Read in the log content
            LogContent = GetEvents(memoryAppender);

            // Add an event handler to handle updates from the MemoryAppender
            memoryAppender.Updated += HandleUpdate;
        }

        /// <summary>
        /// Method to handle the <see cref="MemoryAppenderWithEvents"/> update event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The event arguments <see cref="EventArgs"/></param>
        public void HandleUpdate(object sender, EventArgs e)
        {
            LogContent += GetEvents(memoryAppender);

            // Then alert the Updated event that the LogWatcher has been updated
            Updated?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Method to check if an appender is <see cref="MemoryAppenderWithEvents"/> named MemoryAppender
        /// </summary>
        /// <param name="appender">An appender interface <see cref="IAppender"/></param>
        /// <returns>True if appender name is MemoryAppender, otherwise false.</returns>
        private static bool IsMemoryAppender(IAppender appender)
        {
            // Returns the IAppender named MemoryAppender in the Log4Net.config file
            if (appender.Name.Equals("MemoryAppender"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Method te get the events formated into a string.
        /// </summary>
        /// <param name="memoryAppender">The memory appender with logs events <see cref="MemoryAppenderWithEvents"/></param>
        /// <returns>The events formated into a string.</returns>
        public string GetEvents(MemoryAppenderWithEvents memoryAppender)
        {
            StringBuilder output = new StringBuilder();

            // Get any events that may have occurred
            LoggingEvent[] events = memoryAppender.GetEvents();

            // Check that there are events to return
            if (events != null && events.Length > 0)
            {
                // If there are events, we clear them from the logger, since we're done with them  
                memoryAppender.Clear();

                // Iterate through each event
                foreach (LoggingEvent ev in events)
                {
#if DEBUG
                    // Construct the line we want to trace
                    Trace.WriteLine($"{ev.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss,fff")} [{ev.ThreadName}] {ev.Level} {ev.LoggerName} : {ev.RenderedMessage}");

                    // Append to the StringBuilder
                    output.Append($"{ev.TimeStamp.ToString("HH:mm:ss,fff")} {ev.Level} : {ev.RenderedMessage}\r\n");
#else
                    if (ev.Level >= Level.Warn)
                    {
                        // Construct the line we want to trace
                        Trace.WriteLine($"{ev.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss,fff")} [{ev.ThreadName}] {ev.Level} {ev.LoggerName} : {ev.RenderedMessage}");

                        // Append to the StringBuilder
                        output.Append($"{ev.RenderedMessage}\r\n");
                    }
#endif
                }
            }

            // Return the constructed output
            return output.ToString();
        }

        #endregion

    }
}