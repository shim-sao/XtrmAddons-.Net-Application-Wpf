using Fotootof.Collections.Entities;
using Fotootof.Libraries.Logs;
using Fotootof.Libraries.Models.Systems;
using Fotootof.Libraries.Windows;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using System;
using System.IO;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.Layouts.Windows.Forms.Album
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Common Window Form Model Album.
    /// </summary>
    internal class WindowFormAlbumModel : WindowLayoutFormModel<WindowFormAlbumLayout>
    {
        #region Variables
        
        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
        	log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable Album entity.
        /// </summary>
        private AlbumEntity album;

        /// <summary>
        /// Variable Section entities collection.
        /// </summary>
        private SectionEntityCollection sections;

        /// <summary>
        /// Variable observable collection of quality filters.
        /// </summary>
        private InfoEntityCollection qualityFilters;

        /// <summary>
        /// Variable observable collection of color filters.
        /// </summary>
        private InfoEntityCollection colorFilters;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the Album.
        /// </summary>
        public AlbumEntity Album
        {
            get { return album; }
            set
            {
                album = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Property to access to the Section entities collection.
        /// </summary>
        public SectionEntityCollection Sections
        {
            get { return sections; }
            set
            {
                sections = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Property to access to the observable collection of quality filters.
        /// </summary>
        public InfoEntityCollection FiltersQuality
        {
            get { return qualityFilters; }
            set
            {
                qualityFilters = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Property to access to the observable collection of color filters.
        /// </summary>
        public InfoEntityCollection FiltersColor
        {
            get { return colorFilters; }
            set
            {
                colorFilters = value;
                NotifyPropertyChanged();
            }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Window Form Model Album Constructor.
        /// </summary>
        /// <param name="controlView">The page associated to the model.</param>
        public WindowFormAlbumModel(WindowFormAlbumLayout controlView) : base(controlView)
        {
            FiltersQuality = InfoEntityCollection.TypesQuality();
            FiltersColor = InfoEntityCollection.TypesColor();
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to update a <see cref="PictureEntity"/> property of an <see cref="AlbumEntity"/>.
        /// </summary>
        /// <param name="propertyName">The <see cref="object"/> property name.</param>
        /// <param name="filename">The filename or full path of the image.</param>
        public async void UpdateAlbumPicturePropertyAsync(string propertyName, string filename)
        {
            if(propertyName.IsNullOrWhiteSpace())
            {
                ArgumentNullException e = Exceptions.GetArgumentNull(nameof(propertyName), propertyName);
                log.Error(e.Output());
                throw e;
            }
            
            if(filename.IsNullOrWhiteSpace())
            {
                ArgumentNullException e = Exceptions.GetArgumentNull(nameof(filename), filename);
                log.Error(e.Output());
                throw e;
            }

            // Check if Album has a Picture associated.
            if ((int)Album.GetPropertyValue(propertyName+"Id") <= 0)
            {
                PictureEntity entity = new PictureEntity();
                entity = Db.Pictures.Add(entity);
                Album.SetPropertyValue(propertyName, entity);
            }

            // Bind new image properties.
            Album.GetPropertyValue<PictureEntity>(propertyName).Bind((new StorageInfoModel(new FileInfo(filename))).ToPicture(), new string[] { "PrimaryKey", "PictureId" });

            // Update image properties in database.
            Album.SetPropertyValue(propertyName, await Db.Pictures.UpdateAsync(Album.GetPropertyValue<PictureEntity>(propertyName)));
        }

        #endregion
    }
}