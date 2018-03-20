using Microsoft.EntityFrameworkCore;
using System;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Scheme
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Database Core Scheme.
    /// </summary>
    public class DatabaseCore : IDisposable
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        protected static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable database context entity core.
        /// </summary>
        private DatabaseContextCore context = null;

        /// <summary>
        /// Variable database file name.
        /// </summary>
        private string dbname = "";

        #endregion



        #region Properties

        /// <summary>
        /// Property AclAction entities manager.
        /// </summary>
        public AclActionManager AclActions { get; private set; }

        /// <summary>
        /// Variable AclGroup entities manager.
        /// </summary>
        public AclGroupManager AclGroups { get; private set; }

        /// <summary>
        /// Variable Album Entities manager.
        /// </summary>
        public AlbumManager Albums { get; private set; }

        /// <summary>
        /// Variable Info Entities manager.
        /// </summary>
        public InfoManager Infos { get; private set; }

        /// <summary>
        /// Variable Picture entities manager.
        /// </summary>
        public PictureManager Pictures { get; private set; }

        /// <summary>
        /// Property Section entities manager.
        /// </summary>
        public SectionManager Sections { get; private set; }

        /// <summary>
        /// Variable User entities manager.
        /// </summary>
        public UserManager Users { get; private set; }

        /// <summary>
        /// Accessors for database context entity core.
        /// </summary>
        public DatabaseContextCore Context
        {
            get
            {
                if (dbname != "")
                {
                    Connect();
                    RegisterContext();
                }

                return context;
            }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// <para>Class XtrmAddons PhotoAlbum Server SQLite Database Core constructor.</para>
        /// <para>This class provide database connection and data management.</para>
        /// </summary>
        /// <param name="database">A database filename. Full path of the SQLite database.</param>
        /// <exception cref="ArgumentNullException">Exception thrown if database file name is null.</exception>
        /// <exception cref="ArgumentException">Exception thrown if database file name is invalid.</exception>
        public DatabaseCore(string filename)
        {
            if (filename == null)
            {
                throw new ArgumentNullException(nameof(filename), "Database file name is null !");
            }

            if (filename == "")
            {
                throw new ArgumentException(nameof(filename), "Database file name is empty !");
            }

            dbname = filename;
            Context.Dispose();
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to connect to the database.
        /// </summary>
        /// <exception cref="InvalidOperationException">Exception thrown if database connection fail.</exception>
        private void Connect()
        {
            try
            {
                var options = new DbContextOptionsBuilder<DatabaseContextCore>();
                options.UseSqlite(GetConnectionString());
                context = new DatabaseContextCore(options.Options);
            }
            catch (Exception e)
            {
                string s = string.Format("Cannot open database : {0}", dbname);

                log.Fatal("FATAL : " + s);
                log.Fatal(string.Format("{0} : {1}", e.HResult, e.Message));
                log.Fatal(e.TargetSite);
                log.Fatal(e.Source);
                log.Fatal(e.StackTrace);

                throw new InvalidOperationException(string.Format("Cannot open database : {0}", dbname), e);
            }
        }

        /// <summary>
        /// Method to connect to the database.
        /// </summary>
        private string GetConnectionString()
        {
            return "Data Source=" + dbname;
        }

        /// <summary>
        /// Method to dispose database context.
        /// </summary>
        public void ContextDispose()
        {
            context.Dispose();
            context = null;
        }

        /// <summary>
        /// Method to register entities context manager.
        /// </summary>
        protected void RegisterContext()
        {
            AclActions = new AclActionManager(context);
            AclGroups = new AclGroupManager(context);
            Albums = new AlbumManager(context);
            Infos = new InfoManager(context);
            Pictures = new PictureManager(context);
            Sections = new SectionManager(context);
            Users = new UserManager(context);
        }

        #endregion
        


        #region IDisposable Support

        private bool disposedValue = false; // Pour détecter les appels redondants

        /// <summary>
        /// Method to dispose object and its resources. 
        /// </summary>
        /// <param name="disposing">Dispose object resources ?</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: supprimer l'état managé (objets managés).
                    if (context != null)
                    {
                        ContextDispose();
                    }
                }

                // TODO: libérer les ressources non managées (objets non managés) et remplacer un finaliseur ci-dessous.
                // TODO: définir les champs de grande taille avec la valeur Null.

                disposedValue = true;
            }
        }

        // TODO: remplacer un finaliseur seulement si la fonction Dispose(bool disposing) ci-dessus a du code pour libérer les ressources non managées.
        // ~DatabaseCore() {
        //   // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
        //   Dispose(false);
        // }


        /// <summary>
        /// Method to dispose object and its resources. 
        /// </summary>
        // Ce code est ajouté pour implémenter correctement le modèle supprimable.
        public void Dispose()
        {
            // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
            Dispose(true);// TODO: supprimer les marques de commentaire pour la ligne suivante si le finaliseur est remplacé ci-dessus.// GC.SuppressFinalize(this);
        }

        #endregion
    }
}