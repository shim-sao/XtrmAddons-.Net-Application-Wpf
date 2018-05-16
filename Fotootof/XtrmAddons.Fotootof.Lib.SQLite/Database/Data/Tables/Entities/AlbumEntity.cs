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
using System.Windows;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Libraries SQLite Album Entity.</para>
    /// </summary>
    [Table("Albums")]
    [JsonObject(MemberSerialization.OptIn)]
    public partial class AlbumEntity : CommonEntity
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
        private List<PictureEntity> pictures;

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
        /// Property to access to the alias of the Album.
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
                    alias = value.Sanitize().RemoveDiacritics().ToLower();
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the description of the item.
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
        /// Property to access to the created date.
        /// </summary>
        [Column(Order = 5)]
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
        [Column(Order = 6)]
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
        /// Property to access to the first picture captured date.
        /// </summary>
        [Column(Order = 7)]
        [JsonProperty]
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
        [JsonProperty]
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
        [JsonProperty]
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
        [JsonProperty]
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
        [JsonProperty]
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

        /// <summary>
        /// Property to access to the parameters for the item.
        /// </summary>
        [Column(Order = 13)]
        [JsonProperty]
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
        public PictureEntity BackgroundPicture
        {
            get
            {
                if(backgroundPicture == null)
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
                    BackgroundPictureId = backgroundPicture.PrimaryKey;
                }
            }
        }

        /// <summary>
        /// Property to access to the preview picture entity.
        /// </summary>
        [NotMapped]
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
                    PreviewPictureId = previewPicture.PrimaryKey;
                }
            }
        }

        /// <summary>
        /// Property to access to the thumbnail picture entity.
        /// </summary>
        [NotMapped]
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
                    ThumbnailPictureId = thumbnailPicture.PrimaryKey;
                }
            }
        }

        #endregion



        #region Properties Dependencies Section

        /// <summary>
        /// Property Section id.
        /// </summary>
        [NotMapped]
        public int SectionId { get; set; }

        /// <summary>
        /// Property to access to the list of Sections associated to the Album.
        /// </summary>
        [NotMapped]
        public List<SectionEntity> Sections
        {
            get => ListSections();
            set => sections = value;
        }

        /// <summary>
        /// Propertiy to access to the list of Section dependencies primary key.
        /// </summary>
        [NotMapped]
        public IEnumerable<int> SectionsPK
            => ListOfPrimaryKeys(AlbumsInSections.ToList(), "SectionId");

        /// <summary>
        /// Property to access to the collection of relationship Albums in Sections.
        /// </summary>
        public ObservableCollection<AlbumsInSections> AlbumsInSections { get; set; }
            = new ObservableCollection<AlbumsInSections>();

        #endregion



        #region Properties : Dependencies

        /// <summary>
        /// Property Picture id.
        /// </summary>
        [NotMapped]
        public int PictureId { get; set; }

        /// <summary>
        /// Property Info id.
        /// </summary>
        [NotMapped]
        public int InfoId { get; set; }

        /// <summary>
        /// Property to access to the list of Pictures associated to the Album.
        /// </summary>
        [NotMapped]
        public List<PictureEntity> Pictures
        {
            get => ListPictures();
            set => pictures = value;
        }

        /// <summary>
        /// Property to access to the list of Infos associated to the Album.
        /// </summary>
        [NotMapped]
        public List<InfoEntity> Infos
        {
            get => ListInfos();
            set => infos = value;
        }

        /// <summary>
        /// Propertiy to access to the list of Picture dependencies primary key.
        /// </summary>
        [NotMapped]
        public IEnumerable<int> PicturesPK => ListOfPrimaryKeys(PicturesInAlbums.ToList(), "PictureId");

        /// <summary>
        /// Propertiy to access to the list of Info dependencies primary key.
        /// </summary>
        [NotMapped]
        public IEnumerable<int> InfosPK => ListOfPrimaryKeys(InfosInAlbums.ToList(), "InfoId");

        /// <summary>
        /// Property to access to the collection of relationship Pictures In Albums entities.
        /// </summary>
        public ObservableCollection<PicturesInAlbums> PicturesInAlbums { get; set; }
            = new ObservableCollection<PicturesInAlbums>();

        /// <summary>
        /// Property to access to the collection of relationship Infos In Albums entities.
        /// </summary>
        public ObservableCollection<InfosInAlbums> InfosInAlbums { get; set; }
            = new ObservableCollection<InfosInAlbums>();

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries SQLite Album Entity Constructor.
        /// </summary>
        public AlbumEntity()
        {
            AlbumsInSections.CollectionChanged += (s, e) => { sections = null; };
            PicturesInAlbums.CollectionChanged += (s, e) => { pictures = null; };
            InfosInAlbums.CollectionChanged += (s, e) => { infos = null; };
        }

        #endregion


        #region Methods

        /// <summary>
        /// Method to get a list of associated Picture.
        /// </summary>
        /// <returns></returns>
        private List<PictureEntity> ListPictures()
        {
            if (pictures == null || pictures.Count == 0)
            {
                pictures = new List<PictureEntity>();

                if (PicturesInAlbums != null)
                {
                    pictures = ListEntities<PictureEntity>(PicturesInAlbums);
                }
            }

            return pictures;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pictureId"></param>
        public void LinkPicture(int pictureId)
        {
            try
            {
                int index = PicturesInAlbums.ToList().FindIndex(o => o.PictureId == pictureId);

                if (index < 0)
                {
                    PicturesInAlbums.Add(new PicturesInAlbums { PictureId = pictureId });
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

        /// <summary>
        /// Method to get a list of associated Section.
        /// </summary>
        /// <returns></returns>
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
        public void UnlinkSection(int sectionId)
        {
            try
            {
                int index = AlbumsInSections.ToList().FindIndex(o => o.SectionId == sectionId);
                AlbumsInSections.RemoveAt(index);
            }
            catch { }
        }

        /// <summary>
        /// Method to get a list of associated Info.
        /// </summary>
        /// <returns></returns>
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