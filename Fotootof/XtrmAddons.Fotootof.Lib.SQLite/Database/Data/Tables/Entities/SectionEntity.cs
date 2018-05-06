using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Database Section Entity.
    /// </summary>
    [Table("Sections")]
    public partial class SectionEntity : EntityBase
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable name of the Section.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        private string name = "";

        /// <summary>
        /// Variable alias of the Section.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        private string alias = "";

        /// <summary>
        /// Variable description of the Section.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public string description = "";

        /// <summary>
        /// Variable is default item.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public bool isDefault = false;

        /// <summary>
        /// Variable order place of the item.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public int ordering = 0;


        /// <summary>
        /// Variable the background picture id.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public int backgroundPictureId = 0;


        /// <summary>
        /// Variable the preview picture id.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public int previewPictureId = 0;

        /// <summary>
        /// Variable the thumbnail picture id.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public int thumbnailPictureId = 0;


        /// <summary>
        /// Variable comment for the item.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public string comment = "";


        /// <summary>
        /// Variable parameters for the item.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public string parameters = "";

        /// <summary>
        /// Variable list of Albums associated to the Section.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        private List<AlbumEntity> albums;

        /// <summary>
        /// Variable list of AclGroups associated to the Section.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        private List<AclGroupEntity> aclGroups;

        #endregion



        #region Proprerties

        /// <summary>
        /// Property primary key auto incremented.
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public int SectionId
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
        /// Property to access to the name of the Section.
        /// </summary>
        [Column(Order = 1)]
        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// <para>Property to access to the alias of the Section.</para>
        /// <para>The alias is automaticaly formated when it is changed.</para>
        /// </summary>
        [Column(Order = 2)]
        public string Alias
        {
            get { return alias; }
            set
            {
                if (value != alias)
                {
                    alias = value.Sanitize().RemoveDiacritics().ToLower();
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the description of the item.
        /// </summary>
        [Column(Order = 3)]
        public string Description
        {
            get { return description; }
            set
            {
                if (value != description)
                {
                    description = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the is default item.
        /// </summary>
        [Column(Order = 4)]
        public bool IsDefault
        {
            get { return isDefault; }
            set
            {
                if (value != isDefault)
                {
                    isDefault = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the order place of the item.
        /// </summary>
        [Column(Order = 5)]
        public int Ordering
        {
            get { return ordering; }
            set
            {
                if (value != ordering)
                {
                    ordering = value;
                    NotifyPropertyChanged();
                }
            }
        }


        /// <summary>
        /// Property to access to the background picture id.
        /// </summary>
        [Column(Order = 6)]
        public int BackgroundPictureId
        {
            get { return backgroundPictureId; }
            set
            {
                if (value != backgroundPictureId)
                {
                    backgroundPictureId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the preview picture id.
        /// </summary>
        [Column(Order = 7)]
        public int PreviewPictureId
        {
            get { return previewPictureId; }
            set
            {
                if (value != previewPictureId)
                {
                    previewPictureId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the thumbnail picture id.
        /// </summary>
        [Column(Order = 8)]
        public int ThumbnailPictureId
        {
            get { return thumbnailPictureId; }
            set
            {
                if (value != thumbnailPictureId)
                {
                    thumbnailPictureId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the comment for the item.
        /// </summary>
        [Column(Order = 9)]
        public string Comment
        {
            get { return comment; }
            set
            {
                if (value != comment)
                {
                    comment = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the parameters for the item.
        /// </summary>
        [Column(Order = 10)]
        public string Parameters
        {
            get { return parameters; }
            set
            {
                if (value != parameters)
                {
                    parameters = value;
                    NotifyPropertyChanged();
                }
            }
        }
        
        #endregion



        #region Proprerties : Dependencies

        /// <summary>
        /// Property Album id (required for entity dependency).
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public int AlbumId { get; set; }

        /// <summary>
        /// Property AclGroup id (required for entity dependency).
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public int AclGroupId { get; set; }

        /// <summary>
        /// Property to access to the collection of relationship Albums in Sections.
        /// </summary>
        [JsonIgnore]
        public ObservableCollection<AlbumsInSections> AlbumsInSections { get; set; }
            = new ObservableCollection<AlbumsInSections>();

        /// <summary>
        /// Property to access to the collection of relationship Sections in AclGroups.
        /// </summary>
        [JsonIgnore]
        public ObservableCollection<SectionsInAclGroups> SectionsInAclGroups { get; set; }
            = new ObservableCollection<SectionsInAclGroups>();

        /// <summary>
        /// Property to access to the list of Albums associated to the Section.
        /// </summary>
        [NotMapped]
        public List<AlbumEntity> Albums
        {
            get => ListAlbums();
            set => albums = value;
        }

        /// <summary>
        /// Property to access to the list of Album dependencies primary key.
        /// </summary>
        [NotMapped]
        public List<int> AlbumsPK
            => ListOfPrimaryKeys(AlbumsInSections.ToList(), "AlbumId");

        /// <summary>
        /// Property to access to the list of AclGroups associated to the Section.
        /// </summary>
        [NotMapped]
        public List<AclGroupEntity> AclGroups
        {
            get => ListAclGroups();
            set => aclGroups = value;
        }

        /// <summary>
        /// Property to access to the list of AclGroup dependencies primary key.
        /// </summary>
        [NotMapped]
        public List<int> AclGroupsPK
            => ListOfPrimaryKeys(SectionsInAclGroups.ToList(), "AclGroupId");

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries SQLite Section Entity Constructor.
        /// </summary>
        public SectionEntity()
        {
            Initialize();

            AlbumsInSections.CollectionChanged += (s, e) => { albums = null; };
            SectionsInAclGroups.CollectionChanged += (s, e) => { aclGroups = null; };
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to initialize the Section.
        /// </summary>
        public void Initialize()
        {
            if (PrimaryKey <= 0)
            {
                PrimaryKey = 0;
            }

            this.InitializeNulls();
            Alias = Alias.RemoveDiacritics().Sanitize().ToLower();
        }

        /// <summary>
        /// Method to get the list of associated AclGroups to the Section.
        /// </summary>
        /// <returns>The list of associated AclGroups to the Section.</returns>
        private List<AclGroupEntity> ListAclGroups()
        {
            if (aclGroups == null)
            {
                aclGroups = new List<AclGroupEntity>();

                if (SectionsInAclGroups != null)
                {
                    aclGroups = ListEntities<AclGroupEntity>(SectionsInAclGroups);
                }
            }

            return aclGroups;
        }

        /// <summary>
        /// Method to associate an AclGroup to the Section.
        /// </summary>
        /// <param name="aclGroupPk">An album id or primary key.</param>
        public bool LinkAclGroup(int aclGroupPk)
        {
            try
            {
                int index = SectionsInAclGroups.ToList().FindIndex(o => o.AclGroupId == aclGroupPk);

                if (index < 0)
                {
                    SectionsInAclGroups.Add(new SectionsInAclGroups { AclGroupId = aclGroupPk });
                }
                return true;
            }
            catch(Exception e)
            {
                log.Debug(e.Output());
                return false;
            }
        }

        /// <summary>
        /// Method to unlink an AclGroup of the Section.
        /// </summary>
        /// <param name="aclGroupPk">An album id or primary key.</param>
        public bool UnLinkAclGroup(int aclGroupPk)
        {
            try
            {
                int index = SectionsInAclGroups.ToList().FindIndex(o => o.AclGroupId == aclGroupPk);
                SectionsInAclGroups.RemoveAt(index);
                return true;
            }
            catch (Exception e)
            {
                log.Debug(e.Output());
                return false;
            }
        }

        /// <summary>
        /// Method to get the list of associated Album to the Section.
        /// </summary>
        /// <returns>The list of associated Albums to the Section.</returns>
        private List<AlbumEntity> ListAlbums()
        {
            if (albums == null)
            {
                albums = new List<AlbumEntity>();

                if (AlbumsInSections != null)
                {
                    albums = ListEntities<AlbumEntity>(AlbumsInSections);
                }
            }

            return albums;
        }

        /// <summary>
        /// Method to associate an Album to the Section.
        /// </summary>
        /// <param name="albumPk">An album id or primary key.</param>
        public bool LinkAlbum(int albumPk)
        {
            try
            {
                int index = AlbumsInSections.ToList().FindIndex(o => o.AlbumId == albumPk);

                if (index < 0)
                {
                    AlbumsInSections.Add(new AlbumsInSections { AlbumId = albumPk });
                }
                return true;
            }
            catch (Exception e)
            {
                log.Debug(e.Output());
                return false;
            }
        }

        /// <summary>
        /// Method to unlink an Album of the Section.
        /// </summary>
        /// <param name="albumPk">An album id or primary key.</param>
        public bool UnlinkAlbum(int albumPk)
        {
            try
            {
                int index = AlbumsInSections.ToList().FindIndex(o => o.AlbumId == albumPk);
                AlbumsInSections.RemoveAt(index);
                return true;
            }
            catch (Exception e)
            {
                log.Debug(e.Output());
                return false;
            }
        }
        
        #endregion
    }
}
