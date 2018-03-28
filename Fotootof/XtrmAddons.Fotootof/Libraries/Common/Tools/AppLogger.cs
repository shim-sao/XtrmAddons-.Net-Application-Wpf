using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using XtrmAddons.Fotootof.Culture;

namespace XtrmAddons.Fotootof.Libraries.Common.Tools
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

        #endregion Variables



        #region Methods

        /// <summary>
        /// Method to add message to display in log frame.
        /// </summary>
        /// <param name="s">The message to add to queue.</param>
        public static void LogsDisplay(string s)
        {
            // Enqueue message to display.
            if (logs.Count > 100)
            {
                logs.Dequeue();
            }
            logs.Enqueue(s);

            string output = "";
            foreach (string str in logs.Cast<string>().Reverse())
            {
                output += "\n> " + str;
            }

            AppNavigator.LogsStack.Text = output;
        }

        /// <summary>
        /// Method to add info message to display in log frame.
        /// </summary>
        /// <param name="s">The message to add to queue.</param>
        /// <param name="log4net">Add message to logs handler.</param>
        public static async void Info(string s, bool log4net = false, int delay = 1000)
        {
            if (log4net)
            {
                log.Info(s);
            }

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                AppOverwork.IsBusy = true;
                AppOverwork.BusyContent = s;
                
                LogsDisplay("INFO : " + s);
            }));

            await Task.Delay(delay);
        }

        /// <summary>
        /// Method to add Error message to display in log frame.
        /// </summary>
        /// <param name="s">The message to add to queue.</param>
        /// <param name="log4net">Add message to logs handler.</param>
        public static void Error(string s, bool log4net = false)
        {
            LogsDisplay("ERROR : " + s);

            if (log4net)
            {
                log.Error("ERROR : " + s);
            }

            MessageBox.Show(s, Translation.DWords.Application, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Method to add Warning message to display in log frame.
        /// </summary>
        /// <param name="s">The message to add to queue.</param>
        /// <param name="log4net">Add message to logs handler.</param>
        public static void Warning(string s, bool log4net = false, bool messageBox = false)
        {
            LogsDisplay("WARNING : " + s);

            if (log4net)
            {
                log.Error("ERROR : " + s);
            }

            if (messageBox)
            {
                MessageBox.Show(s, Translation.DWords.Application, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Method to add fatal message to display in log frame.
        /// </summary>
        /// <param name="s">The message to add to queue.</param>
        /// <param name="e">The Exception to add to queue.</param>
        /// <param name="log4net">Add message to logs handler.</param>
        public static void Fatal(string s, Exception e, bool log4net = false)
        {
            LogsDisplay("FATAL : " + s);
            LogsDisplay(string.Format("{0} : {1}", e.HResult, e.Message));
            LogsDisplay(e.TargetSite.Name);
            LogsDisplay(e.Source);
            LogsDisplay(e.StackTrace);

            if (log4net)
            {
                log.Fatal("FATAL : " + s);
                log.Fatal(string.Format("{0} : {1}", e.HResult, e.Message));
                log.Fatal(e.TargetSite);
                log.Fatal(e.Source);
                log.Fatal(e.StackTrace);
            }

            MessageBox.Show(s, Translation.DWords.Application, MessageBoxButton.OK, MessageBoxImage.Stop);
        }

        /// <summary>
        /// Method to add message to display in log frame.
        /// </summary>
        /// <param name="s">The message to add to queue.</param>
        /// <param name="e">The Exception to add to queue.</param>
        /// <param name="log4net">Add message to logs handler.</param>
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
        /// Method to clear logs.
        /// </summary>
        public static void Clear() => logs.Clear();

        /// <summary>
        /// Method proxy to close busy indicator.
        /// </summary>
        public static void Close() => AppOverwork.IsBusy = false;

        /// <summary>
        /// Method proxy to close busy indicator.
        /// </summary>
        public static string Translate(string key)
        {
            return (string)Translation.Logs[key];
        }

        /// <summary>
        /// Method to add info message to display in log frame.
        /// </summary>
        /// <param name="s">The message to add to queue.</param>
        /// <param name="log4net">Add message to logs handler.</param>
        public static void InfoAndClose(string s, bool log4net = false, int delay = 1000)
        {
            Info(s, log4net, delay);
            Close();
        }

        #endregion Methods
    }
}
