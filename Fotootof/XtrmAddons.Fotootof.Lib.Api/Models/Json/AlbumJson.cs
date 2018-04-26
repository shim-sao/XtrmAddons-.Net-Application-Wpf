using System;
using System.Collections.Generic;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;

namespace XtrmAddons.Fotootof.Lib.Api.Models.Json
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib Api Models Json Album.
    /// </summary>
    public class AlbumJson : EntityJson
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
        /// Variable the picture path.
        /// </summary>
        public string PicturePath { get; set; }

        /// <summary>
        /// Variable the picture width.
        /// </summary>
        public int PictureWidth { get; set; }

        /// <summary>
        /// Variable the picture height.
        /// </summary>
        public int PictureHeight { get; set; }

        /// <summary>
        /// Variable the picture length.
        /// </summary>
        //public long PictureLength { get; set; }

        /// <summary>
        /// Variable the thumbnail picture path.
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
        //public int ThumbnailLength { get; set; }

        /// <summary>
        /// Variable the comment.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Variable list of categories in folder.
        /// </summary>
        public List<PictureJson> Pictures { get; set; } = new List<PictureJson>();

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Lib Api Models Json Album constructor.
        /// </summary>
        public AlbumJson() { }

        /// <summary>
        /// Class XtrmAddons Fotootof Lib Api Models Json Album constructor.
        /// </summary>
        /// <param name="album">An Album Entity.</param>
        public AlbumJson(AlbumEntity entity, bool auth = false) : this()
        {
            Bind(entity, auth);
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Lib Api Models Json Album constructor.
        /// </summary>
        /// <param name="album">An Album entity.</param>
        /// <param name="listPictures"></param>
        public AlbumJson(AlbumEntity entity, bool auth = false, bool loadPictures = false) : this(entity, auth)
        {
            if (loadPictures)
            {
                foreach (PictureEntity picture in entity.Pictures)
                {
                    Pictures.Add(new PictureJson(picture));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="auth"></param>
        public void Bind(AlbumEntity entity, bool auth = false)
        {
            PrimaryKey = entity.PrimaryKey;

            Name = entity.Name;
            Alias = entity.Alias;
            Description = entity.Description;
            Ordering = entity.Ordering;
            DateStart = entity.DateStart;
            DateEnd = entity.DateEnd;
            Created = entity.Created;
            Modified = entity.Modified;

            PicturePath = entity.PicturePath;
            PictureWidth = entity.PictureWidth;
            PictureHeight = entity.PictureHeight;
            //PictureLength = entity.PictureLength;

            ThumbnailPath = entity.ThumbnailPath;
            ThumbnailWidth = entity.ThumbnailWidth;
            ThumbnailHeight = entity.ThumbnailHeight;
            //ThumbnailHeight = entity.ThumbnailLength;

            if (auth)
            {
                Comment = entity.Comment;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public AlbumEntity ToEntity()
        {
            AlbumEntity entity = new AlbumEntity();

            entity.PrimaryKey = PrimaryKey;
            entity.Name = Name;
            entity.Alias = Alias;
            entity.Description = Description;
            entity.Ordering = Ordering;
            entity.DateStart = DateStart;
            entity.DateEnd = DateEnd;
            entity.Created = Created;
            Modified = entity.Modified;

            entity.PicturePath = PicturePath;
            entity.PictureWidth = PictureWidth;
            entity.PictureHeight = PictureHeight;

            entity.ThumbnailPath = ThumbnailPath;
            entity.ThumbnailWidth = ThumbnailWidth;
            entity.ThumbnailHeight = ThumbnailHeight;
            entity.Comment = Comment;

            return entity;
        }

        #endregion
    }
}
