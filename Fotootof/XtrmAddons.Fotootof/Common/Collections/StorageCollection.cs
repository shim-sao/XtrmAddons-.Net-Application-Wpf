using System.Collections.ObjectModel;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Systems;

namespace XtrmAddons.Fotootof.Common.Collections
{
    public class StorageCollection : ObservableCollection<StorageInfoModel>
    {
        public int DirectoriesCount => GetDirectoriesCount();

        private int GetDirectoriesCount()
        {
            int count = 0;

            foreach (StorageInfoModel sim in this)
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

            foreach (StorageInfoModel sim in this)
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