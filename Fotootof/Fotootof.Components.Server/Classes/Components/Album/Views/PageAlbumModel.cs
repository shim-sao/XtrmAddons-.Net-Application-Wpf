using Fotootof.Libraries.Components;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.SQLite.EntityManager.Data.Tables.Interfaces;
using System.Diagnostics;
using System.Reflection;

namespace Fotootof.Components.Server.Album
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Component Page Album Model.
    /// </summary>
    public class PageAlbumModel : PageBaseModel<PageAlbumLayout>, IAlbumEntity
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable Album data entity.
        /// </summary>
        private AlbumEntity albumEntity;

        #endregion


        #region Properties

        /// <summary>
        /// Property to access to the Album data entity.
        /// </summary>
        public AlbumEntity AlbumEntity
        {
            get
            {
                Debug.WriteLine($"* Getter : {GetType().Name}.{MethodBase.GetCurrentMethod().Name} => {albumEntity}");
                return albumEntity;
            }
            set
            {
                if(albumEntity != value)
                {
                    Debug.WriteLine($"+ Setter : {GetType().Name}.{MethodBase.GetCurrentMethod().Name} => {value}");
                    albumEntity = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion


        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Page Album Model Constructor.
        /// </summary>
        /// <param name="page">The page associated to the model.</param>
        public PageAlbumModel(PageAlbumLayout controlView) : base(controlView) { }

        #endregion
    }
}
