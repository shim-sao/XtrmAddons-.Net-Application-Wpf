using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies.Observables;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Database Section Entity.
    /// </summary>
    [Table("Sections"), Serializable, JsonObject(MemberSerialization.OptIn)]
    public partial class SectionEntity : EntityBase
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        [NotMapped, XmlIgnore]
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable name of the Section.
        /// </summary>
        [NotMapped, XmlIgnore]
        private string name = "";

        /// <summary>
        /// Variable alias of the Section.
        /// </summary>
        [NotMapped, XmlIgnore]
        private string alias = "";

        /// <summary>
        /// Variable description of the Section.
        /// </summary>
        [NotMapped, XmlIgnore]
        public string description = "";

        /// <summary>
        /// Variable is default item.
        /// </summary>
        [NotMapped, XmlIgnore]
        public bool isDefault = false;

        /// <summary>
        /// Variable order place of the item.
        /// </summary>
        [NotMapped, XmlIgnore]
        public int ordering = 0;

        /// <summary>
        /// Variable the background picture id.
        /// </summary>
        [NotMapped, XmlIgnore]
        public int backgroundPictureId = 0;

        /// <summary>
        /// Variable the preview picture id.
        /// </summary>
        [NotMapped, XmlIgnore]
        public int previewPictureId = 0;

        /// <summary>
        /// Variable the thumbnail picture id.
        /// </summary>
        [NotMapped, XmlIgnore]
        public int thumbnailPictureId = 0;

        /// <summary>
        /// Variable comment for the item.
        /// </summary>
        [NotMapped, XmlIgnore]
        public string comment = "";

        /// <summary>
        /// Variable parameters for the item.
        /// </summary>
        [NotMapped, XmlIgnore]
        public string parameters = "";

        #endregion



        #region Variables Dependencies

        /// <summary>
        /// Variable Album id (required for entity dependency).
        /// </summary>
        [NotMapped, XmlIgnore]
        private int albumId = 0;

        /// <summary>
        /// Variable AclGroup id (required for entity dependency).
        /// </summary>
        [NotMapped, XmlIgnore]
        private int aclGroupId = 0;

        #endregion



        #region Obsolete Variables Dependencies

        /// <summary>
        /// Variables collection of relationship Albums in Sections. Sections dependencies.
        /// </summary>
        [NotMapped, XmlIgnore, NonSerialized]
        [System.Obsolete("Use dependency References);")]
        private ObservableAlbumsInSections<SectionEntity, AlbumEntity> albumsDependencies =
            new ObservableAlbumsInSections<SectionEntity, AlbumEntity>();

        /// <summary>
        /// Variable list of AclGroups associated to the Section.
        /// </summary>
        [NotMapped, XmlIgnore]
        [System.Obsolete("Use dependency References);")]
        private IEnumerable<AclGroupEntity> aclGroups;

        #endregion



        #region Proprerties

        /// <summary>
        /// Property primary key auto incremented.
        /// </summary>
        [Key]
        [Column(Order = 0)]
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
        [Column(Order = 1)]
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
        /// Property to access to the description of the item.
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
        /// Property to access to the is default item.
        /// </summary>
        [Column(Order = 4)]
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
        [Column(Order = 5)]
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
        [Column(Order = 6)]
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
        [Column(Order = 7)]
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
        [Column(Order = 8)]
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
        [Column(Order = 9)]
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
        [Column(Order = 10)]
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

        #endregion



        #region Proprerties Dependencies Album

        /// <summary>
        /// Property Album id (required for entity dependency).
        /// </summary>
        [NotMapped, XmlIgnore]
        public int AlbumId
        {
            get => albumId;
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
        [JsonProperty(PropertyName = "AlbumsPKs")]
        [XmlElement(ElementName = "AlbumsPKs")]
        public ObservableCollection<int> AlbumsPKs
        {
            get
            {
                AlbumsInSections.Populate();
                return AlbumsInSections.DepPKeys;
            }
        }

        /// <summary>
        /// Property to access to the list of Albums associated to the Section.
        /// </summary>
        [NotMapped]
        [JsonProperty(PropertyName = "Albums")]
        [XmlElement(ElementName = "Albums")]
        public ObservableCollection<AlbumEntity> Albums
        {
            get
            {
                AlbumsInSections.Populate();
                return AlbumsInSections.DepReferences;
            }
        }

        /// <summary>
        /// Property to access to the collection of relationship Albums in Sections.
        /// </summary>
        public ObservableAlbumsInSections<SectionEntity, AlbumEntity> AlbumsInSections { get; set; }
            = new ObservableAlbumsInSections<SectionEntity, AlbumEntity>();

        #endregion



        #region Proprerties Dependencies AclGroup

        /// <summary>
        /// Property AclGroup id (required for entity dependency).
        /// </summary>
        [NotMapped]
        public int AclGroupId
        {
            get => aclGroupId;
            set
            {
                if (value != aclGroupId)
                {
                    aclGroupId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the list of AclGroup dependencies primary key.
        /// </summary>
        [NotMapped]
        [JsonProperty(PropertyName = "AclGroupsPKs")]
        [XmlElement(ElementName = "AclGroupsPKs")]
        public ObservableCollection<int> AclGroupsPKs
        {
            get
            {
                SectionsInAclGroups.Populate();
                return SectionsInAclGroups.DepPKeys;
            }
        }

        /// <summary>
        /// Property to access to the list of AclGroups associated to the Section.
        /// </summary>
        [NotMapped]
        [JsonProperty(PropertyName = "AclGroups")]
        [XmlElement(ElementName = "AclGroups")]
        public ObservableCollection<AclGroupEntity> AclGroups
        {
            get
            {
                SectionsInAclGroups.Populate();
                return SectionsInAclGroups.DepReferences;
            }
        }

        /// <summary>
        /// Property to access to the collection of relationship Albums in Sections.
        /// </summary>
        public ObservableSectionsInAclGroups<SectionEntity, AclGroupEntity> SectionsInAclGroups { get; set; }
            = new ObservableSectionsInAclGroups<SectionEntity, AclGroupEntity>();

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries SQLite Section Entity Constructor.
        /// </summary>
        public SectionEntity() { }

        #endregion



        #region Methods AclGroup

        /// <summary>
        /// Method to get the list of associated AclGroups to the Section.
        /// </summary>
        /// <returns>The list of associated AclGroups to the Section.</returns>
        [System.Obsolete("Use dependency References);")]
        private IEnumerable<AclGroupEntity> ListAclGroups()
        {
            if (aclGroups == null)
            {
                aclGroups = new List<AclGroupEntity>();

                if (SectionsInAclGroups != null)
                {
                    aclGroups = ListEntities<AclGroupEntity>(SectionsInAclGroups);
                }
            }

            return aclGroups;
        }

        /// <summary>
        /// Method to associate an AclGroup to the Section.
        /// </summary>
        /// <param name="aclGroupPk">An album id or primary key.</param>
        [System.Obsolete("Use => AclGroupsPKs.Add(aclGroupPk);")]
        public bool LinkAclGroup(int aclGroupPk)
        {
            log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : {aclGroupPk}");

            try
            {
                int index = SectionsInAclGroups.ToList().FindIndex(o => o.AclGroupId == aclGroupPk);
                if (index == -1)
                {
                    SectionsInAclGroups.Add(new SectionsInAclGroups { AclGroupId = aclGroupPk });
                }
                return true;
            }
            catch(Exception e)
            {
                log.Debug(e.Output(), e);
                return false;
            }
        }

        /// <summary>
        /// Method to unlink an AclGroup of the Section.
        /// </summary>
        /// <param name="aclGroupPk">An album id or primary key.</param>
        [System.Obsolete("Use => AclGroupsPKs.Remove(aclGroupPk);")]
        public bool UnLinkAclGroup(int aclGroupPk)
        {
            log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : {aclGroupPk}");

            try
            {
                int index = SectionsInAclGroups.ToList().FindIndex(o => o.AclGroupId == aclGroupPk);
                if (index != -1)
                {
                    SectionsInAclGroups.RemoveAt(index);
                }
                return true;
            }
            catch (Exception e)
            {
                log.Debug(e.Output(), e);
                return false;
            }
        }

        #endregion



        #region Methods Album

        /// <summary>
        /// Method to associate an Album to the Section.
        /// </summary>
        /// <param name="albumPk">An album id or primary key.</param>
        [System.Obsolete("Use => AlbumsPKs.Add(AlbumPk);")]
        public bool LinkAlbum(int albumPk)
        {
            log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : {albumPk}");

            try
            {
                int index = AlbumsInSections.ToList().FindIndex(o => o.AlbumId == albumPk);
                if (index == -1)
                {
                    AlbumsInSections.Add(new AlbumsInSections { AlbumId = albumPk });
                }
                return true;
            }
            catch (Exception e)
            {
                log.Error(e.Output(), e);
                return false;
            }
        }

        /// <summary>
        /// Method to unlink an Album of the Section.
        /// </summary>
        /// <param name="albumPk">An album id or primary key.</param>
        [System.Obsolete("Use => AlbumsPKs.Remove(AlbumPk);")]
        public bool UnlinkAlbum(int albumPk)
        {
            log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : {albumPk}");

            try
            {
                int index = AlbumsInSections.ToList().FindIndex(o => o.AlbumId == albumPk);
                if (index != -1)
                {
                    AlbumsInSections.RemoveAt(index);
                }
                return true;
            }
            catch (Exception e)
            {
                log.Error(e.Output(), e);
                return false;
            }
        }
        
        #endregion
    }
}