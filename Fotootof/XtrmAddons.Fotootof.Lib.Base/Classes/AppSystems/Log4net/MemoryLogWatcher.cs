using log4net;
using log4net.Appender;
using log4net.Core;
using System;
using System.Text;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems.Log4net
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Libraries Common.Tools Application Log Watcher.</para>
    /// </summary>
    public class MemoryLogWatcher
    {
        /// <summary>
        /// 
        /// </summary>
        private MemoryAppenderWithEvents memoryAppender;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler Updated;

        /// <summary>
        /// 
        /// </summary>
        public string LogContent { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public MemoryLogWatcher()
        {
            // Get the memory appender
            memoryAppender = (MemoryAppenderWithEvents)Array.Find(LogManager.GetRepository().GetAppenders(), GetMemoryAppender);

            if(memoryAppender == null)
            {
                throw new NullReferenceException("Log4net memory appender not found !");
            }
            
            // Read in the log content
            LogContent = GetEvents(memoryAppender);

            // Add an event handler to handle updates from the MemoryAppender
            memoryAppender.Updated += HandleUpdate;
        }

        public void HandleUpdate(object sender, EventArgs e)
        {
            LogContent += GetEvents(memoryAppender);

            // Then alert the Updated event that the LogWatcher has been updated
            Updated?.Invoke(this, new EventArgs());
        }

        private static bool GetMemoryAppender(IAppender appender)
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
                    // Construct the line we want to log
                    // string line = ev.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss,fff") + " [" + ev.ThreadName + "] " + ev.Level + " " + ev.LoggerName + ": " + ev.RenderedMessage + "\r\n";
                    string line = ev.TimeStamp.ToString("HH:mm:ss,fff") + " " + ev.Level + " : " + ev.RenderedMessage + "\r\n";

                    // Append to the StringBuilder
                    output.Append(line);
                }
            }

            // Return the constructed output
            return output.ToString();
        }
    }
}