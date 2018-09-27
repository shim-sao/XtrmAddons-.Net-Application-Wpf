using Fotootof.Layouts.Dialogs;
using Fotootof.Libraries.Components;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.SQLite.EntityManager.Enums.EntityHelper;
using Fotootof.SQLite.EntityManager.Event;
using Fotootof.SQLite.EntityManager.Managers;
using System;
using System.Globalization;
using System.Windows;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.Components.Server.Album
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Component Views Server Page Album.</para>
    /// <para>This page provides the entire display for server administration of an image album.</para>
    /// </summary>
    public partial class PageAlbumLayout : ComponentView
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
        /// Property to access to the data model of the page.
        /// </summary>
        public PageAlbumModel Model { get; private set; }

        /// <summary>
        /// Property to access to the album's id or primary key.
        /// </summary>
        public int ItemId { get; }

        #endregion



        #region constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Component Views Server Page Album Constructor.
        /// </summary>
        public PageAlbumLayout()
        {
            NotSupportedException e = new NotSupportedException("An Album Primary Key must be specify as argument. Uses PageAlbum.PageAlbum(int albumId)");
            log.Fatal(e.Output());
            throw e;
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Component Views Server Page Album Constructor.
        /// </summary>
        /// <param name="albumId">An <see cref="AlbumEntity"/> unique identifier or primary key.</param>
        public PageAlbumLayout(int albumId)
        {
            MessageBoxs.IsBusy = true;
            log.Info(string.Format(CultureInfo.CurrentCulture, Local.Properties.Logs.InitializingPageWaiting, "Server Album"));

            // Store Album primary key.
            ItemId = albumId;

            // Constuct page component.
            InitializeComponent();
            AfterInitializedComponent();

            log.Info(string.Format(CultureInfo.CurrentCulture, Local.Properties.Logs.InitializingPageDone, "Server Album"));
            MessageBoxs.IsBusy = false;
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method called on <see cref="FrameworkElement"/> loaded event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/></param>
        public override void Control_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Model;
        }

        /// <summary>
        /// Method to initialize page content asynchronously.
        /// </summary>
        public override async void InitializeModel()
        {
            Model = new PageAlbumModel(this);

            AlbumEntity album = null;
            if (ItemId > 0)
            {
                AlbumOptionsSelect options = new AlbumOptionsSelect
                {
                    Dependencies = { EnumEntitiesDependencies.All },
                    PrimaryKey = ItemId
                };
                album = await PageAlbumModel.Db.Albums.SingleOrNullAsync(options);
            }

            Model.AlbumEntity = album ?? new AlbumEntity();

            /*UcDataGridSections.OnAdd += SectionsDataGrid_OnAdd;
            UcDataGridSections.OnChange += SectionsDataGrid_OnChange;
            UcDataGridSections.OnCancel += SectionsDataGrid_OnCancel;*/

            // Add picture add handler.
            PicturesCollection.Added -= PicturesCollection_Added;
            PicturesCollection.Added += PicturesCollection_Added;

            // Add picture delete handler.
            PicturesCollection.Deleted -= PicturesCollection_Deleted;
            PicturesCollection.Deleted += PicturesCollection_Deleted;
        }

        /// <summary>
        /// Method called on Section delete event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">Entity changes event arguments <see cref="EntityChangesEventArgs"/>.</param>
        private void PicturesCollection_Deleted(object sender, EntityChangesEventArgs e)
        {
            try
            {
                //// Start to busy application.
                //MessageBoxs.IsBusy = true;
                //log.Warn("Starting deleting Picture(s). Please wait...");

                //// Get item from list.
                //PictureEntity[] items = (PictureEntity[])e.OldEntities;

                //// Stop to busy application.
                //log.Warn("Ending deleting Picture(s).");
                //MessageBoxs.IsBusy = false;
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Error(ex.Output());
            }
        }

        /// <summary>
        /// Method called on Section delete event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">Entity changes event arguments <see cref="EntityChangesEventArgs"/>.</param>
        private void PicturesCollection_Added(object sender, EntityChangesEventArgs e)
        {
            try
            {
                //// Start to busy application.
                //MessageBoxs.IsBusy = true;
                //log.Warn("Starting adding Picture(s). Please wait...");

                //// Get item from list.
                //PictureEntity[] items = (PictureEntity[])e.NewEntities;

                //// Stop to busy application.
                //log.Warn("Ending adding Picture(s).");
                //MessageBoxs.IsBusy = false;
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Error(ex.Output());
            }
        }

        #endregion



        #region Methods Size Changed

        /// <summary>
        /// Method called on user control size changed event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">Size changed event arguments <see cref="SizeChangedEventArgs"/>.</param>
        public override void Layout_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = Math.Max(MainBlockContent.ActualWidth, 0);
            this.Height = Math.Max(MainBlockContent.ActualHeight, 0);

            BlockMiddleContentsName.Width = Math.Max(this.Width, 0);
            BlockMiddleContentsName.Height = Math.Max(this.Height, 0);

            PicturesCollection.Height = Math.Max(this.Height, 0);

            TraceSize(MainBlockContent);
            TraceSize(this);
            TraceSize(BlockMiddleContentsName);
            TraceSize(PicturesCollection);
        }

        #endregion
    }
}
