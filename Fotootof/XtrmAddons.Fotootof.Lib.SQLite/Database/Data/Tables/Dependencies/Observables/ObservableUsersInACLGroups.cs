﻿using Newtonsoft.Json;
using System.Collections.Generic;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies.Observables
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Users In AclGroups.
    /// </summary>
    /// <typeparam name="E">The Type of the entity items destination of the dependency.</typeparam>
    [JsonArray(Title = "Users_AclGroups")]
    public class ObservableUsersInAclGroups<E> : ObservableDependenciesBase<UsersInAclGroups, E> where E : class
    {

        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Users In AclGroups Constructor.
        /// </summary>
        public ObservableUsersInAclGroups() : base() { }

        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Users In AclGroups Constructor.
        /// </summary>
        /// <param name="list">A list of items to add at the collection initialization.</param>
        public ObservableUsersInAclGroups(List<UsersInAclGroups> list) : base(list) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Users In AclGroups Constructor.
        /// </summary>
        /// <param name="collection">A enumerable collection of items to add at the collection initialization.</param>
        public ObservableUsersInAclGroups(IEnumerable<UsersInAclGroups> collection) : base(collection) { }
    }
}