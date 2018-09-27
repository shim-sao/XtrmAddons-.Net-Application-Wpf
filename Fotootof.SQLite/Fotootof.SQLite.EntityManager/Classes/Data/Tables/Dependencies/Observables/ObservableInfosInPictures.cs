using Newtonsoft.Json;
using System.Collections.Generic;
using Fotootof.SQLite.EntityManager.Data.Base;

namespace Fotootof.SQLite.EntityManager.Data.Tables.Dependencies.Observables
{
    /// <summary>
    /// Class Fotootof.SQLite.EntityManager Data Tables Dependencies Observable Infos In Pictures.
    /// </summary>
    /// <typeparam name="O">The Type of the entity item to observe.</typeparam>
    /// <typeparam name="E">The Type of the entity items destination of the dependency.</typeparam>
    [JsonArray(Title = "Infos_Pictures")]
    public class ObservableInfosInPictures<O, E> : ObservableDependencyBase<InfosInPictures, O, E> where O : class where E : class
    {
        /// <summary>
        /// Class Fotootof.SQLite.EntityManager Data Tables Dependencies Observable Infos In Pictures Constructor.
        /// </summary>
        public ObservableInfosInPictures() : base() { }

        /// <summary>
        /// Class Fotootof.SQLite.EntityManager Data Tables Dependencies Observable Infos In Pictures Constructor.
        /// </summary>
        /// <param name="list">A list of items to add at the collection initialization. </param>
        public ObservableInfosInPictures(List<InfosInPictures> list) : base(list) { }

        /// <summary>
        /// Class Fotootof.SQLite.EntityManager Data Tables Dependencies Observable Infos In Pictures Constructor.
        /// </summary>
        /// <param name="dependenciesPrimaryKeysName"></param>
        /// <param name="collection">A enumerable collection of items to add at the collection initialization.</param>
        public ObservableInfosInPictures(string dependenciesPrimaryKeysName, IEnumerable<InfosInPictures> collection) : base(collection) { }
    }
}
