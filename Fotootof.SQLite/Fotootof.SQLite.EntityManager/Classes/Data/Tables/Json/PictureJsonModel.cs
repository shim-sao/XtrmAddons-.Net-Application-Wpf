using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using System;

namespace Fotootof.SQLite.EntityManager.Data.Tables.Json.Models
{
    /// <summary>
    /// Class Fotootof.Plugin.Api Models Json Picture.
    /// </summary>
    public class PictureJsonModel : EntityJsonModel<PictureEntity>
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
        public PictureJsonModel() : base() { }

        /// <summary>
        /// Class XtrmAddons Fotootof Lib Api Models Json Picture Constructor.
        /// </summary>
        /// <param name="entity">A entity entity.</param>
        public PictureJsonModel(PictureEntity entity) : base() { }

        #endregion
    }
}
