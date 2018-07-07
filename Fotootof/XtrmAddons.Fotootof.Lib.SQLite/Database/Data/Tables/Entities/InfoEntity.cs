using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies.Observables;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib SQLite Info Entity.
    /// </summary>
    [Serializable]
    [Table("Infos")]
    [JsonObject(MemberSerialization.OptIn)]
    public partial class InfoEntity : EntityBase
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        [NotMapped]
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable associated InfoType primary key.
        /// </summary>
        [NotMapped]
        public int infoTypeId = 0;

        /// <summary>
        /// Variable Name of the Info.
        /// </summary>
        [NotMapped]
        public string name = "";

        /// <summary>
        /// Variable Alias of the Info.
        /// </summary>
        [NotMapped]
        public string alias = "";

        /// <summary>
        /// Variable Description of the Info.
        /// </summary>
        [NotMapped]
        public string description = "";

        /// <summary>
        /// Variable is default.
        /// </summary>
        [NotMapped]
        public bool isDefault = false;

        /// <summary>
        /// Variable ordering.
        /// </summary>
        [NotMapped]
        public int ordering = 0;

        #endregion



        #region Obsolete Dependencies

        /// <summary>
        /// Variable list of Album associated to the Info.
        /// </summary>
        [NotMapped]
        [System.Obsolete("Use dependency References);")]
        private IEnumerable<AlbumEntity> albums;

        /// <summary>
        /// Variable list of Picture associated to the Info.
        /// </summary>
        [NotMapped]
        [System.Obsolete("Use dependency References);")]
        private IEnumerable<PictureEntity> pictures;

        #endregion



        #region Properties

        /// <summary>
        /// Propmerty to access to the primary key auto incremented.
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public int InfoId
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
        /// Propertyto access to the associated InfoType primary key.
        /// </summary>
        [Column(Order = 1)]
        [JsonProperty]
        public int InfoTypeId
        {
            get { return infoTypeId; }
            set
            {
                if (value != infoTypeId)
                {
                    infoTypeId = value;
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
        /// Property to access to the Alias of the Info.
        /// </summary>
        [Column(Order = 3)]
        [JsonProperty]
        public string Alias
        {
            get { return alias; }
            set
            {
                if (value != alias)
                {
                    alias = value.Sanitize().RemoveDiacritics().ToLower();
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the description of the Info.
        /// </summary>
        [Column(Order = 4)]
        [JsonProperty]
        public string Description
        {
            get { return description; }
            set
            {
                if (value != description)
                {
                    description = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to check if the Info is default.
        /// </summary>
        [Column(Order = 5)]
        [JsonProperty]
        public bool IsDefault
        {
            get { return isDefault; }
            set
            {
                if (value != isDefault)
                {
                    isDefault = value;
                    NotifyPropertyChanged();
                }
            }
        }

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
                InfosInAlbums.Populate();
                return InfosInAlbums.DepPKeys;
            }
        }

        /// <summary>
        /// Property to access to the list of Album associated to the Info.
        /// </summary>
        [NotMapped]
        public ObservableCollection<AlbumEntity> Albums
        {
            get
            {
                InfosInAlbums.Populate();
                return InfosInAlbums.DepReferences;
            }
        }

        /// <summary>
        /// Property collection of relationship Infos in Albums.
        /// </summary>
        public ObservableInfosInAlbums<InfoEntity, AlbumEntity> InfosInAlbums { get; set; }
            = new ObservableInfosInAlbums<InfoEntity, AlbumEntity>();

        #endregion



        #region Properties Dependencies Picture

        /// <summary>
        /// Variable Picture id (required for entity dependency).
        /// </summary>
        [NotMapped]
        public int PictureId { get; set; }

        /// <summary>
        /// Property to access to the list of Picture dependencies primary key.
        /// </summary>
        [NotMapped]
        public ObservableCollection<int> PicturesPKs
        {
            get
            {
                InfosInPictures.Populate();
                return InfosInPictures.DepPKeys;
            }
        }

        /// <summary>
        /// Property to access to the list of Picture associated to the Info.
        /// </summary>
        [NotMapped]
        public ObservableCollection<PictureEntity> Pictures
        {
            get
            {
                InfosInPictures.Populate();
                return InfosInPictures.DepReferences;
            }
        }

        /// <summary>
        /// Property collection of relationship Infos in Pictures.
        /// </summary>
        public ObservableInfosInPictures<InfoEntity, PictureEntity> InfosInPictures { get; set; }
            = new ObservableInfosInPictures<InfoEntity, PictureEntity>();

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Info Entity Constructor.
        /// </summary>
        public InfoEntity() { }

        #endregion



        #region Obsolete Dependencies Album

        /// <summary>
        /// Method to get list of associated Album.
        /// </summary>
        [System.Obsolete("Use dependency References);")]
        private IEnumerable<AlbumEntity> ListAlbums()
        {
            if (albums == null)
            {
                albums = new List<AlbumEntity>();

                if (InfosInAlbums != null)
                {
                    albums = ListEntities<AlbumEntity>(InfosInAlbums);
                }
            }

            return albums;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="albumId"></param>
        [System.Obsolete("Use => AlbumsPKeys.Add(AlbumPk);")]
        public void LinkAlbum(int albumId)
        {
            try
            {
                int index = InfosInAlbums.ToList().FindIndex(o => o.AlbumId == albumId);

                if (index < 0)
                {
                    InfosInAlbums.Add(new InfosInAlbums { AlbumId = albumId });
                }
            }
            catch { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="albumId"></param>
        [System.Obsolete("Use => AlbumsPKeys.Remove(AlbumPk);")]
        public void UnLinkAlbum(int albumId)
        {
            try
            {
                int index = InfosInAlbums.ToList().FindIndex(o => o.AlbumId == albumId);
                InfosInAlbums.RemoveAt(index);
            }
            catch { }
        }

        #endregion



        #region Obsolete Dependencies Picture

        /// <summary>
        /// Method to get list of associated Picture.
        /// </summary>
        [System.Obsolete("Use dependency References);")]
        private IEnumerable<PictureEntity> ListPictures()
        {
            if (pictures == null)
            {
                pictures = new List<PictureEntity>();

                if (InfosInPictures != null)
                {
                    pictures = ListEntities<PictureEntity>(InfosInPictures);
                }
            }

            return pictures;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pictureId"></param>
        [System.Obsolete("Use => PicturesPKs.Add(PicturePk);")]
        public void LinkPicture(int pictureId)
        {
            try
            {
                int index = InfosInPictures.ToList().FindIndex(o => o.PictureId == pictureId);

                if (index < 0)
                {
                    InfosInPictures.Add(new InfosInPictures { PictureId = pictureId });
                }
            }
            catch { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pictureId"></param>
        [System.Obsolete("Use => PicturesPKs.Remove(PicturePk);")]
        public void UnLinkPicture(int pictureId)
        {
            try
            {
                int index = InfosInPictures.ToList().FindIndex(o => o.PictureId == pictureId);
                InfosInPictures.RemoveAt(index);
            }
            catch { }
        }

        #endregion
    }
}