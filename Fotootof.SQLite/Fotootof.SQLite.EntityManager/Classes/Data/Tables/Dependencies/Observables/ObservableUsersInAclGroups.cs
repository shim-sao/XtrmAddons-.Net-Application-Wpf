using Newtonsoft.Json;
using System.Collections.Generic;
using Fotootof.SQLite.EntityManager.Data.Base;

namespace Fotootof.SQLite.EntityManager.Data.Tables.Dependencies.Observables
{
    /// <summary>
    /// Class Fotootof.SQLite.EntityManager Data Tables Dependencies Observable Users In AclGroups.
    /// </summary>
    /// <typeparam name="O">The Type of the entity item to observe.</typeparam>
    /// <typeparam name="E">The Type of the entity items destination of the dependency.</typeparam>
    [JsonArray(Title = "Users_AclGroups")]
    public class ObservableUsersInAclGroups<O, E> : ObservableDependencyBase<UsersInAclGroups, O, E> where O : class where E : class
    {

        /// <summary>
        /// Class Fotootof.SQLite.EntityManager Data Tables Dependencies Observable Users In AclGroups Constructor.
        /// </summary>
        public ObservableUsersInAclGroups() : base() { }

        /// <summary>
        /// Class Fotootof.SQLite.EntityManager Data Tables Dependencies Observable Users In AclGroups Constructor.
        /// </summary>
        /// <param name="list">A list of items to add at the collection initialization.</param>
        public ObservableUsersInAclGroups(List<UsersInAclGroups> list) : base(list) { }

        /// <summary>
        /// Class Fotootof.SQLite.EntityManager Data Tables Dependencies Observable Users In AclGroups Constructor.
        /// </summary>
        /// <param name="collection">A enumerable collection of items to add at the collection initialization.</param>
        public ObservableUsersInAclGroups(IEnumerable<UsersInAclGroups> collection) : base(collection) { }
    }
}