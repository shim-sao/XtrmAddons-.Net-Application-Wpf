using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.SQLite.EntityManager.Data.Tables.Json.Models;
using Fotootof.SQLite.EntityManager.Managers;
using System.Collections.Generic;

namespace Fotootof.Collections.Models
{
    /// <summary>
    /// Class XtrmAddons Fotootof Collections Models Albums.
    /// </summary>
    public class AlbumModelCollection : CollectionBaseEntity<AlbumJsonModel, AlbumOptionsList>
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Properties

        /// <summary>
        /// Property to set or check if auto load is enabled.
        /// </summary>
        public override bool IsAutoloadEnabled => true;

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Common Albums Collection Constructor.
        /// </summary>
        public AlbumModelCollection() : base() { }

        /// <summary>
        /// Class XtrmAddons Fotootof Common Albums Collection Constructor.
        /// </summary>
        /// <param name="list">A <see cref="List{AlbumJsonModel}"/> to paste in.</param>
        public AlbumModelCollection(List<AlbumJsonModel> list) : base(list) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Common Albums Collection Constructor.
        /// </summary>
        /// <param name="collection">A <see cref="IEnumerable{AlbumJsonModel}"/> to paste in.</param>
        public AlbumModelCollection(IEnumerable<AlbumJsonModel> collection) : base(collection) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Common Albums Collection Constructor.
        /// </summary>
        /// <param name="list">A list of Album to paste in.</param>
        public AlbumModelCollection(IEnumerable<AlbumEntity> list) : base()
        {
            //if (list == null)
            //{
            //    ArgumentNullException ex = Exceptions.GetArgumentNull(nameof(list), list);
            //    log.Warn(ex.Output(), ex);
            //    return;
            //}

            //if (list.Count() == 0)
            //{
            //    return;
            //}

            //foreach (AlbumJsonModel item in list)
            //{
            //    Add(item.ToEntity());
            //}
        }

        #endregion
    }
}
