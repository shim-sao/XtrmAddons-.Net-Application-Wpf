﻿using System.Windows;
using System.Windows.Controls;
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

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Common Controls Albums List View Constructor.
        /// </summary>
        public ListViewStoragesServer()
        {
            InitializeComponent();
            ItemsCollection.KeyDown += ItemsCollection.AddKeyDownSelectAllItems;
            Loaded += (s, e) => Page_Loaded();
        }

        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        public void Page_Loaded()
        {
            GridRoot.MaxWidth = ActualWidth - 17;
            GridRoot.Width = ActualWidth - 17;

            ItemsCollection.MinHeight = GridRoot.ActualHeight - Block_Header.ActualHeight;
            ItemsCollection.Height = GridRoot.ActualHeight - Block_Header.ActualHeight;

            /*ItemsCollectionStorages.MaxWidth = GridRoot.ActualWidth - 20;
            ItemsCollectionStorages.Width = GridRoot.ActualWidth - 20;*/
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Resize(object sender, SizeChangedEventArgs e)
        {
            Page_Loaded();
        }

        #endregion
    }
}