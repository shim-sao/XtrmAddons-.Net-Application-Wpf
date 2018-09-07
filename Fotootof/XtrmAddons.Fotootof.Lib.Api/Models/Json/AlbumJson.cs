using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using System;
using System.Collections.Generic;

namespace XtrmAddons.Fotootof.Lib.Api.Models.Json
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib Api Models Json Album.
    /// </summary>
    [System.Obsolete("Fotootof.SQLite.EntityManager.Data.Tables.Json.Models", true)]
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
        /// Property to access to the background picture id.
        /// </summary>
        public int BackgroundPictureId { get; set; }

        /// <summary>
        /// Property to access to the preview picture id.
        /// </summary>
        public int PreviewPictureId { get; set; }

        /// <summary>
        /// Property to access to the thumbnail picture id.
        /// </summary>
        public int ThumbnailPictureId { get; set; }

        /// <summary>
        /// Variable the thumbnail length.
        /// </summary>
        //public int ThumbnailLength { get; set; }

        /// <summary>
        /// Variable the comment.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Property parameters for the item.
        /// </summary>
        public string Parameters { get; set; }

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
            entity.Modified = Modified;

            entity.Comment = Comment;

            return entity;
        }

        #endregion
    }
}
