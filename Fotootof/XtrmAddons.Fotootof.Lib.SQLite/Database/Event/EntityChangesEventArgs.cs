using System;

namespace XtrmAddons.Fotootof.Lib.SQLite.Event
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib SQLite Event Arguments.
    /// </summary>
    public class EntityChangesEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Property to access to an array of new entities arguments.
        /// </summary>
        public object[] NewEntities { get; set; }

        /// <summary>
        /// Property to access to an array of old entities arguments.
        /// </summary>
        public object[] OldEntities { get; set; }

        /// <summary>
        /// Property to access to the new entity arguments.
        /// </summary>
        public object NewEntity { get; set; }

        /// <summary>
        /// Property to access to the old entity Arguments.
        /// </summary>
        public object OldEntity { get; set; }

        #endregion



        #region Constructors

        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Event Arguments Constructor.
        /// </summary>
        /// <param name="args">Event Arguments.</param>
        public EntityChangesEventArgs(object[] newEntities, object[] oldEntities = null)
        {
            NewEntities = newEntities;
            OldEntities = oldEntities;
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Event Arguments Constructor.
        /// </summary>
        /// <param name="args">Event Arguments.</param>
        public EntityChangesEventArgs(object newEntity, object oldEntity = null)
        {
            NewEntity = newEntity;
            OldEntity = oldEntity;
        }

        #endregion

    }
}
