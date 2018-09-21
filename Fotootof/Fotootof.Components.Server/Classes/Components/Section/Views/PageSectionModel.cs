using Fotootof.Collections.Entities;
using Fotootof.Components.Client.Section.Layouts;
using Fotootof.Layouts.Controls.DataGrids;
using Fotootof.Layouts.Dialogs;
using Fotootof.Libraries.Components;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.SQLite.EntityManager.Enums.EntityHelper;
using Fotootof.SQLite.EntityManager.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using XtrmAddons.Net.Common.Extensions;
using ListViewAlbumsModel = Fotootof.Layouts.Controls.ListViews.ListViewAlbumsModel;

namespace Fotootof.Components.Server.Section
{
    /// <summary>
    /// Class XtrmAddons Fotootof Component Server Page Section Model.
    /// </summary>
    internal class PageSectionModel : ComponentModel<PageSectionLayout>
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable observable collection of Sections.
        /// </summary>
        private DataGridSectionsModel<DataGridSectionsControl> sections;

        /// <summary>
        /// Variable observable collection of Albums.
        /// </summary>
        private ListViewAlbumsModel albums;

        /// <summary>
        /// Variable observable collection of quality filters.
        /// </summary>
        private InfoEntityCollection qualityFilters;

        /// <summary>
        /// Variable observable collection of color filters.
        /// </summary>
        private InfoEntityCollection colorFilters;

        /// <summary>
        /// 
        /// </summary>
        private readonly Dictionary<string, int> filters = new Dictionary<string, int>();

        #endregion



        #region Properties

        /// <summary>
        /// 
        /// </summary>
        private DataGridSectionsLayout SectionsLayout
            => (DataGridSectionsLayout)ControlView.FindName("DataGridSectionsLayoutName");

        /// <summary>
        /// Property to access to the observable collection of Section.
        /// </summary>
        public DataGridSectionsModel<DataGridSectionsControl> Sections
        {
            get => sections;
            set
            {
                if (sections != value)
                {
                    sections = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the observable collection of Albums
        /// </summary>
        public ListViewAlbumsModel Albums
        {
            get => albums;
            set
            {
                if (albums != value)
                {
                    albums = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the observable collection of quality filters.
        /// </summary>
        public InfoEntityCollection FiltersQuality
        {
            get => qualityFilters;
            set
            {
                if (qualityFilters != value)
                {
                    qualityFilters = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the observable collection of color filters.
        /// </summary>
        public InfoEntityCollection FiltersColor
        {
            get => colorFilters;
            set
            {
                if (colorFilters != value)
                {
                    colorFilters = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, int> Filters
        {
            get => filters;
            set
            {
                if (filters != value)
                {
                    filters.Clear();
                    filters.AppendDictionary(value);
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Accessors to page user model.
        /// </summary>
        public AlbumOptionsList AlbumOptionsListFilters
        {
            get
            {
                SectionEntity a = SectionsLayout.SelectedItem;
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



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Models List Sections Constructor.
        /// </summary>
        /// <param name="controlView">The page associated to the model.</param>
        public PageSectionModel(PageSectionLayout controlView) : base(controlView) { }

        #endregion



        #region Methods Albums

        /// <summary>
        /// Method to load the collection of Albums <see cref="AlbumEntityCollection"/>.
        /// </summary>
        public void LoadAlbums()
        {
            MessageBoxs.IsBusy = true;
            log.Warn("Loading Albums list. Please wait...");

            try
            {
                Albums.Items = new AlbumEntityCollection(true, AlbumOptionsListFilters);
                log.Info($"Loading {Albums?.Items?.Count()} Albums. Done.");
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Fatal(ex, "Loading Albums list. Failed !");
            }
            finally
            {
                log.Warn("Loading Albums list. Done.");
                MessageBoxs.IsBusy = false;
            }
        }

        /// <summary>
        /// Method to add a <see cref="AlbumEntity"/>.
        /// </summary>
        /// <param name="item"></param>
        public void AddAlbum(AlbumEntity item)
        {
            try
            {
                
            }

            catch (Exception ex)
            {
                log.Error(ex.Output());
                throw;
            }
        }

        /// <summary>
        /// Method to update a <see cref="AlbumEntity"/>.
        /// </summary>
        /// <param name="newEntity"></param>
        public async void UpdateAlbum(AlbumEntity newEntity)
        {
            try
            {
                AlbumEntity oldEntity = Albums.Items.Single(x => x.PrimaryKey == newEntity.PrimaryKey);
                await AlbumEntityCollection.DbUpdateAsync(new List<AlbumEntity> { newEntity }, new List<AlbumEntity> { oldEntity });
                oldEntity.Bind(newEntity);
                //LoadAlbums();
            }

            catch (Exception ex)
            {
                log.Error(ex.Output());
                throw;
            }
        }

        #endregion



        #region Methods Sections

        /// <summary>
        /// Method to load the <see cref="SectionEntityCollection"/> from the database.
        /// </summary>
        public void LoadSections()
        {
            MessageBoxs.IsBusy = true;
            log.Warn("Loading Sections list. Please wait...");

            try
            {
                var collec = new SectionEntityCollection(true);
                Sections.Items = new SectionEntityCollection(collec.OrderBy(x => x.Name));
            }

            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Fatal(ex, "Loading Sections list failed !");
            }

            finally
            {
                log.Warn("Loading Sections list. Done.");
                MessageBoxs.IsBusy = false;
            }
        }

        /// <summary>
        /// Method to add a <see cref="SectionEntity"/>.
        /// </summary>
        /// <param name="item"></param>
        public void AddSection(SectionEntity item)
        {
            try
            {
                Sections.Items.Add(item);
                SectionEntityCollection.DbInsert(new List<SectionEntity> { item });
            }

            catch (Exception ex)
            {
                log.Error(ex.Output());
                throw;
            }
        }

        /// <summary>
        /// Method to update a <see cref="SectionEntity"/>.
        /// </summary>
        /// <param name="newEntity"></param>
        public void UpdateSection(SectionEntity newEntity)
        {
            try
            {
                SectionEntity oldEntity = Sections.Items.Single(x => x.PrimaryKey == newEntity.PrimaryKey);
                SectionEntityCollection.DbUpdateAsync(new List<SectionEntity> { newEntity }, new List<SectionEntity> { oldEntity });
                oldEntity.Bind(newEntity);
            }

            catch (Exception ex)
            {
                log.Error(ex.Output());
                throw;
            }
        }

        /// <summary>
        /// Method to delete a <see cref="SectionEntity"/>.
        /// </summary>
        /// <param name="item"></param>
        public void DeleteSection(SectionEntity item)
        {
            try
            {
                if(item == null)
                {
                    log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : No item selected.");
                    return;
                }

                Sections.Items.Remove(item);
                SectionEntityCollection.DbDelete(new List<SectionEntity> { item });
            }

            catch (Exception ex)
            {
                log.Error(ex.Output());
                throw;
            }
        }

        #endregion



        #region Methods Filters

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        public void ChangeFiltersQuality(InfoEntity info)
        {
            string alias = "quality";

            if (info.Alias != "various-quality")
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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        public void ChangeFiltersColor(InfoEntity info)
        {
            string alias = "color";

            if (info.Alias != "various-color")
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
        }

        #endregion
    }
}
