﻿using System;
using System.Diagnostics;
using System.Windows;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.Libraries.System.Messages
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib Base Classes AppSystems Message Base.
    /// </summary>
    public class MessageBase
    {
        /// <summary>
        /// Variable to store the busy indicator <see cref="Xceed.Wpf.Toolkit.BusyIndicator"/>
        /// </summary>
        private static Xceed.Wpf.Toolkit.BusyIndicator busyIndicator;

        /// <summary>
        /// Property to access to the main window busy indicator <see cref="Xceed.Wpf.Toolkit.BusyIndicator"/>.
        /// </summary>
        public static Xceed.Wpf.Toolkit.BusyIndicator BusyIndicator
        {
            get
            {
                if(busyIndicator == null)
                {
                    ApplicationBase.BeginInvokeIfRequired(new Action(() =>
                    {
                        busyIndicator = Application.Current.MainWindow.FindName("BusyIndicator") as Xceed.Wpf.Toolkit.BusyIndicator;
                    }));
                }

                return busyIndicator;
            }
            set
            {
                busyIndicator = value;
            }
        }

        /// <summary>
        /// Property to enable or disable the busy indicator  <see cref="Xceed.Wpf.Toolkit.BusyIndicator"/>.
        /// </summary>
        public static bool IsBusy
        {
            get => BusyIndicator.IsBusy;
            set => ApplicationBase.BeginInvokeIfRequired(() => { BusyIndicator.IsBusy = value; });
        }

        /// <summary>
        /// 
        /// </summary>
        public static object BusyContent
        {
            get => BusyIndicator.BusyContent;
            set => ApplicationBase.BeginInvokeIfRequired(() => { BusyIndicator.BusyContent = value; });
        }

        /// <summary>
        /// Method to display a simple message in a dialog box.
        /// </summary>
        /// <param name="s">The message to display.</param>
        /// <param name="title"></param>
        /// <param name="img"></param>
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
        /// <param name="title"></param>
        /// <param name="img"></param>
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
        /// <param name="title"></param>
        public static void Error(string s, string title = "Error")
        {
            Ok(s, title, MessageBoxImage.Error);
        }

        /// <summary>
        /// Method to display an error message in a dialog box.
        /// </summary>
        /// <param name="e">The exception to add to the message box.</param>
        public static void Error(Exception e)
        {
            Ok(e.Output(), e.GetType().Name, MessageBoxImage.Error);
        }

        /// <summary>
        /// Method to display a warning message in a dialog box.
        /// </summary>
        /// <param name="s">The message to display.</param>
        /// <param name="title"></param>
        public static void Warning(string s, string title = "Warning")
        {
            Ok(s, title, MessageBoxImage.Warning);
        }

        /// <summary>
        /// Method to display a warning message in a dialog box.
        /// </summary>
        /// <param name="s">The message to display.</param>
        /// <param name="title"></param>
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

            ApplicationBase.BeginInvokeIfRequired(new Action(() =>
            {
                MessageBox.Show(s, Local.Properties.Translations.ApplicationName, MessageBoxButton.OK, MessageBoxImage.Stop);
                Application.Current.Shutdown();
            }));
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
