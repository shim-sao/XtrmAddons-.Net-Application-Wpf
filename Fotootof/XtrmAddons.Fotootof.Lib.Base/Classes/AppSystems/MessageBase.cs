using System;
using System.Diagnostics;
using System.Windows;
using XtrmAddons.Fotootof.Culture;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib Base Classes AppSystems Message Base.
    /// </summary>
    public class MessageBase
    {
        /// <summary>
        /// Method to display a simple message in a dialog box.
        /// </summary>
        /// <param name="s">The message to display.</param>
        public static void Ok(string s, MessageBoxImage img = MessageBoxImage.None)
        {
            ApplicationBase.BeginInvokeIfRequired(MessageBox.Show(s, Translation.DWords.Application, MessageBoxButton.OK, img));
        }

        /// <summary>
        /// Method to display an error message in a dialog box.
        /// </summary>
        /// <param name="s">The message to display.</param>
        public static void Error(string s)
        {
            ApplicationBase.BeginInvokeIfRequired(new Action(() => { MessageBox.Show(s, Translation.DWords.Application, MessageBoxButton.OK, MessageBoxImage.Error); }));
        }

        /// <summary>
        /// Method to display an error message in a dialog box.
        /// </summary>
        /// <param name="e">The exception to add to the message box.</param>
        public static void Error(Exception e)
        {
            ApplicationBase.BeginInvokeIfRequired(MessageBox.Show(e.Output(), Translation.DWords.Application, MessageBoxButton.OK, MessageBoxImage.Error));
        }

        /// <summary>
        /// Method to display a warning message in a dialog box.
        /// </summary>
        /// <param name="s">The message to display.</param>
        public static void Warning(string s)
        {
            ApplicationBase.BeginInvokeIfRequired(MessageBox.Show(s, Translation.DWords.Application, MessageBoxButton.OK, MessageBoxImage.Warning));
        }

        /// <summary>
        /// Method to display a warning message in a dialog box.
        /// </summary>
        /// <param name="s">The message to display.</param>
        public static void Info(string s)
        {
            ApplicationBase.BeginInvokeIfRequired(MessageBox.Show(s, Translation.DWords.Application, MessageBoxButton.OK, MessageBoxImage.Information));
        }

        /// <summary>
        /// Method to add not implemented message to display in log frame.
        /// </summary>
        public static void NotImplemented()
        {
            Error((string)Translation.DWords.FunctionalitiesCurrentlyUnavailable);
        }

        /// <summary>
        /// Method to display a warning message in a dialog box.
        /// Application will be shutdown.
        /// </summary>
        /// <param name="e">The Exception to display.</param>
        /// <param name="s">The message to add before exception.</param>
        public static void Fatal(Exception e, string s = "")
        {
            if(s.IsNotNullOrWhiteSpace())
            {
                s += "\n\r" + e.Output();
            }
            else
            {
                s = e.Output();
            }

            ApplicationBase.BeginInvokeIfRequired(() =>
            {
                MessageBox.Show(s, Translation.DWords.Application, MessageBoxButton.OK, MessageBoxImage.Stop);
                Application.Current.Shutdown();
            });
        }



        #region Methods Debug

        /// <summary>
        /// Method to display a warning message in a dialog box.
        /// Application will be shutdown.
        /// </summary>
        /// <param name="e">The Exception to display.</param>
        /// <param name="s">The message to add before exception.</param>
        [Conditional("DEBUG")]
        public static void DebugFatal(Exception e, string s = "")
        {
            Fatal(e, s);
        }

        #endregion
    }
}
