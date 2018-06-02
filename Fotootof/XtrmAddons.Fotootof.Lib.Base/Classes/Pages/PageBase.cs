using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Lib.Base.Interfaces;
using XtrmAddons.Fotootof.SQLiteService;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Pages
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Base Page.
    /// </summary>
    public abstract partial class PageBase : Page, IContentInitializer, ISizeChanged
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable page width marging for content adjustement on size changed.
        /// </summary>
        public double MargingWidth { get; set; } = 0; // SystemParameters.VerticalScrollBarWidth

        /// <summary>
        /// Variable page height marging for content adjustement on size changed.
        /// </summary>
        public double MargingHeight { get; set; } = 0; // SystemParameters.HorizontalScrollBarHeight

        /// <summary>
        /// 
        /// </summary>
        public static object AppWindow 
            = ApplicationSession.Properties.MainWindow;

        #endregion



        #region Properties

        /// <summary>
        /// Property alias to access to the main database connector.
        /// </summary>
        public static SQLiteSvc Db
            => (SQLiteSvc)ApplicationSession.Properties.Database;

        /// <summary>
        /// Property alias to access to the dynamic translation words.
        /// </summary>
        public dynamic DWords => Culture.Translation.DWords;

        /// <summary>
        /// Property alias to access to the dynamic translation logs.
        /// </summary>
        public dynamic DLogs => Culture.Translation.DLogs;

        #endregion



        #region Methods Abstracts

        /// <summary>
        /// Method to initialize page data model.
        /// </summary>
        public abstract void InitializeModel();

        /// <summary>
        /// Method to initialize and display data context.
        /// </summary>
        public abstract void Control_Loaded(object sender, RoutedEventArgs e);

        /// <summary>
        /// Method called on page size changed event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Size changed event arguments.</param>
        public abstract void Control_SizeChanged(object sender, SizeChangedEventArgs e);

        /// <summary>
        /// Method to initialize and display data context.
        /// </summary>
        [Obsolete("Will be remove. None sense...")]
        public abstract void Page_Loaded_Async(object sender, RoutedEventArgs e);

        #endregion



        #region Methods

        /// <summary>
        /// Method called after required component initialized.
        /// </summary>
        protected void AfterInitializedComponent()
        {
            Loaded += Control_Loaded;

            // Initialize for the window size changed event.
            SizeChanged += PageBase_SizeChanged;
            AppWindow.GetPropertyValue<Border>("BlockContent").SizeChanged += PageBase_SizeChanged;

            // Merge main resources.
            Resources.MergedDictionaries.Add(((Window)AppWindow).Resources);
        }

        /// <summary>
        /// Method called on window sized changed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Size changed event arguments.</param>
        protected void PageBase_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Get main window page container dimensions.
            Border win = AppWindow.GetPropertyValue<Border>("BlockContent");
            TraceSize(win);
            
            // Resize page to fit container.
            Width = Math.Max(win.ActualWidth - MargingWidth, 0);
            Height = Math.Max(win.ActualHeight - MargingHeight, 0);
            TraceSize(this);
        }

        /// <summary>
        /// Method to display picture file dialog box selector.
        /// </summary>
        /// <param name="multiselect">Multiple selection enabled ?. False by default. Optional.</param>
        /// <returns>A picture file dialog box selector, null if canceled.</returns>
        protected OpenFileDialog PictureFileDialogBox(bool multiselect = false)
        {
            // Configure open file dialog box 
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = ""
            };

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            string sep = string.Empty;

            foreach (var c in codecs)
            {
                string codecName = c.CodecName.Substring(8).Replace("Codec", "Files").Trim();
                dlg.Filter = string.Format("{0}{1}{2} ({3})|{3}", dlg.Filter, sep, codecName, c.FilenameExtension);
                sep = "|";
            }
            
            dlg.Filter = string.Format("{0}{1}{2} ({3})|{3}", dlg.Filter, sep, "All Files", "*.*");
            dlg.DefaultExt = ".JPG"; // Default file extension 
            dlg.FilterIndex = 2;
            dlg.Multiselect = multiselect;
            dlg.Title = Culture.Translation.DWords.DialogBoxTitle_PictureFileSelector;
            
            // Show open file dialog box 
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                return dlg;
            }

            return null;
        }

        #endregion



        #region Methods Debug

        /// <summary>
        /// Method called on window sized changed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Size changed event arguments.</param>
        [Conditional("DEBUG")]
        protected void TraceSize(FrameworkElement fe)
        {
            #if DEBUG_SIZE

            Trace.WriteLine(string.Format("----> Class({0}) : Object({1}) : Name({2})", GetType().Name, fe.GetType().Name, fe.Name));
            Trace.WriteLine("ActualSize = [" + fe.ActualWidth + "," + fe.ActualHeight + "]");
            Trace.WriteLine("Size = [" + fe.Width + "," + fe.Height + "]");
            Trace.WriteLine("RenderSize = [" + fe.RenderSize.Width + "," + fe.RenderSize.Height + "]");
            Trace.WriteLine("-------------------------------------------------------------------------------------------------------");

            #endif
        }

        #endregion
    }
}
