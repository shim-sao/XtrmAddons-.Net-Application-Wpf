using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.SQLite.EntityManager.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.SQLite.EntityManager.Data.Tables.Json.Models
{
    /// <summary>
    /// Class Fotootof.Plugin.Api Models Json Album.
    /// </summary>
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public class AlbumJsonModel : EntityJsonModel<AlbumEntity>, ITableAlbums
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable name of the Album.
        /// </summary>
        private string name = "";

        /// <summary>
        /// Variable alias of the Album.
        /// </summary>
        private string alias = "";

        /// <summary>
        /// Variable description of the Album.
        /// </summary>
        private string description = "";

        /// <summary>
        /// Variable order place of the item.
        /// </summary>
        private int ordering = 0;

        /// <summary>
        /// Variable created date.
        /// </summary>
        private DateTime created = DateTime.Now;

        /// <summary>
        /// Variable modified date.
        /// </summary>
        private DateTime modified = DateTime.Now;

        /// <summary>
        /// Variable first picture captured date.
        /// </summary>
        private DateTime dateStart = DateTime.Now;

        /// <summary>
        /// Variable last picture captured date.
        /// </summary>
        private DateTime dateEnd = DateTime.Now;

        /// <summary>
        /// Variable the background picture id.
        /// </summary>
        private int backgroundPictureId = 0;

        /// <summary>
        /// Variable the preview picture id.
        /// </summary>
        private int previewPictureId = 0;

        /// <summary>
        /// Variable the thumbnail picture id.
        /// </summary>
        private int thumbnailPictureId = 0;

        /// <summary>
        /// Variable comment for the item.
        /// </summary>
        private string comment = "";

        /// <summary>
        /// Variable parameters for the item.
        /// </summary>
        private string parameters = "";

        #endregion



        #region Variables Associated Pictures

        /// <summary>
        /// Variable background picture.
        /// </summary>
        public PictureJsonModel backgroundPicture;

        /// <summary>
        /// Variable preview picture.
        /// </summary>
        public PictureJsonModel previewPicture;

        /// <summary>
        /// Variable thumbnail picture.
        /// </summary>
        public PictureJsonModel thumbnailPicture;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the id or primary key auto incremented.
        /// </summary>
        public int AlbumId
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
        /// Property to access to the name of the item.
        /// </summary>
        [JsonProperty(PropertyName = "Name")]
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
        /// Property to access to the alias of the Album.
        /// </summary>
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
        /// Property to access to the description of the item.
        /// </summary>
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
        /// Property to access to the created date.
        /// </summary>
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
        /// Property to access to the first picture captured date.
        /// </summary>
        [JsonProperty(PropertyName = "DateStart")]
        [XmlAttribute(DataType = "string", AttributeName = "DateStart")]
        public DateTime DateStart
        {
            get { return dateStart; }
            set
            {
                if (value != dateStart)
                {
                    dateStart = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the last picture captured date.
        /// </summary>
        [JsonProperty(PropertyName = "DateEnd")]
        [XmlAttribute(DataType = "string", AttributeName = "DateEnd")]
        public DateTime DateEnd
        {
            get { return dateEnd; }
            set
            {
                if (value != dateEnd)
                {
                    dateEnd = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the background picture id.
        /// </summary>
        [JsonProperty(PropertyName = "BackgroundPictureId")]
        [XmlAttribute(DataType = "int", AttributeName = "BackgroundPictureId")]
        public int BackgroundPictureId
        {
            get { return backgroundPictureId; }
            set
            {
                if (value != backgroundPictureId)
                {
                    backgroundPictureId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the preview picture id.
        /// </summary>
        [JsonProperty(PropertyName = "PreviewPictureId")]
        [XmlAttribute(DataType = "int", AttributeName = "PreviewPictureId")]
        public int PreviewPictureId
        {
            get { return previewPictureId; }
            set
            {
                if (value != previewPictureId)
                {
                    previewPictureId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the thumbnail picture id.
        /// </summary>
        [JsonProperty(PropertyName = "ThumbnailPictureId")]
        [XmlAttribute(DataType = "int", AttributeName = "ThumbnailPictureId")]
        public int ThumbnailPictureId
        {
            get { return thumbnailPictureId; }
            set
            {
                if (value != thumbnailPictureId)
                {
                    thumbnailPictureId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the comment for the item.
        /// </summary>
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

        /// <summary>
        /// Property to access to the parameters for the item.
        /// </summary>
        [JsonProperty(PropertyName = "Parameters")]
        [XmlAttribute(DataType = "string", AttributeName = "Parameters")]
        public string Parameters
        {
            get { return parameters; }
            set
            {
                if (value != parameters)
                {
                    parameters = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Variable list of categories in folder.
        /// </summary>
        public List<PictureJsonModel> Pictures { get; set; } = new List<PictureJsonModel>();

        #endregion



        #region Constructor

        /// <summary>
        /// Class Fotootof Plugin Api Models Json Album constructor.
        /// </summary>
        /// <param name="album">An Album Entity.</param>
        public AlbumJsonModel() : base() { }

        /// <summary>
        /// Class Fotootof Plugin Api Models Json Album constructor.
        /// </summary>
        /// <param name="album">An Album Entity.</param>
        public AlbumJsonModel(AlbumEntity entity, bool auth = false)
        {
            FromEntity(entity, auth);
        }

        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="auth"></param>
        public override void FromEntity(AlbumEntity entity, bool auth = false)
        {
            PrimaryKey          = entity.PrimaryKey;
            Name                = entity.Name;
            Alias               = entity.Alias;
            Description         = entity.Description;
            Ordering            = entity.Ordering;
            DateStart           = entity.DateStart;
            DateEnd             = entity.DateEnd;
            Created             = entity.Created;
            Modified            = entity.Modified;
            BackgroundPictureId = entity.BackgroundPictureId;
            PreviewPictureId    = entity.PreviewPictureId;
            ThumbnailPictureId  = entity.ThumbnailPictureId;
            Parameters          = entity.Parameters;

            BackgroundPicture   = new PictureJsonModel(entity.BackgroundPicture);
            PreviewPicture      = new PictureJsonModel(entity.PreviewPicture);
            ThumbnailPicture    = new PictureJsonModel(entity.ThumbnailPicture);

            if (auth)
            {
                Comment = entity.Comment;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override AlbumEntity ToEntity()
        {
            AlbumEntity entity = new AlbumEntity
            {
                PrimaryKey          = PrimaryKey,
                Name                = Name,
                Alias               = Alias,
                Description         = Description,
                Ordering            = Ordering,
                DateStart           = DateStart,
                DateEnd             = DateEnd,
                Created             = Created,
                Modified            = Modified,
                BackgroundPictureId = BackgroundPictureId,
                PreviewPictureId    = PreviewPictureId,
                ThumbnailPictureId  = ThumbnailPictureId,
                Parameters          = Parameters,
                Comment             = Comment
            };

            return entity;
        }

        #endregion



        #region Properties Associated Pictures

        /// <summary>
        /// Property to access to the background picture entity.
        /// </summary>
        [JsonProperty(PropertyName = "BackgroundPicture")]
        [XmlElement(ElementName = "BackgroundPicture")]
        public PictureJsonModel BackgroundPicture
        {
            get => backgroundPicture;
            set
            {
                if (value != backgroundPicture)
                {
                    backgroundPicture = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the preview picture entity.
        /// </summary>
        [JsonProperty(PropertyName = "PreviewPicture")]
        [XmlElement(ElementName = "PreviewPicture")]
        public PictureJsonModel PreviewPicture
        {
            get => previewPicture;
            set
            {
                if (value != previewPicture)
                {
                    previewPicture = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the thumbnail picture entity.
        /// </summary>
        [JsonProperty(PropertyName = "ThumbnailPicture")]
        [XmlElement(ElementName = "ThumbnailPicture")]
        public PictureJsonModel ThumbnailPicture
        {
            get => thumbnailPicture;
            set
            {
                if (value != thumbnailPicture)
                {
                    thumbnailPicture = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion
    }
}