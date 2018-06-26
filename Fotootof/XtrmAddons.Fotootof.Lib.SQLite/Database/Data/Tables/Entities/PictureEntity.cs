using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Xml.Serialization;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Picture Entity.
    /// </summary>
    [Table("Pictures"), Serializable, JsonObject(MemberSerialization.OptIn)]
    public class PictureEntity : EntityBase
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        [NotMapped, XmlIgnore]
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable name of the item.
        /// </summary>
        [NotMapped, XmlIgnore]
        private string name = "";

        /// <summary>
        /// Variable alias of the item.
        /// </summary>
        [NotMapped, XmlIgnore]
        private string alias = "";

        /// <summary>
        /// Variable description of the item.
        /// </summary>
        [NotMapped, XmlIgnore]
        private string description = "";

        /// <summary>
        /// Variable order place of the item.
        /// </summary>
        [NotMapped, XmlIgnore]
        private int ordering = 0;


        /// <summary>
        /// Variable capture date.
        /// </summary>
        [NotMapped, XmlIgnore]
        private DateTime captured = DateTime.Now;

        /// <summary>
        /// Variable created date.
        /// </summary>
        [NotMapped, XmlIgnore]
        private DateTime created = DateTime.Now;

        /// <summary>
        /// Variable modified date.
        /// </summary>
        [NotMapped, XmlIgnore]
        private DateTime modified = DateTime.Now;


        /// <summary>
        /// Variable the original path.
        /// </summary>
        [NotMapped, XmlIgnore]
        private string originalPath = "";

        /// <summary>
        /// Variable the original width.
        /// </summary>
        [NotMapped, XmlIgnore]
        private int originalWidth = 0;

        /// <summary>
        /// Variable the original height.
        /// </summary>
        [NotMapped, XmlIgnore]
        private int originalHeight = 0;

        /// <summary>
        /// Variable the original length.
        /// </summary>
        [NotMapped, XmlIgnore]
        private long originalLength = 0;

        /// <summary>
        /// Variable picture path.
        /// </summary>
        [NotMapped, XmlIgnore]
        private string picturePath = "";

        /// <summary>
        /// Variable picture width.
        /// </summary>
        [NotMapped, XmlIgnore]
        private int pictureWidth = 0;

        /// <summary>
        /// Variable picture height.
        /// </summary>
        [NotMapped, XmlIgnore]
        private int pictureHeight = 0;

        /// <summary>
        /// Variable picture length.
        /// </summary>
        [NotMapped, XmlIgnore]
        private long pictureLength = 0;

        /// <summary>
        /// Variable thumbnail picture path.
        /// </summary>
        [NotMapped, XmlIgnore]
        private string thumbnailPath = "";

        /// <summary>
        /// Variable thumbnail width.
        /// </summary>
        [NotMapped, XmlIgnore]
        private int thumbnailWidth = 0;

        /// <summary>
        /// Variable thumbnail height.
        /// </summary>
        [NotMapped, XmlIgnore]
        private int thumbnailHeight = 0;

        /// <summary>
        /// Variable thumbnail length.
        /// </summary>
        [NotMapped, XmlIgnore]
        private long thumbnailLength = 0;

        /// <summary>
        /// Variable comment.
        /// </summary>
        [NotMapped, XmlIgnore]
        private string comment = "";

        #endregion


        #region Variables Dependencies Album

        /// <summary>
        /// Variable Album id (required for entity dependency).
        /// </summary>
        [NotMapped, XmlIgnore]
        public int albumId = 0;

        /// <summary>
        /// Variable list of Albums associated to the item.
        /// </summary>
        [NotMapped, XmlIgnore]
        private List<AlbumEntity> albums;

        #endregion



        #region Variables Dependencies

        /// <summary>
        /// Variable list of Infos associated to the Album.
        /// </summary>
        [NotMapped, XmlIgnore]
        private List<InfoEntity> infos;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the id or primary key auto incremented.
        /// </summary>
        [Key]
        [Column(Order = 0)]
        [XmlIgnore]
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
        [JsonProperty(PropertyName ="Name")]
        [XmlAttribute(DataType = "string", AttributeName = "Name")]
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
        [JsonProperty(PropertyName = "Alias")]
        [XmlAttribute(DataType = "string", AttributeName = "Alias")]
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
        /// Property to access to the description.
        /// </summary>
        [Column(Order = 3)]
        [JsonProperty(PropertyName = "Description")]
        [XmlAttribute(DataType = "string", AttributeName = "Description")]
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
        [JsonProperty(PropertyName = "Ordering")]
        [XmlAttribute(DataType = "int", AttributeName = "Ordering")]
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
        [JsonProperty(PropertyName = "Captured")]
        [XmlAttribute(DataType = "string", AttributeName = "Captured")]
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
        [JsonProperty(PropertyName = "Created")]
        [XmlAttribute(DataType = "string", AttributeName = "Created")]
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
        [JsonProperty(PropertyName = "Modified")]
        [XmlAttribute(DataType = "string", AttributeName = "Modified")]
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
        [JsonProperty(PropertyName = "OriginalPath")]
        [XmlAttribute(DataType = "string", AttributeName = "OriginalPath")]
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
        [JsonProperty(PropertyName = "OriginalWidth")]
        [XmlAttribute(DataType = "int", AttributeName = "OriginalWidth")]
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
        [JsonProperty(PropertyName = "OriginalHeight")]
        [XmlAttribute(DataType = "int", AttributeName = "OriginalHeight")]
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
        [JsonProperty(PropertyName = "OriginalLength")]
        [XmlAttribute(DataType = "int", AttributeName = "OriginalLength")]
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
        [JsonProperty(PropertyName = "PicturePath")]
        [XmlAttribute(DataType = "string", AttributeName = "PicturePath")]
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
        [JsonProperty(PropertyName = "PictureWidth")]
        [XmlAttribute(DataType = "int", AttributeName = "PictureWidth")]
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
        [JsonProperty(PropertyName = "PictureHeight")]
        [XmlAttribute(DataType = "int", AttributeName = "PictureHeight")]
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
        [JsonProperty(PropertyName = "PictureLength")]
        [XmlAttribute(DataType = "int", AttributeName = "PictureLength")]
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
        [JsonProperty(PropertyName = "ThumbnailPath")]
        [XmlAttribute(DataType = "string", AttributeName = "ThumbnailPath")]
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
        [JsonProperty(PropertyName = "ThumbnailWidth")]
        [XmlAttribute(DataType = "int", AttributeName = "ThumbnailWidth")]
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
        [JsonProperty(PropertyName = "ThumbnailHeight")]
        [XmlAttribute(DataType = "int", AttributeName = "ThumbnailHeight")]
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
        [JsonProperty(PropertyName = "ThumbnailLength")]
        [XmlAttribute(DataType = "int", AttributeName = "ThumbnailLength")]
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
        [JsonProperty(PropertyName = "Comment")]
        [XmlAttribute(DataType = "string", AttributeName = "Comment")]
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
        [NotMapped, XmlIgnore]
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
        public IEnumerable<int> AlbumsPKs
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
