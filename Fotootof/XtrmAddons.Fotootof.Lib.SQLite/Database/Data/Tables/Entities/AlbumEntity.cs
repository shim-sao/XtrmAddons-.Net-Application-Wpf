using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base.Interfaces;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies.Observables;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Libraries SQLite Album Entity.</para>
    /// </summary>
    [Serializable]
    [Table("Albums")]
    [JsonObject(MemberSerialization.OptIn)]
    public partial class AlbumEntity : EntityBase, IAlias
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        [NotMapped]
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable name of the Album.
        /// </summary>
        [NotMapped]
        private string name = "";

        /// <summary>
        /// Variable alias of the Album.
        /// </summary>
        [NotMapped]
        private string alias = "";

        /// <summary>
        /// Variable description of the Album.
        /// </summary>
        [NotMapped]
        private string description = "";

        /// <summary>
        /// Variable order place of the item.
        /// </summary>
        [NotMapped]
        private int ordering = 0;

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
        /// Variable first picture captured date.
        /// </summary>
        [NotMapped]
        private DateTime dateStart = DateTime.Now;

        /// <summary>
        /// Variable last picture captured date.
        /// </summary>
        [NotMapped]
        private DateTime dateEnd = DateTime.Now;

        /// <summary>
        /// Variable the background picture id.
        /// </summary>
        [NotMapped]
        public int backgroundPictureId = 0;

        /// <summary>
        /// Variable the preview picture id.
        /// </summary>
        [NotMapped]
        public int previewPictureId = 0;

        /// <summary>
        /// Variable the thumbnail picture id.
        /// </summary>
        [NotMapped]
        public int thumbnailPictureId = 0;

        /// <summary>
        /// Variable comment for the item.
        /// </summary>
        [NotMapped]
        public string comment = "";

        /// <summary>
        /// Variable parameters for the item.
        /// </summary>
        [NotMapped]
        public string parameters = "";

        #endregion



        #region Variables Associated Pictures

        /// <summary>
        /// Variable background picture.
        /// </summary>
        [NotMapped]
        public PictureEntity backgroundPicture;

        /// <summary>
        /// Variable preview picture.
        /// </summary>
        [NotMapped]
        public PictureEntity previewPicture;

        /// <summary>
        /// Variable thumbnail picture.
        /// </summary>
        [NotMapped]
        public PictureEntity thumbnailPicture;


        #endregion



        #region Variables Dependencies

        /// <summary>
        /// Variable list of Section associated to the Album.
        /// </summary>
        [NotMapped]
        private List<SectionEntity> sections;

        /// <summary>
        /// Variable list of Picture associated to the Album.
        /// </summary>
        [NotMapped]
        private ObservableCollection<PictureEntity> pictures = new ObservableCollection<PictureEntity>();

        /// <summary>
        /// Variable list of Info associated to the Album.
        /// </summary>
        [NotMapped]
        private List<InfoEntity> infos;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the id or primary key auto incremented.
        /// </summary>
        [Key]
        [Column(Order = 0)]
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
        /// Property to access to the alias of the Album.
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
        /// Property to access to the created date.
        /// </summary>
        [Column(Order = 5)]
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
        [Column(Order = 6)]
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
        [Column(Order = 7)]
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
        [Column(Order = 8)]
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
        [Column(Order = 9)]
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

                    // Update property to null will raise Background Picture changed event with new Picture entity.
                    BackgroundPicture = null;
                }
            }
        }

        /// <summary>
        /// Property to access to the preview picture id.
        /// </summary>
        [Column(Order = 10)]
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

                    // Update property to null will raise Preview Picture changed event with new Picture entity.
                    PreviewPicture = null;
                }
            }
        }

        /// <summary>
        /// Property to access to the thumbnail picture id.
        /// </summary>
        [Column(Order = 11)]
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

                    // Update property to null will raise Thumbnail Picture changed event with new Picture entity.
                    ThumbnailPicture = null;
                }
            }
        }

        /// <summary>
        /// Property to access to the comment for the item.
        /// </summary>
        [Column(Order = 12)]
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
        [Column(Order = 13)]
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



        #region Properties Associated Pictures

        /// <summary>
        /// Property to access to the background picture entity.
        /// </summary>
        [NotMapped]
        [JsonProperty(PropertyName = "BackgroundPicture")]
        [XmlElement(ElementName = "BackgroundPicture")]
        public PictureEntity BackgroundPicture
        {
            get
            {
                if (backgroundPicture == null)
                {
                    backgroundPicture = GetPicture(BackgroundPictureId);
                }
                return backgroundPicture;
            }
            set
            {
                if (value != backgroundPicture)
                {
                    backgroundPicture = value;
                    NotifyPropertyChanged();

                    // Update the Background Picture Id property.
                    if (backgroundPicture == null)
                    {
                        log.Debug(new NullReferenceException($"Parameter : '{nameof(backgroundPicture)}' type of 'null'"));
                        return;
                    }

                    if (backgroundPicture.PrimaryKey == 0)
                    {
                        log.Debug(new IndexOutOfRangeException($"Parameter : '{nameof(backgroundPicture)}' type of '{backgroundPicture.GetType().Name}' [PrimaryKey:0]"));
                        return;
                    }

                    BackgroundPictureId = backgroundPicture.PrimaryKey;
                }
            }
        }

        /// <summary>
        /// Property to access to the preview picture entity.
        /// </summary>
        [NotMapped]
        [JsonProperty(PropertyName = "PreviewPicture")]
        [XmlElement(ElementName = "PreviewPicture")]
        public PictureEntity PreviewPicture
        {
            get
            {
                if (previewPicture == null)
                {
                    previewPicture = GetPicture(PreviewPictureId);
                }
                return previewPicture;
            }
            set
            {
                if (value != previewPicture)
                {
                    previewPicture = value;
                    NotifyPropertyChanged();

                    // Update the Preview Picture Id property.
                    if (previewPicture == null)
                    {
                        log.Debug(new NullReferenceException($"Parameter : '{nameof(previewPicture)}' type of 'null'"));
                        return;
                    }

                    if (previewPicture.PrimaryKey == 0)
                    {
                        log.Debug(new IndexOutOfRangeException($"Parameter : '{nameof(previewPicture)}' type of '{previewPicture.GetType().Name}' [PrimaryKey:0]"));
                        return;
                    }

                    PreviewPictureId = previewPicture.PrimaryKey;
                }
            }
        }

        /// <summary>
        /// Property to access to the thumbnail picture entity.
        /// </summary>
        [NotMapped]
        [JsonProperty(PropertyName = "ThumbnailPicture")]
        [XmlElement(ElementName = "ThumbnailPicture")]
        public PictureEntity ThumbnailPicture
        {
            get
            {
                if (thumbnailPicture == null)
                {
                    thumbnailPicture = GetPicture(ThumbnailPictureId);
                }
                return thumbnailPicture;
            }
            set
            {
                if (value != thumbnailPicture)
                {
                    thumbnailPicture = value;
                    NotifyPropertyChanged();

                    // Update the Thumbnail Picture Id property.
                    if (thumbnailPicture == null)
                    {
                        log.Debug(new NullReferenceException($"Parameter : '{nameof(thumbnailPicture)}' type of 'null'"));
                        return;
                    }

                    if (thumbnailPicture.PrimaryKey == 0)
                    {
                        log.Debug(new IndexOutOfRangeException($"Parameter : '{nameof(thumbnailPicture)}' type of '{thumbnailPicture.GetType().Name}' [PrimaryKey:0]"));
                        return;
                    }

                    ThumbnailPictureId = thumbnailPicture.PrimaryKey;
                }
            }
        }

        #endregion



        #region Properties Dependency Section

        /// <summary>
        /// Property to access to the Section Id required for dependency.
        /// </summary>
        [NotMapped]
        public int SectionId { get; set; }

        /// <summary>
        /// Property to access to the list of Sections associated to the Album.
        /// </summary>
        [NotMapped]
        public ObservableCollection<SectionEntity> Sections
            => AlbumsInSections.DepReferences;

        /// <summary>
        /// Propertiy to access to the list of Section dependency Primary Keys.
        /// </summary>
        [NotMapped]
        public ObservableCollection<int> SectionsPKs
            => AlbumsInSections.DepPKeys;

        /// <summary>
        /// Property to access to the collection of relationship Albums in Sections.
        /// </summary>
        public ObservableAlbumsInSections<AlbumEntity, SectionEntity> AlbumsInSections { get; set; }
            = new ObservableAlbumsInSections<AlbumEntity, SectionEntity>();

        #endregion



        #region Properties Dependency Picture

        /// <summary>
        /// Property Picture id.
        /// </summary>
        [NotMapped]
        public int PictureId { get; set; }

        /// <summary>
        /// Propertiy to access to the list of Picture dependencies primary key.
        /// </summary>
        [NotMapped]
        public ObservableCollection<int> PicturesPKs
        {
            get
            {
                PicturesInAlbums.Populate();
                return PicturesInAlbums.DepPKeys;
            }
        }

        /// <summary>
        /// Property to access to the list of Pictures associated to the Album.
        /// </summary>
        [NotMapped]
        public ObservableCollection<PictureEntity> Pictures
        {    
            get
            {
                PicturesInAlbums.Populate();
                return PicturesInAlbums.DepReferences;
            }
        }

        /// <summary>
        /// Property to access to the collection of relationship Pictures In Albums entities.
        /// </summary>
        public ObservablePicturesInAlbums<AlbumEntity, PictureEntity> PicturesInAlbums { get; set; }
            = new ObservablePicturesInAlbums<AlbumEntity, PictureEntity>();

        #endregion



        #region Properties Dependency Info

        /// <summary>
        /// Property Info id.
        /// </summary>
        [NotMapped]
        public int InfoId { get; set; }

        /// <summary>
        /// Propertiy to access to the list of Info dependencies primary key.
        /// </summary>
        [NotMapped]
        public ObservableCollection<int> InfosPKs
        {
            get
            {
                InfosInAlbums.Populate();
                return InfosInAlbums.DepPKeys;
            }
        }

        /// <summary>
        /// Property to access to the list of Infos associated to the Album.
        /// </summary>
        [NotMapped]
        public ObservableCollection<InfoEntity> Infos
        {
            get
            {
                InfosInAlbums.Populate();
                return InfosInAlbums.DepReferences;
            }
        }

        /// <summary>
        /// Property to access to the collection of relationship Infos In Albums entities.
        /// </summary>
        public ObservableInfosInAlbums<AlbumEntity, InfoEntity> InfosInAlbums { get; set; }
            = new ObservableInfosInAlbums<AlbumEntity, InfoEntity>();

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries SQLite Album Entity Constructor.
        /// </summary>
        public AlbumEntity() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [System.Obsolete("Use dependency references.", true)]
        private void PicturesInAlbums_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : {e.NewItems?.Count} | {e.OldItems?.Count}");

            if (e.NewItems?.Count != null)
            {
                foreach (PicturesInAlbums pia in e.NewItems)
                {
                    PictureEntity p = Db.Context.Find<PictureEntity>(pia.PictureId);

                    if (p != null && !pictures.Contains(p))
                    {
                        log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : adding Picture {p.PrimaryKey} to observable list.");
                        pictures.Add(p);
                    }
                }
            }

            if (e.OldItems?.Count != null)
            {
                foreach (PicturesInAlbums pia in e.OldItems)
                {
                    int index = Pictures.ToList().FindIndex(x => x.PictureId == pia.PictureId);

                    if (index != 0)
                    {
                        log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : removing Picture {pia.PictureId} from observable list.");
                        pictures.RemoveAt(index);
                    }
                }
            }
        }

        #endregion



        #region Methods Picture

        /// <summary>
        /// Method to get a list of associated Picture.
        /// </summary>
        /// <returns></returns>
        [System.Obsolete("Use dependency references.", true)]
        private ObservableCollection<PictureEntity> ListPictures()
        {
            if (pictures.Count == 0)
            {
                IEnumerable<PictureEntity> picts = null;
                
                if (PicturesInAlbums != null)
                {
                    picts = ListEntities<PictureEntity>(PicturesInAlbums);
                }

                if(picts != null)
                {
                    foreach(var p in picts)
                    {
                        if (p != null && !pictures.Contains(p))
                        {
                            pictures.Add(p);
                        }
                    }
                }
            }
            
            return pictures;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pictureId"></param>
        public void LinkPicture(int pictureId, bool isNew = true)
        {
            try
            {
                int index = PicturesInAlbums.ToList().FindIndex(o => o.PictureId == pictureId);

                if (index < 0)
                {
                    if (isNew)
                    {
                        PicturesInAlbums.Add(new PicturesInAlbums { PictureId = pictureId });
                    }
                    else
                    {
                        PicturesInAlbums.Add(new PicturesInAlbums { PictureId = pictureId, AlbumId = PrimaryKey });
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pictureId"></param>
        public void UnLinkPicture(int pictureId)
        {
            try
            {
                int index = PicturesInAlbums.ToList().FindIndex(o => o.PictureId == pictureId);
                PicturesInAlbums.RemoveAt(index);
            }
            catch { }
        }

        #endregion



        #region Methods Section

        /// <summary>
        /// Method to get a list of associated Section.
        /// </summary>
        /// <returns></returns>
        [System.Obsolete("Use dependency references.")]
        private List<SectionEntity> ListSections()
        {
            if (sections == null)
            {
                sections = new List<SectionEntity>();

                if (AlbumsInSections != null)
                {
                    sections = ListEntities<SectionEntity>(AlbumsInSections);
                }
            }

            return sections;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sectionId"></param>
        [System.Obsolete("Use => SectionsPKs.Add(SectionPk);")]
        public void LinkSection(int sectionId)
        {
            try
            {
                int index = AlbumsInSections.ToList().FindIndex(o => o.SectionId == sectionId);

                if (index < 0)
                {
                    AlbumsInSections.Add(new AlbumsInSections { SectionId = sectionId });
                }
            }
            catch { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sectionId"></param>
        [System.Obsolete("Use => SectionsPKs.Remove(SectionPk);")]
        public void UnlinkSection(int sectionId)
        {
            try
            {
                int index = AlbumsInSections.ToList().FindIndex(o => o.SectionId == sectionId);
                AlbumsInSections.RemoveAt(index);
            }
            catch { }
        }

        #endregion



        #region Methods Info

        /// <summary>
        /// Method to get a list of associated Info.
        /// </summary>
        /// <returns></returns>
        [System.Obsolete("Use dependency references.")]
        private List<InfoEntity> ListInfos()
        {
            if (infos == null)
            {
                infos = new List<InfoEntity>();

                if (InfosInAlbums != null)
                {
                    infos = ListEntities<InfoEntity>(InfosInAlbums);
                }
            }

            return infos;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="infoId"></param>
        [System.Obsolete("Use => InfosPKs.Add(InfoPk);")]
        public void LinkInfo(int infoId)
        {
            try
            {
                int index = InfosInAlbums.ToList().FindIndex(o => o.InfoId == infoId);

                if (index < 0)
                {
                    InfosInAlbums.Add(new InfosInAlbums { InfoId = infoId });
                }
            }
            catch { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="infoId"></param>
        [System.Obsolete("Use => InfosPKs.Remove(InfoPk);", true)]
        public void UnLinkInfo(int infoId)
        {
            try
            {
                int index = InfosInAlbums.ToList().FindIndex(o => o.InfoId == infoId);
                InfosInAlbums.RemoveAt(index);
            }
            catch { }
        }

        #endregion
    }
}