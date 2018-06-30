namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base
{
    public enum EnumEntitiesDependencies
    {
        /// <summary>
        /// None dependencies
        /// </summary>
        None = -1,

        /// <summary>
        /// All dependencies
        /// </summary>
        All = 0,

        /// <summary>
        /// AclGroups In AclActions
        /// </summary>
        AclGroupsInAclActions = 1,

        /// <summary>
        /// Albums In Sections
        /// </summary>
        AlbumsInSections = 2,

        /// <summary>
        /// Pictures In Albums 
        /// </summary>
        PicturesInAlbums = 3,

        /// <summary>
        /// Users In AclGroups
        /// </summary>
        UsersInAclGroups = 4,

        /// <summary>
        /// Sections In AclGroups
        /// </summary>
        SectionsInAclGroups = 5,

        /// <summary>
        /// Infos In Pictures
        /// </summary>
        InfosInPictures = 6,

        /// <summary>
        /// Infos In Albums
        /// </summary>
        InfosInAlbums = 7
    }
}
