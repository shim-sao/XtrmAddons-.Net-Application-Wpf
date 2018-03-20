using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Section Entity.
    /// </summary>
    [Table("Sections")]
    public partial class SectionEntity : EntityBase
    {
        #region Variables

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
        public int SectionId { get; set; }

        /// <summary>
        /// Property name of the item.
        /// </summary>
        [Column(Order = 1)]
        public string Name { get; set; }

        /// <summary>
        /// Property name of the item.
        /// </summary>
        [Column(Order = 2)]
        public string Alias { get; set; }

        /// <summary>
        /// Property description of the item.
        /// </summary>
        [Column(Order = 3)]
        public string Description { get; set; }

        /// <summary>
        /// Property is default item.
        /// </summary>
        [Column(Order = 4)]
        public bool IsDefault { get; set; }

        /// <summary>
        /// Property order place of the item.
        /// </summary>
        [Column(Order = 5)]
        public int Ordering { get; set; }


        /// <summary>
        /// Property the picture path.
        /// </summary>
        [Column(Order = 6)]
        [JsonIgnore]
        public string PicturePath { get; set; }

        /// <summary>
        /// Property the picture width.
        /// </summary>
        [Column(Order = 7)]
        [JsonIgnore]
        public int PictureWidth { get; set; }

        /// <summary>
        /// Property the picture height.
        /// </summary>
        [Column(Order = 8)]
        [JsonIgnore]
        public int PictureHeight { get; set; }


        /// <summary>
        /// Property the thumbnail picture path.
        /// </summary>
        [Column(Order = 9)]
        [JsonIgnore]
        public string ThumbnailPath { get; set; }

        /// <summary>
        /// Property the thumbnail width.
        /// </summary>
        [Column(Order = 10)]
        [JsonIgnore]
        public int ThumbnailWidth { get; set; }

        /// <summary>
        /// Property the thumbnail height.
        /// </summary>
        [Column(Order = 11)]
        [JsonIgnore]
        public int ThumbnailHeight { get; set; }


        /// <summary>
        /// Property comment for the item.
        /// </summary>
        [Column(Order = 12)]
        public string Comment { get; set; }

        #endregion



        #region Proprerties : Dependencies

        /// <summary>
        /// Property alias to access to the primary key of the entity.
        /// </summary>
        [NotMapped]
        public override int PrimaryKey { get => SectionId; set => SectionId = value; }

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
        public List<int> AlbumsPK => ListOfPrimaryKeys(AlbumsInSections.ToList(), "AlbumId");

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
        public List<int> AclGroupsPK => ListOfPrimaryKeys(SectionsInAclGroups.ToList(), "AclGroupId");

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
            Alias = Alias.RemoveDiacritics().Sanitize();
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
        /// <param name="aclGroupId"></param>
        public void LinkAclGroup(int aclGroupId)
        {
            try
            {
                int index = SectionsInAclGroups.ToList().FindIndex(o => o.AclGroupId == aclGroupId);

                if (index < 0)
                {
                    SectionsInAclGroups.Add(new SectionsInAclGroups { AclGroupId = aclGroupId });
                }
            }
            catch { }
        }

        /// <summary>
        /// Method to unlink an AclGroup of the Section.
        /// </summary>
        /// <param name="aclGroupId"></param>
        public void UnLinkAclGroup(int aclGroupId)
        {
            try
            {
                int index = SectionsInAclGroups.ToList().FindIndex(o => o.AclGroupId == aclGroupId);
                SectionsInAclGroups.RemoveAt(index);
            }
            catch { }
        }

        /// <summary>
        /// Method to get the list of associated Album to the Section.
        /// </summary>
        /// <returns>The list of associated Album to the Section.</returns>
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
        /// <param name="albumId"></param>
        public void LinkAlbum(int albumId)
        {
            try
            {
                int index = AlbumsInSections.ToList().FindIndex(o => o.AlbumId == albumId);

                if (index < 0)
                {
                    AlbumsInSections.Add(new AlbumsInSections { AlbumId = albumId });
                }
            }
            catch { }
        }

        /// <summary>
        /// Method to unlink an Album of the Section.
        /// </summary>
        /// <param name="albumId"></param>
        public void UnLinkAlbum(int albumId)
        {
            try
            {
                int index = AlbumsInSections.ToList().FindIndex(o => o.AlbumId == albumId);
                AlbumsInSections.RemoveAt(index);
            }
            catch { }
        }
        
        #endregion
    }
}
