using Fotootof.SQLite.EntityManager.Data.Tables.Dependencies;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.SQLite.EntityManager.Data.Tables.Json.Models;
using Fotootof.SQLite.EntityManager.Enums.EntityHelper;
using Fotootof.SQLite.EntityManager.Managers;
using Fotootof.SQLite.Services;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Fotootof.Plugin.Api.Helpers
{
    public static class RouteHelper
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the application database service <see cref="SQLiteSvc"/>.
        /// </summary>
        public static SQLiteSvc Database
            => SQLiteSvc.GetInstance();

        #endregion


        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">The primary key or id of the <see cref="SectionEntity"/></param>
        /// <param name="user">The <see cref="UserEntity"/>.</param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static SectionEntity GetUserSection(int id, UserEntity user)
        {
            SectionEntity entity = null;

            // Try to found section in dependencies.
            foreach (int groupPK in user.AclGroupsPKeys)
            {
                AclGroupEntity ag = Database.AclGroups.SingleOrDefault
                (
                    new AclGroupOptionsSelect
                    {
                        PrimaryKey = groupPK,
                        Dependencies = new List<EnumEntitiesDependencies> { EnumEntitiesDependencies.All }
                    }
                );

                var dep = ag.SectionsInAclGroups.ToList().Find(x => x.SectionId == id);

                if (dep != null)
                {
                    entity = Database.Sections.SingleOrDefault
                    (
                        new SectionOptionsSelect
                        {
                            PrimaryKey = id,
                            Dependencies = new List<EnumEntitiesDependencies> { EnumEntitiesDependencies.All }
                        }
                    );
                }

                if (entity != null)
                {
                    break;
                }
            }

            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">The primary key or id of the <see cref="SectionEntity"/></param>
        /// <param name="user">The <see cref="UserEntity"/>.</param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static PictureEntity GetUserPicture(int id, UserEntity user)
        {
            PictureEntity p = Database.Pictures.SingleOrNull
            (
                new PictureOptionsSelect
                {
                    PrimaryKey = id,
                    Dependencies = new List<EnumEntitiesDependencies> { EnumEntitiesDependencies.All }
                }
            );

            if (p == null)
            {
                return null;
            }
            
            // Check if Picture if authorized for the User.
            // Search if a Section depend on User.
            SectionEntity entity = null;

            var album = Database.Albums.List(new AlbumOptionsList { Dependencies = new List<EnumEntitiesDependencies> { EnumEntitiesDependencies.All } }).First(x => x.ThumbnailPictureId == id);

            if (album != null)
            {
                foreach (AlbumsInSections sectDep in album.AlbumsInSections)
                {
                    var sect = Database.Sections.SingleOrDefault
                    (
                        new SectionOptionsSelect
                        {
                            PrimaryKey = sectDep.SectionId,
                            Dependencies = new List<EnumEntitiesDependencies> { EnumEntitiesDependencies.All }
                        }
                    );
                    
                    foreach (SectionsInAclGroups aclgDep in sect.SectionsInAclGroups)
                    {
                        entity = GetUserSection(aclgDep.AclGroupId, user);
                        if (entity != null) break;
                    }
                    
                    if (entity != null) break;
                }
            }

            if (entity == null)
            { 
                foreach (PicturesInAlbums albDep in p.PicturesInAlbums)
                {
                    AlbumEntity alb = Database.Albums.SingleOrDefault
                    (
                        new AlbumOptionsSelect
                        {
                            PrimaryKey = albDep.AlbumId,
                            Dependencies = new List<EnumEntitiesDependencies> { EnumEntitiesDependencies.All }
                        }
                    );

                    foreach (AlbumsInSections sectDep in alb.AlbumsInSections)
                    {
                        entity = GetUserSection(sectDep.SectionId, user);
                        if (entity != null) break;
                    }

                    if (entity != null) break;
                }
            }

            if(entity == null)
            {
                return null;
            }

            return p;
        }

        #endregion


    }
}
