using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Libraries.Common.Collections;
using XtrmAddons.Fotootof.Libraries.Common.Tools;
using XtrmAddons.Fotootof.Libraries.Common.Windows.Forms.AlbumForm;
using XtrmAddons.Net.Windows.Controls.Extensions;

namespace XtrmAddons.Fotootof.Libraries.Common.Controls.ListViews
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Common Controls Albums List View.
    /// </summary>
    [Obsolete("Use ListViewAlbums")]
    public partial class UCAlbumsListView : ControlBaseCollection
    {
        #region Properties
        
        public override Control AddControl => Button_Add;

        public override Control EditControl => Button_Edit;

        public override Control DeleteControl => Button_Delete;

        /// <summary>
        /// Property to access to the entities collection.
        /// </summary>
        public AlbumEntityCollection Albums
        {
            get { return (AlbumEntityCollection)GetValue(AlbumsProperty); }
            set { SetValue(AlbumsProperty, value); }
        }

        /// <summary>
        /// Property Using a DependencyProperty as the backing store for Entities.
        /// </summary>
        public static readonly DependencyProperty AlbumsProperty =
            DependencyProperty.Register
            (
                "Albums",
                typeof(AlbumEntityCollection),
                typeof(UCAlbumsListView),
                new PropertyMetadata(new AlbumEntityCollection(false))
            );

        /// <summary>
        /// 
        /// </summary>
        public AlbumEntity SelectedItem => (AlbumEntity)ItemsCollection.SelectedItem;

        /// <summary>
        /// 
        /// </summary>
        public List<AlbumEntity> SelectedItems => new List<AlbumEntity>(ItemsCollection.SelectedItems.Cast<AlbumEntity>());

        /// <summary>
        /// 
        /// </summary>
        public event SelectionChangedEventHandler FilterQualityChanged
        {
            add { FiltersQualitySelector.SelectionChanged += value; }
            remove { FiltersQualitySelector.SelectionChanged -= value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public event SelectionChangedEventHandler FilterColorChanged
        {
            add { FiltersColorSelector.SelectionChanged += value; }
            remove { FiltersColorSelector.SelectionChanged -= value; }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Common Controls Albums List View Constructor.
        /// </summary>
        public UCAlbumsListView()
        {
            InitializeComponent();
            ItemsCollection.KeyDown += ItemsCollection.AddKeyDownSelectAllItems;
            //ItemsCollection.Loaded += (s,e) => ControlHeaderTotal.Text = Albums.Count.ToString();
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method called on click event to add a new Album.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Show open file dialog box 
            WindowFormAlbum dlg = new WindowFormAlbum(new AlbumEntity());
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                RaiseOnAdd(dlg.NewForm);
            }
            else
            {
                RaiseOnCancel(dlg.NewForm);
            }
        }

        /// <summary>
        /// Method called on edit click to navigate to a Album edit window.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnEdit_Click(object sender, RoutedEventArgs e)
        {
            // Check if an AclGroup is founded. 
            if (SelectedItem != null)
            {
                // Show open file dialog box 
                WindowFormAlbum dlg = new WindowFormAlbum(SelectedItem);
                bool? result = dlg.ShowDialog();

                // Process open file dialog box results 
                if (result == true)
                {
                    RaiseOnChange(dlg.NewForm);
                }
                else
                {
                    RaiseOnCancel(dlg.NewForm);
                }
            }
            else
            {
                AppLogger.Warning(string.Format("{0} not found !", typeof(AlbumEntity).Name), true, true);
            }
        }

        /// <summary>
        /// Method called on delete click to delete a Album.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Check if an AclGroup is founded. 
            if(SelectedItem != null)
            {
                // Alert user for acceptation.
                MessageBoxResult result = MessageBox.Show
                (
                    String.Format(Properties.Resources.MessageBox_Acceptation_DeleteGeneric, Properties.Resources.Album, SelectedItem.Name),
                    Properties.Resources.ApplicationName ,
                    MessageBoxButton.YesNoCancel
                );

                // If accepted, try to update page model collection.
                if(result == MessageBoxResult.Yes)
                {
                    RaiseOnDelete(SelectedItem);
                }
            }
            else
            {
                AppLogger.Warning(string.Format("{0} not found !", typeof(AlbumEntity).Name), true, true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemsCollection_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AppNavigator.NavigateToPageAlbumServer(SelectedItem.PrimaryKey);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAlbum_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        /// <summary>
        /// Method called clear items selection click event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void ClearItemsSelection_Click(object sender, RoutedEventArgs e)
        {
            ItemsCollection.SelectedItems.Clear();
        }

        /// <summary>
        /// Method called on select all click event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void SelectAllItems_Click(object sender, RoutedEventArgs e)
        {
            ItemsCollection.SelectAll();
        }

        /// <summary>
        /// Method called on items collection selection changed click event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Selection changed event arguments.</param>
        public override void ItemsCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ControlHeaderSelectedNumber.Text = SelectedItems.Count.ToString();
        }

        public override void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}