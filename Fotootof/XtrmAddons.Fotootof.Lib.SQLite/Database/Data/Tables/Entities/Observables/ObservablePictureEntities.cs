using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Xml.Serialization;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies.Observables;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities.Observables
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Libraries SQLite Observable Pictures Entities.</para>
    /// <para>It provides the management of an observable collection of Pictures and their dependencies.</para>
    /// </summary>
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, Title = "Pictures")]
    [XmlType(TypeName = "Pictures")]
    public class ObservablePictureEntities : ObservableCollection<PictureEntity>
    {
        #region Variables
        
        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
        	log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        #endregion


        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public ObservablePicturesInAlbums PicturesInAlbums { get; set; }
        
        #endregion


        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public ObservablePictureEntities()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public ObservablePictureEntities(List<PictureEntity> list) : base(list)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public ObservablePictureEntities(IEnumerable<PictureEntity> collection) : base(collection)
        {
        }

        #endregion



        #region Methods

        #endregion
    }
}