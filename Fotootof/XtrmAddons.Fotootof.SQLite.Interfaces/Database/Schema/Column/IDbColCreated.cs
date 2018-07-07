using System;

namespace XtrmAddons.Fotootof.SQLite.Interfaces.Database.Schema.Column
{
    /// <summary>
    /// <para>Interface XtrmAddons Fotootof SQLite Database Schema Column Created.</para>
    /// <para>Insures that the object has an Created property according to the table schema.</para>
    /// </summary>
    public interface IDbColCreated
    {
        #region Proprerties

        /// <summary>
        /// Property to accass to the Created of the entity.
        /// </summary>
        DateTime Created { get; set; }

        #endregion
    }
}
