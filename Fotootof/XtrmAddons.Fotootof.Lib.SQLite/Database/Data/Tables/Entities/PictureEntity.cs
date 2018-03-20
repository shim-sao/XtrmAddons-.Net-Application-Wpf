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
    public class PictureEntity : EntityBase
    {
        #region Variables

        /// <summary>
        /// Variable list of Albums associated to the item.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        private List<AlbumEntity> albums;

        /// <summary>
        /// Variable list of Infos associated to the Album.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        private List<InfoEntity> infos;

        #endregion



        #region Properties

        /// <summary>
        /// Variable primary key auto incremented.
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public int PictureId { get; set; }
        
        /// <summary>
        /// Property name.
        /// </summary>
        [Column(Order = 1)]
        public string Name { get; set; }
        
        /// <summary>
        /// Property name.
        /// </summary>
        [Column(Order = 2)]
        public string Alias { get; set; }

        /// <summary>
        /// Property description.
        /// </summary>
        [Column(Order = 3)]
        public string Description { get; set; }
        
        /// <summary>
        /// Property order place of the item.
        /// </summary>
        [Column(Order = 4)]
        public int Ordering { get; set; }


        /// <summary>
        /// Property capture date.
        /// </summary>
        [Column(Order = 5)]
        public DateTime Captured { get; set; }

        /// <summary>
        /// Property created date.
        /// </summary>
        [Column(Order = 6)]
        public DateTime Created { get; set; }

        /// <summary>
        /// Property modified date.
        /// </summary>
        [Column(Order = 7)]
        public DateTime Modified { get; set; }


        /// <summary>
        /// Property the original path.
        /// </summary>
        [Column(Order = 8)]
        [JsonIgnore]
        public string OriginalPath { get; set; }

        /// <summary>
        /// Property the original width.
        /// </summary>
        [Column(Order = 9)]
        [JsonIgnore]
        public int OriginalWidth { get; set; }

        /// <summary>
        /// Property the original height.
        /// </summary>
        [Column(Order = 10)]
        [JsonIgnore]
        public int OriginalHeight { get; set; }


        /// <summary>
        /// Property picture path.
        /// </summary>
        [Column(Order = 11)]
        public string PicturePath { get; set; }

        /// <summary>
        /// Property picture width.
        /// </summary>
        [Column(Order = 12)]
        public int PictureWidth { get; set; }

        /// <summary>
        /// Property picture height.
        /// </summary>
        [Column(Order = 13)]
        public int PictureHeight { get; set; }


        /// <summary>
        /// Property thumbnail picture path.
        /// </summary>
        [Column(Order = 14)]
        public string ThumbnailPath { get; set; }

        /// <summary>
        /// Property thumbnail width.
        /// </summary>
        [Column(Order = 15)]
        public int ThumbnailWidth { get; set; }

        /// <summary>
        /// Property thumbnail height.
        /// </summary>
        [Column(Order = 16)]
        public int ThumbnailHeight { get; set; }


        /// <summary>
        /// Property comment.
        /// </summary>
        [Column(Order = 17)]
        public string Comment { get; set; }

        #endregion



        #region Properties : Dependencies

        /// <summary>
        /// Property alias to access to the primary key of the entity.
        /// </summary>
        [NotMapped]
        public override int PrimaryKey { get => PictureId; set => PictureId = value; }

        /// <summary>
        /// Property Album id (required for entity dependency).
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public int AlbumId { get; set; }

        /// <summary>
        /// Property Info id (required for entity dependency).
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public int InfoId { get; set; }

        /// <summary>
        /// Property to access to the list of Albums associated to the item.
        /// </summary>
        [NotMapped]
        public List<AlbumEntity> Albums { get => ListAlbums(); set => albums = value; }

        /// <summary>
        /// Property to access to the list of Infos associated to the item.
        /// </summary>
        [NotMapped]
        public List<InfoEntity> Infos { get => ListInfos(); set => infos = value; }

        /// <summary>
        /// Propertiy to access to the list of Album dependencies primary key.
        /// </summary>
        [NotMapped]
        public List<int> AlbumsPK => ListOfPrimaryKeys(PicturesInAlbums.ToList(), "AlbumId");

        /// <summary>
        /// Propertiy to access to the list of Info dependencies primary key.
        /// </summary>
        [NotMapped]
        public List<int> InfosPK => ListOfPrimaryKeys(InfosInPictures.ToList(), "InfoId");

        /// <summary>
        /// Property to access to the collection of relationship Pictures In Albums entities.
        /// </summary>
        public ObservableCollection<PicturesInAlbums> PicturesInAlbums { get; set; }
            = new ObservableCollection<PicturesInAlbums>();

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
            Initialize();

            PicturesInAlbums.CollectionChanged += (s, e) => { albums = null; };
            InfosInPictures.CollectionChanged += (s, e) => { infos = null; };
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to initialize a Picture.
        /// </summary>
        public void Initialize()
        {
            if (PrimaryKey <= 0)
            {
                PrimaryKey = 0;
            }

            this.InitializeNulls();
        }

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
