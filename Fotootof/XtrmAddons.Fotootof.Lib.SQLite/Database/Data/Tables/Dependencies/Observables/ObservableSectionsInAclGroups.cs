using Newtonsoft.Json;
using System.Collections.Generic;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies.Observables
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Sections In AclGroups.
    /// </summary>
    /// <typeparam name="E">The Type of the entity items destination of the dependency.</typeparam>
    [JsonArray(Title = "Sections_AclGroups")]
    public class ObservableSectionsInAclGroups<E> : ObservableDependenciesBase<SectionsInAclGroups, E> where E : class
    {
        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Sections In AclGroups Constructor.
        /// </summary>
        public ObservableSectionsInAclGroups() : base() { }

        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Sections In AclGroups Constructor.
        /// </summary>
        /// <param name="list">A list of items to add at the collection initialization. </param>
        public ObservableSectionsInAclGroups(List<SectionsInAclGroups> list) : base(list) { }


        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Sections In AclGroups Constructor.
        /// </summary>
        /// <param name="collection">A enumerable collection of items to add at the collection initialization.</param>
        public ObservableSectionsInAclGroups(IEnumerable<SectionsInAclGroups> collection) : base(collection) { }
    }
}
