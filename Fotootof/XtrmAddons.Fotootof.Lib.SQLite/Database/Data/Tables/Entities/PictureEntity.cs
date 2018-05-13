using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Picture Entity.
    /// </summary>
    [Table("Pictures")]
    [JsonObject(MemberSerialization.OptIn)]
    public class PictureEntity : EntityBase
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable name of the item.
        /// </summary>
        [NotMapped]
        private string name = "";

        /// <summary>
        /// Variable alias of the item.
        /// </summary>
        [NotMapped]
        private string alias = "";

        /// <summary>
        /// Variable description of the item.
        /// </summary>
        [NotMapped]
        private string description = "";

        /// <summary>
        /// Variable order place of the item.
        /// </summary>
        [NotMapped]
        private int ordering = 0;


        /// <summary>
        /// Variable capture date.
        /// </summary>
        [NotMapped]
        private DateTime captured = DateTime.Now;

        /// <summary>
        /// Variable created date.
        /// </summary>
        [NotMapped]
        private DateTime created = DateTime.Now;

        /// <summary>
        /// Variable modified date.
        /// </summary>
        [NotMapped]
        private DateTime modified = DateTime.Now;


        /// <summary>
        /// Variable the original path.
        /// </summary>
        [NotMapped]
        private string originalPath = "";

        /// <summary>
        /// Variable the original width.
        /// </summary>
        [NotMapped]
        private int originalWidth = 0;

        /// <summary>
        /// Variable the original height.
        /// </summary>
        [NotMapped]
        private int originalHeight = 0;

        /// <summary>
        /// Variable the original length.
        /// </summary>
        [NotMapped]
        private long originalLength = 0;

        /// <summary>
        /// Variable picture path.
        /// </summary>
        [NotMapped]
        private string picturePath = "";

        /// <summary>
        /// Variable picture width.
        /// </summary>
        [NotMapped]
        private int pictureWidth = 0;

        /// <summary>
        /// Variable picture height.
        /// </summary>
        [NotMapped]
        private int pictureHeight = 0;

        /// <summary>
        /// Variable picture length.
        /// </summary>
        [NotMapped]
        private long pictureLength = 0;

        /// <summary>
        /// Variable thumbnail picture path.
        /// </summary>
        [NotMapped]
        private string thumbnailPath = "";

        /// <summary>
        /// Variable thumbnail width.
        /// </summary>
        [NotMapped]
        private int thumbnailWidth = 0;

        /// <summary>
        /// Variable thumbnail height.
        /// </summary>
        [NotMapped]
        private int thumbnailHeight = 0;

        /// <summary>
        /// Variable thumbnail length.
        /// </summary>
        [NotMapped]
        private long thumbnailLength = 0;

        /// <summary>
        /// Variable comment.
        /// </summary>
        [NotMapped]
        private string comment = "";

        #endregion


        #region Variables Dependencies

        /// <summary>
        /// Variable list of Albums associated to the item.
        /// </summary>
        [NotMapped]
        private List<AlbumEntity> albums;

        /// <summary>
        /// Variable list of Infos associated to the Album.
        /// </summary>
        [NotMapped]
        private List<InfoEntity> infos;

        #endregion



        #region Variables Dependencies Album

        /// <summary>
        /// Variable Album id (required for entity dependency).
        /// </summary>
        [NotMapped]
        public int albumId = 0;


        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the id or primary key auto incremented.
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public int PictureId
        {
            get { return primaryKey; }
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
        /// Property to access to the name.
        /// </summary>
        [Column(Order = 1)]
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
        /// Property to access to the alias.
        /// </summary>
        [Column(Order = 2)]
        [JsonProperty]
        public string Alias
        {
            get { return alias; }
            set
            {
                if (value != alias)
                {
                    alias = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the description.
        /// </summary>
        [Column(Order = 3)]
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
        /// Property to access to the order place of the item.
        /// </summary>
        [Column(Order = 4)]
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

        /// <summary>
        /// Property to access to the capture date.
        /// </summary>
        [Column(Order = 5)]
        [JsonProperty]
        public DateTime Captured
        {
            get { return captured; }
            set
            {
                if (value != captured)
                {
                    captured = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the created date.
        /// </summary>
        [Column(Order = 6)]
        [JsonProperty]
        public DateTime Created
        {
            get { return created; }
            set
            {
                if (value != created)
                {
                    created = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the modified date.
        /// </summary>
        [Column(Order = 7)]
        [JsonProperty]
        public DateTime Modified
        {
            get { return modified; }
            set
            {
                if (value != modified)
                {
                    modified = value;
                    NotifyPropertyChanged();
                }
            }
        }


        /// <summary>
        /// Property to access to the the original path.
        /// </summary>
        [Column(Order = 8)]
        [JsonIgnore]
        [JsonProperty]
        public string OriginalPath
        {
            get { return originalPath; }
            set
            {
                if (value != originalPath)
                {
                    originalPath = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the the original width.
        /// </summary>
        [Column(Order = 9)]
        [JsonProperty]
        public int OriginalWidth
        {
            get { return originalWidth; }
            set
            {
                if (value != originalWidth)
                {
                    originalWidth = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the the original height.
        /// </summary>
        [Column(Order = 10)]
        [JsonProperty]
        public int OriginalHeight
        {
            get { return originalHeight; }
            set
            {
                if (value != originalHeight)
                {
                    originalHeight = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the the original length.
        /// </summary>
        [Column(Order = 11)]
        [JsonProperty]
        public long OriginalLength
        {
            get { return originalLength; }
            set
            {
                if (value != originalLength)
                {
                    originalLength = value;
                    NotifyPropertyChanged();
                }
            }
        }


        /// <summary>
        /// Property to access to the picture path.
        /// </summary>
        [Column(Order = 12)]
        [JsonProperty]
        public string PicturePath
        {
            get { return picturePath; }
            set
            {
                if (value != picturePath)
                {
                    picturePath = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the picture width.
        /// </summary>
        [Column(Order = 13)]
        [JsonProperty]
        public int PictureWidth
        {
            get { return pictureWidth; }
            set
            {
                if (value != pictureWidth)
                {
                    pictureWidth = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the picture height.
        /// </summary>
        [Column(Order = 14)]
        [JsonProperty]
        public int PictureHeight
        {
            get { return pictureHeight; }
            set
            {
                if (value != pictureHeight)
                {
                    pictureHeight = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the picture length.
        /// </summary>
        [Column(Order = 15)]
        [JsonProperty]
        public long PictureLength
        {
            get { return pictureLength; }
            set
            {
                if (value != pictureLength)
                {
                    pictureLength = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the thumbnail picture path.
        /// </summary>
        [Column(Order = 16)]
        [JsonProperty]
        public string ThumbnailPath
        {
            get { return thumbnailPath; }
            set
            {
                if (value != thumbnailPath)
                {
                    thumbnailPath = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the thumbnail width.
        /// </summary>
        [Column(Order = 17)]
        [JsonProperty]
        public int ThumbnailWidth
        {
            get { return thumbnailWidth; }
            set
            {
                if (value != thumbnailWidth)
                {
                    thumbnailWidth = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the thumbnail height.
        /// </summary>
        [Column(Order = 18)]
        [JsonProperty]
        public int ThumbnailHeight
        {
            get { return thumbnailHeight; }
            set
            {
                if (value != thumbnailHeight)
                {
                    thumbnailHeight = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the thumbnail length.
        /// </summary>
        [Column(Order = 19)]
        [JsonProperty]
        public long ThumbnailLength
        {
            get { return thumbnailLength; }
            set
            {
                if (value != thumbnailLength)
                {
                    thumbnailLength = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the comment.
        /// </summary>
        [Column(Order = 20)]
        [JsonProperty]
        public string Comment
        {
            get { return comment; }
            set
            {
                if (value != comment)
                {
                    comment = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion



        #region Properties Dependencies Album

        /// <summary>
        /// Property to access to the collection of relationship Pictures In Albums entities.
        /// </summary>
        public ObservableCollection<PicturesInAlbums> PicturesInAlbums { get; set; }
            = new ObservableCollection<PicturesInAlbums>();

        /// <summary>
        /// Property to access to the Album id (required for entity dependency).
        /// </summary>
        [NotMapped]
        public int AlbumId
        {
            get { return albumId; }
            set
            {
                if (value != albumId)
                {
                    albumId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the list of Album dependencies primary key.
        /// </summary>
        [NotMapped]
        public IEnumerable<int> AlbumsPK
            => ListOfPrimaryKeys(PicturesInAlbums.ToList(), "AlbumId");

        /// <summary>
        /// Property to access to the list of Albums associated to the item.
        /// </summary>
        [NotMapped]
        public List<AlbumEntity> Albums { get => ListAlbums(); set => albums = value; }


        #endregion



        #region Properties Dependencies Info

        /// <summary>
        /// Property Info id (required for entity dependency).
        /// </summary>
        [NotMapped]
        public int InfoId { get; set; }

        /// <summary>
        /// Property to access to the list of Infos associated to the item.
        /// </summary>
        [NotMapped]
        public List<InfoEntity> Infos { get => ListInfos(); set => infos = value; }

        /// <summary>
        /// Propertiy to access to the list of Info dependencies primary key.
        /// </summary>
        [NotMapped]
        public IEnumerable<int> InfosPK => ListOfPrimaryKeys(InfosInPictures.ToList(), "InfoId");

        /// <summary>
        /// Property to access to the collection of relationship Infos In Pictures entities.
        /// </summary>
        public ObservableCollection<InfosInPictures> InfosInPictures { get; set; }
            = new ObservableCollection<InfosInPictures>();

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries SQLite Picture Entity Constructor.
        /// </summary>
        public PictureEntity()
        {
            PicturesInAlbums.CollectionChanged += (s, e) => { albums = null; };
            InfosInPictures.CollectionChanged += (s, e) => { infos = null; };
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to get a list of associated Album of the Picture.
        /// </summary>
        /// <returns>A list of associated Album to the Picture.</returns>
        private List<AlbumEntity> ListAlbums()
        {
            if (albums == null)
            {
                albums = new List<AlbumEntity>();

                if (PicturesInAlbums != null)
                {
                    albums = ListEntities<AlbumEntity>(PicturesInAlbums);
                }
            }

            return albums;
        }

        /// <summary>
        /// Method to get a list of associated Info to the Picture.
        /// </summary>
        /// <returns>A list of associated Info to the Picture.</returns>
        private List<InfoEntity> ListInfos()
        {
            if (infos == null)
            {
                infos = new List<InfoEntity>();

                if (InfosInPictures != null)
                {
                    infos = ListEntities<InfoEntity>(InfosInPictures);
                }
            }

            return infos;
        }

        #endregion
    }
}
