using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using XtrmAddons.Net.Common.Extensions;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server SQLite User table entity.
    /// </summary>
    [Table("InfosTypes")]
    [JsonObject(MemberSerialization.OptIn)]
    public partial class InfoTypeEntity : EntityBase
    {
        #region Variables
        #endregion



        #region Properties

        /// <summary>
        /// Propmerty primary key auto incremented.
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public int InfoTypeId
        {
            get { return primaryKey; }
            set
            {
                if (value != primaryKey)
                {
                    primaryKey = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property Name of the Info.
        /// </summary>
        [Column(Order = 1)]
        public string Name { get; set; }

        /// <summary>
        /// Property Alias of the Info.
        /// </summary>
        [Column(Order = 2)]
        public string Alias { get; set; }

        /// <summary>
        /// Property Description of the Info.
        /// </summary>
        [Column(Order = 3)]
        public string Description { get; set; }

        /// <summary>
        /// Property is default of the item.
        /// </summary>
        [Column(Order = 4)]
        public bool IsDefault { get; set; }
        
        /// <summary>
        /// Property order place of the item.
        /// </summary>
        [Column(Order = 5)]
        public int Ordering { get; set; }

        #endregion



        #region Constructor

        public InfoTypeEntity()
        {
            Initialize();
        }

        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        public void Initialize()
        {
            if (PrimaryKey <= 0)
            {
                PrimaryKey = 0;
            }

            this.InitializeNulls();
            Alias = Alias.RemoveDiacritics().Sanitize();
        }

        #endregion
    }
}