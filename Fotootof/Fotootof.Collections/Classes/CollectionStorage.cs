using Fotootof.Libraries.Models.Systems;
using System.Collections.ObjectModel;

namespace Fotootof.Collections
{
    /// <summary>
    /// Class Fotootof Libraries Collections.
    /// </summary>
    public class CollectionStorage : ObservableCollection<StorageInfoModel>
    {
        /// <summary>
        /// 
        /// </summary>
        public int DirectoriesCount => GetDirectoriesCount();

        /// <summary>
        /// 
        /// </summary>
        public int ImagesCount => GetImagesCount();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <returns></returns>
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
    }
}