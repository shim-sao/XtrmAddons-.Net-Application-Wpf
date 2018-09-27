using Fotootof.Collections.Entities;
using Fotootof.Layouts.Dialogs;
using Fotootof.Libraries.Windows;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.SQLite.EntityManager.Event;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.Layouts.Controls.DataGrids
{
    /// <summary>
    /// Class XtrmAddons Fotootof Layouts Controls Window Albums Data Grid.
    /// </summary>
    public partial class DataGridAlbumsWindow : WindowLayoutForm
    {
        #region Variable

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the Window model <see cref="DataGridAlbumsWindowModel"/>.
        /// </summary>
        public new DataGridAlbumsWindowModel Model { get; private set; }

        /// <summary>
        /// Property to get the selected Albums <see cref="ObservableCollection{AlbumEntity}"/>.
        /// </summary>
        public ObservableCollection<AlbumEntity> SelectedAlbums 
            => FindName<DataGridAlbumsLayout>("DataGridAlbumsLayoutName").SelectedAlbums;

        /// <summary>
        /// Variable old Album informations backup.
        /// </summary>
        public AlbumEntityCollection OldForm { get; set; }

        /// <summary>
        /// Variable old Album informations backup.
        /// </summary>
        public AlbumEntityCollection NewForm
        {
            get => Model.Albums;
            set => Model.Albums = value;
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Layouts Window Albums Data Grid Constructor.
        /// </summary>
        public DataGridAlbumsWindow() : base()
        {
            InitializeComponent();
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method called on <see cref="Window"/> load event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/></param>
        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Start to busy application.
                MessageBoxs.IsBusy = true;
                log.Warn("Window loading event. Please wait...");

                // Assign Window closing event.
                Closing += Window_Closing;

                // Assign list of AclGroup to the model.
                Model = new DataGridAlbumsWindowModel(this);
                LoadAlbums();

                // Add events handlers to the albums container.
                DataGridAlbumsLayout layout = FindName<DataGridAlbumsLayout>("DataGridAlbumsLayoutName");
                layout.Added += UCAlbumsContainer_Added;
                layout.Canceled += UCAlbumsContainer_Canceled;
                layout.Changed += UCAlbumsContainer_ChangedAsync;
                layout.Deleted += UCAlbumsContainer_DeletedAsync;

                // Add model to the Window context.
                DataContext = Model;

                // Add selection changed handler
                SelectedAlbums.CollectionChanged += SelectedAlbums_CollectionChanged;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Output(), ex);
                MessageBoxs.Fatal(ex);
            }

            // Stop to busy application.
            finally
            {
                log.Warn("Window loading event. Done.");
                MessageBoxs.IsBusy = false;
            }
        }

        /// <summary>
        /// Method called on albums collection selection changed.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">Notify collection changed event arguments.</param>
        private void SelectedAlbums_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            try
            {
                if (SelectedAlbums.Count > 0)
                {
                    ((Button)FindName("ButtonSaveName")).IsEnabled = true;
                }
                else
                {
                    ((Button)FindName("ButtonCancelName")).IsEnabled = false;
                }
            }

            catch (Exception ex)
            {
                log.Fatal(ex.Output(), ex);
                MessageBoxs.Error(ex);
            }
        }

        /// <summary>
        /// Method to load the list of Album from database.
        /// </summary>
        private void LoadAlbums()
        { 
            try
            {
                // Start to busy application.
                MessageBoxs.IsBusy = true;
                log.Warn("Loading Albums list. Please wait...");

                // Load Albums from database.
                Model.Albums = new AlbumEntityCollection(true);
            }
            catch (Exception e)
            {
                log.Error(e.Output(), e);
                MessageBoxs.Error(new InvalidOperationException("An error occurs while loading Albums list from database ! See logs for further informations."));
            }

            // Stop to busy application.
            finally
            {
                log.Warn("Loading Albums list. Please wait.");
                MessageBoxs.IsBusy = false;
            }
        }

        /// <summary>
        /// Method called on Album add event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">Entity changes event arguments.</param>
        private void UCAlbumsContainer_Added(object sender, EntityChangesEventArgs e)
        {
            try
            {
                // Start to busy application.
                MessageBoxs.IsBusy = true;
                log.Warn("Saving Album informations. Please wait...");

                AlbumEntity item = (AlbumEntity)e.NewEntity;
                Model.Albums.Add(item);
                AlbumEntityCollection.DbInsert(new List<AlbumEntity> { item });
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Error(ex.Output());
            }

            // Stop to busy application.
            finally
            {
                log.Warn("Saving Album informations. Done.");
                MessageBoxs.IsBusy = false;
            }
        }

        /// <summary>
        /// Method called on Album view cancel event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">Entity changes event arguments.</param>
        private void UCAlbumsContainer_Canceled(object sender, EntityChangesEventArgs e)
        {
            try
            {
                // Start to busy application.
                MessageBoxs.IsBusy = true;
                log.Warn("Canceling operation. Please wait...");

                // No operation at this moment.
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Error(ex.Output());
            }

            // Stop to busy application.
            finally
            {
                log.Warn("Canceling operation. Done.");
                MessageBoxs.IsBusy = false;
            }
        }

        /// <summary>
        /// Method called on Album change event asynchounously.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">Entity changes event arguments.</param>
        private async void UCAlbumsContainer_ChangedAsync(object sender, EntityChangesEventArgs e)
        {
            try
            {
                // Start to busy application.
                MessageBoxs.IsBusy = true;
                log.Warn("Updating Album informations. Please wait...");

                // Get old & new entity informations.
                AlbumEntity newEntity = (AlbumEntity)e.NewEntity;
                AlbumEntity old = Model.Albums.Single(x => x.PrimaryKey == newEntity.PrimaryKey);

                // Update the database.
                newEntity = (await AlbumEntityCollection.DbUpdateAsync(new AlbumEntity[] { newEntity }, new AlbumEntity[] { old }))[0];

                // Replace the old entity in the model by the new one. 
                int index = Model.Albums.IndexOf(old);
                Model.Albums[index] = newEntity;
            }

            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Error(ex.Output());
            }

            // Stop to busy application.
            finally
            {
                log.Warn("Updating Album informations. Done.");
                MessageBoxs.IsBusy = false;
            }
        }

        /// <summary>
        /// Method called on Album delete event asynchounously.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">Entity changes event arguments.</param>
        private async void UCAlbumsContainer_DeletedAsync(object sender, EntityChangesEventArgs e)
        {
            try
            {
                // Start to busy application.
                MessageBoxs.IsBusy = true;
                log.Warn("Deleting Album(s). Please wait...");

                // Remove item from list.
                AlbumEntity item = (AlbumEntity)e.NewEntity;
                Model.Albums.Remove(item);

                // Delete item from database.
                await AlbumEntityCollection.DbDeleteAsync(new List<AlbumEntity> { item });
            }

            catch(Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Error(ex.Output());
            }

            // Stop to busy application.
            finally
            {
                log.Warn("Deleting Album(s). Done.");
                MessageBoxs.IsBusy = false;
            }
        }

        #endregion
    }
}
