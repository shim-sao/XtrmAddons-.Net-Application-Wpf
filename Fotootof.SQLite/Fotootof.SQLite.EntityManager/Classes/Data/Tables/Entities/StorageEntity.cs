using Fotootof.SQLite.EntityManager.Data.Base;
using Fotootof.SQLite.EntityManager.Data.Tables.Dependencies.Observables;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fotootof.SQLite.EntityManager.Data.Tables.Entities
{
    /// <summary>
    /// Class XtrmAddons Fotootof  SQLite Entity Manager Data Table Storages.
    /// </summary>
    [Serializable]
    [Table("Storages")]
    [JsonObject(MemberSerialization.OptIn)]
    public partial class StorageEntity : EntityBase
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        [NotMapped]
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable Name of the Info.
        /// </summary>
        [NotMapped]
        public string name = "";

        /// <summary>
        /// Variable Full Name of the Info.
        /// </summary>
        [NotMapped]
        public string fullName = "";

        /// <summary>
        /// Variable ordering.
        /// </summary>
        [NotMapped]
        public int ordering = 0;

        #endregion



        #region Properties

        /// <summary>
        /// Propmerty to access to the primary key auto incremented.
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public int StorageId
        {
            get => primaryKey;
            set
            {
                if (value != primaryKey)
                {
                    primaryKey = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the Name of the Info.
        /// </summary>
        [Column(Order = 2)]
        [JsonProperty]
        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the Full Name of the Info.
        /// </summary>
        [Column(Order = 3)]
        [JsonProperty]
        public string FullName
        {
            get { return fullName; }
            set
            {
                if (value != fullName)
                {
                    fullName = value;
                    NotifyPropertyChanged();
                }
            }
        }
        
        /*
        /// <summary>
        /// Property to acces to the ordering.
        /// </summary>
        [Column(Order = 6)]
        [JsonProperty]
        public int Ordering
        {
            get { return ordering; }
            set
            {
                if (value != ordering)
                {
                    ordering = value;
                    NotifyPropertyChanged();
                }
            }
        }
        */

        #endregion



        #region Properties Dependencies Album

        /// <summary>
        /// Variable Album id (required for entity dependency).
        /// </summary>
        [NotMapped]
        public int AlbumId { get; set; }

        /// <summary>
        /// Property to access to the list of Album dependencies primary key.
        /// </summary>
        [NotMapped]
        public ObservableCollection<int> AlbumsPKeys
        {
            get
            {
                StoragesInAlbums.Populate();
                return StoragesInAlbums.DepPKeys;
            }
        }

        /// <summary>
        /// Property to access to the list of Album associated to the Storage.
        /// </summary>
        [NotMapped]
        public ObservableCollection<AlbumEntity> Albums
        {
            get
            {
                StoragesInAlbums.Populate();
                return StoragesInAlbums.DepReferences;
            }
        }

        /// <summary>
        /// Property collection of relationship Storages in Albums.
        /// </summary>
        public ObservableStoragesInAlbums<StorageEntity, AlbumEntity> StoragesInAlbums { get; set; }
            = new ObservableStoragesInAlbums<StorageEntity, AlbumEntity>();

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof  SQLite Entity Manager Data Table Storages Constructor.
        /// </summary>
        public StorageEntity() { }

        #endregion
    }
}