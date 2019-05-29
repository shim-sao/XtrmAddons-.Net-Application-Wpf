using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.SQLite.EntityManager.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.SQLite.EntityManager.Data.Tables.Json.Models
{
    /// <summary>
    /// Class XtrmAddons Fotootof SQLite Entity Manager Data Tables Json Model Entity.
    /// </summary>
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public class SectionJsonModel : EntityJsonModel<SectionEntity>,
        IColumnNameAlias,
        IColumnDescription,
        IColumnIsDefault,
        IColumnOrdering,
        IColumnBackgroundPictureId,
        IColumnPreviewPictureId,
        IColumnThumbnailPictureId,
        IColumnComment
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        [XmlIgnore]
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable name of the Section.
        /// </summary>
        [XmlIgnore]
        private string name = "";

        /// <summary>
        /// Variable alias of the Section.
        /// </summary>
        [XmlIgnore]
        private string alias = "";

        /// <summary>
        /// Variable description of the Section.
        /// </summary>
        [XmlIgnore]
        public string description = "";

        /// <summary>
        /// Variable is default item.
        /// </summary>
        [XmlIgnore]
        public bool isDefault = false;

        /// <summary>
        /// Variable order place of the item.
        /// </summary>
        [XmlIgnore]
        public int ordering = 0;

        /// <summary>
        /// Variable the background picture id.
        /// </summary>
        [XmlIgnore]
        public int backgroundPictureId = 0;

        /// <summary>
        /// Variable the preview picture id.
        /// </summary>
        [XmlIgnore]
        public int previewPictureId = 0;

        /// <summary>
        /// Variable the thumbnail picture id.
        /// </summary>
        [XmlIgnore]
        public int thumbnailPictureId = 0;

        /// <summary>
        /// Variable comment for the item.
        /// </summary>
        [XmlIgnore]
        public string comment = "";

        /// <summary>
        /// Variable parameters for the item.
        /// </summary>
        [XmlIgnore]
        public string parameters = "";

        #endregion



        #region Properties

        /// <summary>
        /// Property primary key auto incremented.
        /// </summary>
        public int SectionId
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
        /// Property to access to the name of the Section.
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
        /// <para>Property to access to the alias of the Section.</para>
        /// <para>The alias is automaticaly formated when it is changed.</para>
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
        /// Property to access to the is default item.
        /// </summary>
        [JsonProperty(PropertyName = "IsDefault")]
        [XmlAttribute(DataType = "boolean", AttributeName = "IsDefault")]
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
        /// Property to access to the list of Album dependencies primary key.
        /// </summary>
        [JsonProperty(PropertyName = "AlbumsPKeys")]
        [XmlArray(elementName: "AlbumsPKeys")]
        public ObservableCollection<int> AlbumsPKeys { get; set; }

        /// <summary>
        /// Property to access to the list of Album dependencies.
        /// </summary>
        [JsonProperty(PropertyName = "Albums")]
        [XmlArray(elementName: "Albums")]
        public ObservableCollection<AlbumJsonModel> Albums { get; set; }

        /// <summary>
        /// Property to access to the list of AclGroup dependencies primary key.
        /// </summary>
        [JsonProperty(PropertyName = "AclGroupsPKeys")]
        [XmlArray(elementName: "AclGroupsPKeys")]
        public ObservableCollection<int> AclGroupsPKeys { get; set; }

        #endregion



        #region Constructors

        /// <summary>
        /// Class XtrmAddons Fotootof Lib Api Models Json Picture Constructor.
        /// </summary>
        public SectionJsonModel() : base() { }

        /// <summary>
        /// Class XtrmAddons PhotoAlbum Server Api Models Json Category constructor.
        /// </summary>
        /// <param name="category"></param>
        public SectionJsonModel(SectionEntity entity, bool auth = false)
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
        public override void FromEntity(SectionEntity entity, bool auth = false)
        {
            PrimaryKey = entity.PrimaryKey;
            Name = entity.Name;
            Alias = entity.Alias;
            Description = entity.Description;
            Ordering = entity.Ordering;
            IsDefault = entity.IsDefault;
            BackgroundPictureId = entity.BackgroundPictureId;
            PreviewPictureId = entity.PreviewPictureId;
            ThumbnailPictureId = entity.ThumbnailPictureId;
            Parameters = entity.Parameters;
            AlbumsPKeys = entity.AlbumsPKeys;
            AclGroupsPKeys = entity.AclGroupsPKeys;

            if (auth)
            {
                Comment = entity.Comment;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override SectionEntity ToEntity()
        {
            SectionEntity entity = new SectionEntity
            {
                PrimaryKey = PrimaryKey,
                Name = Name,
                Alias = Alias,
                Description = Description,
                Ordering = Ordering,
                IsDefault = IsDefault,
                BackgroundPictureId = BackgroundPictureId,
                PreviewPictureId = PreviewPictureId,
                ThumbnailPictureId = ThumbnailPictureId,
                Parameters = Parameters,
                Comment = Comment
            };

            if(AlbumsPKeys != null)
            {
                foreach(int k in AlbumsPKeys)
                {
                    entity.AlbumsPKeys?.Add(k);
                }
            }

            if(AclGroupsPKeys != null)
            {
                foreach(int k in AclGroupsPKeys)
                {
                    entity.AclGroupsPKeys?.Add(k);
                }
            }

            return entity;
        }

        #endregion
    }
}
