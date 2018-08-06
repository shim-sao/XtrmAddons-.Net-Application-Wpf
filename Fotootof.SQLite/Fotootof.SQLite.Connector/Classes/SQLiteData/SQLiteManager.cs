using System.Collections.Generic;
using System.Linq;
using XtrmAddons.Net.SQLiteBundle;

namespace Fotootof.SQLite.Connector.SQLiteData
{
    /// <summary>
    /// Class to provide XtrmAddons Fotootof Server SQLite management.
    /// </summary>
    public class SQLiteManager : WpfSQLiteData
    {
        /// <summary>
        /// Class SQLite Manager constructor.
        /// </summary>
        /// <param name="database">The name of the database (full filename).</param>
        /// <param name="createFile">Create file if not exists.</param>
        /// <param name="scheme">Add database scheme on creation.</param>
        /// </summary>
        public SQLiteManager(string database, bool createFile = false, string scheme = "")
            : base(database, createFile, scheme) { }

        /// <summary>
        /// Variable dictionary of databases connection instance using System.Data.SQLite
        /// </summary>
        private static Dictionary<string, SQLiteManager> _instance = new Dictionary<string, SQLiteManager>();

        /// <summary>
        /// Method to get a connection instance of a database.
        /// </summary>
        /// <param name="database">The name of the database (full filename).</param>
        /// <param name="createFile">Create file if not exists.</param>
        /// <param name="scheme">Add database scheme on creation.</param>
        /// <returns>A SQLite manager for the database.</returns>
        public static SQLiteManager Instance(string database, bool createFile = false, string scheme = "")
        {
            if(_instance.ContainsKey(database).Equals(false))
            {
                _instance.Add(database, new SQLiteManager(database, createFile, scheme));
            }

            return _instance[database];
        }

        /// <summary>
        /// Method to get a connection instance of a database.
        /// </summary>
        /// <param name="database">The name of the database (full filename).</param>
        /// <returns>A SQLite manager for the database.</returns>
        public static SQLiteManager Instance(string database)
        {
            if (_instance.ContainsKey(database).Equals(false))
            {
                _instance.Add(database, new SQLiteManager(database));
            }

            return _instance[database];
        }

        /// <summary>
        /// Method to get a connection instance of a database.
        /// </summary>
        /// <returns>A SQLite manager for the database.</returns>
        public static SQLiteManager Instance()
        {
            return _instance.First().Value;
        }

        /// <summary>
        /// Method to initialize database settings.
        /// </summary>
        protected new void InitializeSetting()
        {
            // Do nothing yet.
        }
    }
}
