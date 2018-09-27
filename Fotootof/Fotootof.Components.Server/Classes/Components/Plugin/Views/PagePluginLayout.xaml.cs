using Fotootof.Libraries.Components;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Fotootof.Components.Server.Plugin
{
    /// <summary>
    /// Class XtrmAddons Fotootof Component Server Plugin View Layout.
    /// </summary>
    public partial class PagePluginLayout : ComponentView
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the page model.
        /// </summary>
        public PagePluginModel Model { get; private set; }

        #endregion



        #region constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Component Server Plugin View Layout Constructor.
        /// </summary>
        public PagePluginLayout()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Component Server Plugin View Layout Constructor.
        /// </summary>
        /// <param name="uc">A plugin component <see cref="UserControl"/>.</param>
        public PagePluginLayout(UserControl uc)
        {
            InitializeComponent();

            if(uc.Parent != null)
            {
                ((Grid)uc.Parent).Children.Remove(uc);
            }

            ((Grid)FindName("GridBlockRootName")).Children.Add(uc);
        }

        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void Control_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Model;
        }

        /// <summary>
        /// Method to initialize and display data context.
        /// </summary>
        public override void InitializeModel()
        {
            // Paste page to the model & child elements.
            Model = new PagePluginModel(this);
        }

        #endregion



        #region Methods Size Changed

        /// <summary>
        /// Method called on page size changed event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Size changed event arguments.</param>
        public override void Layout_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Width = Math.Max(MainBlockContent.ActualWidth, 0);
            Height = Math.Max(MainBlockContent.ActualHeight, 0);
        }

        #endregion
    }
}
