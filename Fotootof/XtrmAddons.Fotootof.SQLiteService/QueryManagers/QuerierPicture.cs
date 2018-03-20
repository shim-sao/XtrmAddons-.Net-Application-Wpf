using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;

namespace XtrmAddons.Fotootof.SQLiteService.QueryManagers
{
    /// <summary>
    /// Class XtrmAddons Fotootof SQLiteService Pictures.
    /// </summary>
    public partial class QuerierPicture : Querier
    {
        #region Methods List

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dependencies"></param>
        /// <returns></returns>
        public ObservableCollection<PictureEntity> List(PictureOptionsList options = null)
        {
            using (Db.Context)
            {
                return new ObservableCollection<PictureEntity>(PictureManager.List(options));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<ObservableCollection<PictureEntity>> ListAsync(PictureOptionsList options = null)
        {
            return Task.Run(() =>
            {
                return List(options);
            });
        }

        #endregion



        #region Methods Single

        /// <summary>
        /// 
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        public PictureEntity SingleOrNull(PictureOptionsSelect op)
        {
            using (Db.Context)
            {
                return PictureManager.Select(op);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pictureId"></param>
        /// <param name="dependencies"></param>
        /// <returns></returns>
        public Task<PictureEntity> SingleOrNullAsync(PictureOptionsSelect op)
            => Task.Run(() => SingleOrNull(op));

        #endregion



        #region Methods Add

        /// <summary>
        /// 
        /// </summary>
        /// <param name="picture"></param>
        /// <returns></returns>
        public PictureEntity Add(PictureEntity picture)
        {
            using (Db.Context)
            {
                try { Db.Context.Attach(picture); } catch { }
                return PictureManager.Add(picture);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="picture"></param>
        /// <returns></returns>
        public Task<PictureEntity> AddAsync(PictureEntity picture) => Task.Run(() => Add(picture));

        #endregion



        #region Methods Update

        /// <summary>
        /// 
        /// </summary>
        /// <param name="picture"></param>
        /// <returns></returns>
        public PictureEntity Update(PictureEntity picture)
        {
            using (Db.Context)
            {
                try { Db.Context.Attach(picture); } catch { }
                return PictureManager.Update(picture);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Task<PictureEntity> UpdateAsync(PictureEntity picture)
            => Task.Run(() => Update(picture));

        #endregion



        #region Methods Delete

        /// <summary>
        /// Method to delete a picture.
        /// </summary>
        /// <param name="picture">The Picture entity to delete.</param>
        /// <returns>The deleted Picture entity.</returns>
        public PictureEntity Delete(PictureEntity picture)
        {
            using (Db.Context)
            {
                Db.Context.Attach(picture);
                return PictureManager.Delete(picture);
            }
        }

        /// <summary>
        /// Method to delete a list of pictures.
        /// </summary>
        /// <param name="pictures">A list of Picture entity to delete.</param>
        /// <returns>The deleted Picture entity list.</returns>
        public List<PictureEntity> Delete(List<PictureEntity> pictures)
        {
            using (Db.Context)
            {
                foreach(PictureEntity p in pictures)
                {
                    Db.Context.Attach(p);
                }

                return PictureManager.Delete(pictures);
            }
        }

        /// <summary>
        /// Method to delete a picture asynchronously.
        /// </summary>
        /// <param name="picture">The Picture entity to delete.</param>
        /// <returns>>The deleted Picture entity.</returns>
        public Task<PictureEntity> DeleteAsync(PictureEntity picture) 
            => Task.Run(() => { return Delete(picture); });

        /// <summary>
        /// Method to delete a list of pictures asynchronously.
        /// </summary>
        /// <param name="pictures">A list of Picture entity to delete.</param>
        /// <returns>The deleted Picture entity list.</returns>
        public Task<List<PictureEntity>> DeleteAsync(List<PictureEntity> pictures)
            => Task.Run(() => Delete(pictures));

        #endregion
    }
}
