using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using XtrmAddons.Fotootof.Culture;
using XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Common.Tools
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server UI Logs.
    /// Provide display page logs management.
    /// </summary>
    public static class AppLogger
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable queue of logs.
        /// </summary>
        private static Queue logs = new Queue();

        /// <summary>
        /// 
        /// </summary>
        private static int maxLogsLinesLength = 150;

        #endregion Variables



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static void UpdateLogTextbox(string value)
        {
            try
            {
                string[] buffer = value.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in buffer)
                {
                    Trace.TraceInformation(s);
                    AddLog(s + "\r\n");
                }
            }
            catch(Exception e)
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
                buffer += $"({(index--).ToString().PadLeft(4, '0')}) {str}";
            }

            ApplicationBase.BeginInvokeIfRequired(() => { MessageBase.BusyContent = s; });
            ApplicationBase.BeginInvokeIfRequired(() => { AppNavigator.LogsStack.Text = buffer; });
        }

        /// <summary>
        /// Method to clear logs.
        /// </summary>
        public static void Clear()
            => logs.Clear();

        /// <summary>
        /// Method to add Error message to display in a dialog box.
        /// </summary>
        /// <param name="s">The message to add to queue.</param>
        [Obsolete("use MessageBase.Error(s)")]
        public static void Error(string s)
        {
            MessageBox.Show(s, Translation.DWords.Application, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Method to add Error message to display in a dialog box.
        /// </summary>
        /// <param name="e">The exception to add to the message box.</param>
        [Obsolete("use MessageBase.Error(s)")]
        public static void Error(Exception e)
        {
            MessageBox.Show(e.Output(), Translation.DWords.Application, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Method to add not implemented message to display in log frame.
        /// </summary>
        [Obsolete("use MessageBase.NotImplemented()")]
        public static void NotImplemented()
        {
            MessageBase.NotImplemented();
        }

        /// <summary>
        /// Method to add Warning message to display in log frame.
        /// </summary>
        /// <param name="s">The message to add to queue.</param>
        [Obsolete("use MessageBase.Warning(s)")]
        public static void Warning(string s)
        {
            MessageBox.Show(s, Translation.DWords.Application, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        /// <summary>
        /// Method to add fatal message to display in log frame.
        /// </summary>
        /// <param name="s">The message to add to queue.</param>
        /// <param name="e">The Exception to add to queue.</param>
        [Obsolete("use MessageBase.Fatal(e, s)")]
        public static void Fatal(string s, Exception e)
        {
            s += "\n\r" + e.Output();
            MessageBox.Show(s, Translation.DWords.Application, MessageBoxButton.OK, MessageBoxImage.Stop);
        }

        #endregion Methods
    }
}
