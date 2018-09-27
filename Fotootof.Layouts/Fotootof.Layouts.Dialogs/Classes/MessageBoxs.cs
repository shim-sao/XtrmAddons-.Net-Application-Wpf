using System;
using System.Diagnostics;
using System.Windows;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.Layouts.Dialogs
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib Base Classes AppSystems Message Base.
    /// </summary>
    public class MessageBoxs
    {
        #region Variables
        
        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
        	log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable busy indicator.
        /// </summary>
        private static Xceed.Wpf.Toolkit.BusyIndicator busyIndicator;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the <see cref="Application"/> <see cref="Xceed.Wpf.Toolkit.BusyIndicator"/>.
        /// </summary>
        public static Xceed.Wpf.Toolkit.BusyIndicator BusyIndicator
        {
            get
            {
                if (busyIndicator == null)
                {
                    ApplicationBase.BeginInvokeIfRequired(new Action(() =>
                    {
                        busyIndicator = Application.Current.MainWindow.GetPropertyValue<Xceed.Wpf.Toolkit.BusyIndicator>("BusyIndicator");
                    }));
                }

                return busyIndicator;
            }
            set => busyIndicator = value;
        }

        /// <summary>
        /// Property to check if the <see cref="Application"/> is busy.
        /// </summary>
        public static bool IsBusy
        {
            get => BusyIndicator.IsBusy;
            set => ApplicationBase.BeginInvokeIfRequired(() => { BusyIndicator.IsBusy = value; });
        }

        /// <summary>
        /// Property to access to the <see cref="Xceed.Wpf.Toolkit.BusyIndicator"/> content.
        /// </summary>
        public static object BusyContent
        {
            get => BusyIndicator.BusyContent;
            set => ApplicationBase.BeginInvokeIfRequired(() => { BusyIndicator.BusyContent = value; });
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to display a simple message in a dialog box.
        /// </summary>
        /// <param name="s">The message to display.</param>
        /// <param name="title">The title of the <see cref="MessageBox"/></param>
        /// <param name="img">The icon <see cref="MessageBoxImage"/> of the <see cref="MessageBox"/></param>
        public static void Ok(string s, string title = "", MessageBoxImage img = MessageBoxImage.None)
        {
            ApplicationBase.BeginInvokeIfRequired(new Action(() =>
            {
                MessageBox.Show(s, $"{Local.Properties.Translations.ApplicationName} - {title}", MessageBoxButton.OK, img);
            }));
        }

        /// <summary>
        /// Method to display a simple message in a dialog box.
        /// </summary>
        /// <param name="s">The message to display.</param>
        /// <param name="title">The title of the <see cref="MessageBox"/></param>
        /// <param name="img">The icon <see cref="MessageBoxImage"/> of the <see cref="MessageBox"/></param>
        public static MessageBoxResult YesNo(string s, string title = "", MessageBoxImage img = MessageBoxImage.Question)
        {
            MessageBoxResult result = MessageBoxResult.None;
            ApplicationBase.BeginInvokeIfRequired(new Action(() =>
            {
                result = MessageBox.Show(s, $"{Local.Properties.Translations.ApplicationName} - {title}", MessageBoxButton.YesNo, img);
            }));
            return result;
        }

        /// <summary>
        /// Method to display an error message in a dialog box.
        /// </summary>
        /// <param name="s">The message to display.</param>
        /// <param name="title">The title of the <see cref="MessageBox"/></param>
        public static void Error(string s, string title = "Error")
        {
            Ok(s, title, MessageBoxImage.Error);
        }

        /// <summary>
        /// Method to display an error message in a dialog box.
        /// </summary>
        /// <param name="e">The <see cref="Exception"/> to add to the <see cref="MessageBox"/>.</param>
        public static void Error(Exception e)
        {
            Ok(e.Message, e.GetType().Name, MessageBoxImage.Error);
        }

        /// <summary>
        /// Method to display a warning message in a dialog box.
        /// </summary>
        /// <param name="s">The message to display.</param>
        /// <param name="title">The title of the <see cref="MessageBox"/></param>
        public static void Warning(string s, string title = "Warning")
        {
            Ok(s, title, MessageBoxImage.Warning);
        }

        /// <summary>
        /// Method to display a warning message in a dialog box.
        /// </summary>
        /// <param name="s">The message to display.</param>
        /// <param name="title">The title of the <see cref="MessageBox"/></param>
        public static void Info(string s, string title = "Information")
        {
            Ok(s, title, MessageBoxImage.Information);
        }

        /// <summary>
        /// Method to add not implemented message to display in log frame.
        /// </summary>
        public static void NotImplemented()
        {
            Error(Properties.Translations.FunctionalitiesCurrentlyUnavailable);
        }

        /// <summary>
        /// Method to add not implemented message to display in log frame.
        /// </summary>
        public static void NotImplemented(string title)
        {
            Error(Properties.Translations.FunctionalitiesCurrentlyUnavailable, title);
        }

        /// <summary>
        /// Method to display a warning message in a dialog box.
        /// Application will be shutdown.
        /// </summary>
        /// <param name="e">The <see cref="Exception"/> to display.</param>
        /// <param name="s">The message to add before <see cref="Exception"/>.</param>
        /// <param name="shutdown">True to shutdown the <see cref="Application"/>, false by default.</param>
        public static void Fatal(Exception e, string s = "", bool shutdown = false)
        {
            if(s.IsNotNullOrWhiteSpace())
            {
                s += "\n\r" + e.Output();
            }
            else
            {
                s = e.Output();
            }

            if(shutdown)
            {
                ApplicationBase.BeginInvokeIfRequired(new Action(() =>
                {
                    MessageBox.Show(s, Local.Properties.Translations.ApplicationName, MessageBoxButton.OK, MessageBoxImage.Error);
                    MessageBox.Show("The application will be shutdown", Local.Properties.Translations.ApplicationName, MessageBoxButton.OK, MessageBoxImage.Stop);
                    Application.Current.Shutdown();
                }));
            }
        }
        
        #endregion



        #region Methods Debug

        /// <summary>
        /// Method to display a warning message in a dialog box.
        /// Application will be shutdown.
        /// </summary>
        /// <param name="e">The <see cref="Exception"/> to display.</param>
        /// <param name="s">A custom message to add before the <see cref="Exception"/>.</param>
        [Conditional("DEBUG")]
        public static void DebugFatal(Exception e, string s = "")
        {
            Fatal(e, s);
        }

        #endregion
    }
}