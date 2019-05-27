using Fotootof.Layouts.Dialogs;
using Fotootof.Navigator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server UI Logs.
    /// Provide display page logs management.
    /// </summary>
    public static class AppLogger
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable queue of logs.
        /// </summary>
        private static Queue logs = new Queue();

        /// <summary>
        /// Variable max number line in queue.
        /// </summary>
        private static int maxLogsLinesLength = 150;

        #endregion Variables



        #region Methods

        /// <summary>
        /// Method to update the <see cref="System.Windows.Controls.TextBox"/> container of the logs.
        /// </summary>
        /// <param name="value">A string value to display in the logs <see cref="System.Windows.Controls.TextBox"/> text.</param>
        public static void UpdateLogTextbox(string value)
        {
            try
            {
                string[] buffer = value.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in buffer)
                {
                    //Trace.WriteLine(s);
                    AddLog(s + "\r\n");
                }
                buffer = null;
            }
            catch (ArgumentException e)
            {
                Trace.TraceError(e.Output());
            }
        }

        /// <summary>
        /// Method to add message to display in log frame.
        /// </summary>
        /// <param name="s">The message to add to the queue.</param>
        public static void AddLog(string s)
        {
            // Enqueue message to display.
            if (logs.Count > maxLogsLinesLength)
            {
                logs.Dequeue();
            }
            logs.Enqueue(s);

            IEnumerable<string> outputs = logs.Cast<string>().Reverse();
            string buffer = "";
            int index = outputs.Count();

            foreach (string str in logs.Cast<string>().Reverse())
            {
                //buffer += $"({(index--).ToString().PadLeft(4, '0')}) {str}";
                buffer += $"- {str}";
            }

            ApplicationBase.BeginInvokeIfRequired(() => { MessageBoxs.BusyContent = s; });
            ApplicationBase.BeginInvokeIfRequired(() => { AppNavigatorBase.LogsStack.Text = buffer; });
        }

        /// <summary>
        /// Method to clear logs.
        /// </summary>
        public static void Clear()
            => logs.Clear();

        #endregion Methods
    }
}
