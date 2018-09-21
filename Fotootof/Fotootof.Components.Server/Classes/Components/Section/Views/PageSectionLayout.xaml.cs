using Fotootof.Collections.Entities;
using Fotootof.Components.Server.Section.Layouts;
using Fotootof.Layouts.Controls.DataGrids;
using Fotootof.Layouts.Controls.ListViews;
using Fotootof.Layouts.Dialogs;
using Fotootof.Libraries.Components;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.SQLite.EntityManager.Enums.EntityHelper;
using Fotootof.SQLite.EntityManager.Event;
using Fotootof.SQLite.EntityManager.Managers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.Components.Server.Section
{
    /// <summary>
    /// Class XtrmAddons Fotootof Component Server Page Section Model.
    /// </summary>
    public partial class PageSectionLayout : ComponentView
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
        private Dictionary<string, int> filters = new Dictionary<string, int>();

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the page model.
        /// </summary>
        internal PageSectionModel Model { get; private set; }

        /// <summary>
        /// Accessors to page user model.
        /// </summary>
        public AlbumOptionsList AlbumOptionsListFilters
        {
            get
            {
                SectionEntity a = (FindName("DataGridSectionsLayoutName") as DataGridSectionsLayout).SelectedItem;

                AlbumOptionsList op = new AlbumOptionsList
                {
                    Dependencies = { EnumEntitiesDependencies.AlbumsInSections, EnumEntitiesDependencies.InfosInAlbums }
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
        public PageSectionLayout()
        {
            MessageBoxs.IsBusy = true;
            log.Warn(string.Format(CultureInfo.CurrentCulture, Local.Properties.Logs.InitializingPageWaiting, "Section Server"));

            // Constuct page component.
            InitializeComponent();
            AfterInitializedComponent();

            log.Info(string.Format(CultureInfo.CurrentCulture, Local.Properties.Logs.InitializingPageDone, "Section Server"));
            MessageBoxs.IsBusy = false;
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
            var UcDataGridSections = (DataGridSectionsLayout)FindName("DataGridSectionsLayoutName");
            var UcListViewAlbums = (ListViewAlbumsLayout)FindName("ListViewAlbumsLayoutName");

            // Paste page to the model & child elements.
            Model = new PageSectionModel(this)
            {
                Sections = new DataGridSectionsModel<DataGridSectionsControl>(UcDataGridSections),
                Albums = new ListViewAlbumsModel(UcListViewAlbums)
            };
            
            Model.LoadSections();

            if (Settings.Controls.Default.ServerSectionLayoutShowAllAlbums)
            {
                Model.LoadAlbums();
            }
            
            Model.FiltersQuality = InfoEntityCollection.TypesQuality();
            Model.FiltersColor = InfoEntityCollection.TypesColor();

            UcDataGridSections.Added += SectionsDataGrid_Added;
            UcDataGridSections.Changed += SectionsDataGrid_Changed;
            UcDataGridSections.Canceled += SectionsDataGrid_Canceled;
            UcDataGridSections.Deleted += SectionsDataGrid_Deleted;
            UcDataGridSections.DefaultChanged += SectionsDataGrid_DefaultChanged;
            UcDataGridSections.SelectionChanged += (s, es) => { RefreshAlbums(); };

            UcListViewAlbums.Added += AlbumsListView_OnAdd;
            UcListViewAlbums.Changed += AlbumsListView_OnChange;
            UcListViewAlbums.Canceled += AlbumsListView_OnCancel;
           // UcListViewAlbums.OnDelete += AlbumsListView_OnDeleteAsync;
        }

        #endregion



        #region Methods : Section

        /// <summary>
        /// Method called on Section view candel event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Entity changes event arguments.</param>
        private void SectionsDataGrid_Canceled(object sender, EntityChangesEventArgs e)
        {
            MessageBoxs.IsBusy = true;
            log.Warn("Adding or editing Section operation canceled. Please wait...");

            //Model.LoadSections();

            log.Info("Adding or editing Section operation canceled. Done.");
            MessageBoxs.IsBusy = false;
        }

        /// <summary>
        /// Method called on Section add event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Entity changes event arguments.</param>
        private void SectionsDataGrid_Added(object sender, EntityChangesEventArgs e)
        {
            try
            {
                MessageBoxs.IsBusy = true;
                log.Warn("Saving new Section informations. Please wait...");

                Model.AddSection((SectionEntity)e.NewEntity);
            }

            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Fatal(ex, "Saving new Section informations. Fail.");
            }

            finally
            {
                log.Warn("Saving new Section informations. Done.");
                MessageBoxs.IsBusy = false;
            }
        }

        /// <summary>
        /// Method called on Section change event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Entity changes event arguments.</param>
        private void SectionsDataGrid_Changed(object sender, EntityChangesEventArgs e)
        {
            try
            {
                MessageBoxs.IsBusy = true;
                log.Warn("Saving Section informations. Please wait...");

                SectionEntity newEntity = (SectionEntity)e.NewEntity;
                SectionEntity oldEntity = Model.Sections.Items.Single(x => x.PrimaryKey == newEntity.PrimaryKey);
                Model.UpdateSection(newEntity, oldEntity);
            }

            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Fatal(ex, "Saving Section informations. Fail.");
            }

            finally
            {
                log.Warn("Saving Section informations. Done.");
                MessageBoxs.IsBusy = false;
            }
        }

        /// <summary>
        /// Method called on Section delete event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Entity changes event arguments.</param>
        private void SectionsDataGrid_Deleted(object sender, EntityChangesEventArgs e)
        {
            try
            {
                MessageBoxs.IsBusy = true;
                log.Warn("Deleting Section informations. Please wait...");

                Model.DeleteSection((SectionEntity)e.OldEntity);
            }

            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Fatal(ex, "Deleting Section informations. Fail.");
            }

            finally
            {
                log.Warn("Deleting Section informations. Done.");
                MessageBoxs.IsBusy = false;
            }
        }

        /// <summary>
        /// Method called on Section default change event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Entity changes event arguments.</param>
        private void SectionsDataGrid_DefaultChanged(object sender, EntityChangesEventArgs e)
        {
            MessageBoxs.IsBusy = true;
            log.Warn("Setting default Section. Please wait...");

            SectionEntity newEntity = (SectionEntity)e.NewEntity;
            SectionEntityCollection.SetDefault(newEntity);
            Model.LoadSections();

            log.Info("Setting default Section. Done.");
            MessageBoxs.IsBusy = false;
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
                MessageBoxs.IsBusy = true;
                log.Warn("Loading Albums list. Please wait...");

                Model.Albums.Items = new AlbumEntityCollection(true, AlbumOptionsListFilters);
                log.Info($"Loading {Model?.Albums?.Items?.Count()} Albums. Done.");
            }

            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Fatal(ex, "Loading Albums list. Failed !");
            }

            finally
            {
                MessageBoxs.IsBusy = false;
            }
        }

        /// <summary>
        /// Method to refresh the list of Album from database.
        /// </summary>
        private void RefreshAlbums()
        {
            FindName<ListViewAlbumsLayout>("ListViewAlbumsLayoutName").Visibility = Visibility.Hidden;
            LoadAlbums();
            FindName<ListViewAlbumsLayout>("ListViewAlbumsLayoutName").Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Method called on <see cref="ListView"/> <see cref="AlbumEntity"/> candeled event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The entity changes event arguments <see cref="EntityChangesEventArgs"/>.</param>
        private void AlbumsListView_OnCancel(object sender, EntityChangesEventArgs e)
        {
            MessageBoxs.IsBusy = true;
            log.Warn("Adding or editing Album operation canceled. Please wait...");

            LoadAlbums();

            log.Info("Adding or editing Album operation canceled. Done.");
            MessageBoxs.IsBusy = false;
        }

        /// <summary>
        /// Method called on <see cref="ListView"/> <see cref="AlbumEntity"/> added event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The entity changes event arguments <see cref="EntityChangesEventArgs"/>.</param>
        private void AlbumsListView_OnAdd(object sender, EntityChangesEventArgs e)
        {
            MessageBoxs.IsBusy = true;
            log.Warn("Saving new Album informations. Please wait...");

            AlbumEntity item = (AlbumEntity)e.NewEntity;
            Model.Albums.Items.Add(item);
            AlbumEntityCollection.DbInsert(new List<AlbumEntity> { item });

            log.Info("Saving new Album informations. Done.");
            MessageBoxs.IsBusy = false;
        }

        /// <summary>
        /// Method called on <see cref="ListView"/> <see cref="AlbumEntity"/> changed event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The entity changes event arguments <see cref="EntityChangesEventArgs"/>.</param>
        private void AlbumsListView_OnChange(object sender, EntityChangesEventArgs e)
        {
            try
            {
                MessageBoxs.IsBusy = true;
                log.Warn("Saving Album informations. Please wait...");

                Model.UpdateAlbum((AlbumEntity)e.NewEntity);
            }

            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Fatal(ex, "Saving Album informations. Fail.");
            }

            finally
            {
                log.Warn("Saving Album informations. Done.");
                MessageBoxs.IsBusy = false;
            }
        }

        /// <summary>
        /// Method called on <see cref="ListView"/> <see cref="AlbumEntity"/> deleted event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The entity changes event arguments <see cref="EntityChangesEventArgs"/>.</param>
        private async void AlbumsListView_OnDeleteAsync(object sender, EntityChangesEventArgs e)
        {
            MessageBoxs.IsBusy = true;
            log.Warn("Deleting Album(s). Please wait...");

            // Remove item from list.
            AlbumEntity item = (AlbumEntity)e.NewEntity;
            if(Model.Albums.Items.Remove(item))
            {
                // Delete item from database.
                await AlbumEntityCollection.DbDeleteAsync(new List<AlbumEntity> { item });
            }

            log.Info("Deleting Album(s). Done.");
            MessageBoxs.IsBusy = false;
        }

        #endregion



        #region Methods Filters

        /// <summary>
        /// Method called on quality filter selection changed event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The celection changed event arguments <see cref="SelectionChangedEventArgs"/></param>
        private void FiltersQuality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((ComboBox)((ListViewAlbumsLayout)FindName("ListViewAlbumsLayoutName"))
                .FindName("FiltersQualitySelector")).IsEditable = false;

            if (((ComboBox)sender).SelectedItem is InfoEntity info)
            {
                Model.ChangeFiltersQuality(info);
            }

            RefreshAlbums();
        }

        /// <summary>
        /// Method called on color filter selection changed event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The celection changed event arguments <see cref="SelectionChangedEventArgs"/></param>
        private void FiltersColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((ComboBox)((ListViewAlbumsLayout)FindName("ListViewAlbumsLayoutName"))
                .FindName("FiltersColorSelector")).IsEditable = false;

            if (((ComboBox)sender).SelectedItem is InfoEntity info)
            {
                Model.ChangeFiltersColor(info);
            }

            RefreshAlbums();
        }

        #endregion



        #region Methods Size Changed

        /// <summary>
        /// Method called on <see cref="FrameworkElement"/> size changed event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The size changed event arguments <see cref="SizeChangedEventArgs"/>.</param>
        public override void Layout_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                var UcDataGridSections = FindName<DataGridSectionsLayout>("DataGridSectionsLayoutName");
                var UcListViewAlbums = FindName<ListViewAlbumsLayout>("ListViewAlbumsLayoutName");
                var BlockMiddleContents = FindName<Grid>("BlockMiddleContentsName");
            
                Width = MainBlockContent.ActualWidth;
                Height = MainBlockContent.ActualHeight;

                double height = Math.Max(Height - FindName<StackPanel>("BlockTopControlsName").RenderSize.Height, 0);

                BlockMiddleContents.Width = Width;
                BlockMiddleContents.Height = height;

                UcDataGridSections.Height = height;
                UcListViewAlbums.Height = height;
            }

            catch(Exception ex)
            {
                log.Debug(ex.Output());
            }
        }

        #endregion
    }
}