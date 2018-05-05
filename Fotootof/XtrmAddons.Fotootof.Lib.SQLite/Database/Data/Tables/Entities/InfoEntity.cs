using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using XtrmAddons.Net.Common.Extensions;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server SQLite User Entity.
    /// </summary>
    [Table("Infos")]
    public partial class InfoEntity : EntityBase
    {
        #region Variables

        /// <summary>
        /// Variable list of Album associated to the Info.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        private List<AlbumEntity> albums;

        /// <summary>
        /// Variable list of Picture associated to the Info.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        private List<PictureEntity> pictures;

        #endregion



        #region Properties

        /// <summary>
        /// Propmerty primary key auto incremented.
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public int InfoId
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
        /// Property assaciated InfoType primary key.
        /// </summary>
        [Column(Order = 1)]
        public int InfoTypeId { get; set; }

        /// <summary>
        /// Property Name of the Info.
        /// </summary>
        [Column(Order = 2)]
        public string Name { get; set; }

        /// <summary>
        /// Property Alias of the Info.
        /// </summary>
        [Column(Order = 3)]
        public string Alias { get; set; }

        /// <summary>
        /// Property Description of the Info.
        /// </summary>
        [Column(Order = 4)]
        public string Description { get; set; }

        /// <summary>
        /// Property is default.
        /// </summary>
        [Column(Order = 5)]
        public bool IsDefault { get; set; }

        /// <summary>
        /// Property ordering.
        /// </summary>
        [Column(Order = 6)]
        public int Ordering { get; set; }

        #endregion



        #region Properties : Dependencies

        /// <summary>
        /// Variable Album id (required for entity dependency).
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public int AlbumId { get; set; }

        /// <summary>
        /// Variable Picture id (required for entity dependency).
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public int PictureId { get; set; }

        /// <summary>
        /// Property to access to the list of Album associated to the Info.
        /// </summary>
        [NotMapped]
        public List<AlbumEntity> Albums
        {
            get => ListAlbums();
            set => albums = value;
        }

        /// <summary>
        /// Property to access to the list of Picture associated to the Info.
        /// </summary>
        [NotMapped]
        public List<PictureEntity> Pictures
        {
            get => ListPictures();
            set => pictures = value;
        }

        /// <summary>
        /// Property to access to the list of Album dependencies primary key.
        /// </summary>
        [NotMapped]
        public List<int> AlbumsPK => ListOfPrimaryKeys(InfosInAlbums.ToList(), "AlbumId");

        /// <summary>
        /// Property to access to the list of Picture dependencies primary key.
        /// </summary>
        [NotMapped]
        public List<int> PicturesPK => ListOfPrimaryKeys(InfosInPictures.ToList(), "PictureId");

        /// <summary>
        /// Property collection of relationship Infos in Albums.
        /// </summary>
        [JsonIgnore]
        public ObservableCollection<InfosInAlbums> InfosInAlbums { get; set; }
            = new ObservableCollection<InfosInAlbums>();

        /// <summary>
        /// Property collection of relationship Infos in Pictures.
        /// </summary>
        [JsonIgnore]
        public ObservableCollection<InfosInPictures> InfosInPictures { get; set; }
            = new ObservableCollection<InfosInPictures>();

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server SQLite User Entity Constructor.
        /// </summary>
        public InfoEntity()
        {
            Initialize();

            InfosInAlbums.CollectionChanged += (s, e) => { albums = null; };
            InfosInPictures.CollectionChanged += (s, e) => { pictures = null; };
        }

        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        public void Initialize()
        {
            if (PrimaryKey <= 0)
            {
                PrimaryKey = 0;
            }

            this.InitializeNulls();
            Alias = Alias ?? Alias.RemoveDiacritics().Sanitize();
        }

        /// <summary>
        /// Method to get list of associated Album.
        /// </summary>
        private List<AlbumEntity> ListAlbums()
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
        /// Method to get list of associated Picture.
        /// </summary>
        private List<PictureEntity> ListPictures()
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
        /// <param name="albumId"></param>
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
        /// <param name="pictureId"></param>
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
        /// <param name="albumId"></param>
        public void UnLinkAlbum(int albumId)
        {
            try
            {
                int index = InfosInAlbums.ToList().FindIndex(o => o.AlbumId == albumId);
                InfosInAlbums.RemoveAt(index);
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
                int index = InfosInPictures.ToList().FindIndex(o => o.PictureId == pictureId);
                InfosInPictures.RemoveAt(index);
            }
            catch { }
        }

        #endregion
    }
}