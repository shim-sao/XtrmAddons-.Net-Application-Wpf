using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XtrmAddons.Fotootof.Libraries.Common.Controls.ListViews;
using XtrmAddons.Net.Windows.Controls.Extensions;

namespace XtrmAddons.Fotootof.Libraries.ServerSide.Controls.ListViews
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Server Controls Albums List View.
    /// </summary>
    public partial class ListViewStoragesServer : ListViewStorages
    {
        #region Properties

        /// <summary>
        /// Property to access to the main add to collection control.
        /// </summary>
        public override Control AddControl => null;

        /// <summary>
        /// Property to access to the main edit item control.
        /// </summary>
        public override Control EditControl => null;

        /// <summary>
        /// Property to access to the main delete items control.
        /// </summary>
        public override Control DeleteControl => null;

        /// <summary>
        /// Property to access to the items collection.
        /// </summary>
        public override ListView ItemsCollection { get => ItemsCollectionStorages; set => ItemsCollectionStorages = value; }

        /// <summary>
        /// Property proxy to the combo box selection changed event handler.
        /// </summary>
        public event SelectionChangedEventHandler ImageSize_SelectionChanged { add => ComboBox_ImageSize.SelectionChanged += value; remove => ComboBox_ImageSize.SelectionChanged -= value; }

        /// <summary>
        /// Property proxy to the combo box selection changed event handler.
        /// </summary>
        public event MouseButtonEventHandler ItemsCollection_MouseDoubleClick { add => ItemsCollection.MouseDoubleClick += value; remove => ItemsCollection.MouseDoubleClick -= value; }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Common Controls Albums List View Constructor.
        /// </summary>
        public ListViewStoragesServer()
        {
            InitializeComponent();
            ItemsCollection.KeyDown += ItemsCollection.AddKeyDownSelectAllItems;
        }

        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Block_Root.Width = this.ActualWidth;
            Block_Root.Height = this.ActualHeight;

            Block_Items.Height = this.ActualHeight - Block_Header.RenderSize.Height;
            ItemsCollection.Height = this.ActualHeight - Block_Header.RenderSize.Height;

            Trace_Control_SizeChanged(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trace"></param>
        public void Trace_Control_SizeChanged(bool trace)
        {
            if (!trace) return;
            
            Trace.WriteLine("ListViewStoragesServer --------------------------------------------------------------------------------");
            Trace.WriteLine("this.ActualSize = [" + this.ActualWidth + ":" + this.ActualHeight + "]");
            Trace.WriteLine("this.Size = [" + this.Width + ":" + this.Height + "]");
            Trace.WriteLine("Block_Root.ActualSize = [" + Block_Root.ActualWidth + ":" + Block_Root.ActualHeight + "]");
            Trace.WriteLine("Block_Root.Size = [" + Block_Root.Width + ":" + Block_Root.Height + "]");
            Trace.WriteLine("-------------------------------------------------------------------------------------------------------");
        }

        #endregion
    }
}