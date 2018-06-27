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
using XtrmAddons.Fotootof.Common.Collections;
using XtrmAddons.Fotootof.Common.Controls.DataGrids;
using XtrmAddons.Fotootof.Common.Controls.ListViews;
using XtrmAddons.Fotootof.Common.Models.DataGrids;
using XtrmAddons.Fotootof.Common.Tools;
using System.Globalization;
using XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems;
using XtrmAddons.Net.Common.Extensions;
using XtrmAddons.Fotootof.Culture;

namespace XtrmAddons.Fotootof.Component.ServerSide.Views.ViewCatalog
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Component Server Side View Catalog Page.
    /// </summary>
    public partial class PageCatalog : PageBase
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, int> filters =
            new Dictionary<string, int>();

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the page model.
        /// </summary>
        public PageCatalogModel Model { get; private set; }

        /// <summary>
        /// Accessors to page user model.
        /// </summary>
        public AlbumOptionsList AlbumOptionsListFilters
        {
            get
            {
                SectionEntity a = UcDataGridSections.SelectedItem;

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
            MessageBase.IsBusy = true;
            log.Warn(string.Format(CultureInfo.CurrentCulture, Translation.DLogs.InitializingPageWaiting, "Catalog"));

            // Constuct page component.
            InitializeComponent();
            AfterInitializedComponent();

            log.Info(string.Format(CultureInfo.CurrentCulture, Translation.DLogs.InitializingPageDone, "Catalog"));
            MessageBase.IsBusy = false;
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
            Model = new PageCatalogModel(this)
            {
                Sections = new DataGridSectionsModel<DataGridSections>(UcDataGridSections),
                Albums = new ListViewAlbumsModel<ListViewAlbums>(UcListViewAlbums)
            };
            
            LoadSections();
            LoadAlbums();

            Model.FiltersQuality = InfoEntityCollection.TypesQuality();
            Model.FiltersColor = InfoEntityCollection.TypesColor();

            UcDataGridSections.OnAdd += SectionsDataGrid_OnAdd;
            UcDataGridSections.OnChange += SectionsDataGrid_OnChange;
            UcDataGridSections.OnCancel += SectionsDataGrid_OnCancel;
            UcDataGridSections.OnDelete += SectionsDataGrid_OnDelete;
            UcDataGridSections.DefaultChanged += SectionsDataGrid_OnDefaultChange;
            UcDataGridSections.SelectionChanged += (s, es) => { RefreshAlbums(); };

            UcListViewAlbums.OnAdd += AlbumsListView_OnAdd;
            UcListViewAlbums.OnChange += AlbumsListView_OnChange;
            UcListViewAlbums.OnCancel += AlbumsListView_OnCancel;
           // UcListViewAlbums.OnDelete += AlbumsListView_OnDeleteAsync;
        }

        #endregion



        #region Methods : Section

        /// <summary>
        /// Method to load the list of Section from database.
        /// </summary>
        private void LoadSections()
        {
            MessageBase.IsBusy = true;
            log.Warn("Loading Sections list : Start. Please wait...");

            try
            {
                Model.Sections.Items = new SectionEntityCollection(true);
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBase.Fatal(ex, "Loading Sections list failed !");
            }
            finally
            {
                log.Info("Loading Sections list : End.");
                MessageBase.IsBusy = false;
            }
        }

        /// <summary>
        /// Method called on Section view candel event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Entity changes event arguments.</param>
        private void SectionsDataGrid_OnCancel(object sender, EntityChangesEventArgs e)
        {
            MessageBase.IsBusy = true;
            log.Warn("Adding or editing Section operation canceled. Please wait...");

            LoadSections();

            log.Info("Adding or editing Section operation canceled. Done.");
            MessageBase.IsBusy = false;
        }

        /// <summary>
        /// Method called on Section add event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Entity changes event arguments.</param>
        private void SectionsDataGrid_OnAdd(object sender, EntityChangesEventArgs e)
        {
            MessageBase.IsBusy = true;
            log.Warn("Saving new Section informations. Please wait...");

            SectionEntity item = (SectionEntity)e.NewEntity;
            Model.Sections.Items.Add(item);
            SectionEntityCollection.DbInsert(new List<SectionEntity> { item });

            log.Info("Saving new Section informations. Done.");
            MessageBase.IsBusy = false;
        }

        /// <summary>
        /// Method called on Section change event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Entity changes event arguments.</param>
        private void SectionsDataGrid_OnChange(object sender, EntityChangesEventArgs e)
        {
            MessageBase.IsBusy = true;
            log.Warn("Saving Section informations. Please wait...");

            SectionEntity newEntity = (SectionEntity)e.NewEntity;
            SectionEntity old = Model.Sections.Items.Single(x => x.PrimaryKey == newEntity.PrimaryKey);
            /*int index = model.Sections.Items.IndexOf(old);
            model.Sections.Items[index] = newEntity;*/
            SectionEntityCollection.DbUpdateAsync(new List<SectionEntity> { newEntity }, new List<SectionEntity> { old });
            LoadSections();

            log.Info("Saving Section informations. Done.");
            MessageBase.IsBusy = false;
        }

        /// <summary>
        /// Method called on Section delete event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Entity changes event arguments.</param>
        private void SectionsDataGrid_OnDelete(object sender, EntityChangesEventArgs e)
        {
            MessageBase.IsBusy = true;
            log.Warn("Deleting Section(s). Please wait...");
            
            // Remove item from list.
            SectionEntity item = (SectionEntity)e.NewEntity;
            Model.Sections.Items.Remove(item);

            // Delete item from database.
            SectionEntityCollection.DbDelete(new List<SectionEntity> { item });

            log.Info("Deleting Section(s). Done.");
            MessageBase.IsBusy = false;
        }

        /// <summary>
        /// Method called on Section default change event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Entity changes event arguments.</param>
        private void SectionsDataGrid_OnDefaultChange(object sender, EntityChangesEventArgs e)
        {
            MessageBase.IsBusy = true;
            log.Warn("Setting default Section. Please wait...");

            SectionEntity newEntity = (SectionEntity)e.NewEntity;
            SectionEntityCollection.SetDefault(newEntity);
            LoadSections();

            log.Info("Setting default Section. Done.");
            MessageBase.IsBusy = false;
        }

        #endregion



        #region Methods : Album

        /// <summary>
        /// Method to load the list of Album from database.
        /// </summary>
        private void LoadAlbums()
        {
            MessageBase.IsBusy = true;
            log.Warn("Loading Albums list. Please wait...");

            try
            {
                Model.Albums.Items = new AlbumEntityCollection(true, AlbumOptionsListFilters);
                log.Info("Loading Albums list. Done.");
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBase.Fatal(ex, "Loading Albums list. Failed !");
            }
            finally
            {
                MessageBase.IsBusy = false;
            }
        }

        /// <summary>
        /// Method to refresh the list of Album from database.
        /// </summary>
        private void RefreshAlbums()
        {
            UcListViewAlbums.Visibility = Visibility.Hidden;
            LoadAlbums();
            UcListViewAlbums.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Method called on Album view candel event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Entity changes event arguments.</param>
        private void AlbumsListView_OnCancel(object sender, EntityChangesEventArgs e)
        {
            MessageBase.IsBusy = true;
            log.Warn("Adding or editing Album operation canceled. Please wait...");

            LoadAlbums();

            log.Info("Adding or editing Album operation canceled. Done.");
            MessageBase.IsBusy = false;
        }

        /// <summary>
        /// Method called on Album add event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void AlbumsListView_OnAdd(object sender, EntityChangesEventArgs e)
        {
            MessageBase.IsBusy = true;
            log.Warn("Saving new Album informations. Please wait...");

            AlbumEntity item = (AlbumEntity)e.NewEntity;
            Model.Albums.Items.Add(item);
            AlbumEntityCollection.DbInsert(new List<AlbumEntity> { item });

            log.Info("Saving new Album informations. Done.");
            MessageBase.IsBusy = false;
        }

        /// <summary>
        /// Method called on Album change event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private async void AlbumsListView_OnChange(object sender, EntityChangesEventArgs e)
        {
            MessageBase.IsBusy = true;
            log.Warn("Saving Album informations. Please wait...");

            AlbumEntity newEntity = (AlbumEntity)e.NewEntity;
            AlbumEntity old = Model.Albums.Items.Single(x => x.PrimaryKey == newEntity.PrimaryKey);
            /*int index = model.Albums.Items.IndexOf(old);
            model.Albums.Items[index] = newEntity;*/
            await AlbumEntityCollection.DbUpdateAsync(new List<AlbumEntity> { newEntity }, new List<AlbumEntity> { old });
            LoadAlbums();

            log.Info("Saving Album informations. Done.");
            MessageBase.IsBusy = false;
        }

        /// <summary>
        /// Method called on Album delete event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Entity changes event arguments.</param>
        private async void AlbumsListView_OnDeleteAsync(object sender, EntityChangesEventArgs e)
        {
            MessageBase.IsBusy = true;
            log.Warn("Deleting Album(s). Please wait...");

            // Remove item from list.
            AlbumEntity item = (AlbumEntity)e.NewEntity;
            if(Model.Albums.Items.Remove(item))
            {
                // Delete item from database.
                await AlbumEntityCollection.DbDeleteAsync(new List<AlbumEntity> { item });
            }

            log.Info("Deleting Album(s). Done.");
            MessageBase.IsBusy = false;
        }

        #endregion



        #region Methods Filters

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void FiltersQuality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UcListViewAlbums.FiltersQualitySelector.IsEditable = false;

            string alias = "quality";

            if ((sender as ComboBox).SelectedItem is InfoEntity info && info.Alias != "various-quality")
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
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void FiltersColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UcListViewAlbums.FiltersColorSelector.IsEditable = false;

            string alias = "color";

            if ((sender as ComboBox).SelectedItem is InfoEntity info && info.Alias != "various-color")
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



        #region Methods Size Changed

        /// <summary>
        /// Method called on page size changed event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Size changed event arguments.</param>
        public override void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            FrameworkElement fe = ((MainWindow)AppWindow).Block_Content as FrameworkElement;

            this.Width = fe.ActualWidth;
            this.Height = fe.ActualHeight;

            double height = Math.Max(Height - Block_TopControls.RenderSize.Height, 0);

            Block_MiddleContents.Width = this.Width;
            Block_MiddleContents.Height = height;

            UcDataGridSections.Height = height;
            UcListViewAlbums.Height = height;
        }

        #endregion
    }
}