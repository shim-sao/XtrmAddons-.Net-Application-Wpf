using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Windows;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Album Entity.
    /// </summary>
    [Table("Albums")]
    public partial class AlbumEntity : EntityBase
    {
        #region Variables

        /// <summary>
        /// Variable list of Section associated to the Album.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        private List<SectionEntity> sections;

        /// <summary>
        /// Variable list of Picture associated to the Album.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        private List<PictureEntity> pictures;

        /// <summary>
        /// Variable list of Info associated to the Album.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        private List<InfoEntity> infos;

        #endregion



        #region Properties

        /// <summary>
        /// Property primary key auto incremented.
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public int AlbumId { get; set; }
        
        /// <summary>
        /// Property name of the item.
        /// </summary>
        [Column(Order = 1)]
        public string Name { get; set; }

        /// <summary>
        /// Property name of the item.
        /// </summary>
        [Column(Order = 2)]
        public string Alias { get; set; }

        /// <summary>
        /// Property description of the item.
        /// </summary>
        [Column(Order = 3)]
        public string Description { get; set; }
        
        /// <summary>
        /// Property order place of the item.
        /// </summary>
        [Column(Order = 4)]
        public int Ordering { get; set; }

        
        /// <summary>
        /// Property created date.
        /// </summary>
        [Column(Order = 5)]
        public DateTime Created { get; set; }

        /// <summary>
        /// Property modified date.
        /// </summary>
        [Column(Order = 6)]
        public DateTime Modified { get; set; }
        
        /// <summary>
        /// Property first picture captured date.
        /// </summary>
        [Column(Order = 7)]
        public DateTime DateStart { get; set; }

        /// <summary>
        /// Property last picture captured date.
        /// </summary>
        [Column(Order = 8)]
        public DateTime DateEnd { get; set; }


        /// <summary>
        /// Property the original path.
        /// </summary>
        [Column(Order = 9)]
        [JsonIgnore]
        public string OriginalPath { get; set; }

        /// <summary>
        /// Property the original width.
        /// </summary>
        [Column(Order = 10)]
        [JsonIgnore]
        public int OriginalWidth { get; set; }

        /// <summary>
        /// Property the original height.
        /// </summary>
        [Column(Order = 11)]
        [JsonIgnore]
        public int OriginalHeight { get; set; }


        /// <summary>
        /// Property the picture path.
        /// </summary>
        [Column(Order = 12)]
        [JsonIgnore]
        public string PicturePath { get; set; }

        /// <summary>
        /// Property the picture width.
        /// </summary>
        [Column(Order = 13)]
        [JsonIgnore]
        public int PictureWidth { get; set; }

        /// <summary>
        /// Property the picture height.
        /// </summary>
        [Column(Order = 14)]
        [JsonIgnore]
        public int PictureHeight { get; set; }


        /// <summary>
        /// Property the thumbnail picture path.
        /// </summary>
        [Column(Order = 15)]
        [JsonIgnore]
        public string ThumbnailPath { get; set; }

        /// <summary>
        /// Property the thumbnail width.
        /// </summary>
        [Column(Order = 16)]
        [JsonIgnore]
        public int ThumbnailWidth { get; set; }

        /// <summary>
        /// Property the thumbnail height.
        /// </summary>
        [Column(Order = 17)]
        [JsonIgnore]
        public int ThumbnailHeight { get; set; }


        /// <summary>
        /// Property comment for the item.
        /// </summary>
        [Column(Order = 18)]
        public string Comment { get; set; }

        #endregion



        #region Properties : Dependencies

        /// <summary>
        /// Property alias to access to the primary key of the entity.
        /// </summary>
        [NotMapped]
        public override int PrimaryKey { get => AlbumId; set => AlbumId = value; }

        /// <summary>
        /// Property Section id.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public int SectionId { get; set; }

        /// <summary>
        /// Property Picture id.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public int PictureId { get; set; }

        /// <summary>
        /// Property Info id.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public int InfoId { get; set; }

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
        /// Propertiy to access to the list of Section dependencies primary key.
        /// </summary>
        [NotMapped]
        public List<int> SectionsPK => ListOfPrimaryKeys(AlbumsInSections.ToList(), "SectionId");

        /// <summary>
        /// Propertiy to access to the list of Picture dependencies primary key.
        /// </summary>
        [NotMapped]
        public List<int> PicturesPK => ListOfPrimaryKeys(PicturesInAlbums.ToList(), "PictureId");

        /// <summary>
        /// Propertiy to access to the list of Info dependencies primary key.
        /// </summary>
        [NotMapped]
        public List<int> InfosPK => ListOfPrimaryKeys(InfosInAlbums.ToList(), "InfoId");

        /// <summary>
        /// Property to access to the collection of relationship Albums in Sections.
        /// </summary>
        [JsonIgnore]
        public ObservableCollection<AlbumsInSections> AlbumsInSections { get; set; }
            = new ObservableCollection<AlbumsInSections>();

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
            Initialize();

            AlbumsInSections.CollectionChanged += (s, e) => { sections = null; };
            PicturesInAlbums.CollectionChanged += (s, e) => { pictures = null; };
            InfosInAlbums.CollectionChanged += (s, e) => { infos = null; };
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to initialize an Album.
        /// </summary>
        public void Initialize()
        {
            if (PrimaryKey <= 0)
            {
                PrimaryKey = 0;
            }

            this.InitializeNulls();
            InitializeImages();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void InitializeImages()
        {
            if (OriginalPath.IsNullOrWhiteSpace())
            {
                OriginalPath = (string)Application.Current.Resources["ImageAlbumDefaultOriginal"];
                OriginalWidth = 512;
                OriginalHeight = 512;
            }

            if (PicturePath.IsNullOrWhiteSpace())
            {
                PicturePath = (string)Application.Current.Resources["ImageAlbumDefaultPicture"];
                PictureWidth = 256;
                PictureHeight = 256;
            }

            if (ThumbnailPath.IsNullOrWhiteSpace())
            {
                ThumbnailPath = (string)Application.Current.Resources["ImageAlbumDefaultThumbnail"];
                ThumbnailWidth = 128;
                ThumbnailHeight = 128;
            }
        }

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
        public void UnLinkSection(int sectionId)
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