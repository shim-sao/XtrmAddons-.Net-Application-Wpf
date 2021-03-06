﻿using Newtonsoft.Json;
using System.Collections.Generic;
using Fotootof.SQLite.EntityManager.Data.Base;

namespace Fotootof.SQLite.EntityManager.Data.Tables.Dependencies.Observables
{
    /// <summary>
    /// Class Fotootof.SQLite.EntityManager Data Tables Dependencies Observable Sections In AclGroups.
    /// </summary>
    /// <typeparam name="O">The Type of the entity item to observe.</typeparam>
    /// <typeparam name="E">The Type of the entity items destination of the dependency.</typeparam>
    [JsonArray(Title = "Sections_AclGroups")]
    public class ObservableSectionsInAclGroups<O, E> : ObservableDependencyBase<SectionsInAclGroups, O, E> where O : class where E : class
    {
        /// <summary>
        /// Class Fotootof.SQLite.EntityManager Data Tables Dependencies Observable Sections In AclGroups Constructor.
        /// </summary>
        public ObservableSectionsInAclGroups() : base() { }

        /// <summary>
        /// Class Fotootof.SQLite.EntityManager Data Tables Dependencies Observable Sections In AclGroups Constructor.
        /// </summary>
        /// <param name="list">A list of items to add at the collection initialization. </param>
        public ObservableSectionsInAclGroups(List<SectionsInAclGroups> list) : base(list) { }


        /// <summary>
        /// Class Fotootof.SQLite.EntityManager Data Tables Dependencies Observable Sections In AclGroups Constructor.
        /// </summary>
        /// <param name="collection">A enumerable collection of items to add at the collection initialization.</param>
        public ObservableSectionsInAclGroups(IEnumerable<SectionsInAclGroups> collection) : base(collection) { }
    }
}
