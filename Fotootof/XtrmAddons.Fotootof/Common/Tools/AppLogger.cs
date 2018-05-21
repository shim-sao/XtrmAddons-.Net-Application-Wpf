using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using XtrmAddons.Fotootof.Culture;
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
        private static int maxLogsLinesLength = 100;

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
                AppNavigator.MainWindow.Dispatcher.Invoke(new Action(() =>
                {
                    string[] buffer = value.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach(string s in buffer)
                    {
                        AddLog(s+ "\r\n");
                    }
                }));
            }
            catch(Exception e)
            {
                Trace.WriteLine(string.Format("{0} : {1}", e.HResult, e.Message));
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
                buffer += string.Format("[{0}]> {1}", index--, str);
            }

            AppOverwork.BusyContent = s;
            AppNavigator.LogsStack.Text = buffer;
        }

        /// <summary>
        /// Method to clear logs.
        /// </summary>
        public static void Clear()
            => logs.Clear();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        [Obsolete("Use log4net instead.", true)]
        public static void DispatchLogs(string s)
        {
            AppOverwork.IsBusy = true;

            AppNavigator.MainWindow.Dispatcher.Invoke(new Action(() =>
            {
                AppOverwork.BusyContent = s;
                LogsDisplay("INFO : " + s);
            }));
        }

        /// <summary>
        /// Method to add message to display in log frame.
        /// </summary>
        /// <param name="s">The message to add to queue.</param>
        [Obsolete("Use AddLog(string s)", true)]
        public static void LogsDisplay(string s)
        {
            AddLog(s);
            return;

            //// Enqueue message to display.
            //if (logs.Count > 100)
            //{
            //    logs.Dequeue();
            //}
            //logs.Enqueue(s);
            
            //string output = "";
            //int index = logs.Cast<string>().Reverse().Count();
            //foreach (string str in logs.Cast<string>().Reverse())
            //{
            //    output += string.Format("[{0}] > {1}", index--, str);
            //}

            //AppNavigator.LogsStack.Text = output;
        }

        /// <summary>
        /// Method to add info message to display in log frame.
        /// </summary>
        /// <param name="s">The message to add to queue.</param>
        /// <param name="log4net">Add message to logs handler.</param>
        [Obsolete("Use log4net instead.", true)]
        public static async void Info(string s, bool log4net = false, int delay = 0)
        {
            if (log4net)
            {
                log.Info(s);
            }

            DispatchLogs(s);
            await Task.Delay(delay);
        }

        /// <summary>
        /// Method to add info message to display in log frame.
        /// </summary>
        /// <param name="s">The message to add to queue.</param>
        /// <param name="log4net">Add message to logs handler.</param>
        [Obsolete("Use log4net instead.", true)]
        public static void InfoAndClose(string s, bool log4net = false, int delay = 0)
        {
            Info(s, log4net, delay);
            AppOverwork.IsBusy = false;
        }

        /// <summary>
        /// Method to add Error message to display in a dialog box.
        /// </summary>
        /// <param name="s">The message to add to queue.</param>
        public static void Error(string s)
        {
            MessageBox.Show(s, Translation.DWords.Application, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Method to add Error message to display in a dialog box.
        /// </summary>
        /// <param name="e">The exception to add to the message box.</param>
        public static void Error(Exception e)
        {
            MessageBox.Show(e.Output(), Translation.DWords.Application, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Method to add not implemented message to display in log frame.
        /// </summary>
        public static void NotImplemented()
        {
            Error("Not Implemented Exception");
        }

        /// <summary>
        /// Method to add Warning message to display in log frame.
        /// </summary>
        /// <param name="s">The message to add to queue.</param>
        public static void Warning(string s)
        {
            MessageBox.Show(s, Translation.DWords.Application, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        /// <summary>
        /// Method to add fatal message to display in log frame.
        /// </summary>
        /// <param name="s">The message to add to queue.</param>
        /// <param name="e">The Exception to add to queue.</param>
        public static void Fatal(string s, Exception e)
        {
            s += "\n\r" + e.Output();
            MessageBox.Show(s, Translation.DWords.Application, MessageBoxButton.OK, MessageBoxImage.Stop);
        }

        /// <summary>
        /// Method to add message to display in log frame.
        /// </summary>
        /// <param name="s">The message to add to queue.</param>
        /// <param name="e">The Exception to add to queue.</param>
        /// <param name="log4net">Add message to logs handler.</param>
        [Obsolete("Use log4net instead.", true)]
        public static void Exception(string s, Exception e, bool log4net = false)
        {
            LogsDisplay("EXCEPTION : " + s);
            LogsDisplay("EXCEPTION : " + e.ToString());
            LogsDisplay("EXCEPTION : " + e.Message);
            LogsDisplay(e.StackTrace);


            if (log4net)
            {
                log.Fatal("EXCEPTION : " + s);
                log.Fatal("EXCEPTION : " + e.ToString());
                log.Fatal("EXCEPTION : " + e.Message);
                log.Fatal(e.StackTrace);
            }

            MessageBox.Show(s, Translation.DWords.Application, MessageBoxButton.OK, MessageBoxImage.Stop);
        }

        /// <summary>
        /// Method proxy to close busy indicator.
        /// </summary>
        [Obsolete("use Translation.DLogs")]
        public static string Translate(string key)
            => (string)Translation.Logs[key];

        #endregion Methods
    }
}
