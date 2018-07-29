using Fotootof.Layouts.Windows.Forms.Section.Controls;
using XtrmAddons.Fotootof.Lib.Base.Classes.Collections.Entities;
using XtrmAddons.Fotootof.Lib.Base.Classes.Windows;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;

namespace Fotootof.Layouts.Windows.Forms.Section.Models
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Common Window Section Form Model.
    /// </summary>
    public class WindowFormSectionModel : WindowBaseFormModel<WindowFormSectionControl>
    {
        #region Variables

        /// <summary>
        /// Variable Section entity.
        /// </summary>
        private SectionEntity section;

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
        /// Property to access to the Section.
        /// </summary>
        public SectionEntity Section
        {
            get { return section; }
            set
            {
                section = value;
                NotifyPropertyChanged();
            }
        }

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

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Window Section Form Model Constructor.
        /// </summary>
        /// <param name="windowBase">The associated window form base.</param>
        public WindowFormSectionModel(WindowFormSectionControl windowBase) : base(windowBase) { }

        #endregion
    }
}