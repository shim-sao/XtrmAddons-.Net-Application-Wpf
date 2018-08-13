using Fotootof.Libraries.Models.Systems;
using System.Collections.ObjectModel;

namespace Fotootof.Collections
{
    /// <summary>
    /// Class XtrmAddons Fotootof Collection Storage.
    /// </summary>
    public class CollectionStorage : ObservableCollection<StorageInfoModel>
    {
        #region Properties

        /// <summary>
        /// Property to access to the count of directories.
        /// </summary>
        public int DirectoriesCount => GetDirectoriesCount();

        /// <summary>
        /// Property to access to the count of pictures.
        /// </summary>
        public int ImagesCount => GetImagesCount();

        #endregion



        #region Methods

        /// <summary>
        /// Method to get the count of directories.
        /// </summary>
        /// <returns>The count of directories.</returns>
        private int GetDirectoriesCount()
        {
            int count = 0;

            foreach (var sim in this)
            {
                if (!sim.IsPicture)
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Method to get the count of images.
        /// </summary>
        /// <returns>The count of images</returns>
        private int GetImagesCount()
        {
            int count = 0;

            foreach (var sim in this)
            {
                if (sim.IsPicture)
                {
                    count++;
                }
            }

            return count;
        }

        #endregion
    }
}