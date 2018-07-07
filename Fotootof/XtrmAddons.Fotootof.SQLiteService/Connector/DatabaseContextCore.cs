using Microsoft.EntityFrameworkCore;
using System;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;

namespace XtrmAddons.Fotootof.SQLiteService.Connector
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Database Context Core.
    /// </summary>
    public class DatabaseContextCore : DbContext
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
        /// Property database table ACLAction entity queries link.
        /// </summary>
        public DbSet<AclActionEntity> AclActions { get; set; }

        /// <summary>
        /// Property database table AclGroup entity queries link.
        /// </summary>
        public DbSet<AclGroupEntity> AclGroups { get; set; }

        /// <summary>
        /// Property database table Albums entity queries link.
        /// </summary>
        public DbSet<AlbumEntity> Albums { get; set; }

        /// <summary>
        /// Property database table Infos entity queries link.
        /// </summary>
        public DbSet<InfoEntity> Infos { get; set; }

        /// <summary>
        /// Property database table InfoTypes entity queries link.
        /// </summary>
        public DbSet<InfoTypeEntity> InfoTypes { get; set; }

        /// <summary>
        /// Property database table Pictures entity queries link.
        /// </summary>
        public DbSet<PictureEntity> Pictures { get; set; }

        /// <summary>
        /// Property database table Section entity queries link.
        /// </summary>
        public DbSet<SectionEntity> Sections { get; set; }

        /// <summary>
        /// Property database table Users entity queries link.
        /// </summary>
        public DbSet<UserEntity> Users { get; set; }

        /// <summary>
        /// Property database table Versions entity queries link.
        /// </summary>
        public DbSet<VersionEntity> Versions { get; set; }

        

        /// <summary>
        /// Property database table ACLGroups In ACLActions entity queries link.
        /// </summary>
        public DbSet<AclGroupsInAclActions> AclGroupsInAclActions { get; set; }

        /// <summary>
        /// Property database table Albums In Sections entity queries link.
        /// </summary>
        public DbSet<AlbumsInSections> AlbumsInSections { get; set; }

        /// <summary>
        /// Property database table Infos In Albums entity queries link.
        /// </summary>
        public DbSet<InfosInAlbums> InfosInAlbums { get; set; }

        /// <summary>
        /// Property database table Infos In Pictures entity queries link.
        /// </summary>
        public DbSet<InfosInPictures> InfosInPictures { get; set; }

        /// <summary>
        /// Property database Pictures in Albums entity queries link.
        /// </summary>
        public DbSet<PicturesInAlbums> PicturesInAlbums { get; set; }

        /// <summary>
        /// Property database Sections in ACLGroups entity queries link.
        /// </summary>
        public DbSet<SectionsInAclGroups> SectionsInAclGroups { get; set; }

        /// <summary>
        /// Property database Users in ACLGroups entity queries link.
        /// </summary>
        public DbSet<UsersInAclGroups> UsersInAclGroups { get; set; }

        #endregion



        #region Methods

        /// <summary>
        /// Class XtrmAddons PhotoAlbum Server SQLite Database Context Core constructor.
        /// </summary>
        /// <param name="options">Database context options.</param>
        public DatabaseContextCore(DbContextOptions<DatabaseContextCore> options) : base(options) { }

        /// <summary>
        /// <para>Method called on configuring options.</para>
        /// <para>Warning: this method overrides options pasted in constructor.</para>
        /// <para>See documentation :</para>
        /// </summary>
        /// <param name="optionsBuilder">The database context builder options.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=" + ConnectionString);
        }

        /// <summary>
        /// Method called on model creating.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelCreateAclGroupsInAclActionsDependencies(modelBuilder);
            ModelCreateAlbumsInSectionsDependencies(modelBuilder);
            ModelCreatePicturesInAlbumsDependencies(modelBuilder);
            ModelCreateSectionsInAclGroupsDependencies(modelBuilder);
            ModelCreateUsersInAclGroupsDependencies(modelBuilder);
            ModelCreateInfosInAlbumsDependencies(modelBuilder);
            ModelCreateInfosInPicturesDependencies(modelBuilder);
        }

        /// <summary>
        /// Method to add AclGroups In AclActions dependencies.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        private void ModelCreateAclGroupsInAclActionsDependencies(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.Entity<AclGroupsInAclActions>()
                    .HasKey(d => new { d.AclGroupId, d.AclActionId });
            
                modelBuilder.Entity<AclGroupsInAclActions>()
                    .HasOne(d => d.AclGroupEntity)
                    .WithMany(x => x.AclGroupsInAclActions)
                    .HasForeignKey(d => d.AclGroupId);

                modelBuilder.Entity<AclGroupsInAclActions>()
                    .HasOne(d => d.AclActionEntity)
                    .WithMany(x => x.AclGroupsInAclActions)
                    .HasForeignKey(d => d.AclActionId);
            }
            catch (Exception e)
            {
                log.Fatal(e);
                throw new Exception("Method to add AclGroups In AclActions dependencies failed !", e);
            }
        }

        /// <summary>
        /// Method to add Albums In Sections dependencies.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        private void ModelCreateAlbumsInSectionsDependencies(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.Entity<AlbumsInSections>()
                .HasKey(d => new { d.AlbumId, d.SectionId });

            modelBuilder.Entity<AlbumsInSections>()
                .HasOne(d => d.AlbumEntity)
                .WithMany(x => x.AlbumsInSections)
                .HasForeignKey(d => d.AlbumId);

            modelBuilder.Entity<AlbumsInSections>()
                .HasOne(d => d.SectionEntity)
                .WithMany(x => x.AlbumsInSections)
                .HasForeignKey(d => d.SectionId);
            }
            catch (Exception e)
            {
                log.Fatal(e);
                throw new Exception("Method to add Albums In Sections dependencies failed !", e);
            }
        }

        /// <summary>
        /// Method to add Pictures In Albums dependencies.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        private void ModelCreatePicturesInAlbumsDependencies(ModelBuilder modelBuilder)
        {
            try
            { 
                modelBuilder.Entity<PicturesInAlbums>()
                    .HasKey(d => new { d.PictureId, d.AlbumId });

                modelBuilder.Entity<PicturesInAlbums>()
                    .HasOne(d => d.PictureEntity)
                    .WithMany(x => x.PicturesInAlbums)
                    .HasForeignKey(d => d.PictureId);

                modelBuilder.Entity<PicturesInAlbums>()
                    .HasOne(d => d.AlbumEntity)
                    .WithMany(x => x.PicturesInAlbums)
                    .HasForeignKey(d => d.AlbumId);
            }
            catch (Exception e)
            {
                log.Fatal(e);
                throw new Exception("Method to add Pictures In Albums dependencies failed !", e);
            }
        }

        /// <summary>
        /// Method to add Users in AclGroups dependencies.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        private void ModelCreateUsersInAclGroupsDependencies(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.Entity<UsersInAclGroups>()
                .HasKey(d => new { d.UserId, d.AclGroupId });

                modelBuilder.Entity<UsersInAclGroups>()
                    .HasOne(d => d.UserEntity)
                    .WithMany(x => x.UsersInAclGroups)
                    .HasForeignKey(d => d.UserId);

                modelBuilder.Entity<UsersInAclGroups>()
                    .HasOne(d => d.AclGroupEntity)
                    .WithMany(x => x.UsersInAclGroups)
                    .HasForeignKey(d => d.AclGroupId);
            }
            catch (Exception e)
            {
                log.Fatal(e);
                throw new Exception("Method to add users in AclGroups dependencies failed !", e);
            }
        }

        /// <summary>
        /// Method to add Sections in AclGroups dependencies.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        private void ModelCreateSectionsInAclGroupsDependencies(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.Entity<SectionsInAclGroups>()
                    .HasKey(d => new { d.SectionId, d.AclGroupId });

                modelBuilder.Entity<SectionsInAclGroups>()
                    .HasOne(d => d.SectionEntity)
                    .WithMany(x => x.SectionsInAclGroups)
                    .HasForeignKey(d => d.SectionId);

                modelBuilder.Entity<SectionsInAclGroups>()
                    .HasOne(d => d.AclGroupEntity)
                    .WithMany(x => x.SectionsInAclGroups)
                    .HasForeignKey(d => d.AclGroupId);
            }
            catch (Exception e)
            {
                log.Fatal(e);
                throw new Exception("Method to add Sections in AclGroups dependencies failed !", e);
            }
        }

        /// <summary>
        /// Method to add Infos In Albums dependencies.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        private void ModelCreateInfosInAlbumsDependencies(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.Entity<InfosInAlbums>()
                    .HasKey(d => new { d.InfoId, d.AlbumId });

                modelBuilder.Entity<InfosInAlbums>()
                    .HasOne(d => d.InfoEntity)
                    .WithMany(x => x.InfosInAlbums)
                    .HasForeignKey(d => d.InfoId);

                modelBuilder.Entity<InfosInAlbums>()
                    .HasOne(d => d.AlbumEntity)
                    .WithMany(x => x.InfosInAlbums)
                    .HasForeignKey(d => d.AlbumId);
            }
            catch (Exception e)
            {
                log.Fatal(e);
                throw new Exception("Method to add Infos in Albums dependencies failed !", e);
            }
        }

        /// <summary>
        /// Method to add Infos In Pictures dependencies.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        private void ModelCreateInfosInPicturesDependencies(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.Entity<InfosInPictures>()
                    .HasKey(d => new { d.InfoId, d.PictureId });

                modelBuilder.Entity<InfosInPictures>()
                    .HasOne(d => d.InfoEntity)
                    .WithMany(x => x.InfosInPictures)
                    .HasForeignKey(d => d.InfoId);

                modelBuilder.Entity<InfosInPictures>()
                    .HasOne(d => d.PictureEntity)
                    .WithMany(x => x.InfosInPictures)
                    .HasForeignKey(d => d.PictureId);
            }
            catch (Exception e)
            {
                log.Fatal(e);
                throw new Exception("Method to add Infos in Pictures dependencies failed !", e);
            }
        }

        #endregion
    }
}