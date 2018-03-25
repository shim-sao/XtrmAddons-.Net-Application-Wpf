using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;
using XtrmAddons.Fotootof.Lib.SQLite.Event;
using XtrmAddons.Fotootof.Libraries.Common.Collections;
using XtrmAddons.Fotootof.Libraries.Common.Controls.DataGrids;
using XtrmAddons.Fotootof.Libraries.Common.Controls.ListViews;
using XtrmAddons.Fotootof.Libraries.Common.Models.DataGrids;
using XtrmAddons.Fotootof.Libraries.Common.Tools;

namespace XtrmAddons.Fotootof.Component.ServerSide.ViewCatalog
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Component Server Side View Catalog Page.
    /// </summary>
    public partial class PageCatalog : PageBase
    {
        #region Variables

        /// <summary>
        /// Variable model associated to the page.
        /// </summary>
        private PageCatalogModel<PageCatalog> model;

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, int> filters = new Dictionary<string, int>();

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the page model.
        /// </summary>
        public PageCatalogModel<PageCatalog> Model
        {
            get => model;
            private set { model = value; }

        }

        /// <summary>
        /// Accessors to page user model.
        /// </summary>
        public AlbumOptionsList AlbumOptionsListFilters
        {
            get
            {
                SectionEntity a = UCDataGridSections.SelectedItem;

                AlbumOptionsList op = new AlbumOptionsList
                {
                    Dependencies = { EnumEntitiesDependencies.All }
                };

                if (a != null)
                {
                    op.IncludeSectionKeys = new List<int>() { a.PrimaryKey };
                }

                if (filters.Count > 0)
                {
                    op.IncludeInfoKeys = filters.Values.ToList();
                }

                return op;
            }
        }

        #endregion



        #region constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Sections Page Constructor.
        /// </summary>
        public PageCatalog()
        {
            InitializeComponent();
            AfterInitializedComponent();
        }

        #endregion



        #region Methods
        
        /// <summary>
        /// Method to initialize and display data context.
        /// </summary>
        public override void InitializeContent()
        {
            InitializeContentAsync();
        }

        /// <summary>
        /// Method to initialize and display data context.
        /// </summary>
        public override void InitializeContentAsync()
        {
            // Paste page to the model & child elements.
            DataContext = model = new PageCatalogModel<PageCatalog>(this)
            {
                Sections = new DataGridSectionsModel<DataGridSections>(UCDataGridSections),
                Albums = new ListViewAlbumsModel<ListViewAlbums>(UCListViewAlbums)
            };

            AppOverwork.IsBusy = true;
            
            LoadSections();
            LoadAlbums();
            model.FiltersQuality = InfoEntityCollection.TypesQuality();
            model.FiltersColor = InfoEntityCollection.TypesColor();

            UCDataGridSections.OnAdd += SectionsDataGrid_OnAdd;
            UCDataGridSections.OnChange += SectionsDataGrid_OnChange;
            UCDataGridSections.OnCancel += SectionsDataGrid_OnCancel;
            UCDataGridSections.OnDelete += SectionsDataGrid_OnDelete;
            UCDataGridSections.DefaultChanged += SectionsDataGrid_OnDefaultChange;
            UCDataGridSections.SelectionChanged += (s, e) => { RefreshAlbums(); };

            UCListViewAlbums.OnAdd += AlbumsListView_OnAdd;
            UCListViewAlbums.OnChange += AlbumsListView_OnChange;
            UCListViewAlbums.OnCancel += AlbumsListView_OnCancel;
            UCListViewAlbums.OnDelete += AlbumsListView_OnDelete;

            AppOverwork.IsBusy = false;
        }

        #endregion



        #region Methods : Section

        /// <summary>
        /// Method to load the list of Section from database.
        /// </summary>
        private void LoadSections()
        {
            try
            {

                AppLogger.Info("Loading Sections list. Please wait...");
                model.Sections.Items = new SectionEntityCollection(true);
                AppLogger.InfoAndClose("Loading Sections list. Done.");
            }
            catch (Exception e)
            {
                AppLogger.Fatal("Loading Sections list failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Method called on Section view candel event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Entity changes event arguments.</param>
        private void SectionsDataGrid_OnCancel(object sender, EntityChangesEventArgs e)
        {
            AppLogger.Info("Adding or editing Section operation canceled. Please wait...");
            LoadSections();
            AppLogger.InfoAndClose("Adding or editing Section operation canceled. Done.");
        }

        /// <summary>
        /// Method called on Section add event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void SectionsDataGrid_OnAdd(object sender, EntityChangesEventArgs e)
        {
            AppLogger.Info("Saving new Section informations. Please wait...");
            SectionEntity item = (SectionEntity)e.NewEntity;
            model.Sections.Items.Add(item);
            SectionEntityCollection.DbInsert(new List<SectionEntity> { item });
            AppLogger.InfoAndClose("Saving new AclGroup informations. Done.");
        }

        /// <summary>
        /// Method called on Section change event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void SectionsDataGrid_OnChange(object sender, EntityChangesEventArgs e)
        {
            AppLogger.Info("Saving Section informations. Please wait...");
            SectionEntity newEntity = (SectionEntity)e.NewEntity;
            SectionEntity old = model.Sections.Items.Single(x => x.PrimaryKey == newEntity.PrimaryKey);
            int index = model.Sections.Items.IndexOf(old);
            model.Sections.Items[index] = newEntity;
            SectionEntityCollection.DbUpdateAsync(new List<SectionEntity> { newEntity }, new List<SectionEntity> { old });
            AppLogger.InfoAndClose("Saving AclGroup informations. Done.");
        }

        /// <summary>
        /// Method called on Section delete event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void SectionsDataGrid_OnDelete(object sender, EntityChangesEventArgs e)
        {
            AppLogger.Info("Deleting Section(s). Please wait...");
            SectionEntity item = (SectionEntity)e.NewEntity;

            // Remove item from list.
            model.Sections.Items.Remove(item);

            // Delete item from database.
            SectionEntityCollection.DbDelete(new List<SectionEntity> { item });
            AppLogger.InfoAndClose("Deleting Section(s). Done.");
        }

        /// <summary>
        /// Method called on Section default change event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void SectionsDataGrid_OnDefaultChange(object sender, EntityChangesEventArgs e)
        {
            AppLogger.Info("Setting default Section. Please wait...");

            SectionEntity newEntity = (SectionEntity)e.NewEntity;

            SectionEntityCollection.SetDefault(newEntity);
            LoadSections();

            AppLogger.InfoAndClose("Setting default Section. Done.");
        }

        #endregion



        #region Methods : Album

        /// <summary>
        /// Method to load the list of Album from database.
        /// </summary>
        private void LoadAlbums()
        {
            try
            {
                AppLogger.Info("Loading Albums list. Please wait...");
                model.Albums.Items = new AlbumEntityCollection(AlbumOptionsListFilters, true);
                AppLogger.InfoAndClose("Loading Albums list. Done.");
            }
            catch (Exception e)
            {
                AppLogger.Fatal("Loading Albums list failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Method to refresh the list of Album from database.
        /// </summary>
        private void RefreshAlbums()
        {
            UCListViewAlbums.Visibility = Visibility.Hidden;
            LoadAlbums();
            UCListViewAlbums.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Method called on Album view candel event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Entity changes event arguments.</param>
        private void AlbumsListView_OnCancel(object sender, EntityChangesEventArgs e)
        {
            AppLogger.Info("Adding or editing Album operation canceled. Please wait...");
            LoadAlbums();
            AppLogger.InfoAndClose("Adding or editing Album operation canceled. Done.");
        }

        /// <summary>
        /// Method called on Album add event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void AlbumsListView_OnAdd(object sender, EntityChangesEventArgs e)
        {
            AppLogger.Info("Saving new Album informations. Please wait...");
            AlbumEntity item = (AlbumEntity)e.NewEntity;
            model.Albums.Items.Add(item);
            AlbumEntityCollection.DbInsert(new List<AlbumEntity> { item });
            AppLogger.InfoAndClose("Saving new Album informations. Done.");
        }

        /// <summary>
        /// Method called on Album change event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void AlbumsListView_OnChange(object sender, EntityChangesEventArgs e)
        {
            AppLogger.Info("Saving Album informations. Please wait...");
            AlbumEntity newEntity = (AlbumEntity)e.NewEntity;
            AlbumEntity old = model.Albums.Items.Single(x => x.PrimaryKey == newEntity.PrimaryKey);
            int index = model.Albums.Items.IndexOf(old);
            model.Albums.Items[index] = newEntity;
            AlbumEntityCollection.DbUpdateAsync(new List<AlbumEntity> { newEntity }, new List<AlbumEntity> { old });
            AppLogger.InfoAndClose("Saving Album informations. Done.");
        }

        /// <summary>
        /// Method called on Album delete event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void AlbumsListView_OnDelete(object sender, EntityChangesEventArgs e)
        {
            AppLogger.Info("Deleting Album(s). Please wait...");
            AlbumEntity item = (AlbumEntity)e.NewEntity;

            // Remove item from list.
            model.Albums.Items.Remove(item);

            // Delete item from database.
            AlbumEntityCollection.DbDelete(new List<AlbumEntity> { item });

            AppLogger.InfoAndClose("Deleting Album(s). Done.");
        }

        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FiltersQuality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UCListViewAlbums.FiltersQualitySelector.IsEditable = false;

            string alias = "quality";
            InfoEntity info = (sender as ComboBox).SelectedItem as InfoEntity;

            if (info != null && info.Alias != "various-quality")
            {
                if (filters.Keys.Contains(alias))
                    filters[alias] = info.PrimaryKey;
                else
                    filters.Add(alias, info.PrimaryKey);
            }
            else if (filters.Keys.Contains(alias))
            {
                filters.Remove(alias);
            }

            RefreshAlbums();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FiltersColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UCListViewAlbums.FiltersColorSelector.IsEditable = false;

            string alias = "color";
            InfoEntity info = (sender as ComboBox).SelectedItem as InfoEntity;

            if (info != null && info.Alias != "various-color")
            {
                if (filters.Keys.Contains(alias))
                    filters[alias] = info.PrimaryKey;
                else
                    filters.Add(alias, info.PrimaryKey);
            }
            else if (filters.Keys.Contains(alias))
            {
                filters.Remove(alias);
            }

            RefreshAlbums();
        }

        #endregion
    }
}