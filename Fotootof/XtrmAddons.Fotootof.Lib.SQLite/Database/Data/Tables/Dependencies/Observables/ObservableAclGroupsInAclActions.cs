using Newtonsoft.Json;
using System.Collections.Generic;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies.Observables
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable AclGroups In AclActions.
    /// </summary>
    /// <typeparam name="E">The Type of the entity items destination of the dependency.</typeparam>
    [JsonArray(Title = "AclGroups_AclActions")]
    public class ObservableAclGroupsInAclActions<E> : ObservableDependenciesBase<AclGroupsInAclActions, E> where E : class
    {
        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable AclGroups In AclActions Constructor.
        /// </summary>
        public ObservableAclGroupsInAclActions() : base() { }

        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable AclGroups In AclActions Constructor.
        /// </summary>
        /// <param name="list">A list of items to add at the collection initialization. </param>
        public ObservableAclGroupsInAclActions(List<AclGroupsInAclActions> list) : base(list) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable AclGroups In AclActions Constructor.
        /// </summary>
        /// <param name="collection">A enumerable collection of items to add at the collection initialization.</param>
        public ObservableAclGroupsInAclActions(IEnumerable<AclGroupsInAclActions> collection) : base(collection) { }
    }
}