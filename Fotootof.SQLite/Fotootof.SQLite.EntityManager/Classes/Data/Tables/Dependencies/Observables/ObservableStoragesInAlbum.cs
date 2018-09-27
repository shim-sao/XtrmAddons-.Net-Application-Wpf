using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Fotootof.SQLite.EntityManager.Data.Base;

namespace Fotootof.SQLite.EntityManager.Data.Tables.Dependencies.Observables
{
    /// <summary>
    /// Class Fotootof.SQLite.EntityManager Data Tables Dependencies Observable Storages In Albums.
    /// </summary>
    /// <typeparam name="O">The Type of the entity item to observe.</typeparam>
    /// <typeparam name="E">The Type of the entity items destination of the dependency.</typeparam>
    [JsonArray(Title = "Storages_Albums")]
    public class ObservableStoragesInAlbums<O, E> : ObservableDependencyBase<StoragesInAlbums, O, E> where O : class where E : class
    {
        #region Constructs

        /// <summary>
        /// Class Fotootof.SQLite.EntityManager Data Tables Dependencies Observable Storages In Albums Constructor.
        /// </summary>
        public ObservableStoragesInAlbums() : base() { }

        /// <summary>
        /// Class Fotootof.SQLite.EntityManager Data Tables Dependencies Observable Storages In Albums Constructor.
        /// </summary>
        /// <param name="list">A list of items to add at the collection initialization. </param>
        public ObservableStoragesInAlbums(List<StoragesInAlbums> list) : base(list) { }

        /// <summary>
        /// Class Fotootof.SQLite.EntityManager Data Tables Dependencies Observable Storages In Albums Constructor.
        /// </summary>
        /// <param name="collection">A enumerable collection of items to add at the collection initialization.</param>
        public ObservableStoragesInAlbums(IEnumerable<StoragesInAlbums> collection) : base(collection) { }

        #endregion
    }
}
