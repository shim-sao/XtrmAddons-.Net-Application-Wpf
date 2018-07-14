using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;

namespace XtrmAddons.Fotootof.Component.ServerSide.Views.ViewPlugin
{
    /// <summary>
    /// Logique d'interaction pour PagePlugin.xaml
    /// </summary>
    public partial class PagePlugin : PageBase
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

        /// <summary>
        /// 
        /// </summary>
        [System.Obsolete("Use FindName('Block_Root')")]
        public Grid BlockRoot => Block_Root;

        #endregion



        #region constructor

        /// <summary>
        /// 
        /// </summary>
        public PagePlugin()
        {
            InitializeComponent();
        }

        public PagePlugin(UserControl uc)
        {
            InitializeComponent();

            if(uc.Parent != null)
            {
                ((Grid)uc.Parent).Children.Remove(uc);
            }

            ((Grid)FindName("Block_Root")).Children.Add(uc);
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to initialize and display data context.
        /// </summary>
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
        public override void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Width = MainBlockContent.ActualWidth;
            Height = MainBlockContent.ActualHeight;
        }

        #endregion
    }
}
