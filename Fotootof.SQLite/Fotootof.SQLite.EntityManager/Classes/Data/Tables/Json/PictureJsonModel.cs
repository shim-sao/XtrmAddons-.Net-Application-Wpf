using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.SQLite.EntityManager.Interfaces;
using Newtonsoft.Json;
using System;
using System.Xml.Serialization;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.SQLite.EntityManager.Data.Tables.Json.Models
{
    /// <summary>
    /// Class Fotootof.Plugin.Api Models Json Picture.
    /// </summary>
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public class PictureJsonModel : EntityJsonModel<PictureEntity>, ITablePictures
    {
        #region Variables

        /// <summary>
        /// Variable name of the item.
        /// </summary>
        private string name = "";

        /// <summary>
        /// Variable alias of the item.
        /// </summary>
        private string alias = "";

        /// <summary>
        /// Variable description of the item.
        /// </summary>
        private string description = "";

        /// <summary>
        /// Variable order place of the item.
        /// </summary>
        private int ordering = 0;

        /// <summary>
        /// Variable capture date.
        /// </summary>
        private DateTime captured = DateTime.Now;

        /// <summary>
        /// Variable created date.
        /// </summary>
        private DateTime created = DateTime.Now;

        /// <summary>
        /// Variable modified date.
        /// </summary>
        private DateTime modified = DateTime.Now;

        /// <summary>
        /// Variable the original path.
        /// </summary>
        private string originalPath = "";

        /// <summary>
        /// Variable the original width.
        /// </summary>
        private int originalWidth = 0;

        /// <summary>
        /// Variable the original height.
        /// </summary>
        private int originalHeight = 0;

        /// <summary>
        /// Variable the original length.
        /// </summary>
        private long originalLength = 0;

        /// <summary>
        /// Variable picture path.
        /// </summary>
        private string picturePath = "";

        /// <summary>
        /// Variable picture width.
        /// </summary>
        private int pictureWidth = 0;

        /// <summary>
        /// Variable picture height.
        /// </summary>
        private int pictureHeight = 0;

        /// <summary>
        /// Variable picture length.
        /// </summary>
        private long pictureLength = 0;

        /// <summary>
        /// Variable thumbnail picture path.
        /// </summary>
        private string thumbnailPath = "";

        /// <summary>
        /// Variable thumbnail width.
        /// </summary>
        private int thumbnailWidth = 0;

        /// <summary>
        /// Variable thumbnail height.
        /// </summary>
        private int thumbnailHeight = 0;

        /// <summary>
        /// Variable thumbnail length.
        /// </summary>
        private long thumbnailLength = 0;

        /// <summary>
        /// Variable comment.
        /// </summary>
        private string comment = "";

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the id or primary key auto incremented.
        /// </summary>
        public int PictureId
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
        /// Property to access to the name.
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
        /// Property to access to the alias.
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
        /// Property to access to the description.
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
        /// Property to access to the capture date.
        /// </summary>
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
        /// Property to access to the the original path.
        /// </summary>
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
        [JsonProperty(PropertyName = "Comment")]
        [XmlAttribute(DataType = "string", AttributeName = "Comment")]
        public string Comment
        {
            get => comment;
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



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Lib Api Models Json Picture Constructor.
        /// </summary>
        public PictureJsonModel() : base() { }

        /// <summary>
        /// Class XtrmAddons Fotootof Lib Api Models Json Picture Constructor.
        /// </summary>
        /// <param name="entity">A entity entity.</param>
        public PictureJsonModel(PictureEntity entity, bool auth = false)
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
        public override void FromEntity(PictureEntity entity, bool auth = false)
        {
            PrimaryKey = entity.PrimaryKey;
            Name = entity.Name;
            Alias = entity.Alias;
            Description = entity.Description;
            Ordering = entity.Ordering;
            Captured = entity.Captured;
            Created = entity.Created;
            Modified = entity.Modified;
            OriginalPath = entity.OriginalPath;
            OriginalWidth = entity.OriginalWidth;
            OriginalHeight = entity.OriginalHeight;
            OriginalLength = entity.OriginalLength;
            PicturePath = entity.PicturePath;
            PictureWidth = entity.PictureWidth;
            PictureHeight = entity.PictureHeight;
            PictureLength = entity.PictureLength;
            ThumbnailPath = entity.ThumbnailPath;
            ThumbnailWidth = entity.ThumbnailWidth;
            ThumbnailHeight = entity.ThumbnailHeight;
            ThumbnailLength = entity.ThumbnailLength;

            if (auth)
            {
                Comment = entity.Comment;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override PictureEntity ToEntity()
        {
            PictureEntity entity = new PictureEntity
            {
                PrimaryKey = PrimaryKey,
                Name = Name,
                Alias = Alias,
                Description = Description,
                Ordering = Ordering,
                Captured = Captured,
                Created = Created,
                Modified = Modified,
                OriginalPath = OriginalPath,
                OriginalWidth = OriginalWidth,
                OriginalHeight = OriginalHeight,
                OriginalLength = OriginalLength,
                PicturePath = PicturePath,
                PictureWidth = PictureWidth,
                PictureHeight = PictureHeight,
                PictureLength = PictureLength,
                ThumbnailPath = ThumbnailPath,
                ThumbnailWidth = ThumbnailWidth,
                ThumbnailHeight = ThumbnailHeight,
                ThumbnailLength = ThumbnailLength,
                Comment = Comment
            };

            return entity;
        }

        #endregion
    }
}
