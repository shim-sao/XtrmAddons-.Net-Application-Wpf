using System;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;

namespace XtrmAddons.Fotootof.Lib.Api.Models.Json
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib Api Models Json Picture.
    /// </summary>
    public class PictureJson : EntityJson
    {
        #region Properties

        /// <summary>
        /// Variable primary key auto incremented.
        /// </summary>
        public int PictureId { get; set; }

        /// <summary>
        /// Variable name of the item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Variable is default item.
        /// </summary>
        public bool Default { get; set; }

        /// <summary>
        /// Variable order place of the item.
        /// </summary>
        public int Ordering { get; set; }

        /// <summary>
        /// Variable created date.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Variable modified date.
        /// </summary>
        public DateTime Modified { get; set; }

        /// <summary>
        /// Variable Captured date.
        /// </summary>
        public DateTime Captured { get; set; }

        /// <summary>
        /// Variable the entity path.
        /// </summary>
        public string PicturePath { get; set; }

        /// <summary>
        /// Variable the entity width.
        /// </summary>
        public int PictureWidth { get; set; }

        /// <summary>
        /// Variable the entity height.
        /// </summary>
        public int PictureHeight { get; set; }

        /// <summary>
        /// Variable the picture length.
        /// </summary>
        public long PictureLength { get; set; }

        /// <summary>
        /// Variable the thumbnail entity path.
        /// </summary>
        public string ThumbnailPath { get; set; }

        /// <summary>
        /// Variable the thumbnail width.
        /// </summary>
        public int ThumbnailWidth { get; set; }

        /// <summary>
        /// Variable the thumbnail height.
        /// </summary>
        public int ThumbnailHeight { get; set; }

        /// <summary>
        /// Variable the thumbnail length.
        /// </summary>
        public long ThumbnailLength { get; set; }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Lib Api Models Json Picture Constructor.
        /// </summary>
        public PictureJson() { }

        /// <summary>
        /// Class XtrmAddons Fotootof Lib Api Models Json Picture Constructor.
        /// </summary>
        /// <param name="entity">A entity entity.</param>
        public PictureJson(PictureEntity entity) : this()
        {
            PictureId = entity.PictureId;
            Name = entity.Name;

            Ordering = entity.Ordering;

            Created = entity.Created;
            Modified = entity.Modified;
            Captured = entity.Captured;

            PicturePath = entity.PicturePath;
            PictureWidth = entity.PictureWidth;
            PictureHeight = entity.PictureHeight;
            PictureLength = entity.PictureLength;

            ThumbnailPath = entity.ThumbnailPath;
            ThumbnailWidth = entity.ThumbnailWidth;
            ThumbnailHeight = entity.ThumbnailHeight;
            ThumbnailLength = entity.ThumbnailLength;
        }

        #endregion
    }
}
