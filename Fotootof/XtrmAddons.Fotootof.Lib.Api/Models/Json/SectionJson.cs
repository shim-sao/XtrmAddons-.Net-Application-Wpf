using Fotootof.SQLite.EntityManager.Data.Tables.Dependencies;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using System;
using System.Collections.Generic;

namespace XtrmAddons.Fotootof.Lib.Api.Models.Json
{
    /// <summary>
    /// Class XtrmAddons PhotoAlbum Server Api Models Json Category
    /// </summary>
    public class SectionJson : EntityJson
    {
        #region Properties

        /// <summary>
        /// Variable primary key auto incremented.
        /// </summary>
        public int PrimaryKey { get; set; }

        /// <summary>
        /// Variable name of the item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Variable alias of the item.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Variable description of the item.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Variable is default item.
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Variable order place of the item.
        /// </summary>
        public int Ordering { get; set; }

        /// <summary>
        /// Variable start date.
        /// </summary>
        public DateTime DateStart { get; set; }

        /// <summary>
        /// Variable end date.
        /// </summary>
        public DateTime DateEnd { get; set; }

        /// <summary>
        /// Variable created date.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Variable modified date.
        /// </summary>
        public DateTime Modified { get; set; }

        /// <summary>
        /// Variable last pictures add date.
        /// </summary>
        public DateTime LastAdded { get; set; }

        /// <summary>
        /// Variable the background picture id.
        /// </summary>
        public int BackgroundPictureId { get; set; }

        /// <summary>
        /// Variable the picture width.
        /// </summary>
        public int PreviewPictureId { get; set; }

        /// <summary>
        /// Variable the picture height.
        /// </summary>
        public int ThumbnailPictureId { get; set; }

        /// <summary>
        /// Variable the comment.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Variable the paremeters.
        /// </summary>
        public string Parameters { get; set; }

        /// <summary>
        /// Variable list of albums in category.
        /// </summary>
        public List<AlbumJson> Albums { get; set; } = new List<AlbumJson>();

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons PhotoAlbum Server Api Models Json Category constructor.
        /// </summary>
        public SectionJson() { }

        /// <summary>
        /// Class XtrmAddons PhotoAlbum Server Api Models Json Category constructor.
        /// </summary>
        /// <param name="category"></param>
        public SectionJson(SectionEntity entity, bool auth = false) : this()
        {
            Bind(entity, auth);
        }

        /// <summary>
        /// Class XtrmAddons PhotoAlbum Server Api Models Json Category constructor.
        /// </summary>
        /// <param name="section"></param>
        /// <param name="loadAlbums"></param>
        /// <param name="loadPictures"></param>
        public SectionJson(SectionEntity entity, bool auth = false, bool loadAlbums = false, bool loadPictures = false) : this(entity, auth)
        {
            if (loadAlbums)
            {
                foreach (AlbumEntity album in entity.Albums)
                {
                    Albums.Add(new AlbumJson(album, auth, loadPictures));
                }
            }
        }

        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="auth"></param>
        public void Bind(SectionEntity entity, bool auth = false)
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

            if (auth)
            {
                Comment = entity.Comment;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SectionEntity ToEntity()
        {
            SectionEntity entity = new SectionEntity();

            entity.PrimaryKey = PrimaryKey;
            entity.Name = Name;
            entity.Alias = Alias;
            entity.Description = Description;
            entity.Ordering = Ordering;
            entity.IsDefault = IsDefault;

            entity.BackgroundPictureId = BackgroundPictureId;
            entity.PreviewPictureId = PreviewPictureId;
            entity.ThumbnailPictureId = ThumbnailPictureId;
            entity.Parameters = Parameters;
            entity.Comment = Comment;

            AddAlbumToEntity(entity);

            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        private void AddAlbumToEntity(SectionEntity entity)
        {
            if (Albums.Count > 0)
            {
                foreach (AlbumJson a in Albums)
                {
                    entity.AlbumsInSections.Add(new AlbumsInSections { AlbumId = a.PrimaryKey });
                }
            }
        }

        #endregion
    }
}
