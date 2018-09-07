using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using System;
using System.Collections.Generic;

namespace Fotootof.SQLite.EntityManager.Data.Tables.Json.Models
{
    /// <summary>
    /// Class XtrmAddons PhotoAlbum Server Api Models Json Category
    /// </summary>
    public class SectionJsonModel
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
        /// Property to access to the list of Album dependencies primary key.
        /// </summary>
        public IEnumerable<int> AlbumsPKeys { get; set; }

        /// <summary>
        /// Property to access to the list of AclGroup dependencies primary key.
        /// </summary>
        public IEnumerable<int> AclGroupsPKeys { get; set; }

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

        /// <summary>
        /// Class XtrmAddons PhotoAlbum Server Api Models Json Category constructor.
        /// </summary>
        /// <param name="section"></param>
        /// <param name="loadAlbums"></param>
        /// <param name="loadPictures"></param>
        [System.Obsolete("", true)]
        public SectionJsonModel(SectionEntity entity, bool auth = false, bool loadAlbums = false, bool loadPictures = false)
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
        public void FromEntity(SectionEntity entity, bool auth = false)
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

            return entity;
        }

        #endregion
    }
}
