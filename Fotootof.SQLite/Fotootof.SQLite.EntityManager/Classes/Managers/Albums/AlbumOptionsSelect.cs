using Fotootof.SQLite.EntityManager.Base;

namespace Fotootof.SQLite.EntityManager.Managers
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Album Entities Select Options.
    /// </summary>
    public class AlbumOptionsSelect : EntitiesOptionsSelect
    {
        #region Properties

        /// <summary>
        /// Property to filter Album query by Alias.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Property to filter Album query by BackgroundPictureId.
        /// </summary>
        public int BackgroundPictureId { get; set; } = 0;

        /// <summary>
        /// Property to filter Album query by PreviewPPictureId.
        /// </summary>
        public int PreviewPictureId { get; set; } = 0;

        /// <summary>
        /// Property to filter Album query by BackgroundPictureId.
        /// </summary>
        public int ThumbnailPictureId { get; set; } = 0;

        #endregion

    }
}