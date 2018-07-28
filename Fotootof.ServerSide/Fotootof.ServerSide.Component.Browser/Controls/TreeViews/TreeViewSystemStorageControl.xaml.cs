using System;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.ServerSide.Component.Browser.Controls
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server UI Control Browser Tree View Directory.
    /// </summary>
    public partial class TreeViewSystemStorageControl : ControlBase
    {
        #region Variables
        
        /// <summary>
        /// Variable logger.
        /// </summary>
        private new static readonly log4net.ILog log =
        	log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Properties

        /// <summary>
        /// Accessors to Window AclGroup Form model.
        /// </summary>
        internal TreeViewSystemStorageModel Model { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Visibility IsHeaderVisible
        {
            get
            {
                return (FindName("StackPanelBlockHeaderName") as FrameworkElement).Visibility;
            }
            set
            {
                (FindName("StackPanelBlockHeaderName") as FrameworkElement).Visibility = value;
            }
        }
        
        #endregion


        /// <summary>
        /// Class XtrmAddons Fotootof Server UI Control Browser Tree View Directory Constructor.
        /// </summary>
        public TreeViewSystemStorageControl() : base ()
        {
            InitializeComponent();
            InitializeContent();
        }

        /// <summary>
        /// Method called on user control loaded event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">Routed event arguments <see cref="RoutedEventArgs"/></param>
        private void Control_Loaded(object sender, RoutedEventArgs e)
        {
            ArrangeTreeView();
            DataContext = Model;
        }


        /// <summary>
        /// Method to initialize control content.
        /// </summary>
        public void InitializeContent()
        {
            Model = new TreeViewSystemStorageModel(this);
        }

        /// <summary>
        /// Method to expand the tree of sub-directories <see cref="TreeViewItem"/> of a directory.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        public void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            try
            {
                Model.ExpandTreeViewItem(e.Source as TreeViewItem);
            }
            catch(Exception ex)
            {
                log.Debug(ex.Output(), ex);
                MessageBase.Error(ex);
            }
        }

        private void ClearSelection_Click(object sender, RoutedEventArgs e)
        {
            Model.Reinitialize();
        }

        /// <summary>
        /// Method to arrange the new size of the <see cref="TreeView"/>
        /// </summary>
        private void ArrangeTreeView()
        {
            // Get framework elements.
            var root = (FindName("GridBlockRootName") as FrameworkElement);
            var header = (FindName("StackPanelBlockHeaderName") as FrameworkElement);
            var tv = (FindName("TreeViewDirectoryInfoName") as System.Windows.Controls.TreeView);

            // Process resize of the tree view.
            tv.Height = root.ActualHeight;
            if(header.IsVisible)
            {
                tv.Height -= header.ActualHeight;
            }
        }

        /// <summary>
        /// Method called on <see cref="FrameworkElement"/> size changed event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The size changed event arguments <see cref="SizeChangedEventArgs"/>.</param>
        public override void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ArrangeTreeView();
        }
    }
}
