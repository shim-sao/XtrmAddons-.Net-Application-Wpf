using Fotootof.Libraries.Models.Systems;
using System.Collections.ObjectModel;

namespace Fotootof.Libraries.Collections
{
    public class CollectionStorage : ObservableCollection<StorageInfoModel>
    {
        public int DirectoriesCount => GetDirectoriesCount();

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

        public int ImagesCount => GetImagesCount();

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