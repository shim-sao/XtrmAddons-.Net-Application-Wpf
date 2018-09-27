using Fotootof.Layouts.Interfaces;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Fotootof.SQLite.Services;
using XtrmAddons.Net.Application;

namespace Fotootof.Libraries.Components
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Component View.
    /// </summary>
    public abstract partial class ComponentView : Page, IContentInitializer, ISizeChanged
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Properties

        /// <summary>
        /// Property alias to access to the main database service connector <see cref="SQLiteSvc"/>.
        /// </summary>
        public static SQLiteSvc Db
            => (SQLiteSvc)ApplicationSession.Properties.Database;

        /// <summary>
        /// Property alias to access to the MainWindow.
        /// </summary>
        public static object AppWindow 
            => Application.Current.MainWindow;

        /// <summary>
        /// Property to access to the MainWindow content Block.
        /// </summary>
        public static FrameworkElement MainBlockContent
            => (AppWindow as Window).FindName("BlockContent") as FrameworkElement;

        /// <summary>
        /// Variable page width marging for content adjustement on size changed.
        /// </summary>
        public double MargingWidth { get; set; } = 0; // SystemParameters.VerticalScrollBarWidth

        /// <summary>
        /// Variable page height marging for content adjustement on size changed.
        /// </summary>
        public double MargingHeight { get; set; } = 0; // SystemParameters.HorizontalScrollBarHeight

        #endregion



        #region Methods Abstracts

        /// <summary>
        /// Method to initialize the model <see cref="ComponentModel{T}"/>.
        /// </summary>
        public abstract void InitializeModel();

        /// <summary>
        /// Method to add to data to DataContext to display the component content when it is loaded.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/></param>
        public abstract void Control_Loaded(object sender, RoutedEventArgs e);

        /// <summary>
        /// Method called on <see cref="FrameworkElement"/> size changed event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The size changed event arguments <see cref="SizeChangedEventArgs"/>.</param>
        public abstract void Layout_SizeChanged(object sender, SizeChangedEventArgs e);

        #endregion



        #region Methods

        /// <summary>
        /// Method to find an child element by its name.
        /// </summary>
        /// <typeparam name="T">The Type of element to get.</typeparam>
        /// <param name="name">The name of the element to searh.</param>
        /// <returns>The lement to be found or null.</returns>
        public T FindName<T>(string name) where T : class
        {
            return (T)FindName(name);
        }

        /// <summary>
        /// Method called after required component initialized.
        /// </summary>
        protected void AfterInitializedComponent()
        {
            InitializeModel();

            // Initialize for the window size changed event.
            MainBlockContent.SizeChanged += PageBase_SizeChanged;

            // Merge main resources.
            Resources.MergedDictionaries.Add(((Window)AppWindow).Resources);
        }

        /// <summary>
        /// Method called on <see cref="Page"/> sized changed.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The size changed event arguments <see cref="SizeChangedEventArgs"/>.</param>
        public void PageBase_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Get main window page container dimensions.
            TraceSize(MainBlockContent);
            
            // Resize page to fit container.
            Width = Math.Max(MainBlockContent.ActualWidth - MargingWidth, 0);
            Height = Math.Max(MainBlockContent.ActualHeight - MargingHeight, 0);
            TraceSize(this);
        }

        #endregion



        #region Methods Debug

        /// <summary>
        /// Method to trace a <see cref="FrameworkElement"/> size on debug.
        /// </summary>
        /// <param name="fe">A <see cref="FrameworkElement"/>.</param>
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
