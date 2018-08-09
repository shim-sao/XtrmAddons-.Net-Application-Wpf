using Fotootof.Libraries.Collections.Entities;
using Fotootof.Libraries.Models.Interfaces;
using Fotootof.Libraries.Windows;
using Fotootof.SQLite.EntityConverters.ValueConverters;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.SQLite.EntityManager.Enums.EntityHelper;
using Fotootof.SQLite.EntityManager.Managers;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.Layouts.Windows.Forms.Section
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Common Window Section Form Model.
    /// </summary>
    public class WindowFormSectionModel : WindowLayoutFormModel<WindowFormSectionLayout>, IFormData<SectionEntity>
    {
        #region Variables

        /// <summary>
        /// 
        /// </summary>
        private SectionEntity newFormData;

        /// <summary>
        /// 
        /// </summary>
        private SectionEntity oldFormData;

        /// <summary>
        /// Variable AclGroups collection.
        /// </summary>
        private AclGroupEntityCollection aclGroups;

        /// <summary>
        /// Variable Albums collection.
        /// </summary>
        public AlbumEntityCollection albums;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the AclGroups collection.
        /// </summary>
        public AclGroupEntityCollection AclGroups
        {
            get { return aclGroups; }
            set
            {
                aclGroups = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Property to access to the Albums collection.
        /// </summary>
        public AlbumEntityCollection Albums
        {
            get { return albums; }
            set
            {
                albums = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SectionEntity NewFormData
        {
            get { return newFormData; }
            set
            {
                if (newFormData != value)
                {
                    newFormData = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SectionEntity OldFormData
        {
            get { return oldFormData; }
            set
            {
                if (oldFormData != value)
                {
                    oldFormData = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Window Section Form Model Constructor.
        /// </summary>
        /// <param name="owner">The associated window form base.</param>
        public WindowFormSectionModel(WindowFormSectionLayout owner) : base(owner) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Window Section Form Model Constructor.
        /// </summary>
        /// <param name="owner">The associated window form base.</param>
        /// <param name="SectionId"></param>
        public WindowFormSectionModel(WindowFormSectionLayout owner, int SectionId) : this(owner)
        {
            LoadSection(SectionId);

            // Set model entity to dependencies converters.
            IsAclGroupInSection.Entity = NewFormData;
            IsAlbumInSection.Entity = NewFormData;

            // Assign list of AclGroup to the model for dependencies.
            // Assign list of Album to the model for dependencies.
            AclGroups = new AclGroupEntityCollection(true);
            Albums = new AlbumEntityCollection(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityId"></param>
        public void LoadSection(int entityId)
        {
            NewFormData = null;
            if (entityId > 0)
            {
                var op = new SectionOptionsSelect
                {
                    PrimaryKey = entityId,
                    Dependencies = { EnumEntitiesDependencies.All }
                };

                NewFormData = Db.Sections.SingleOrNull(op);
            }

            NewFormData = NewFormData ?? new SectionEntity();
            OldFormData = NewFormData?.CloneJson();
        }

        #endregion
    }
}